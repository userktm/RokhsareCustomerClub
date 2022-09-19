using System;
using System.Collections.Generic;

namespace Rokhsare.Base.Data.Models
{
    public partial class UserRole
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
