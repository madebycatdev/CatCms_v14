using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EuroCMS.Admin.entity;
using EuroCMS.Model;
using EuroCMS.Core;
using System.EnterpriseServices;


namespace EuroCMS.Admin.Models
{


	public class ArticleOrderDbContext : BaseDbContext
	{

        public ArticleOrderDbContext()
			: base()
		{ }

		// Entity Framework'e çevrilmedi
		public List<cms_article_search_result> SearchArticles(int? ZoneID)
		{
			string zone_joiner = "";
			string sql = "";

			if (ZoneID > 0)
				zone_joiner = " and zr.zone_id = " + ZoneID;

			sql = "select top 1000 zr.az_order, r.article_id, r.rev_id, r.rev_name, r.created, r.created_by, r.rev_date, r.revised_by, r.menu_text, r.headline, r.summary, r.startdate, r.enddate, r.approval_date, r.approval_id, r.revision_status, r.status, a.clicks, " +
						 " (select top 1 cs.site_name + ' | ' + czg.zone_group_name + ' | ' + cz.zone_name from dbo.cms_zones cz with (nolock) left join dbo.cms_zone_groups czg with (nolock) on czg.zone_group_id = cz.zone_group_id left join dbo.cms_sites cs with (nolock) on cs.site_id = czg.site_id where cz.zone_id = zr.zone_id) as zone_name, zr.zone_id, a.locked_by, a.locked, p.UserName, zr.az_alias" +
						 " from dbo.vArticlesLiveRevisions r with (nolock) inner join dbo.cms_article_zones_revision zr with (nolock) on zr.article_id = r.article_id  " + zone_joiner + " and  zr.rev_id = r.rev_id left join dbo.cms_articles a with (nolock) on a.article_id = r.article_id left join dbo.vw_aspnet_MembershipUsers p with (nolock)  on a.locked_by = p.UserId   where 1 = 1  order by zr.az_order";

			return this.Database.SqlQuery<cms_article_search_result>
				(sql)
				.ToList();
		}

	}
}