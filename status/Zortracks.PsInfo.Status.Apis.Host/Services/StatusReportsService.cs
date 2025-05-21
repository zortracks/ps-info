using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using Zortracks.PsInfo.Status.Apis.Host.Objects;
using Zortracks.PsInfo.Status.Data.DbContexts;
using Zortracks.PsInfo.Status.Data.Entities;
using Zortracks.PsInfo.Status.Models;

namespace Zortracks.PsInfo.Status.Apis.Host.Services {

    public sealed class StatusReportsService {
        private readonly StatusDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly IPublishEndpoint _publishEndpoint;

        public StatusReportsService(StatusDbContext context, IMemoryCache memoryCache, IPublishEndpoint publishEndpoint) {
            _context = context;
            _memoryCache = memoryCache;
            _publishEndpoint = publishEndpoint;
        }

        public void UpdateStatus(StatusReport message) {
            var projectEntity = null as ProjectEntity;
            var cachedProject = null as CacheProject;

            if (!_memoryCache.TryGetValue(message.Name, out cachedProject)) {
                if ((projectEntity = GetProjectEntity(message.Name)) is null)
                    projectEntity = AddProjectEntity(message);

                cachedProject = _memoryCache.Set(message.Name, new CacheProject() {
                    Name = message.Name,
                    Status = projectEntity.Entries.FirstOrDefault()?.Status ?? message.Status
                });
            }

            if (cachedProject.Status != message.Status) {
                if (projectEntity is null)
                    projectEntity = GetProjectEntity(message.Name);

                _context.Database.CreateExecutionStrategy().Execute(() => {
                    _context.Entries.Add(new EntryEntity() {
                        Id = Guid.NewGuid(),
                        Project = projectEntity,
                        Issued = message.Issued,
                        Status = message.Status
                    });
                    _context.SaveChanges();
                });
                _publishEndpoint.Publish(new StatusChanged() {
                    Name = message.Name,
                    CurrentStatus = cachedProject.Status,
                    NewStatus = message.Status
                });
                _memoryCache.Set(message.Name, new CacheProject() {
                    Name = message.Name,
                    Status = message.Status
                });
            }
        }

        private ProjectEntity AddProjectEntity(StatusReport message) {
            var projectEntity = null as ProjectEntity;

            _context.Database.CreateExecutionStrategy().Execute(() => {
                _context.Projects.Add(projectEntity = new ProjectEntity() {
                    Name = message.Name,
                    Entries = [
                        new EntryEntity() {
                            Id = Guid.NewGuid(),
                            Issued = message.Issued,
                            Status = message.Status
                        }
                    ]
                });
                _context.SaveChanges();
            });

            return projectEntity;
        }

        private IQueryable<ProjectEntity> GetProjectEntities() => _context.Projects.Include(e => e.Entries.OrderByDescending(e => e.Issued).Take(20));

        private ProjectEntity GetProjectEntity(string name) => GetProjectEntities().FirstOrDefault(e => e.Name == name);
    }
}