using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class LayoutDbContext : BaseDbContext
    {
        public DbSet<cms_templates> Layouts { get; set; }
        public DbSet<cms_template_revisions> LayoutRevisions { get; set; }

        public LayoutDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_templates>()
                .Map(m => m.ToTable("cms_templates"));

            modelBuilder.Entity<cms_template_revisions>()
                .Map(m => m.ToTable("cms_template_revisions"));


            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_admin_select_templates_Result> SelectLayouts(int? GroupID)
        {
            List<cms_asp_admin_select_templates_Result> returnList = new List<cms_asp_admin_select_templates_Result>();

            // Procedure'de statik olarak 'a' verilmiş.
            string templateStatus = "A";

            CmsDbContext dbContext = new CmsDbContext();

            if (GroupID == null)
            {
                GroupID = -1;
            }

            List<Template> listTemplates = new List<Template>();

            if (GroupID == -1)
            {
                listTemplates = dbContext.Templates.Where(t => t.Status == templateStatus).ToList();
            }
            else
            {
                listTemplates = dbContext.Templates.Where(t => t.GroupID == GroupID && t.Status == templateStatus).ToList();
                //listTemplates = dbContext.Templates.Where(t =>  t.Status == templateStatus).ToList();
            }

            foreach (Template temp in listTemplates)
            {
                StructureGroup getStructureGroup = new StructureGroup();
                getStructureGroup = dbContext.StructureGroups.Where(s => s.Id == temp.GroupID).FirstOrDefault();

                string groupName = "";

                if (getStructureGroup != null)
                {
                    if (!string.IsNullOrEmpty(getStructureGroup.Name))
                    {
                        groupName = getStructureGroup.Name;
                    }
                }

                cms_asp_admin_select_templates_Result caas = new cms_asp_admin_select_templates_Result();
                caas.template_id = temp.Id;
                caas.template_type = Convert.ToByte(temp.Type);
                caas.template_name = temp.Name;
                caas.publisher_id = temp.PublisherID;
                caas.group_id = temp.GroupID;
                caas.created = temp.Created;
                caas.updated = temp.Updated;
                caas.publisher_name = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == temp.PublisherID).FirstOrDefault().UserName;
                caas.group_name = groupName;
                returnList.Add(caas);
            }

            returnList = returnList.OrderBy(o => o.group_name).ThenBy(o => o.publisher_name).ToList();

            return returnList;
            //return this.Database.SqlQuery<cms_asp_admin_select_templates_Result> ("dbo.cms_asp_admin_select_templates @group_id={0}", GroupID == null ? 0 : GroupID).ToList();
        }

        public List<cms_asp_admin_select_template_revisions_Result> SelectLayoutRevisions(int LayoutID)
        {
            List<cms_asp_admin_select_template_revisions_Result> returnList = new List<cms_asp_admin_select_template_revisions_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            if (LayoutID == null)
            {
                LayoutID = 0;
            }

            List<TemplateRevision> listTemplateRevisions = new List<TemplateRevision>();
            listTemplateRevisions = dbContext.TemplateRevisions.Where(t => t.TemplateID == LayoutID).OrderByDescending(o => o.Created).Take(50).ToList();

            foreach (TemplateRevision templateRev in listTemplateRevisions)
            {
                cms_asp_admin_select_template_revisions_Result caas = new cms_asp_admin_select_template_revisions_Result();
                caas.template_id = templateRev.Id;
                caas.created = templateRev.Created;
                caas.history_id = templateRev.Id;
                caas.publisher_id = templateRev.PublisherID;
                caas.publisher_name = dbContext.vAspNetMembershipUsers.Where(u => u.UserId == templateRev.PublisherID).FirstOrDefault().UserName;
                returnList.Add(caas);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_select_template_revisions_Result>("dbo.cms_asp_admin_select_template_revisions @template_id={0}",LayoutID).ToList();
        }

        public cms_templates SelectLayout(int LayoutID)
        {
            //cms_asp_select_template_history_html_Result c =
            //    this.Database.SqlQuery<cms_asp_select_template_history_html_Result>
            //    ("dbo.cms_asp_select_template_html @template_id = {0}",
            //        LayoutID).FirstOrDefault();

            CmsDbContext dbContext = new CmsDbContext();

            Template temp = new Template();
            cms_templates t = new cms_templates();
            temp = dbContext.Templates.Where(s => s.Id == LayoutID).FirstOrDefault();
            t.template_id = temp.Id;
            t.template_name = temp.Name;
            t.template_html = temp.Html;
            t.template_type = temp.Type;
            t.group_id = temp.GroupID;
            t.structure_description = temp.StructureDescription;
            t.content_1_editor_type = temp.Content1EditorType;
            t.template_doctype = temp.TemplateDoctype;

            return t;
        }

        public List<cms_templates> SelectLayoutRevision(long RevisionID)
        {
            List<cms_templates> returnList = new List<cms_templates>();

            CmsDbContext dbContext = new CmsDbContext();

            List<TemplateRevision> listTemplateRevision = new List<TemplateRevision>();

            listTemplateRevision = dbContext.TemplateRevisions.Where(t => t.Id == RevisionID).ToList();

            foreach (TemplateRevision item in listTemplateRevision)
            {
                cms_templates returnTemplate = new cms_templates();
                Template template = new Template();
                template = dbContext.Templates.Where(t => t.Id == item.TemplateID).FirstOrDefault();

                returnTemplate.content_1_editor_type = item.Content1EditorType;
                returnTemplate.created = template.Created;
                returnTemplate.updated = template.Updated;
                returnTemplate.group_id = template.GroupID;
                returnTemplate.publisher_id = template.PublisherID;
                returnTemplate.template_name = template.Name;
                returnTemplate.template_html = item.TemplateHtml;
                returnTemplate.template_type = item.TemplateType;
                returnTemplate.structure_description = template.StructureDescription;
                returnTemplate.template_doctype = item.TemplateDoctype;
                returnTemplate.template_id = template.Id;
                returnList.Add(returnTemplate);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_templates>("dbo.cms_asp_select_template_history_html @history_id = {0}",RevisionID).ToList();
        }

        public List<cms_asp_admin_update_template_Result> CreateLayout(cms_templates layout)
        {

            CmsDbContext dbContext = new CmsDbContext();

            TemplateRepository tr = new TemplateRepository();
            TemplateService templateService = new TemplateService(tr);

            TemplateRevisionRepository trr = new TemplateRevisionRepository();
            TemplateRevisionService templateRevisionService = new TemplateRevisionService(trr);

            List<cms_asp_admin_update_template_Result> returnList = new List<cms_asp_admin_update_template_Result>();

            List<Template> listTemplates = new List<Template>();
            listTemplates = dbContext.Templates.Where(s => s.Id != -1 && s.Name.Trim() == layout.template_name.Trim() && s.Status == "A").ToList();

            if (listTemplates == null || listTemplates.Count <= 0)
            {

                //listTemplates = templateService.GetAll().Where(s => s.Id != -1).ToList();

                if (layout.template_id > 0)
                {
                    TemplateRevision insertTempRevision = new TemplateRevision();
                    insertTempRevision.TemplateID = layout.template_id;
                    insertTempRevision.TemplateHtml = HttpUtility.HtmlEncode(layout.template_html);
                    insertTempRevision.PublisherID = (Guid)layout.publisher_id;
                    insertTempRevision.TemplateType = layout.template_type;
                    insertTempRevision.Content1EditorType = layout.content_1_editor_type;
                    insertTempRevision.TemplateDoctype = HttpUtility.HtmlEncode(layout.template_doctype);
                    templateRevisionService.Insert(insertTempRevision);

                    // Update
                    Template updateTemplate = new Template();

                    updateTemplate = dbContext.Templates.Where(s => s.Id == layout.template_id).FirstOrDefault();
                    updateTemplate.Name = layout.template_name;
                    updateTemplate.Html = HttpUtility.HtmlEncode(layout.template_html);
                    updateTemplate.PublisherID = (Guid)layout.publisher_id;
                    updateTemplate.Type = layout.template_type;
                    updateTemplate.Updated = DateTime.Now;
                    updateTemplate.GroupID = layout.group_id;
                    updateTemplate.StructureDescription = layout.structure_description;
                    updateTemplate.Content1EditorType = layout.content_1_editor_type;
                    updateTemplate.TemplateDoctype = HttpUtility.HtmlEncode(layout.template_doctype);
                    templateService.Update(updateTemplate);

                    cms_asp_admin_update_template_Result caautr = new cms_asp_admin_update_template_Result();
                    caautr.created = DateTime.Now;
                    caautr.template_id = -1;
                    caautr.tStat = "U";
                    returnList.Add(caautr);
                }
                else
                {
                    Template getInsertTemplate = new Template();

                    Template insertTemp = new Template();
                    insertTemp.Name = layout.template_name;
                    insertTemp.Html = HttpUtility.HtmlEncode(layout.template_html);
                    insertTemp.PublisherID = (Guid)layout.publisher_id;
                    insertTemp.Type = layout.template_type;
                    insertTemp.GroupID = layout.group_id;
                    insertTemp.StructureDescription = layout.structure_description;
                    insertTemp.Content1EditorType = layout.content_1_editor_type;
                    insertTemp.TemplateDoctype = HttpUtility.HtmlEncode(layout.template_doctype);
                    getInsertTemplate = templateService.Insert(insertTemp);

                    cms_asp_admin_update_template_Result caautr = new cms_asp_admin_update_template_Result();
                    caautr.created = DateTime.Now;
                    caautr.template_id = getInsertTemplate.Id;
                    caautr.tStat = "I";
                    returnList.Add(caautr);
                }

            }
            else
            {
                cms_asp_admin_update_template_Result caautr = new cms_asp_admin_update_template_Result();
                caautr.created = "";
                caautr.template_id = "";
                caautr.tStat = "D";
                returnList.Add(caautr);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_update_template_Result>
            //("dbo.cms_asp_admin_update_template @template_id={0},@template_name={1},@template_html={2},@template_type={3},@publisher_id={4},@group_id={5},@structure_description={6},@content_1_editor_type={7},@template_doctype={8} ",
            //     -1,
            //     layout.template_name,
            //     HttpUtility.HtmlEncode(layout.template_html),
            //     layout.template_type,
            //     layout.publisher_id,
            //     layout.group_id,
            //     layout.structure_description,
            //     layout.content_1_editor_type,
            //     HttpUtility.HtmlEncode(layout.template_doctype)
            //     )
            // .ToList();
        }

        public List<cms_asp_admin_update_template_Result> UpdateLayout(cms_templates layout)
        {
            CmsDbContext dbContext = new CmsDbContext();

            TemplateRepository tr = new TemplateRepository();
            TemplateService templateService = new TemplateService(tr);

            TemplateRevisionRepository trr = new TemplateRevisionRepository();
            TemplateRevisionService templateRevisionService = new TemplateRevisionService(trr);

            List<cms_asp_admin_update_template_Result> returnList = new List<cms_asp_admin_update_template_Result>();

            Template getTemplate = new Template();
            getTemplate = dbContext.Templates.Where(s => s.Id == layout.template_id).ToList().FirstOrDefault();
            if (getTemplate != null)
            {
                //listTemplates = templateService.GetAll().Where(s => s.Id != -1).ToList();

                if (layout.template_id > 0)
                {
                    TemplateRevision insertTempRevision = new TemplateRevision();
                    insertTempRevision.TemplateID = layout.template_id;
                    insertTempRevision.TemplateHtml = HttpUtility.HtmlEncode(layout.template_html);
                    insertTempRevision.PublisherID = (Guid)layout.publisher_id;
                    insertTempRevision.TemplateType = layout.template_type;
                    insertTempRevision.Content1EditorType = layout.content_1_editor_type;
                    insertTempRevision.TemplateDoctype = layout.template_doctype;
                    templateRevisionService.Insert(insertTempRevision);

                    // Update
                    Template updateTemplate = new Template();

                    updateTemplate = dbContext.Templates.Where(s => s.Id == layout.template_id).FirstOrDefault();
                    updateTemplate.Name = layout.template_name;
                    updateTemplate.Html = HttpUtility.HtmlEncode(layout.template_html);
                    updateTemplate.PublisherID = (Guid)layout.publisher_id;
                    updateTemplate.Type = layout.template_type;
                    updateTemplate.Updated = DateTime.Now;
                    updateTemplate.GroupID = layout.group_id;
                    updateTemplate.StructureDescription = layout.structure_description;
                    updateTemplate.Content1EditorType = layout.content_1_editor_type;
                    updateTemplate.TemplateDoctype = HttpUtility.HtmlEncode(layout.template_doctype);
                    templateService.Update(updateTemplate);

                    cms_asp_admin_update_template_Result caautr = new cms_asp_admin_update_template_Result();
                    caautr.created = DateTime.Now;
                    caautr.template_id = layout.template_id;
                    caautr.tStat = "U";
                    returnList.Add(caautr);
                }
                else
                {
                    Template getInsertTemplate = new Template();

                    Template insertTemp = new Template();
                    insertTemp.Name = layout.template_name;
                    insertTemp.Html = HttpUtility.HtmlEncode(layout.template_html);
                    insertTemp.PublisherID = (Guid)layout.publisher_id;
                    insertTemp.Type = layout.template_type;
                    insertTemp.GroupID = layout.group_id;
                    insertTemp.StructureDescription = layout.structure_description;
                    insertTemp.Content1EditorType = layout.content_1_editor_type;
                    insertTemp.TemplateDoctype = layout.template_doctype;
                    getInsertTemplate = templateService.Insert(insertTemp);

                    cms_asp_admin_update_template_Result caautr = new cms_asp_admin_update_template_Result();
                    caautr.created = DateTime.Now;
                    caautr.template_id = getInsertTemplate.Id;
                    caautr.tStat = "I";
                    returnList.Add(caautr);
                }

            }
            else
            {
                cms_asp_admin_update_template_Result caautr = new cms_asp_admin_update_template_Result();
                caautr.created = "";
                caautr.template_id = "";
                caautr.tStat = "D";
                returnList.Add(caautr);
            }



            return returnList;
            //return this.Database.SqlQuery<cms_asp_admin_update_template_Result>
            //("dbo.cms_asp_admin_update_template @template_id={0},@template_name={1},@template_html={2},@template_type={3},@publisher_id={4},@group_id={5},@structure_description={6},@content_1_editor_type={7},@template_doctype={8} ",
            //     layout.template_id,
            //     layout.template_name,
            //     HttpUtility.HtmlEncode(layout.template_html),
            //     layout.template_type,
            //     layout.publisher_id,
            //     layout.group_id,
            //     layout.structure_description,
            //     layout.content_1_editor_type,
            //     HttpUtility.HtmlEncode(layout.template_doctype))
            //.ToList();
        }

        public List<cms_asp_admin_delete_template_Result> DeleteLayout(int LayoutID, object publisherID, int PublisherLevel)
        {

            List<cms_asp_admin_delete_template_Result> returnList = new List<cms_asp_admin_delete_template_Result>();

            CmsDbContext dbContext = new CmsDbContext();

            TemplateRepository tr = new TemplateRepository();
            TemplateService templateService = new TemplateService(tr);

            TemplateRevisionRepository trr = new TemplateRevisionRepository();
            TemplateRevisionService templateRevisionService = new TemplateRevisionService(trr);


            cms_asp_admin_delete_template_Result deleteResult = new cms_asp_admin_delete_template_Result();

            List<Template> listTemplates = new List<Template>();
            listTemplates = dbContext.Templates.Where(s => s.Id == LayoutID && s.Status == "A").ToList();

            if (listTemplates != null && listTemplates.Count > 0)
            {
                List<Site> listSites = new List<Site>();
                listSites = dbContext.Sites.Where(s => s.TemplateId == LayoutID).OrderBy(o => o.Name).ToList();

                if (listSites != null && listSites.Count > 0)
                {
                    foreach (Site site in listSites)
                    {
                        deleteResult = new cms_asp_admin_delete_template_Result();
                        deleteResult.found_name = site.Name;
                        deleteResult.rCode = "2";
                        returnList.Add(deleteResult);
                    }
                }

                List<ZoneGroup> listZoneGroups = new List<ZoneGroup>();
                listZoneGroups = dbContext.ZoneGroups.Where(s => s.TemplateId == LayoutID).OrderBy(o => o.Name).ToList();

                if (listZoneGroups != null && listZoneGroups.Count > 0)
                {
                    foreach (ZoneGroup zoneGroup in listZoneGroups)
                    {
                        deleteResult = new cms_asp_admin_delete_template_Result();
                        deleteResult.found_name = zoneGroup.Name;
                        deleteResult.rCode = "3";
                        returnList.Add(deleteResult);
                    }
                }

                List<Zone> listZones = new List<Zone>();
                listZones = dbContext.Zones.Where(s => s.TemplateId == LayoutID && s.Status != "D").OrderBy(o => o.Name).ToList();

                if (listZones != null && listZones.Count > 0)
                {
                    foreach (Zone zone in listZones)
                    {
                        deleteResult = new cms_asp_admin_delete_template_Result();
                        deleteResult.found_name = zone.Name;
                        deleteResult.rCode = "4";
                        returnList.Add(deleteResult);
                    }
                }

                TemplateRevision insertTemplateRevision = new TemplateRevision();

                Template getTemplate = new Template();
                getTemplate = dbContext.Templates.Where(s => s.Id == LayoutID).FirstOrDefault();

                insertTemplateRevision.TemplateID = getTemplate.Id;
                insertTemplateRevision.TemplateHtml = getTemplate.Html;
                insertTemplateRevision.PublisherID = getTemplate.PublisherID;
                templateRevisionService.Insert(insertTemplateRevision);

                getTemplate.Status = "D";
                getTemplate.Updated = DateTime.Now;
                templateService.Update(getTemplate);

                deleteResult = new cms_asp_admin_delete_template_Result();
                deleteResult.found_name = "";
                deleteResult.rCode = "0";
                returnList.Add(deleteResult);
            }
            else
            {
                deleteResult = new cms_asp_admin_delete_template_Result();
                deleteResult.found_name = "";
                deleteResult.rCode = "1";
                returnList.Add(deleteResult);
            }

            return returnList;

            //return this.Database.SqlQuery<cms_asp_admin_delete_template_Result>
            //    ("dbo.cms_asp_admin_delete_template @template_id = {0}, @publisher_id = {1}, @publisher_level = {2}",
            //        LayoutID,
            //        publisherID,
            //        PublisherLevel)
            //    .ToList();
        }
    }
}