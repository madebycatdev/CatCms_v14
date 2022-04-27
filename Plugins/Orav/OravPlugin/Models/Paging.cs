using System.Collections.Generic;

namespace OravPlugin.Models
{
    public class Paging<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public List<T> Data { get; set; }

        public Paging()
        {
            Data = new List<T>();
        }
    }
}
