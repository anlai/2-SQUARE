using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstMembershipDemoSharp.Data
{
    public class Role
    {
        //Membership required
        //[Key()]
        public virtual Guid RoleId { get; set; }
        [Required]
        [StringLength(100)]
        public virtual string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }

        //Optional
        [StringLength(250)]
        public virtual string Description { get; set; }
    }
}