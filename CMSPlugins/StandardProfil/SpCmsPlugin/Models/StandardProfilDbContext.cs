using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.CMSPlugin.StandardProfil.Models
{
    public class StandardProfilDbContext : CmsDbContext
    {
        public DbSet<TraceLog> TraceLogs { get; set; }
        public DbSet<CountryList> CountryLists { get; set; }
        public DbSet<InvestorUser> InvestorUsers { get; set; }
        public DbSet<ImportInvestorUser> ImportInvestorUsers { get; set; }
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
        [NotMapped]
        public string DecryptPassword { get; set; }

    }

    public class ImportInvestorUser
    {
        public int Id { get; set; }
        public string ImportGroupName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}