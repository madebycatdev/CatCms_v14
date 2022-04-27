using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EuroCMS.Admin.entity;
using System.Web.Security;
using EuroCMS.Model;


namespace EuroCMS.Admin.Models
{
    public class ZoneDbContext : BaseDbContext
    {
        public DbSet<cms_zones> Zones { get; set; }
        public DbSet<cms_zone_revision> ZoneRevisions { get; set; }

        public ZoneDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_zones>()
                  .HasMany(z => z.articles)
                  .WithMany(a => a.zones)
                  .Map(m =>
                  {
                      m.ToTable("cms_article_zones");
                      m.MapLeftKey("zone_id");
                      m.MapRightKey("article_id");
                  });

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_all_zones_Result> SelectAllZones()
        {
            List<cms_asp_select_all_zones_Result> returnList = new List<cms_asp_select_all_zones_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<Zone> listZone = new List<Zone>();
            listZone = dbContext.Zones.Where(z => z.Status != "D").ToList();

            foreach (Zone zone in listZone)
            {
                cms_asp_select_all_zones_Result zoneResult = new cms_asp_select_all_zones_Result();

                ZoneGroup zoneGroup = new ZoneGroup();
                zoneGroup = dbContext.ZoneGroups.Where(zg => zg.Id == zone.ZoneGroupId).FirstOrDefault();

                Site site = new Site();
                site = dbContext.Sites.Where(s => s.Id == zoneGroup.SiteId).FirstOrDefault();

                zoneResult.site_name = site.Name;
                zoneResult.zone_group_name = zoneGroup.Name;
                zoneResult.zone_id = zone.Id;
                zoneResult.zone_name = zone.Name;
                returnList.Add(zoneResult);
            }

            returnList = returnList.OrderBy(o => o.site_name).ThenBy(o => o.zone_group_name).ThenBy(o => o.zone_name).ToList();
            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_all_zones_Result>
            //    ("dbo.cms_asp_select_all_zones")
            //    .ToList();
        }

        public List<cms_asp_select_zones_by_group_Result> SelectZonesByZoneGroup(int? ZoneGroupID)
        {

            List<cms_asp_select_zones_by_group_Result> returnList = new List<cms_asp_select_zones_by_group_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<Zone> listZone = new List<Zone>();

            if (ZoneGroupID == null | ZoneGroupID == -1)
            {
                listZone = dbContext.Zones.Where(z => z.Status != "D").ToList();
            }
            else
            {
                listZone = dbContext.Zones.Where(z => z.ZoneGroupId == ZoneGroupID && z.Status != "D").ToList();
            }

            foreach (Zone zone in listZone)
            {
                cms_asp_select_zones_by_group_Result zoneResult = new cms_asp_select_zones_by_group_Result();

                ZoneGroup zoneGroup = new ZoneGroup();
                zoneGroup = dbContext.ZoneGroups.Where(zg => zg.Id == zone.ZoneGroupId).FirstOrDefault();

                Site site = new Site();
                site = dbContext.Sites.Where(s => s.Id == zoneGroup.SiteId).FirstOrDefault();

                vAspNetMembershipUser user = new vAspNetMembershipUser();
                user = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == zone.CreatedBy).FirstOrDefault();

                zoneResult.zone_id = zone.Id;
                zoneResult.zone_desc = zone.Description;
                zoneResult.zone_status = zone.Status;
                zoneResult.site_name = site.Name;
                zoneResult.zone_group_name = zoneGroup.Name;
                zoneResult.zone_name = zone.Name;
                zoneResult.publisher_name = user.UserName;
                zoneResult.created = zone.Created;
                zoneResult.updated = zone.Updated;
                zoneResult.zone_type_id = zone.ZoneTypeId;
                zoneResult.locked = zone.Locked;
                //zoneResult.locked_by = zone.locked_by;
                // Procedure'de locked ve locked_by var bunlar için bakılacak.
                returnList.Add(zoneResult);
            }


            return returnList;

            //return this.Database.SqlQuery<cms_asp_select_zones_by_group_Result>
            //    ("dbo.cms_asp_select_zones_by_group @zone_group_id={0}",
            //    ZoneGroupID == null ? -1 : ZoneGroupID)
            //    .ToList();
        }

