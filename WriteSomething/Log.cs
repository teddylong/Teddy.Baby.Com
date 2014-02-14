using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WriteSomething
{
    public class Log
    {
        public static void WriteLog(string SourceName, string message, EventLogEntryType type)
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, SourceName);
                //EventLog.CreateEventSource(
                EventLog myLog = new EventLog();
                myLog.Source = SourceName;
                myLog.WriteEntry(message);
            }
            else
            {
                EventLog myLog = new EventLog();
                myLog.Source = SourceName;
                myLog.WriteEntry(message, type);
            }
        }
    }
}
