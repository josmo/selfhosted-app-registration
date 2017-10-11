using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NancyService.models
{
    [Table("ref_APPLICATION")]
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }
        [StringLength(255)]
        public string ApplicationName { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(255)]
        public string ApplicationURL { get; set; }
        [StringLength(255)]
        public string BaseUserClaim { get; set; }

        public bool IsActive { get; set; }
    }
}

