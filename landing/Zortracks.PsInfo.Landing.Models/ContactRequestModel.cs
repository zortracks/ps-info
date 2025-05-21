using System.ComponentModel.DataAnnotations;

namespace Zortracks.PsInfo.Landing.Models {

    public sealed class ContactRequestModel {

        [Required]
        public string FromEmail { get; set; }

        [Required]
        public string FromName { get; set; }
        [Required]
        public string FromPhone { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Subject { get; set; }
    }
}