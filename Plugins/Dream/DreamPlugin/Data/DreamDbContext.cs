using EuroCMS.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DreamPlugin.Data
{
    public class DreamDbContext : CmsDbContext
    {
        public DbSet<ContactForm> ContactFormDatas { get; set; }
    }

    [Table("ContactFormDatas")]
    public class ContactForm
    {
        [Key]
        public int Id { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; }
        public string Message { get; set; }
        public bool? IsAllowSMS { get; set; }
        public bool? IsAllowEmail { get; set; }
        public bool IsConfirmKVKK { get; set; }
        public bool IsConfirmPromotion { get; set; }
        public string IpAddress { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}