using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class StructureGroupDbContext
        : BaseDbContext
    {
        public DbSet<cms_structure_groups> Articles { get; set; }

        public StructureGroupDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_structure_groups>()
                .Map(m => m.ToTable("cms_structure_groups"));

            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_admin_select_structure_group_Result> SelectStructureGroups()
        {
            return this.Database.SqlQuery<cms_asp_admin_select_structure_group_Result>
                   ("dbo.cms_asp_admin_select_structure_group @group_id={0},@group_type={1}",
                   -1, 0)
                   .ToList();  
        }

        public List<cms_asp_admin_select_structure_group_Result> SelectStructureGroup(int GroupID, int GroupType)
        {
            return this.Database.SqlQuery<cms_asp_admin_select_structure_group_Result>
                   ("dbo.cms_asp_admin_select_structure_group @group_id={0},@group_type={1}",
                   GroupID, GroupType)
                   .ToList();
        }

        public List<cms_asp_admin_select_structure_group_Result> SelectStructureGroupByType(int GroupType)
        {
            return this.Database.SqlQuery<cms_asp_admin_select_structure_group_Result>
                   ("dbo.cms_asp_admin_select_structure_group @group_id={0},@group_type={1}",
                   -1, GroupType)
                   .ToList();
        }

        public List<cms_asp_admin_update_structure_group_Result> CreateStructureGroup(cms_structure_groups structureGroup)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_structure_group_Result>
                ("dbo.cms_asp_admin_update_structure_group @publisher_id = {0}, @group_id={1}, @group_name={2}, @group_type={3}",
                    structureGroup.created_by,
                    -1,
                    structureGroup.group_name,
                    structureGroup.group_type)
                 .ToList();
        }

        public List<cms_asp_admin_update_structure_group_Result> UpdateStructureGroup(cms_structure_groups structureGroup)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_structure_group_Result>
                ("dbo.cms_asp_admin_update_structure_group @publisher_id = {0}, @group_id={1}, @group_name={2}, @group_type={3}",
                    structureGroup.created_by,
                    structureGroup.group_id,
                    structureGroup.group_name,
                    structureGroup.group_type)
                 .ToList();
        }

        public List<string> DeleteStructureGroup(int GroupID, int GroupType)
        {
            return this.Database.SqlQuery<string>
                ("dbo.cms_asp_admin_delete_structure_group  @group_id={0}, @group_type={1}",
                   GroupID,
                   GroupType)
                 .ToList();
        }
    }
}