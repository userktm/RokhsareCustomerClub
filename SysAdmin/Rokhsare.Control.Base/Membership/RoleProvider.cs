using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rokhsare.Utility;

namespace Rokhsare.Control.Base.Membership
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        int ProjectId = 0;
        //public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        //{
        //    var Pcode = Shop.ConfigAdmin.ConfigReader.ProjectCode;
        //    try
        //    {
        //        var prj = Clinic.Admin.DAL.DALUtility.GetAclContext.AppProjectInfoes.FirstOrDefault(a => a.Id == Pcode);
        //        if (prj != null)
        //            ProjectId = prj.Id;
        //    }
        //    catch
        //    {

        //    }
        //    base.Initialize(name, config);
        //}
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var db = ConfigReader.ConfigReader.GetRokhsarehClubDb;

            var user = db.Users.FirstOrDefault(a => a.UserName == username);
            var get = db.UserRoles.Where(u => u.UserId == user.UserID).Select(u => u.Role.RoleName).ToList().ToArray();
            return get;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var db = ConfigReader.ConfigReader.GetRokhsarehClubDb;
            var role = db.Roles.FirstOrDefault(u => u.RoleName == roleName.RemoveDangerousChars().ToLower());
            if (role != null)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == username);
                var userroles = user.UserRoles.Select(u => u.RoleId).ToList();
                return userroles.Contains(role.RoleId);
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
