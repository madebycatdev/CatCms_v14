using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace EuroCMS.Model
{
    public class AccessRuleViewModel
    {
        public int RuleId { get; set; }

        public string RuleName { get; set; }

        public string ContentId { get; set; }

        public string ContentType { get; set; }

        public string ContentItemName { get; set; }

        public string Roles { get; set; }

        public string Users { get; set; }

        public string Permissions { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

    }

    public class BaseDbContext : DbContext
    {
       
        public BaseDbContext() : base("eurocms.db")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Build(new System.Data.Entity.Infrastructure.DbProviderInfo("System.Data.SqlClient", "2005"));
            //BaseDbContext hede = new BaseDbContext();
            //hede.Database.ExecuteSqlCommand("ALTER TABLE  ADD {COLUMNNAME} {TYPE} {NULL|NOT NULL} CONSTRAINT {CONSTRAINT_NAME} DEFAULT {DEFAULT_VALUE}");
            base.OnModelCreating(modelBuilder);
        }

        public void CreateAccessRule(string ruleName, string contentId, string contentType, string roles, string users, string permissions, string createdBy)
        {
            this.Database.ExecuteSqlCommand
                ("dbo.cms_AccessRule_Create {0},{1},{2},{3},{4},{5},{6}", ruleName, contentId, contentType, roles, users, permissions, createdBy);
        }

        public void UpdateAccessRule(int ruleId, string ruleName, string contentId, string contentType, string roles, string users, string permissions, string updatedBy)
        {
            this.Database.ExecuteSqlCommand
                ("dbo.cms_AccessRule_Update {0},{1},{2},{3},{4},{5},{6},{7}", ruleId, ruleName, contentId, contentType, roles, users, permissions, updatedBy);
        }

        public void DeleteAccessRule(int ruleId)
        {
            this.Database.ExecuteSqlCommand
                ("dbo.cms_AccessRule_Delete {0}", ruleId);
        }

        public List<AccessRuleViewModel> GetAllRules(string type)
        {
            return this.Database.SqlQuery<AccessRuleViewModel>
                 ("dbo.cms_AccessRules_GetAllRules {0}", type).ToList();
        }

        public List<vw_aspnet_Roles> GetAllRoles()
        {
            return this.Database.SqlQuery<vw_aspnet_Roles>
                 ("Select * from dbo.vw_aspnet_Roles").ToList();
        }

        public List<vw_aspnet_UsersInRoles> GetAllUsersInRoles()
        {
            return this.Database.SqlQuery<vw_aspnet_UsersInRoles>
                 ("Select * from dbo.vw_aspnet_UsersInRoles").ToList();
        }

        public List<AccessRuleViewModel> HasPermission(string userRoles, string permission, string contentId, string contentType)
        {
            return this.Database.SqlQuery<AccessRuleViewModel>
                 ("dbo.cms_AccessRules_HasPermission {0}, {1}, {2}, {3}", userRoles, permission, contentId, contentType).ToList();
        }


        //public DbContext GetObjectContext(SqlConnection connection)
        //{
        //    EntityConnection ecn =null;
        //    bool isOpen = connection.State != System.Data.ConnectionState.Closed;

        //    if( isOpen ) connection.Close();

        //    try
        //    {
        //            ecn =new EntityConnection( new MetadataWorkspace( new string[]{ "res://*/" }, new Assembly[]{ Assembly.GetExecutingAssembly() } ), connection );
        //            return new DbContext( ecn );
        //    }
        //    finally
        //    {
        //        if( isOpen )
        //        {
        //            if( ecn !=null )
        //                ecn.Open();
        //            else
        //                connection.Open();
        //        }
        //    }
        //}


    }

    public static class DataReaderExtensions
    {
        /// <summary>
        /// Checks if a column's value is DBNull
        /// </summary>
        /// <param name="dataReader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>A bool indicating if the column's value is DBNull</returns>
        public static bool IsDBNull(this IDataReader dataReader, string columnName)
        {
            return dataReader[columnName] == DBNull.Value;
        }

        /// <summary>
        /// Checks if a column exists in a data reader
        /// </summary>
        /// <param name="dataReader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>A bool indicating the column exists</returns>
        public static bool ContainsColumn(this IDataReader dataReader, string columnName)
        {
            /// See: http://stackoverflow.com/questions/373230/check-for-column-name-in-a-sqldatareader-object/7248381#7248381
            try
            {
                return dataReader.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }


        //using (ObjectContext objContext = ((IObjectContextAdapter)dbContext).ObjectContext;)
        //{
        //     var pageSize = 10;
        //     var total = objectContext.Items.Count();
        //     int pages = total/pageSize;
        //     int pageNumber = 0;
        //     do
        //     {
        //          var currentSet = objectContext.Items.Skip(pageNumber*pageSize).Take(pageSize);
        //          pageNumber++;
        //     }while(pageNumber < pages)
        //}
    }
}