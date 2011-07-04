using System;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Aspnet
{
    public class aspnet_User
    {
        [Required]
        public aspnet_Application ApplicationId { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [StringLength(256)]
        [Required]
        public string UserName { get; set; }
        [StringLength(256)]
        [Required]
        public string LoweredUserName { get; set; }

        [StringLength(16)]
        public string MobileAlias { get; set; }
        
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
