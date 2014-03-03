using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeCoverage
{
    public class LogService
    {
        public static void WriteLog(string message, EventLogEntryType eventType)
        {
            if (!EventLog.SourceExists("CodeCoverageLog"))
            {

                EventLog.CreateEventSource("CodeCoverageLog", "CodeCoverageLog");
                EventLog el = new EventLog();
                el.Source = "CodeCoverageLog";
                el.Log = "CodeCoverageLog";
                el.WriteEntry(message, eventType);
            }
            else
            {
                EventLog myLog = new EventLog();
                myLog.Source = "CodeCoverageLog";
                myLog.Log = "CodeCoverageLog";
                myLog.WriteEntry(message, eventType);
            }

            string machineName = System.Environment.MachineName;
            string fileName = machineName + ".txt";
            string LogFolder = ConfigurationManager.AppSettings["LogFolder"];
            File.AppendAllText(LogFolder + fileName, "**********************");
            File.AppendAllText(LogFolder + fileName, "\r\n");
            File.AppendAllText(LogFolder + fileName, message);
            File.AppendAllText(LogFolder + fileName, "\r\n");
            File.AppendAllText(LogFolder + fileName, DateTime.Now.ToString());
            File.AppendAllText(LogFolder + fileName, "\r\n");
            
        }
    }
}
