using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.LogServices
{
    public class Logger : ILogger
    {
        private static readonly log4net.ILog log = 
            log4net.LogManager.GetLogger(typeof(Object));

        public void WriteInfoLog(string Msg)
        {
            log.Info(Msg);
        }
    }
}