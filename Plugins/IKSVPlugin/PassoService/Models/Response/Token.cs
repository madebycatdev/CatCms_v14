using System;
namespace PassoService.Models.Response
{
    public class Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