        public List<long> SelectZoneLastRevision(int ZoneID)
        {
            CmsDbContext dbContext = new CmsDbContext();

            List<ZoneRevision> listRevision = new List<ZoneRevision>();

            List<long> returnList = new List<long>();
            long returnItem = 0;
            listRevision = dbContext.ZoneRevisions.Where(zr => zr.ZoneId == ZoneID && zr.ZoneStatus != "D" && zr.RevisionStatus == "L").OrderByDescending(o => o.RevisionDate).ToList();
            if (listRevision != null && listRevision.Count > 0)
            {
                returnItem = listRevision.Take(1).FirstOrDefault().RevisionId;
            }
            else
            {
                listRevision = dbContext.ZoneRevisions.Where(zr => zr.ZoneId == ZoneID && zr.ZoneStatus != "D").OrderBy(ob => ob.RevisionStatus).OrderByDescending(o => o.RevisionDate).ToList();
                if (listRevision.Count > 0)
                {
                    returnItem = listRevision.Take(1).FirstOrDefault().RevisionId;
                }
            }

            returnList.Add(returnItem);

            return returnList;

            //return this.Database.SqlQuery<long>("dbo.cms_asp_admin_select_zone_last_revision @zone_id={0}", ZoneID).ToList();
        }

        public List<cms_asp_admin_select_zone_revision_list_Result> SelectZoneRevisions(int ZoneID)
        {
            List<cms_asp_admin_select_zone_revision_list_Result> returnList = new List<cms_asp_admin_select_zone_revision_list_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            List<ZoneRevision> listZoneRevision = new List<ZoneRevision>();

            listZoneRevision = dbContext.ZoneRevisions.Where(zr => zr.ZoneId == ZoneID && zr.RevisionStatus != "X").OrderByDescending(o => o.RevisionDate).Take(50).ToList();

            foreach (ZoneRevision zoneRevision in listZoneRevision)
            {
                cms_asp_admin_select_zone_revision_list_Result caas = new cms_asp_admin_select_zone_revision_list_Result();

                string revisedName = "";
                string approvalName = "";

                vAspNetMembershipUser user = new vAspNetMembershipUser();
                user = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == zoneRevision.RevisedBy).FirstOrDefault();

                if (user != null)
                {
                    revisedName = user.UserName;
                }

                user = new vAspNetMembershipUser();
                user = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == zoneRevision.ApprovedBy).FirstOrDefault();
                if (user != null)
                {
                    approvalName = user.UserName;
                }

