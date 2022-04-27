using EuroCMS.Hangfire;
using System.Net;

namespace HangfireJobsPlugin
{
    public class MilesAndSmiles : IHangfireJob
    {
        public void Run()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("");
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
            catch (System.Exception){}
        }
    }
}
