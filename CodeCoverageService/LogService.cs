using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeCoverageService
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
                myLog.WriteEntry(message,eventType);
            }
        }
    }
}