                caas.rev_id = zoneRevision.RevisionId;
                caas.rev_date = zoneRevision.RevisionDate;
                caas.revision_status = zoneRevision.RevisionStatus;
                caas.approval_date = zoneRevision.Approved;
                caas.zone_status = zoneRevision.ZoneStatus;
                caas.rev_name = zoneRevision.RevisionName;
                caas.revised_name = revisedName;
                caas.approval_name = approvalName;
                returnList.Add(caas);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_select_zone_revision_list_Result>("dbo.cms_asp_admin_select_zone_revision_list @zone_id={0}", ZoneID).ToList();
        }

        public cms_zone_revision SelectZonesRevisionDetails(long RevisionID)
        {
            cms_zone_revision returnZoneRevision = new cms_zone_revision();

            CmsDbContext dbContext = new CmsDbContext();

            ZoneRevision zoneRevision = new ZoneRevision();
            zoneRevision = dbContext.ZoneRevisions.Where(z => z.RevisionId == RevisionID).FirstOrDefault();

            if (zoneRevision != null)
            {
                string revisedName = "";
                string approvalName = "";
                string publisherName = "";

                vAspNetMembershipUser user = new vAspNetMembershipUser();
                Zone zone = new Zone();

                zone = dbContext.Zones.Where(z => z.Id == zoneRevision.ZoneId).FirstOrDefault();

                returnZoneRevision.rev_id = zoneRevision.RevisionId;
                returnZoneRevision.rev_date = zoneRevision.RevisionDate;
                returnZoneRevision.revision_status = zoneRevision.RevisionStatus;
                returnZoneRevision.zone_id = zoneRevision.ZoneId;
                returnZoneRevision.zone_name = zoneRevision.Name;
                returnZoneRevision.zone_desc = zoneRevision.Description;
                returnZoneRevision.zone_keywords = zoneRevision.Keywords;
                returnZoneRevision.revised_by = zoneRevision.RevisedBy;
                returnZoneRevision.approval_date = zoneRevision.Approved;
                returnZoneRevision.approval_id = zoneRevision.ApprovedBy;
                returnZoneRevision.css_merge = zoneRevision.CssMerge;
                returnZoneRevision.css_id = zoneRevision.CssId;
                returnZoneRevision.template_id = zoneRevision.TemplateId;
                returnZoneRevision.zone_group_id = zoneRevision.ZoneGroupId;
                returnZoneRevision.zone_status = zoneRevision.ZoneStatus;
                returnZoneRevision.current_status = zone.Status;
                returnZoneRevision.css_id_mobile = zoneRevision.MobileCssId;
                returnZoneRevision.css_id_print = zoneRevision.PrintCssId;
                returnZoneRevision.template_id_mobile = zoneRevision.MobileTemplateId;
                returnZoneRevision.custom_body = zoneRevision.CustomBody;
                returnZoneRevision.append_1 = zoneRevision.Append1;
                returnZoneRevision.append_2 = zoneRevision.Append2;
                returnZoneRevision.append_3 = zoneRevision.Append3;
                returnZoneRevision.append_4 = zoneRevision.Append4;
                returnZoneRevision.append_5 = zoneRevision.Append5;
                returnZoneRevision.article_1 = zoneRevision.Article1;
                returnZoneRevision.article_2 = zoneRevision.Article2;
                returnZoneRevision.article_3 = zoneRevision.Article3;
                returnZoneRevision.article_4 = zoneRevision.Article4;
                returnZoneRevision.article_5 = zoneRevision.Article5;
                returnZoneRevision.rev_name = zoneRevision.RevisionName;
                returnZoneRevision.rev_note = zoneRevision.RevisionNote;
                returnZoneRevision.zone_type_id = zoneRevision.ZoneTypeId;
                returnZoneRevision.analytics = zoneRevision.Analytics;

                user = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == zoneRevision.RevisedBy).FirstOrDefault();
                if (user != null)
                {
                    revisedName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : "";
                }

                user = new vAspNetMembershipUser();
                user = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == zoneRevision.ApprovedBy).FirstOrDefault();
                if (user != null)
                {
                    approvalName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : "";
                }

                user = new vAspNetMembershipUser();
                user = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == zone.CreatedBy).FirstOrDefault();
                if (user != null)
                {
                    publisherName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : "";
                }

                returnZoneRevision.revised_name = revisedName;
                returnZoneRevision.approval_name = approvalName;
                returnZoneRevision.publisher_name = publisherName;
                returnZoneRevision.meta_description = zoneRevision.MetaDescription;
                returnZoneRevision.zone_name_display = zoneRevision.Zone.DisplayName;
                returnZoneRevision.content_1_editor_type = zoneRevision.ContentEditorType1;
                returnZoneRevision.content_2_editor_type = zoneRevision.ContentEditorType2;
                returnZoneRevision.content_3_editor_type = zoneRevision.ContentEditorType3;
                returnZoneRevision.content_4_editor_type = zoneRevision.ContentEditorType4;
                returnZoneRevision.content_5_editor_type = zoneRevision.ContentEditorType5;
                returnZoneRevision.default_article = zoneRevision.DefaultArticle;
                returnZoneRevision.omniture_code = zoneRevision.OmnitureCode;
                returnZoneRevision.lang_id = zoneRevision.LangId;
            }


            return returnZoneRevision;

            //cms_asp_admin_select_zone_revision_details_Result r = this.Database.SqlQuery<cms_asp_admin_select_zone_revision_details_Result>
            //    ("dbo.cms_asp_admin_select_zone_revision_details @rev_id={0}",
            //    RevisionID)
            //    .ToList().FirstOrDefault();
            //
            //cms_zone_revision rz = new cms_zone_revision();
            //if (r != null)
            //{
            //    rz.analytics = r.analytics;
            //    rz.append_1 = r.append_1;
            //    rz.append_2 = r.append_2;
            //    rz.append_3 = r.append_3;
            //    rz.append_4 = r.append_4;
            //    rz.append_5 = r.append_5;
            //    rz.approval_date = r.approval_date;
            //    rz.approval_id = r.approval_id;
            //    rz.approval_name = r.approval_name;
            //    rz.article_1 = r.article_1;
            //    rz.article_2 = r.article_2;
            //    rz.article_3 = r.article_3;
            //    rz.article_4 = r.article_4;
            //    rz.article_5 = r.article_5;
            //    rz.content_1_editor_type = r.content_1_editor_type;
            //    rz.content_2_editor_type = r.content_2_editor_type;
            //    rz.content_3_editor_type = r.content_3_editor_type;
            //    rz.content_4_editor_type = r.content_4_editor_type;
            //    rz.content_5_editor_type = r.content_5_editor_type;
            //
            //
            //    rz.css_id = r.css_id;
            //    rz.css_id_mobile = r.css_id_mobile;
            //    rz.css_id_print = r.css_id_print;
            //
            //    rz.css_merge = r.css_merge;
            //    rz.current_status = r.current_status;
            //    rz.custom_body = r.custom_body;
            //    rz.default_article = r.default_article;
            //    rz.lang_id = r.lang_id;
            //
            //    rz.meta_description = r.meta_description;
            //    rz.omniture_code = r.omniture_code;
            //    rz.publisher_name = r.publisher_name;
            //    rz.rev_date = r.rev_date;
            //    rz.rev_id = r.rev_id;
            //    rz.rev_name = r.rev_name;
            //    rz.rev_note = r.rev_note;
            //    rz.revised_by = r.revised_by;
            //    rz.revised_name = r.revised_name;
            //    rz.revision_status = r.revision_status;
            //    rz.template_id = r.template_id;
            //    rz.template_id_mobile = r.template_id_mobile;
            //    rz.updated = r.updated;
            //    rz.zone_desc = r.zone_desc;
            //    rz.zone_group_id = r.zone_group_id;
            //
            //    rz.zone_id = r.zone_id;
            //    rz.zone_keywords = r.zone_keywords;
            //    rz.zone_name = r.zone_name;
            //    rz.zone_name_display = r.zone_name_display;
            //    rz.zone_status = r.zone_status;
            //    rz.zone_type_id = r.zone_type_id;
            //}
            //return rz;
        }

        public List<string> DiscardZoneRevision(long RevisionID, object ApprovalID)
        {
            List<string> returnList = new List<string>();

            ZoneRevisionRepository zrr = new ZoneRevisionRepository();
            ZoneRevisionService zoneRevisionService = new ZoneRevisionService(zrr);

            CmsDbContext dbContext = new CmsDbContext();

            List<ZoneRevision> listZoneRevision = new List<ZoneRevision>();

            listZoneRevision = dbContext.ZoneRevisions.Where(z => z.RevisionId == RevisionID && (z.RevisionStatus.Contains("N") || z.RevisionStatus.Contains("A") || z.RevisionStatus.Contains("W"))).ToList();

            if (listZoneRevision != null && listZoneRevision.Count > 0)
            {
                ZoneRevision updateZoneRevision = new ZoneRevision();

                updateZoneRevision = dbContext.ZoneRevisions.Where(z => z.RevisionId == RevisionID).FirstOrDefault();
                updateZoneRevision.RevisionStatus = "X";
                updateZoneRevision.Approved = DateTime.Now;
                updateZoneRevision.ApprovedBy = (Guid)ApprovalID;
                zoneRevisionService.Update(updateZoneRevision);
                returnList.Add("OK");
            }
            else
            {
                returnList.Add("STATUS");
            }

            return returnList;

            //return this.Database.SqlQuery<string>
            //    ("dbo.cms_asp_admin_update_zone_revision_status @rev_id={0},@revision_status={1},@approval_id={2}",
            //    RevisionID, "X", ApprovalID)
            //    .ToList();
        }

        // Entitiy Framework'e çevrilmedi
        public List<cms_asp_approval_approve_zone_revision_Result> ApproveZoneRevision(long RevisionID, int ApproveLevel, object publisherID, int PublisherLevel, string cio)
        {
            return this.Database.SqlQuery<cms_asp_approval_approve_zone_revision_Result>
                ("dbo.cms_asp_approval_approve_zone_revision @rev_id={0},@approve_level={1},@publisher_id={2},@publisher_level={3},@cio={4}",
                RevisionID, ApproveLevel, publisherID, PublisherLevel, cio)
                .ToList();
        }

        // Entitiy Framework'e çevrilmedi
        public List<cms_asp_admin_delete_zone_Result> DeleteZone(int ZoneID, int ApproveLevel, object publisherID, int PublisherLevel, string cio)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_zone_Result>
                ("dbo.cms_asp_admin_delete_zone @zone_id={0},@approve_level={1},@publisher_id={2},@publisher_level={3},@cio={4}",
                ZoneID, ApproveLevel, publisherID, PublisherLevel, cio)
                .ToList();
        }


        public List<cms_asp_admin_update_zone_revision_Result> SaveZoneRevision(cms_zone_revision revision)
        {

            List<cms_asp_admin_update_zone_revision_Result> returnList = new List<cms_asp_admin_update_zone_revision_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            ZoneRevisionRepository zrr = new ZoneRevisionRepository();
            ZoneRevisionService zoneRevisionService = new ZoneRevisionService(zrr);

            ZoneRepository zr = new ZoneRepository();
            ZoneService zoneService = new ZoneService(zr);

            //UserRepository ur = new UserRepository();
            //UserService userService = new UserService(ur);

            revision.zone_id = revision.zone_id == -1 ? 0 : revision.zone_id;
            revision.rev_id = revision.rev_id == -1 ? 0 : revision.rev_id;
            revision.rev_name = revision.rev_name ?? "";
            revision.rev_note = revision.rev_note ?? "";
            revision.zone_status = revision.zone_status ?? "P";
            revision.zone_desc = revision.zone_desc ?? "";
            revision.custom_body = revision.custom_body ?? "";
            revision.zone_keywords = revision.zone_keywords ?? "";
            revision.article_1 = HttpUtility.HtmlEncode(revision.article_1 ?? "");
            revision.article_2 = HttpUtility.HtmlEncode(revision.article_2 ?? "");
            revision.article_3 = HttpUtility.HtmlEncode(revision.article_3 ?? "");
            revision.article_4 = HttpUtility.HtmlEncode(revision.article_4 ?? "");
            revision.article_5 = HttpUtility.HtmlEncode(revision.article_5 ?? "");
            revision.analytics = revision.analytics ?? "H";
            revision.meta_description = revision.meta_description ?? "";
            revision.zone_name_display = revision.zone_name_display ?? "";
            revision.cio = revision.cio ?? "";
            revision.content_1_editor_type = revision.content_1_editor_type ?? "H";
            revision.content_2_editor_type = revision.content_2_editor_type ?? "H";
            revision.content_3_editor_type = revision.content_3_editor_type ?? "H";
            revision.content_4_editor_type = revision.content_4_editor_type ?? "H";
            revision.content_5_editor_type = revision.content_5_editor_type ?? "H";
            revision.default_article = revision.default_article ?? "";
            revision.omniture_code = revision.omniture_code ?? "";

            ZoneRevision getZoneRevision = new ZoneRevision();
            getZoneRevision = dbContext.ZoneRevisions.Where(z => z.ZoneId != revision.zone_id &&
                                                                      z.RevisionId == revision.rev_id &&
                                                                      z.ZoneGroupId == revision.zone_group_id &&
                                                                      z.ZoneStatus != "D").FirstOrDefault();
            cms_asp_admin_update_zone_revision_Result returnItem = new cms_asp_admin_update_zone_revision_Result();
            if (getZoneRevision == null)
            {
                if (revision.zone_id == -1)
                {
                    Zone insertZone = new Zone();
                    insertZone.ZoneGroupId = revision.zone_group_id;
                    insertZone.ZoneTypeId = revision.zone_type_id;
                    insertZone.Status = revision.zone_status;
                    insertZone.Name = revision.zone_name;
                    insertZone.Description = revision.zone_desc;
                    insertZone.CreatedBy = (Guid)revision.revised_by;

                    Zone getZone = zoneService.Insert(insertZone);
                    returnItem.zone_id = getZone.Id;
                    returnItem.zstat = "C";
                }

                if (revision.rev_id == -1)
                {
                    ZoneRevision insertZoneRevision = new ZoneRevision();
                    insertZoneRevision.RevisionName = revision.rev_name;
                    insertZoneRevision.RevisionNote = revision.rev_note;
                    insertZoneRevision.ZoneId = revision.zone_id;
                    insertZoneRevision.ZoneGroupId = revision.zone_group_id;
                    insertZoneRevision.ZoneTypeId = revision.zone_type_id;
                    insertZoneRevision.ZoneStatus = revision.zone_status;
                    insertZoneRevision.Name = revision.zone_name;
                    insertZoneRevision.Description = revision.zone_desc;
                    insertZoneRevision.CssMerge = revision.css_merge;
                    insertZoneRevision.CssId = revision.css_id;
                    insertZoneRevision.MobileCssId = revision.css_id_mobile;
                    insertZoneRevision.PrintCssId = revision.css_id_print;
                    insertZoneRevision.TemplateId = revision.template_id;
                    insertZoneRevision.MobileTemplateId = revision.template_id_mobile;
                    insertZoneRevision.CustomBody = revision.custom_body;
                    insertZoneRevision.Keywords = revision.zone_keywords;
                    insertZoneRevision.Article1 = revision.article_1;
                    insertZoneRevision.Article2 = revision.article_2;
                    insertZoneRevision.Article3 = revision.article_3;
                    insertZoneRevision.Article4 = revision.article_4;
                    insertZoneRevision.Article5 = revision.article_5;
                    insertZoneRevision.Append1 = revision.append_1;
                    insertZoneRevision.Append2 = revision.append_2;
                    insertZoneRevision.Append3 = revision.append_3;
                    insertZoneRevision.Append4 = revision.append_4;
                    insertZoneRevision.Append5 = revision.append_5;
                    insertZoneRevision.Analytics = revision.analytics;
                    insertZoneRevision.RevisedBy = (Guid)revision.revised_by;
                    insertZoneRevision.CreatedBy = (Guid)revision.created_by;
                    insertZoneRevision.MetaDescription = revision.meta_description;
                    insertZoneRevision.DisplayName = revision.zone_name_display;
                    insertZoneRevision.ContentEditorType1 = revision.content_1_editor_type;
                    insertZoneRevision.ContentEditorType2 = revision.content_2_editor_type;
                    insertZoneRevision.ContentEditorType3 = revision.content_3_editor_type;
                    insertZoneRevision.ContentEditorType4 = revision.content_4_editor_type;
                    insertZoneRevision.ContentEditorType5 = revision.content_5_editor_type;
                    insertZoneRevision.DefaultArticle = revision.default_article;
                    insertZoneRevision.OmnitureCode = revision.omniture_code;
                    insertZoneRevision.LangId = revision.lang_id;

                    ZoneRevision getZoneR = zoneRevisionService.Insert(insertZoneRevision);

                    returnItem.rev_id = getZoneR.RevisionId;
                    returnItem.rstat = "C";
                }
                else
                {
                    // Check for article lock
                    Zone getZone = dbContext.Zones.Where(z => z.Id == revision.zone_id && z.LockedBy == (Guid)revision.revised_by).FirstOrDefault();
                    if (getZone == null && revision.cio == "1")
                    {
                        //User getUser = new User();
                        MembershipUser getUser;
                        getZone = dbContext.Zones.Where(z => z.Id == revision.zone_id).FirstOrDefault();

                        if (getZone != null)
                        {
                            //getUser = userService.GetAll().Where(u => u.Id == getZone.LockedBy).FirstOrDefault();
                            getUser = Membership.GetUser(getZone.LockedBy);
                            returnItem.locked = getZone.Locked;
                            returnItem.locked_by = getUser.UserName;
                            returnItem.rstat = "L";
                        }
                    }

                    // Revision exist. Check status
                    ZoneRevision getZoneR = new ZoneRevision();
                    getZoneR = dbContext.ZoneRevisions.Where(s => s.RevisionId == revision.rev_id && s.RevisionStatus == "N").FirstOrDefault();

                    if (getZoneR != null)
                    {
                        ZoneRevision uZoneR = new ZoneRevision();
                        uZoneR = dbContext.ZoneRevisions.Where(z => z.RevisionId == revision.rev_id).FirstOrDefault();
                        if (uZoneR != null)
                        {
                            // Revision not approved yet.. Update it

                            uZoneR.RevisionName = revision.rev_name;
                            uZoneR.RevisionNote = revision.rev_note;
                            uZoneR.ZoneId = revision.zone_id;
                            uZoneR.ZoneGroupId = revision.zone_group_id;
                            uZoneR.ZoneTypeId = revision.zone_type_id;
                            uZoneR.ZoneStatus = revision.zone_status;
                            uZoneR.Name = revision.zone_name;
                            uZoneR.Description = revision.zone_desc;

                            uZoneR.CssMerge = revision.css_merge;
                            uZoneR.CssId = revision.css_id;
                            uZoneR.MobileCssId = revision.css_id_mobile;
                            uZoneR.PrintCssId = revision.css_id_print;
                            uZoneR.TemplateId = revision.template_id;
                            uZoneR.MobileTemplateId = revision.template_id_mobile;
                            uZoneR.CustomBody = revision.custom_body;
                            uZoneR.Keywords = revision.zone_keywords;
                            uZoneR.Article1 = revision.article_1;
                            uZoneR.Article2 = revision.article_2;
                            uZoneR.Article3 = revision.article_3;
                            uZoneR.Article4 = revision.article_4;
                            uZoneR.Article5 = revision.article_5;
                            uZoneR.Append1 = revision.append_1;
                            uZoneR.Append2 = revision.append_2;
                            uZoneR.Append3 = revision.append_3;
                            uZoneR.Append4 = revision.append_4;
                            uZoneR.Append5 = revision.append_5;

                            uZoneR.Analytics = revision.analytics;
                            uZoneR.RevisedBy = (Guid)revision.revised_by;
                            uZoneR.RevisionDate = DateTime.Now;
                            uZoneR.MetaDescription = revision.meta_description;
                            uZoneR.DisplayName = revision.zone_name_display;
                            uZoneR.ContentEditorType1 = revision.content_1_editor_type;
                            uZoneR.ContentEditorType2 = revision.content_2_editor_type;
                            uZoneR.ContentEditorType3 = revision.content_3_editor_type;
                            uZoneR.ContentEditorType4 = revision.content_4_editor_type;
                            uZoneR.ContentEditorType5 = revision.content_5_editor_type;

                            uZoneR.DefaultArticle = revision.default_article;
                            uZoneR.OmnitureCode = revision.omniture_code;
                            uZoneR.LangId = revision.lang_id;

                            zoneRevisionService.Update(uZoneR);

                            returnItem.rstat = "U";
                        }
                    }
                    else
                    {
                        // Revision already approved or discarded. Save as new revision

                        ZoneRevision iZoneR = new ZoneRevision();
                        iZoneR.RevisionName = revision.rev_name;
                        iZoneR.RevisionNote = revision.rev_note;
                        iZoneR.ZoneId = revision.zone_id;
                        iZoneR.ZoneGroupId = revision.zone_group_id;
                        iZoneR.ZoneTypeId = revision.zone_type_id;
                        iZoneR.ZoneStatus = revision.zone_status;
                        iZoneR.Name = revision.zone_name;
                        iZoneR.Description = revision.zone_desc;

                        iZoneR.CssMerge = revision.css_merge;
                        iZoneR.CssId = revision.css_id;
                        iZoneR.MobileCssId = revision.css_id_mobile;
                        iZoneR.PrintCssId = revision.css_id_print;
                        iZoneR.TemplateId = revision.template_id;
                        iZoneR.MobileTemplateId = revision.template_id_mobile;
                        iZoneR.CustomBody = revision.custom_body;
                        iZoneR.Keywords = revision.zone_keywords;
                        iZoneR.Article1 = revision.article_1;
                        iZoneR.Article2 = revision.article_2;
                        iZoneR.Article3 = revision.article_3;
                        iZoneR.Article4 = revision.article_4;
                        iZoneR.Article5 = revision.article_5;
                        iZoneR.Append1 = revision.append_1;
                        iZoneR.Append2 = revision.append_2;
                        iZoneR.Append3 = revision.append_3;
                        iZoneR.Append4 = revision.append_4;
                        iZoneR.Append5 = revision.append_5;

                        iZoneR.Analytics = revision.analytics;
                        iZoneR.RevisedBy = (Guid)revision.revised_by;
                        iZoneR.CreatedBy = (Guid)revision.revised_by;
                        iZoneR.MetaDescription = revision.meta_description;
                        iZoneR.DisplayName = revision.zone_name_display;
                        iZoneR.ContentEditorType1 = revision.content_1_editor_type;
                        iZoneR.ContentEditorType2 = revision.content_2_editor_type;
                        iZoneR.ContentEditorType3 = revision.content_3_editor_type;
                        iZoneR.ContentEditorType4 = revision.content_4_editor_type;
                        iZoneR.ContentEditorType5 = revision.content_5_editor_type;

                        iZoneR.DefaultArticle = revision.default_article;
                        iZoneR.OmnitureCode = revision.omniture_code;
                        iZoneR.LangId = revision.lang_id;

                        ZoneRevision getZR = zoneRevisionService.Insert(iZoneR);

                        returnItem.rev_id = getZR.RevisionId;
                        returnItem.rstat = "N";
                    }

                }


            }
            else
            {
                returnItem.rstat = "";
                returnItem.zstat = "D";
                returnItem.zone_id = -1;
                returnItem.rev_id = -1;
            }

            returnList.Add(returnItem);
            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_update_zone_revision_Result>
            //    ("dbo.cms_asp_admin_update_zone_revision @rev_id={0}," +
            //        "@rev_name={1}," +
            //        "@rev_note={2}," +
            //        "@zone_id={3}," +
            //        "@zone_group_id={4}," +
            //        "@zone_type_id={5}," +
            //        "@zone_status={6}," +
            //        "@zone_name={7}," +
            //        "@zone_desc={8}," +
            //        "@css_merge={9}," +
            //        "@css_id={10}," +
            //        "@css_id_mobile={11}," +
            //        "@css_id_print={12}," +
            //        "@template_id={13}," +
            //        "@template_id_mobile={14}," +
            //        "@custom_body={15}," +
            //        "@zone_keywords={16}," +
            //        "@article_1={17}," +
            //        "@article_2={18}," +
            //        "@article_3={19}," +
            //        "@article_4={20}," +
            //        "@article_5={21}," +
            //        "@append_1={22}," +
            //        "@append_2={23}," +
            //        "@append_3={24}," +
            //        "@append_4={25}," +
            //        "@append_5={26}," +
            //        "@analytics={27}," +
            //        "@revised_by={28}," +
            //        "@meta_description={29}," +
            //        "@zone_name_display={30}," +
            //        "@cio={31}," +
            //        "@content_1_editor_type={32}," +
            //        "@content_2_editor_type={33}," +
            //        "@content_3_editor_type={34}," +
            //        "@content_4_editor_type={35}," +
            //        "@content_5_editor_type={36}," +
            //        "@default_article={37}," +
            //        "@omniture_code={38}," +
            //        "@lang_id={39}",
            //    revision.rev_id == 0 ? -1 : revision.rev_id,
            //    revision.rev_name ?? "",
            //    revision.rev_note ?? "",
            //    revision.zone_id == 0 ? -1 : revision.zone_id,
            //    revision.zone_group_id,
            //    revision.zone_type_id,
            //    revision.zone_status ?? "P",
            //    revision.zone_name,
            //    revision.zone_desc ?? "",
            //    revision.css_merge,
            //    revision.css_id,
            //    revision.css_id_mobile,
            //    revision.css_id_print,
            //    revision.template_id,
            //    revision.template_id_mobile,
            //    revision.custom_body ?? "",
            //    revision.zone_keywords ?? "",
            //    HttpUtility.HtmlEncode(revision.article_1 ?? ""),
            //    HttpUtility.HtmlEncode(revision.article_2 ?? ""),
            //    HttpUtility.HtmlEncode(revision.article_3 ?? ""),
            //    HttpUtility.HtmlEncode(revision.article_4 ?? ""),
            //    HttpUtility.HtmlEncode(revision.article_5) ?? "",
            //    revision.append_1,
            //    revision.append_2,
            //    revision.append_3,
            //    revision.append_4,
            //    revision.append_5,
            //    revision.analytics ?? "H",
            //    revision.revised_by,
            //    revision.meta_description ?? "",
            //    revision.zone_name_display ?? "",
            //    revision.cio ?? "",
            //    revision.content_1_editor_type ?? "H",
            //    revision.content_2_editor_type ?? "H",
            //    revision.content_3_editor_type ?? "H",
            //    revision.content_4_editor_type ?? "H",
            //    revision.content_5_editor_type ?? "H",
            //    revision.default_article ?? "",
            //    revision.omniture_code ?? "",
            //    revision.lang_id)
            //    .ToList();
        }

    }
}