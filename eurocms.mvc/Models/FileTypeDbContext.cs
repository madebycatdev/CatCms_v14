using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class FileTypeDbContext : BaseDbContext
    {
        public DbSet<cms_file_types> FileTypes { get; set; }

        public FileTypeDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_file_types>()
                .Map(m => m.ToTable("cms_file_types"));
 
            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_file_types_Result> SelectFileTypes()
        {
            return this.Database.SqlQuery<cms_asp_select_file_types_Result>
                ("dbo.cms_asp_select_file_types")
                .ToList();
        }

        public List<string> DeleteFileType(int TypeID)
        {
            return this.Database.SqlQuery<string>
                ("dbo.cms_asp_admin_delete_file_type {0}", TypeID)
                .ToList();
        }

        public List<cms_asp_admin_select_file_type_details_Result> SelectFileType(int TypeID)
        {
            return this.Database.SqlQuery<cms_asp_admin_select_file_type_details_Result>
                ("dbo.cms_asp_admin_select_file_type_details {0}", TypeID)
                .ToList();
        }

        public List<cms_asp_admin_update_file_type_Result> SaveFileType(cms_file_types fileType)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_file_type_Result>
                ("dbo.cms_asp_admin_update_file_type " +
                "@type_id={0}," +
                "@type_name={1}," +
                "@type_alias={2}," +
                "@file1_name={3}," +
                "@file1_extension={4}," +
                "@file1_size={5}," +
                "@file1_wh={6}," +
                "@file2_name={7}," +
                "@file2_extension={8}," +
                "@file2_size={9}," +
                "@file2_wh={10}," +
                "@file3_name={11}," +
                "@file3_extension={12}," +
                "@file3_size={13}," +
                "@file3_wh={14}," +
                "@file4_name={15}," +
                "@file4_extension={16}," +
                "@file4_size={17}," +
                "@file4_wh={18}," +
                "@file5_name={19}," +
                "@file5_extension={20}," +
                "@file5_size={21}," +
                "@file5_wh={22}," +
                "@file6_name={23}," +
                "@file6_extension={24}," +
                "@file6_size={25}," +
                "@file6_wh={26}," +
                "@file7_name={27}," +
                "@file7_extension={28}," +
                "@file7_size={29}," +
                "@file7_wh={30}," +
                "@file8_name={31}," +
                "@file8_extension={32}," +
                "@file8_size={33}," +
                "@file8_wh={34}," +
                "@file9_name={35}," +
                "@file9_extension={36}," +
                "@file9_size={37}," +
                "@file9_wh={38}," +
                "@file10_name={39}," +
                "@file10_extension={40}," +
                "@file10_size={41}," +
                "@file10_wh={42}," +
                "@group_id={43}," +
                "@structure_description={44}",
                    fileType.type_id,
                    fileType.type_name,
                    fileType.type_alias,
                    fileType.file1_name,
                    fileType.file1_extension,
                    fileType.file1_size,
                    fileType.file1_wh,
                    fileType.file2_name,
                    fileType.file2_extension,
                    fileType.file2_size,
                    fileType.file2_wh,
                    fileType.file3_name,
                    fileType.file3_extension,
                    fileType.file3_size,
                    fileType.file3_wh,
                    fileType.file4_name,
                    fileType.file4_extension,
                    fileType.file4_size,
                    fileType.file4_wh,
                    fileType.file5_name,
                    fileType.file5_extension,
                    fileType.file5_size,
                    fileType.file5_wh,
                    fileType.file6_name,
                    fileType.file6_extension,
                    fileType.file6_size,
                    fileType.file6_wh,
                    fileType.file7_name,
                    fileType.file7_extension,
                    fileType.file7_size,
                    fileType.file7_wh,
                    fileType.file8_name,
                    fileType.file8_extension,
                    fileType.file8_size,
                    fileType.file8_wh,
                    fileType.file9_name,
                    fileType.file9_extension,
                    fileType.file9_size,
                    fileType.file9_wh,
                    fileType.file10_name,
                    fileType.file10_extension,
                    fileType.file10_size,
                    fileType.file10_wh,
                    fileType.group_id,
                    fileType.structure_description
                    )
                .ToList();
        }
    }
}