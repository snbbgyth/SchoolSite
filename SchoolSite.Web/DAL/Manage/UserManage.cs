using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchoolSite.Web.Areas.Admin.Models;

namespace SchoolSite.Web.DAL.Manage
{
    public static class UserRoleManage
    {
        public static UserManager<ApplicationUser> UserManager { get; private set; }
        public static RoleManager<IdentityRole> RoleManager { get; private set; }
        public static ApplicationDbContext context { get; private set; }

        static UserRoleManage()
        {
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        public async static Task<bool> IsUserIdInRole(string id, string roleName)
        {
            var roleList = await UserManager.GetRolesAsync(id);
            return roleList.Contains(roleName);
        }


    }
}