using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace UATService
{
    class Program
    {
        private static Dictionary<string, bool> result = new Dictionary<string, bool>();
        static void Main(string[] args)
        {
             RunResult();
        }

        private static void RunResult()
        {
            while (true)
            {
                result.Clear();

                Console.WriteLine("Start Checking... ");

                bool ESBResult = CheckUAT.CheckESB();
                result.Add("ESB", ESBResult);

                bool ABTesingResult = CheckUAT.CheckABTesting();
                result.Add("ABTesing", ABTesingResult);

                bool DashBoardResult = CheckUAT.CheckDashBoard();
                result.Add("DashBoard", DashBoardResult);

                bool SearchEngineResult = CheckUAT.CheckSearchEngine();
                result.Add("SearchEngine", SearchEngineResult);

                bool MarkLandResult = CheckUAT.CheckMarkLand();
                result.Add("MarkLand", MarkLandResult);

                bool ScenicspotResult = CheckUAT.CheckScenicspot();
                result.Add("Scenicspot", ScenicspotResult);

                bool MemcachedResult = CheckUAT.CheckMemcached();
                result.Add("Memcached", MemcachedResult);

                bool CentralLoggingResult = CheckUAT.CheckCentralLogging();
                result.Add("CentralLogging", CentralLoggingResult);

                foreach (var item in result)
                {
                    Console.WriteLine(item.Key + ": " + item.Value);
                }

                if (ESBResult && ABTesingResult && DashBoardResult && SearchEngineResult && MarkLandResult && ScenicspotResult && MemcachedResult && CentralLoggingResult)
                {
                    //do nothing
                }
                else
                {
                    SendNotificationEmail();
                    Console.WriteLine("THERE MAY HAVE ONE OR MORE ERROR(S) ABOUT THE SERVICE, SENT THE EMAIL ALREADY!");
                }

                Console.WriteLine("Result Created at: " + DateTime.Now);
                Thread.Sleep(3600 * 1000);
            }
        }
        public static void SendNotificationEmail()
        {
            if (result.Count > 0)
            {
                string emailBody = "";
                foreach (var item in result)
                {
                    emailBody += item.Key + ": " + item.Value + "<br/>";
                }
                Email.SendEmail("yqlong@ctrip.com", "UAT Service May Have Error!", "<br/>" + emailBody + "<br/><br/>" + "Please see the detail  message in http://autotest/Pages/Reports/UAT.aspx");
            }
        }
        

    }
    
}
