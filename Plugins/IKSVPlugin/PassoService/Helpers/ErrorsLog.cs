using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassoService.Helpers
{
    public class ErrorsLog
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void WriteLog(string controller, string log)
        {
            logger.Info(controller + ": " + log + " - " + Environment.NewLine + DateTime.Now);
        }
    }
}