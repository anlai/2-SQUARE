using System;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Aspnet
{
    public class aspnet_Application
    {
        [StringLength(256)]
        [Required]
        public string ApplicationName { get; set; }
        [StringLength(256)]
        [Required]
        public string LoweredApplicationName { get; set; }
        [Required]
        public Guid ApplicationId { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
    }
}
