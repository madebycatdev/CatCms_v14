using EuroCMS.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EuroCMS.Plugin.Antur
{
    public class AnturDbContext : CmsDbContext
    {
        public DbSet<ContactForm> ContactFormDatas { get; set; }
    }

    [Table("ContactFormDatas")]
    public class ContactForm
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public bool? IsAllowSMS { get; set; }
        public bool? IsAllowEmail { get; set; }
        public bool ConfirmKVKK { get; set; }
        public bool ConfirmEnlightenment { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
