using EuroCMS.Admin.entity;
using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.Models
{
    public class XmlGeneratorDbContext: BaseDbContext
    {
        public DbSet<EuroCMS.Admin.entity.cms_xml> Xmls { get; set; }

        public XmlGeneratorDbContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EuroCMS.Admin.entity.cms_xml>()
                .Map(m => m.ToTable("cms_xml"));
 
             base.OnModelCreating(modelBuilder);
        }

        public List<cms_asp_select_xml_list_Result> SelectXmls(int? GroupID)
        {
            return this.Database.SqlQuery<cms_asp_select_xml_list_Result>
                ("dbo.cms_asp_select_xml_list @group_id={0}",
                GroupID == null ? -1 : GroupID)
                .ToList();
        }

        public List<cms_asp_select_xml_details_Result> SelectXml(int XmlId)
        {
            return this.Database.SqlQuery<cms_asp_select_xml_details_Result>
                ("dbo.cms_asp_select_xml_details @xml_id={0}",
                XmlId)
                .ToList();
        }
  
        public List<cms_asp_admin_update_xml_details_Result> UpdateXml(cms_xml xml)
        {
            return this.Database.SqlQuery<cms_asp_admin_update_xml_details_Result>
                 ("dbo.cms_asp_admin_update_xml_details " +
                    "@xml_id={0}," +
                    "@xml_name={1}," +
                    "@xml_main_node={2}," +
                    "@xml_main_node_attrib={3}," +
                    "@xml_per_node={4}," +
                    "@xml_per_node_attrib={5}, " +
                    "@xml_sub_node={6}," +
                    "@xml_sub_template={7}," +
                    "@xml_level={8}," +
                    "@xml_related_line={9}," +
                    "@xml_xml={10}," +
                    "@created_by={11}," +
                    "@group_id={12}," +
                    "@structure_description={13}",
                 xml.xml_id,
                 xml.xml_name,
                 xml.xml_main_node,
                 xml.xml_main_node_attrib,
                 xml.xml_per_node,
                 xml.xml_per_node_attrib,
                 xml.xml_sub_node,
                 xml.xml_sub_template,
                 xml.xml_level,
                 xml.xml_related_line,
                 xml.xml_xml,
                 xml.created_by,
                 xml.group_id,
                 xml.structure_description).ToList();
        }

        public List<cms_asp_admin_delete_xml_Result> DeleteXml(int XmlID, object  publisherID, int Publisherlevel)
        {
            return this.Database.SqlQuery<cms_asp_admin_delete_xml_Result>
                ("dbo.cms_asp_admin_delete_xml @xml_id={0},@publisher_id={1},@publisher_level={2}",
                XmlID,
                publisherID,
                Publisherlevel)
                .ToList();
        }
    }
}