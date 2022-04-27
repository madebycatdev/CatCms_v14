using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class PermissionDbContext : BaseDbContext
    {
        public PermissionDbContext()
            : base()
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public bool UserHasPermission(string applicationName, string userName, string PermissionName)
        {
            return this.Database.SqlQuery<int>("cms_UserHasPermission", applicationName, userName, PermissionName).FirstOrDefault() == 1;
        }

        public IEnumerable<string> GetPermissionsForUser(string applicationName, string userName)
        {
            return this.Database.SqlQuery<string>("cms_GetPermissionsForUser", applicationName, userName);
        }

        public void CreateRolePermission(string applicationName, string permissionName, string roleName)
        {
            this.Database.ExecuteSqlCommand("cms_PermissionsInRole_AddPermissionToRole", applicationName, permissionName, roleName);
        }
    }
}