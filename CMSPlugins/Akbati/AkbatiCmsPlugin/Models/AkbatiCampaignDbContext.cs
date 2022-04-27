using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.CMSPlugin.AKBATI.Models
{
    public class AkbatiCampaignDbContext : CmsDbContext
    {
        public DbSet<AkbatiCampaign> AkbatiCampaigns { get; set; }
    }

    public class AkbatiCampaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Town { get; set; }
        public string Photo { get; set; }
        public DateTime CreateDt { get; set; }
        public int Status { get; set; }
    }

   
}