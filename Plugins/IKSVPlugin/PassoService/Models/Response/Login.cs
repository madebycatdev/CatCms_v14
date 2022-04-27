using System;
namespace PassoService.Models.Response
{
    public class LoginResult
    {
        public string message { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

}
