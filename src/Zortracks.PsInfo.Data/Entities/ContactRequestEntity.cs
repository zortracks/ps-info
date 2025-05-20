using System;

namespace Zortracks.PsInfo.Data.Entities {

    public sealed class ContactRequestEntity {
        public Guid Id { get; set; }
        public DateTime SentAt { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string FromPhone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}