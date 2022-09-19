using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            this.UserRoles = new List<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
