using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.CMSPlugin.IKSV.Models
{
    public class IksvDbContext : CmsDbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
    }

    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Event { get; set; }
        public int EventId { get; set; }
        public bool Kvkk { get; set; }
        public bool IsAccept { get; set; }
        public DateTime CreateDt { get; set; }
        public int Status { get; set; }
    }
}