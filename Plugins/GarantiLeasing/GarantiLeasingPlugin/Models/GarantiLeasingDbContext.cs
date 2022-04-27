using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.GarantiLeasing.Models
{
    public class GarantiLeasingDbContext : CmsDbContext
    {
        public DbSet<CallMeForm> CallMeForms { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<HrForm> HrForms { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

        public DbSet<LeasingApplication> LeasingApplications { get; set; }
    }

    public class CallMeForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Kvkk { get; set; }
        public DateTime CreateDt { get; set; }
        public int Status { get; set; }
    }
    public class Complaint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string MarsNo { get; set; }
        public bool Kvkk { get; set; }
        public DateTime CreateDt { get; set; }
        public int Status { get; set; }
    }
    public class ContactForm
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string CompanyName { get; set; }
        public string CityTown { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public bool Kvkk { get; set; }
        public DateTime CreateDt { get; set; }
        public int Status { get; set; }
    }
    public class HrForm
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string FileUrl { get; set; }
        public string Address { get; set; }
        public bool Kvkk { get; set; }
        public DateTime CreateDt { get; set; }
        public int Status { get; set; }
    }
    public class Newsletter
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Kvkk { get; set; }
        public DateTime CreateDt { get; set; }
        public int Status { get; set; }
    }

    public class LeasingApplication
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public string ProductPrice { get; set; }
        public int Expiration { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Company { get; set; }
        public string Person { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Address{ get; set; }
        public bool IsAccept { get; set; }
        public DateTime CreateDt { get; set; }
        public int Status { get; set; }
        public string Source { get; set; }
    }
}