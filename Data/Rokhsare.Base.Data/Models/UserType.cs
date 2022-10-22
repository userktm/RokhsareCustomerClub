using System;
using System.Collections.Generic;

namespace Rokhsare.Models
{
    public partial class UserType
    {
        public UserType()
        {
            this.Users = new List<User>();
        }

        public int UserTypeID { get; set; }
        public string UserTypeName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
