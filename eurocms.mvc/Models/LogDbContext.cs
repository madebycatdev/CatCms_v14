using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using EuroCMS.Admin.Models;
using EuroCMS.Admin.entity;
using EuroCMS.Model;

namespace eurocms.mvc.Models
{
    public class LogDbContext : BaseDbContext
    {

        public DbSet<cms_breadcrumbs> Logs { get; set; }

        public LogDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_publisher_logs>()
                 .Map(m => m.ToTable("cms_publisher_logs"));

            base.OnModelCreating(modelBuilder);
        }
    }
}