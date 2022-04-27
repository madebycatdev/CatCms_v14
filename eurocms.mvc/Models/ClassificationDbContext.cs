using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class ClassificationDbContext : BaseDbContext
    {
        public DbSet<cms_classifications> Clasifications { get; set; }

        public ClassificationDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cms_classifications>()
                .Map(m => m.ToTable("cms_classifications"));


            base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_classifications_Result> SelectClasifications(int? GroupID)
        {
            return this.Database.SqlQuery<cms_asp_select_classifications_Result>
                ("dbo.cms_asp_select_classifications @group_id={0}",
                GroupID == null ? -1 : GroupID)
                .ToList();
        }
        public List<cms_asp_select_combo_values_Result> SelectClassificationComboValues(int ClasificationID, int ColumnNo)
        {
            return this.Database.SqlQuery<cms_asp_select_combo_values_Result>
                  ("cms_asp_select_combo_values @classification_id={0}, @column_no={1}",
                  ClasificationID,ColumnNo)
                  .ToList();
        }
        public List<cms_asp_select_classification_details_Result> SelectClassificationDetails(int ClasificationID)
        {
            return this.Database.SqlQuery<cms_asp_select_classification_details_Result>
                ("dbo.cms_asp_select_classification_details @classification_id={0}",
                ClasificationID)
                .ToList();
        }

        public List<cms_asp_admin_delete_classification_Result> DeleteClassification(int ClasificationID, object  publisherID, int PublisherLevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_classification_Result>
                ("dbo.cms_asp_admin_delete_classification @classification_id={0}, @publisher_id={1}, @publisher_level={2}",
                ClasificationID, publisherID, PublisherLevel)
                .ToList();
        }

        public void DeleteClassificationComboValues(int ClasificationID, int ColumnNo)
        {

            this.Database.ExecuteSqlCommand
               ("dbo.cms_asp_admin_delete_classification_combo_values @classification_id={0}, @column_no={1}",
               ClasificationID, (short)ColumnNo);
                
        }



        public List<cms_asp_admin_update_classification_combo_values_Result> UpdateClassificationComboValues(int ClasificationID, int ColumnNo, string ComboSupid, string ComboLabel, string ComboValue, int ComboOrder,object CreatedBy)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_classification_combo_values_Result>
                ("cms_asp_admin_update_classification_combo_values @classification_id={0},@column_no={1},@combo_supid={2},@combo_label={3},@combo_value={4},@combo_order={5},@created_by={6}",
                ClasificationID, ColumnNo, ComboSupid, ComboLabel, ComboValue, ComboOrder, CreatedBy)
                .ToList();
        }
        public List<cms_asp_admin_update_classification_details_Result> UpdateClassification(cms_classifications classification)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_classification_details_Result>
                ("dbo.cms_asp_admin_update_classification_details " +
                	"@classification_id={0},"+
                    "@classification_name={1},"+
                    "@summary_cb={2},"+
                    "@enddate_cb={3},"+
                    "@keywords_cb={4},"+
                    "@custom1_cb={5},"+
                    "@custom2_cb={6},"+
                    "@custom3_cb={7},"+
                    "@custom4_cb={8},"+
                    "@custom5_cb={9},"+
                    "@custom6_cb={10},"+
                    "@custom7_cb={11},"+
                    "@custom8_cb={12},"+
                    "@custom9_cb={13},"+
                    "@custom10_cb={14},"+
                    "@custom11_cb={15},"+
                    "@custom12_cb={16},"+
                    "@custom13_cb={17},"+
                    "@custom14_cb={18},"+
                    "@custom15_cb={19},"+
                    "@custom16_cb={20},"+
                    "@custom17_cb={21},"+
                    "@custom18_cb={22},"+
                    "@custom19_cb={23},"+
                    "@custom20_cb={24},"+
                    "@date1_cb={25},"+
                    "@date2_cb={26},"+
                    "@date3_cb={27},"+
                    "@date4_cb={28},"+
                    "@date5_cb={29},"+
                    "@custom1_text={30},"+
                    "@custom2_text={31},"+
                    "@custom3_text={32},"+
                    "@custom4_text={33},"+
                    "@custom5_text={34},"+
                    "@custom6_text={35},"+
                    "@custom7_text={36},"+
                    "@custom8_text={37},"+
                    "@custom9_text={38},"+
                    "@custom10_text={39},"+
                    "@custom11_text={40},"+
                    "@custom12_text={41},"+
                    "@custom13_text={42},"+
                    "@custom14_text={43},"+
                    "@custom15_text={44},"+
                    "@custom16_text={45},"+
                    "@custom17_text={46},"+
                    "@custom18_text={47},"+
                    "@custom19_text={48},"+
                    "@custom20_text={49},"+
                    "@custom1_type={50},"+
                    "@custom2_type={51},"+
                    "@custom3_type={52},"+
                    "@custom4_type={53},"+
                    "@custom5_type={54},"+
                    "@custom6_type={55},"+
                    "@custom7_type={56},"+
                    "@custom8_type={57},"+
                    "@custom9_type={58},"+
                    "@custom10_type={59},"+
                    "@flag1_text={60},"+
                    "@flag2_text={61},"+
                    "@flag3_text={62},"+
                    "@flag4_text={63},"+
                    "@flag5_text={64},"+
                    "@date1_text={65},"+
                    "@date2_text={66},"+
                    "@date3_text={67},"+
                    "@date4_text={68},"+
                    "@date5_text={69},"+
                    "@summary_text={70},"+
                    "@enddate_text={71},"+
                    "@keywords_text={72},"+
                    "@article1_text={73},"+
                    "@article2_text={74},"+
                    "@article3_text={75},"+
                    "@article4_text={76},"+
                    "@article5_text={77},"+
                    "@article1_cb={78},"+
                    "@article2_cb={79},"+
                    "@article3_cb={80},"+
                    "@article4_cb={81},"+
                    "@article5_cb={82} ,"+
                    "@custom1_subcolumn={83},"+
                    "@custom2_subcolumn={84},"+
                    "@custom3_subcolumn={85},"+
                    "@custom4_subcolumn={86},"+
                    "@custom5_subcolumn={87},"+
                    "@custom6_subcolumn={88},"+
                    "@custom7_subcolumn={89},"+
                    "@custom8_subcolumn={90},"+
                    "@custom9_subcolumn={91},"+
                    "@custom10_subcolumn={92},"+
                    "@file_required_cb={93},"+
                    "@file_title_required_cb={94},"+
                    "@file_description_required_cb={95},"+
                    "@required_file_types={96},"+
                    "@created_by={97},"+
                    "@group_id={98},"+
                    "@structure_description={99}",
                    classification.classification_id,
                    classification.classification_name,
                    classification.summary_cb,
                    classification.enddate_cb,
                    classification.keywords_cb,
                    classification.custom1_cb,
                    classification.custom2_cb,
                    classification.custom3_cb,
                    classification.custom4_cb,
                    classification.custom5_cb,
                    classification.custom6_cb,
                    classification.custom7_cb,
                    classification.custom8_cb,
                    classification.custom9_cb,
                    classification.custom10_cb,
                    classification.custom11_cb,
                    classification.custom12_cb,
                    classification.custom13_cb,
                    classification.custom14_cb,
                    classification.custom15_cb,
                    classification.custom16_cb,
                    classification.custom17_cb,
                    classification.custom18_cb,
                    classification.custom19_cb,
                    classification.custom20_cb,
                    classification.date1_cb,
                    classification.date2_cb,
                    classification.date3_cb,
                    classification.date4_cb,
                    classification.date5_cb,
                    classification.custom1_text,
                    classification.custom2_text,
                    classification.custom3_text,
                    classification.custom4_text,
                    classification.custom5_text,
                    classification.custom6_text,
                    classification.custom7_text,
                    classification.custom8_text,
                    classification.custom9_text,
                    classification.custom10_text,
                    classification.custom11_text,
                    classification.custom12_text,
                    classification.custom13_text,
                    classification.custom14_text,
                    classification.custom15_text,
                    classification.custom16_text,
                    classification.custom17_text,
                    classification.custom18_text,
                    classification.custom19_text,
                    classification.custom20_text,
                    classification.custom1_type,
                    classification.custom2_type,
                    classification.custom3_type,
                    classification.custom4_type,
                    classification.custom5_type,
                    classification.custom6_type,
                    classification.custom7_type,
                    classification.custom8_type,
                    classification.custom9_type,
                    classification.custom10_type,
                    classification.flag1_text,
                    classification.flag2_text,
                    classification.flag3_text,
                    classification.flag4_text,
                    classification.flag5_text,
                    classification.date1_text,
                    classification.date2_text,
                    classification.date3_text,
                    classification.date4_text,
                    classification.date5_text,
                    classification.summary_text,
                    classification.enddate_text,
                    classification.keywords_text,
                    classification.article1_text,
                    classification.article2_text,
                    classification.article3_text,
                    classification.article4_text,
                    classification.article5_text,
                    classification.article1_cb,
                    classification.article2_cb,
                    classification.article3_cb,
                    classification.article4_cb,
                    classification.article5_cb,
                    classification.custom1_subcolumn,
                    classification.custom2_subcolumn,
                    classification.custom3_subcolumn,
                    classification.custom4_subcolumn,
                    classification.custom5_subcolumn,
                    classification.custom6_subcolumn,
                    classification.custom7_subcolumn,
                    classification.custom8_subcolumn,
                    classification.custom9_subcolumn,
                    classification.custom10_subcolumn,
                    classification.file_required_cb,
                    classification.file_title_required_cb,
                    classification.file_description_required_cb,
                    classification.required_file_types,
                    classification.created_by,
                    classification.group_id,
                    classification.structure_description)
                .ToList();
        }

    }
}