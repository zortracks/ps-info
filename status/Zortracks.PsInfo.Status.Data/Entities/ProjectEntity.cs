using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zortracks.PsInfo.Status.Data.Entities {

    public class ProjectEntity {
        public ICollection<EntryEntity> Entries { get; set; }

        [Key]
        public string Name { get; set; }
    }
}