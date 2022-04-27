using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Plugin.Kale.Models
{
    public class KaleDbContext : CmsDbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }
    }

    [Table("contact_country")]
    public class Country
    {
        [Key]
        public string country_id { get; set; }
        public string Value { get; set; }
    }

    [Table("contact_city")]
    public class City
    {
        [Key]
        public string city_id { get; set; }
        public string Value { get; set; }
        public string country_id { get; set; }

    }

    [Table("contact_town")]
    public class Town
    {
        [Key]
        public string town_id { get; set; }
        public string Value { get; set; }
        public string city_id { get; set; }

    }
}