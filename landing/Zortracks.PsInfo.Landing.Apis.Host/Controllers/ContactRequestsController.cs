using Microsoft.AspNetCore.Mvc;
using System;
using Zortracks.PsInfo.Landing.Data.DbContexts;
using Zortracks.PsInfo.Landing.Data.Entities;
using Zortracks.PsInfo.Landing.Models;

namespace Zortracks.PsInfo.Landing.Apis.Host.Controllers {

    [Route("contact-requests")]
    public sealed class ContactRequestsController : ControllerBase {
        private readonly LandingDbContext _context;

        public ContactRequestsController(LandingDbContext context) {
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