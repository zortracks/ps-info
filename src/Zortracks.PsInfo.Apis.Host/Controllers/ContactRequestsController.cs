using Microsoft.AspNetCore.Mvc;
using System;
using Zortracks.PsInfo.Data.DbContexts;
using Zortracks.PsInfo.Data.Entities;
using Zortracks.PsInfo.Models;

namespace Zortracks.PsInfo.Apis.Host.Controllers {

    [Route("contact-requests")]
    public sealed class ContactRequestsController : ControllerBase {
        private readonly PsInfoDbContext _context;

        public ContactRequestsController(PsInfoDbContext context) {
            _context = context;
        }

        [HttpPost]
        public ActionResult AddContactRequest([FromBody] ContactRequestModel model) {
            _context.ContactRequests.Add(new ContactRequestEntity() {
                Id = Guid.NewGuid(),
                SentAt = DateTime.Now,
                FromName = model.FromName,
                FromEmail = model.FromEmail,
                FromPhone = model.FromPhone,
                Subject = model.Subject,
                Message = model.Message
            });
            _context.SaveChanges();

            return Ok();
        }
    }
}