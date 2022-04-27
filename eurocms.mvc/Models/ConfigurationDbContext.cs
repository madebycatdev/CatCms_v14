using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class ConfigurationDbContext : BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_config> Portlets { get; set; }

        public ConfigurationDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_config>()
                .Map(m => m.ToTable("cms_config"));
 
             base.OnModelCreating(modelBuilder);
        }

        public void CacheUpdateUpdateStatus(string serverIP, int status, int timeout)
        {
            this.Database.ExecuteSqlCommand("dbo.cms_asp_cache_update_update_status @server_ip={0}, @status={1}, @timeout={2}", serverIP, status, timeout);
        }

        public List<cms_asp_config_select_config_parameters_Result> SelectAll()
        {
            return this.Database.SqlQuery<cms_asp_config_select_config_parameters_Result>
                ("dbo.cms_asp_config_select_config_parameters")
                .ToList();
        }

        public List<cms_asp_config_select_config_parameters_Result> Select(int ConfigId)
        {
            return this.SelectAll().Where(w => w.config_id == ConfigId).ToList();
        }

        public List<cms_asp_admin_update_config_parameter_Result> Update(cms_config config)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_config_parameter_Result>
                 ("dbo.cms_asp_admin_update_config_parameter " +
                    "@config_id={0}," +
                    "@config_name={1}, " +
                    "@config_value={2}, " +
                    "@ws={3}, " +
                    "@publisher_id={4}",
                     config.config_id,
                     config.config_name,
                     ConfigurationManager.AppSettings["EuroCMS.WS"] != null && ConfigurationManager.AppSettings["EuroCMS.WS"] == "remote" ? config.config_value_remote : config.config_value_local,
                     ConfigurationManager.AppSettings["EuroCMS.WS"] ?? "local",
                     config.publisher_id).ToList();
        }

        public List<cms_asp_admin_delete_config_parameter_Result> Delete(int ConfigId, object  publisherID, int Publisherlevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_config_parameter_Result>
                ("dbo.cms_asp_admin_delete_config_parameter @config_id={0},@ws={1},@publisher_id={2},@publisher_level={3}",
                ConfigId,
                ConfigurationManager.AppSettings["EuroCMS.WS"] ?? "local",
                publisherID,
                Publisherlevel)
                .ToList();
        }

        public int UpdateLocalConfigValue(string name, string value, object pubId)
        {
            return this.Database.ExecuteSqlCommand
                 ("dbo.cms_asp_config_update_local_value " +
                    "@config_name={0}, " +
                    "@config_value_local={1}, " +
                    "@publisher_id={2}",
                     name,
                     value,
                     pubId);
        }

        public int UpdateRemoteConfigValue(string name, string value, object pubId)
        {
            return this.Database.ExecuteSqlCommand
                 ("dbo.cms_asp_config_update_remote_value " +
                    "@config_name={0}, " +
                    "@config_value_remote={1}, " +
                    "@publisher_id={2}",
                     name,
                     value,
                     pubId);
        }

        public cms_asp_config_check_done_status_Result CheckDoneStatus(string lip)
        {
            return this.Database.SqlQuery<cms_asp_config_check_done_status_Result>
                ("dbo.cms_asp_config_select_config_parameters")
                .ToList().FirstOrDefault();
        }

        public int InsertCacheServer(string server_ip)
        {
            return this.Database.ExecuteSqlCommand
                 ("dbo.cms_asp_admin_insert_cache_server " +
                    "@server_ip={0} ",
                     server_ip);
        }
    }
}