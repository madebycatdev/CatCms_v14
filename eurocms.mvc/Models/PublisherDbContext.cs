using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class PublisherDbContext : BaseDbContext
    {
        public DbSet<cms_publishers> Publishers { get; set; }


        public PublisherDbContext()
            : base()
        {

        }
        [Required]

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }


        public List<cms_asp_login_select_publisher_by_username_Result> IsValid(string _username, string _password, bool rememberme)
        {
            List<cms_asp_login_select_publisher_by_username_Result> _publisher = this.Database.SqlQuery<cms_asp_login_select_publisher_by_username_Result>
                ("dbo.cms_asp_login_select_publisher_by_username  @username={0}",
                _username == null ? "-" : _username).ToList();
            UserName = _username;


            return _publisher;
        }
        public List<cms_asp_select_publishers_Result> SelectPublishers()
        {
            return this.Database.SqlQuery<cms_asp_select_publishers_Result>
                ("dbo.cms_asp_select_publishers")
                .ToList();
        }
        public List<cms_asp_select_publisher_details_Result> SelectPublisher(int PublisherID)
        {
            return this.Database.SqlQuery<cms_asp_select_publisher_details_Result>
                ("dbo.cms_asp_select_publisher_details  @publisher_id = {0}",
                    PublisherID)
                .ToList();
        }
        public List<cms_asp_admin_update_domains_Result> CreatePublisher(cms_publishers publisher)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_domains_Result>
                ("dbo.cms_asp_admin_update_publisher_details @publisher_id = {0}, @publisher_name = {1}, @username = {2}, @password = {3}, @publisher_status = {4}, @publisher_email = {5}, @publisher_level = {6}, @publisher_department = {7}, @publisher_note = {8}, @updated_by = {9}",
                    -1,
                    publisher.publisher_name,
                   publisher.username,
                    publisher.password,
                     publisher.publisher_status,
                      publisher.publisher_email,
                       publisher.publisher_level,
                        publisher.publisher_department,
                         publisher.publisher_note,
                         HttpContext.Current.Session["publisher_id"]
                         )
                .ToList();
        }

        public List<cms_asp_admin_delete_domain_Result> DeletePublisher(int DomainID, object  publisherID, int PublisherLevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_domain_Result>
                ("dbo.cms_asp_admin_delete_publisher @publisher_id = {0}, @admin_id = {1}, @publisher_level = {2}",
                    DomainID,
                    publisherID,
                    PublisherLevel)
                .ToList();
        }

        public List<cms_asp_admin_update_publisher_details_Result> UpdatePublisher(cms_publishers publisher)
        {
             
            return this.Database.SqlQuery<cms_asp_admin_update_publisher_details_Result>
                ("dbo.cms_asp_admin_update_publisher_details @publisher_id = {0}, @publisher_name = {1}, @username = {2}, @password = {3}, @publisher_status = {4}, @publisher_email = {5}, @publisher_level = {6}, @publisher_department = {7}, @publisher_note = {8}, @updated_by = {9}",
                    publisher.publisher_id,
                    publisher.publisher_name,
                    publisher.username,
                    publisher.password,
                    publisher.publisher_status,
                    publisher.publisher_email,
                    publisher.publisher_level,
                    publisher.publisher_department,
                    publisher.publisher_note,
                    publisher.updated_by)
                 .ToList();
        }
    }
}
