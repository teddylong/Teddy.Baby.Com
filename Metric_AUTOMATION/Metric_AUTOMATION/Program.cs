using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using Freeway.Metrics;
using System.Threading;

namespace Metric_AUTOMATION
{
    class Program
    {
        private static IMetric MetricLogger = Freeway.Metrics.MetricManager.GetMetricLogger();
        private static string MetricNameSpace = "__automation__";
        private static string ConnectionStr = ConfigurationManager.AppSettings["db"];

        static void Main(string[] args)
        {
            string MetricName = MetricNameSpace + "Auto.Portal.testD";
            MyLinqDataContext DB = new MyLinqDataContext();
            string timePast = ConfigurationManager.AppSettings["PastTime"];
         
            DateTime a = Convert.ToDateTime(DateTime.Now).AddHours(-10);
            
            while (true)
            {
                //SELECT Count(ResultCode) as Count, ResultCode,[RequestType],[CallerIP] 
                //FROM [ATDataBase].[dbo].[CI_API_Run] 
                //where DATEDIFF(n,calltime,GetDate())<360 and ResultCode<>'Success' 
                //group by RequestType,CallerIP,ResultCode 
                try
                {
                    DateTime b = a.AddMinutes(10);
                    var disQueryLinqQuery = from CI_API_Runs in DB.CI_API_Runs
                                            where
                                              CI_API_Runs.CallTime >= a &&
                                              CI_API_Runs.CallTime < b 
                                              //CI_API_Runs.ResultCode != "Success"
                                            group CI_API_Runs by new
                                            {
                                                CI_API_Runs.RequestType,
                                                CI_API_Runs.CallerIP
                                            } into g
                                            select new
                                            {
                                                Result = (int?)g.Sum(p => Convert.ToInt32(p.ActionTime)),
                                                g.Key.RequestType,
                                                g.Key.CallerIP
                                            };

                    foreach (var item in disQueryLinqQuery)
                    {
                        Dictionary<string, string> disDictionary = new Dictionary<string, string>();
                        disDictionary.Add("RequestType", item.RequestType);
                        disDictionary.Add("CallerIP", item.CallerIP);
                        MetricLogger.log(MetricName, item.Result.Value, disDictionary);
                        Console.WriteLine(item.Result + "        " + item.RequestType + "         " + item.CallerIP);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                a = a.AddMinutes(5);
                Console.WriteLine("5 mins later will get the data again...");
                Thread.Sleep(5 * 60 * 1000);
            }
            
        }

        //private static string Send
    }
}
