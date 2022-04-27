using EuroCMS.Admin.entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EuroCMS.Admin.Controllers;
using System.Web.Security;
using EuroCMS.Model;



namespace EuroCMS.Admin.Models
{
    public class ArticleFileDbContext : BaseDbContext
    {
        public DbSet<cms_article_files> ArticleFiles { get; set; }
        public DbSet<cms_article_files_revision> ArticleFilesRevisions { get; set; }

        public ArticleFileDbContext()
            : base()
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<cms_article_files>()
                .Map(m => m.ToTable("cms_article_files"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_approval_approve_article_files_revision_Result> DiscardArticleFiles(int rev_id, int article_id)
        {
            //Biraz uzun - sonraya bıraktım

            #region Emre
            //List<cms_asp_approval_approve_article_files_revision_Result> lst = new List<cms_asp_approval_approve_article_files_revision_Result>();

            //CmsDbContext dbContext = new CmsDbContext();

            //ArticleFileRepository afr = new ArticleFileRepository();
            //ArticleFileService afs = new ArticleFileService(afr);

            //List<ArticleFileRevision> arFileRevisions = dbContext.FileRevisions.Where(x => x.RevisionId == rev_id).ToList();
            //arFileRevisions = arFileRevisions.Where(x => x.RevisionStatus == "N" || x.RevisionStatus == "A" || x.RevisionStatus == "W").ToList();
            //if (arFileRevisions.Count > 0)
            //{
            //    for (int i = 0; i < arFileRevisions.Count; i++)
            //    {
            //        arFileRevisions[i].RevisionStatus = "X"; 
            //    }
            //    lst.Add(new cms_asp_approval_approve_article_files_revision_Result { rCode = "OK" });
            //}
            //else
            //{
            //    lst.Add(new cms_asp_approval_approve_article_files_revision_Result { rCode = "NOK" });
            //}

            //return lst;
            #endregion

            #region Eski
            return this.Database.SqlQuery<cms_asp_approval_approve_article_files_revision_Result>
                ("dbo.cms_asp_admin_discard_article_files_revision @article_id={0},@rev_id={1}",
                 article_id, rev_id).ToList();
            #endregion
        }

        public List<cms_asp_admin_delete_article_files_revision_file_Result> DeleteArticleFile(long FileRevisionId, int ArticleId, int FileId)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_article_files_revision_file_Result>
                ("dbo.cms_asp_admin_delete_article_files_revision_file  @af_rf_id={0},@rev_id={1},@article_id={2}",
               FileId, FileRevisionId, ArticleId).ToList();
        }

        public List<cms_asp_approval_approve_article_files_revision_Result> ApproveArticleFiles(int rev_id, object publisher_id)
        {

            return this.Database.SqlQuery<cms_asp_approval_approve_article_files_revision_Result>
                ("dbo.cms_asp_approval_approve_article_files_revision @rev_id={0},@approve_level={1},@publisher_id={2},@publisher_level={3}",
                rev_id, 4, publisher_id, 100).ToList();

        }


        public List<cms_asp_admin_select_article_last_revision_Result> SelectArticleFileLastRevision(int article_id)
        {
            List<cms_asp_admin_select_article_last_revision_Result> lst = new List<cms_asp_admin_select_article_last_revision_Result>();
            CmsDbContext dbContext = new CmsDbContext();
            ArticleFileRevision afr = new ArticleFileRevision();
            List<ArticleFileRevision> fileRevisions = dbContext.FileRevisions.Where(x => x.RevisionStatus == "L" && x.ArticleId == article_id).ToList();
            if (fileRevisions.Count > 0)
            {
                afr = fileRevisions.OrderByDescending(x => x.RevisionDate).ToList().FirstOrDefault();
            }
            else
            {
                fileRevisions = dbContext.FileRevisions.Where(x => x.ArticleId == article_id).ToList().OrderBy(x => x.RevisionStatus).OrderByDescending(x => x.RevisionDate).ToList();
                afr = fileRevisions.FirstOrDefault();
            }

            if (afr != null)
            {
                lst.Add(new cms_asp_admin_select_article_last_revision_Result { rev_id = afr.RevisionId });
            }


            return lst;

            //return this.Database.SqlQuery<cms_asp_admin_select_article_last_revision_Result>
            //       ("dbo.cms_asp_admin_select_article_files_last_revision @article_id={0}",
            //       article_id)
            //       .ToList(); 
        }

        public List<cms_article_files_revision_files> SelectArticleFiles(long rev_id)
        {
            //Framework e çevrilmemiş

            return this.Database.SqlQuery<cms_article_files_revision_files>
                   ("dbo.cms_asp_admin_select_article_files_revision_files @rev_id={0}",
                   rev_id)
                   .ToList();
        }

        public List<cms_asp_copy_article_files_revision_files_Result> CopyArticleFiles(string old_rev_id, string revID, string old_af_rf_id, string article_id)
        {
            return this.Database.SqlQuery<cms_asp_copy_article_files_revision_files_Result>
                ("dbo.cms_asp_copy_article_files_revision_files @old_rev_id={0},@rev_id={1},@af_rf_id={2},@article_id={3}",
                old_rev_id, revID, old_af_rf_id, article_id)
                .ToList();
        }
        public List<cms_asp_admin_update_article_files_revision_Result> UpdateArticleFileRevision(string afrfID, string revID, string articleID, string fileTitle, string fileOrder, string fileName1, string fileName2, string fileName3, string fileName4, string fileName5, string fileName6, string fileName7, string fileName8, string fileName9, string fileName10, string fileTypeID, string fileComment, object revisedBy)
        {

            return this.Database.SqlQuery<cms_asp_admin_update_article_files_revision_Result>
                ("dbo.cms_asp_admin_update_article_files_revision @af_rf_id={0},@rev_id={1},@article_id={2},@file_title={3},@file_order={4},@file_name_1={5},@file_name_2={6},@file_name_3={7},@file_name_4={8},@file_name_5={9},@file_name_6={10},@file_name_7={11},@file_name_8={12},@file_name_9={13},@file_name_10={14},@file_type_id={15},@file_comment={16},@revised_by={17}",
                afrfID, revID, articleID, fileTitle, fileOrder, fileName1, fileName2, fileName3, fileName4, fileName5, fileName6, fileName7, fileName8, fileName9, fileName10, fileTypeID, fileComment, revisedBy)
                .ToList();

        }

        public List<cms_asp_admin_select_article_files_revision_list_Result> SelectArticleFilesRevisions(int a)
        {
            List<cms_asp_admin_select_article_files_revision_list_Result> lst = new List<cms_asp_admin_select_article_files_revision_list_Result>(); try
            {

                CmsDbContext dbContext = new CmsDbContext();
                List<ArticleFileRevision> fileRevisions = dbContext.FileRevisions.Where(x => x.ArticleId == a).OrderByDescending(x => x.RevisionDate).Take(50).ToList();

                foreach (ArticleFileRevision arf in fileRevisions)
                {
                    //if (arf.RevisedBy != null && arf.ApprovedBy != null)
                    //{
                    string approvalName = arf.ApprovedBy != null ? dbContext.vAspNetMembershipUsers.Where(x => x.UserId == arf.ApprovedBy).FirstOrDefault().UserName : "";
                    string revisedName = arf.RevisedBy != null ? dbContext.vAspNetMembershipUsers.Where(x => x.UserId == arf.RevisedBy).FirstOrDefault().UserName : "";

                    lst.Add(new cms_asp_admin_select_article_files_revision_list_Result { rev_id = arf.RevisionId, rev_date = arf.RevisionDate, revision_status = arf.RevisionStatus, approval_date = arf.Approved, approval_name = approvalName, revised_name = revisedName });
                    //}
                }
            }
            catch (Exception ex)
            {

            }

            return lst;

            //return this.Database.SqlQuery<cms_asp_admin_select_article_files_revision_list_Result>
            //    ("dbo.cms_asp_admin_select_article_files_revision_list @article_id={0}",
            //    a)
            //    .ToList();
        }
        public List<cms_asp_select_file_types_Result> SelectFileTypes()
        {
            List<cms_asp_select_file_types_Result> lst = new List<cms_asp_select_file_types_Result>();
            CmsDbContext dbContext = new CmsDbContext();
            List<FileType> fTypes = dbContext.FileTypes.ToList();
            //List<StructureGroup> sGroups = dbContext.StructureGroups.ToList();

            foreach (FileType ft in fTypes)
            {
                try
                {
                    cms_asp_select_file_types_Result item = new cms_asp_select_file_types_Result();
                    StructureGroup sg = new StructureGroup();

                    sg = dbContext.StructureGroups.Where(s => s.Id == ft.GroupId).FirstOrDefault();
                    if (sg != null)
                    {
                        item.group_id = sg.Id;
                        item.group_name = sg.Name;
                    }
                    else
                    {
                        item.group_id = 0;
                        item.group_name = "";
                    }

                    item.type_id = ft.ID;
                    item.type_name = ft.Name;
                    item.type_alias = ft.Alias;
                    item.created = ft.Created;
                    item.updated = ft.Updated;

                    lst.Add(item);

                    //lst.Add(new cms_asp_select_file_types_Result { type_id = ft.ID, type_name = ft.Name, type_alias = ft.Alias, created = ft.Created, updated = ft.Updated, group_id = ft.GroupId, group_name = sGroups.Where(x => x.Id == ft.GroupId).FirstOrDefault().Name });
                }
                catch
                {
                    //özellikle boş bıraktım - eğer sGroupsta yoksa eklemesin
                }
            }
            lst = lst.OrderBy(x => x.group_name).OrderByDescending(x => x.updated).ToList();

            return lst;

            //return this.Database.SqlQuery<cms_asp_select_file_types_Result>
            //    ("dbo.cms_asp_select_file_types")
            //    .ToList();
        }

        public List<cms_asp_admin_select_file_type_details_Result> SelectFileTypesDetails(int type_id)
        {
            List<cms_asp_admin_select_file_type_details_Result> lst = new List<cms_asp_admin_select_file_type_details_Result>();
            CmsDbContext dbContext = new CmsDbContext();
            List<FileType> fTypes = dbContext.FileTypes.Where(x => x.ID == type_id).ToList();

            foreach (FileType ft in fTypes)
            {
                lst.Add(new cms_asp_admin_select_file_type_details_Result { created = ft.Created, file1_extension = ft.File1Extension, file1_name = ft.File1Name, file1_size = ft.File1Size, file1_wh = ft.File1WidthHeight, file10_extension = ft.File10Extension, file10_name = ft.File10Name, file10_size = ft.File10Size, file10_wh = ft.File10WidthHeight, file2_extension = ft.File2Extension, file2_name = ft.File2Name, file2_size = ft.File2Size, file2_wh = ft.File2WidthHeight, file3_extension = ft.File3Extension, file3_name = ft.File3Name, file3_size = ft.File3Size, file3_wh = ft.File3WidthHeight, file4_extension = ft.File4Extension, file4_name = ft.File4Name, file4_size = ft.File4Size, file4_wh = ft.File4WidthHeight, file5_extension = ft.File5Extension, file5_name = ft.File5Name, file5_size = ft.File5Size, file5_wh = ft.File5WidthHeight, file6_extension = ft.File6Extension, file6_name = ft.File6Name, file6_size = ft.File6Size, file6_wh = ft.File6WidthHeight, file7_extension = ft.FileExtension7, file7_name = ft.FileName7, file7_size = ft.FileSize7, file7_wh = ft.FileWidthHeight7, file8_extension = ft.File8Extension, file8_name = ft.File8Name, file8_size = ft.File8Size, file8_wh = ft.File8WidthHeight, file9_extension = ft.File9Extension, file9_name = ft.File9Name, file9_size = ft.File9Size, file9_wh = ft.File9WidthHeight, group_id = ft.GroupId, structure_description = ft.StructureDescription, type_alias = ft.Alias, type_id = ft.ID, type_name = ft.Name, updated = ft.Updated });
            }

            return lst;

            //return this.Database.SqlQuery<cms_asp_admin_select_file_type_details_Result>
            //    ("dbo.cms_asp_admin_select_file_type_details @type_id={0}",
            //    type_id).ToList(); 
        }

        public List<cms_asp_admin_select_article_files_revision_file_details_Result> SelectArticleFileRevisionFileDetails(int FileId, long FileRevisionId, int ArticleId)
        {
            //Framework e çevrilmemiş

            return this.Database.SqlQuery<cms_asp_admin_select_article_files_revision_file_details_Result>
                           ("dbo.cms_asp_admin_select_article_files_revision_file_details @af_rf_id={0},@rev_id={1},@article_id={2}",
                           FileId, FileRevisionId, ArticleId).ToList();
        }
    }
}