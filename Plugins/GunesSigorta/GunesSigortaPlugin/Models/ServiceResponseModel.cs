using System.Collections.Generic;

namespace EuroCMS.Plugin.GunesSigorta
{
    public class ServiceResponseModel<T>
    {
        public string Status { get; set; }
        public string RequestId { get; set; }
        public List<string> Messages { get; set; }
        public T Result { get; set; }
    }
}