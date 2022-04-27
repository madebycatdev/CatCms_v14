using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.StandardProfil.Models
{
    public class StandardProfilDbContext : CmsDbContext
    {
        public DbSet<InvestorUser> InvestorUsers { get; set; }
        public DbSet<TraceLog> TraceLogs { get; set; }
        public DbSet<CountryList> CountryLists { get; set; }
    }

    public class TraceLog
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string SelectedCountryCode { get; set; }
        public string SelectedCountryName { get; set; }
        public string Name { get; set; }
        public string NameOfQib { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public bool Permission { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
    }

    public class CountryList
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
    }

    public class InvestorUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public bool isExcelUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LoginToken { get; set; }

    }
}