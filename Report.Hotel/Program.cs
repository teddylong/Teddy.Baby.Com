using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Report.Hotel
{
    class Program
    {

        private static int DeadLine = int.Parse(ConfigurationManager.AppSettings["DeadLine"]);
        private static int ReSetTime = int.Parse(ConfigurationManager.AppSettings["ReSetTime"]);
        private static int CheckIntervals = int.Parse(ConfigurationManager.AppSettings["CheckIntervals"]);
        public static Dictionary<string, List<string>> list = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            if (Support.SetupStatus())
            {
                while (true)
                {
                    List<string> totalDep = Support.TotalDep();
                    if (totalDep != null)
                    {
                        foreach (string depName in totalDep)
                        {
                            if (!Support.CheckStatus(depName))
                            {
                                Result result = CheckRunStatus(depName);
                                if (result == Result.NotYet)
                                {
                                    Console.WriteLine("********************************");
                                    Console.WriteLine("Not Completed: " + depName + "  (" + DateTime.Now.ToString() + ")");
                                    Console.WriteLine("********************************");
                                }
                                else if (result == Result.Completed || result == Result.NotCompleteButSend)
                                {
                                    list.Clear();
                                    list = Support.GetInfo(depName);
                                    if (list != null)
                                    {
                                        foreach (var item in list)
                                        {
                                            List<string> infoList = item.Value;
                                            UpdateImgLink(infoList);
                                            if (infoList.Count > 0)
                                            {
                                                bool runResult = GetImgAndSendEmail(depName, infoList);
                                                if (runResult)
                                                {
                                                    Support.OKStatus(depName);
                                                    Console.WriteLine("********************************");
                                                    Console.WriteLine("Completed: " + depName + "  (" + DateTime.Now.ToString() + ")");
                                                    Console.WriteLine("********************************");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("There may some errors in UpdateImgLink...");
                                                Console.WriteLine(DateTime.Now.ToString());
                                            }
                                        } 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Job RUN Completed, but may have some errors...");
                                        Console.WriteLine(DateTime.Now.ToString());
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("There may some errors...");
                                    Console.WriteLine(DateTime.Now.ToString());
                                }
                            }
                        }
                    }
                    Thread.Sleep(CheckIntervals * 60 * 1000);
                    if (DateTime.Now.Hour == ReSetTime)
                    {
                        Support.ReSetStatus();
                    }
                }
            }
            else
            {
                Console.WriteLine("Setup failed...");
                Console.ReadLine();
            }
        }
        
        private static void UpdateImgLink(List<string> list)
        {
            if (list.Count == 2)
            {
                string LastRunID = GetLatestRunID();
                if (!LastRunID.Equals(String.Empty))
                {
                    list[1] = list[1].Trim() + LastRunID;
                }
                else
                {
                    list.Clear();
                }
            }
        }

        private static string GetLatestRunID()
        {
            try
            {
                string sendDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                string query0 = @"select max(a.runid) from[ATDataBase].[dbo].[CI_LOG_RUN] a inner join ci_log_job b on a.runid=b.runid where b.casecount > 0 and a.RunType=0 and b.type=0 and Convert(nvarchar(10),a.GmtCreate,23)='" + sendDate + "'";
                //string query1 = @"select max(a.runid) from[ATDataBase].[dbo].[CI_LOG_RUN] a inner join ci_log_job b on a.runid=b.runid where b.casecount > 0 and a.RunType=0 and b.type=1 and Convert(nvarchar(10),a.GmtCreate,23)='" + sendDate + "'";


                DataTable dt0 = SQLHelper.GetDataTableBySql(query0);
                //DataTable dt1 = SQLHelper.GetDataTableBySql(query1);

                return dt0.Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something wrong happened in GetLastestRunID...");
                Console.WriteLine(e.ToString());
                Console.WriteLine(DateTime.Now.ToString());
                return String.Empty;
            }

        } 
        private static Result CheckRunStatus(string dep)
        {
            //try
            //{
            //    string sendDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //    string queryRunID = @"select max(a.runid) from [ATDataBase].[dbo].[CI_LOG_RUN] a inner join ci_log_job b on a.runid=b.runid where b.casecount > 0 and a.RunType=0 and b.type=0 and Convert(nvarchar(10),a.GmtCreate,23)='" + sendDate + "'";
            //    DataTable dtRunID = SQLHelper.GetDataTableBySql(queryRunID);
            //    string runID = dtRunID.Rows[0][0].ToString();
            //    string apiJobName = "API." + dep + "%";
            //    string uiJobName = "UI." + dep + "%";
            //    string queryResult = @"select COUNT(*) from [ATDataBase].[dbo].[ci_log_job] where runid = " + runID + " and ( Jobname like '" + apiJobName + "' or Jobname like '" + uiJobName + "') and GmtEnd is null";
            //    DataTable dtResult = SQLHelper.GetDataTableBySql(queryResult);

            //    if (DateTime.Now.Hour >= DeadLine)
            //    {
            //        return Result.NotCompleteButSend;
            //    }
            //    else if (int.Parse(dtResult.Rows[0][0].ToString()) == 0)
            //    {
            //        return Result.Completed;
            //    }
            //    else if (int.Parse(dtResult.Rows[0][0].ToString()) > 0)
            //    {
            //        return Result.NotYet;
            //    }
            //    return Result.Default;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //    Console.WriteLine(DateTime.Now.ToString());
            //    return Result.Default;
            //}

            // Change to 17:00 
            if (DateTime.Now.Hour >= DeadLine)
            {
                return Result.Completed;
            }
            else
            {
                return Result.NotYet;
            }

        }

        public enum Result
        { 
            NotYet = 0,
            Completed = 1,
            NotCompleteButSend = 2,
            Default = 3
        }

        public static bool GetImgAndSendEmail(string depName, List<string> linkList)
        {
            Console.WriteLine("Start getting img...");
            string phantomJSPath = ConfigurationManager.AppSettings["phantomJSPath"];
            string phantomJSArguments = ConfigurationManager.AppSettings["phantomJSArguments"];
            string serverPath = ConfigurationManager.AppSettings["serverPath"];


            string emailAccouont = linkList[0];
            linkList.Remove(linkList[0]);

            string fileName = "ScreenShot";
            string folderName = DateTime.Now.Date.ToString("yyyyMMdd");
            try
            {
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                    if (!Directory.Exists(depName))
                    {
                        Directory.CreateDirectory(folderName + "/" + depName);
                    }
                }

                var startInfo = new ProcessStartInfo
                {
                    FileName = phantomJSPath,
                    Arguments = " " + phantomJSArguments + " " + linkList[0] + " " + folderName + "/" + depName + "/" + fileName + ".png",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                };
                var p = new Process();
                p.StartInfo = startInfo;
                p.Start();
                p.WaitForExit();
                string error = p.StandardError.ReadToEnd();
                string output = p.StandardOutput.ReadToEnd();



                string imgWebAddress = linkList[0];

                Console.WriteLine("Got the imgs from dep: " + depName);
                Dictionary<string, string> dic = new Dictionary<string, string>();


                string address = folderName + "/" + depName + "/" + fileName + ".png";

                if (File.Exists(address))
                {
                    if (!Directory.Exists(serverPath + folderName))
                    {
                        Directory.CreateDirectory(serverPath + folderName);
                    }
                    if (!Directory.Exists(serverPath + folderName + "\\" + depName))
                    {
                        Directory.CreateDirectory(serverPath + folderName + "\\" + depName);
                    }
                    File.Copy(address, serverPath + folderName + "\\" + depName + "\\" + fileName + ".png", true);
                }

                dic.Add(imgWebAddress, serverPath + folderName + "\\" + depName + "\\" + fileName + ".png");



                Console.WriteLine("Uploaded to the server...");
                string emailTitle = depName + " 自动化执行结果" + " （" + DateTime.Now.ToString() + ")";
                if (Email.SendEmail(depName, emailTitle, emailAccouont, dic))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something wrong happened in GetImgAndSendEmail...");
                Console.WriteLine(e.ToString());
                Console.WriteLine(DateTime.Now.ToString());
                return false;
            }
        }
    }
}
