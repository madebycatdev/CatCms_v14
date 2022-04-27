using EuroCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroCMS.Admin.ViewModels
{
    public class ArticleCompareViewModel
    {
        public Rev live { get; set; }
        public Rev revision { get; set; }

        public List<ArticleRevision> revisionList { get; set; }
    }

    public class Rev
    {
        public int ArticleId { get; set; }
        public int ClassificationId { get; set; }
        public long RevisionId { get; set; }
        public string RevisionStatus { get; set; }
        public int Status { get; set; }
        public string Classification { get; set; }
        public string Language { get; set; }
        public string LanguageRelations { get; set; }
        public List<ZoneDetailModel> Zones { get; set; }
        public int Order { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public DateTime RevisionDate { get; set; }
        public Guid RevisedBy { get; set; }
        public string ArticleType { get; set; }
        public string NavigationDisplay { get; set; }
        public string SubZone { get; set; }
        public string MenuText { get; set; }
        public string Headline { get; set; }
        public string Summary { get; set; }
        public string BeforeHead { get; set; }
        public string BeforeBody { get; set; }
        public string CustomBody { get; set; }
        public string CustomHtml { get; set; }
        public List<string> RelatedArticles { get; set; }
        public string Tags { get; set; }
        public bool NoIndexFollow { get; set; }
        public string MetaDescription { get; set; }
        public string Canonical { get; set; }
        public string MetaTitle { get; set; }
        public string Container1 { get; set; }
        public string Container2 { get; set; }
        public string Container3 { get; set; }
        public string Container4 { get; set; }
        public string Container5 { get; set; }
        public Dictionary<string, string> Customs { get; set; }

    }

    public class ZoneDetailModel
    {
        public string Zone { get; set; }

        public int Order { get; set; }

        public string Alias { get; set; }

        public bool IsPage { get; set; }

        public List<string> LanguageRelations { get; set; }
    }
}