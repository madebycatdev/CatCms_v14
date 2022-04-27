using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EuroCMS.Admin.Models
{
    public class ArticleCompareRepository
    {
        public SideBySideDiffModel Compare(string oldText, string newText)
        {
            var d = new Differ();
            ISideBySideDiffBuilder bidiffBuilder = new SideBySideDiffBuilder(d);

            var result = bidiffBuilder.BuildDiffModel(oldText, newText);
            return result;
        }

        public string CompareHtml(string oldText, string newText)
        {
            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(oldText, newText);
            string result = diffHelper.Build();
            return result;
        }
    }
}