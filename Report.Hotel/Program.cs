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
        public static Dictionary<string, bool> status = new Dictionary<string, bool>();
        
        


        static void Main(string[] args)
        {
            Support.SetupStatus();

            while (true)
            {
                List<string> totalDep = Support.TotalDep();
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
                            foreach (var item in list)
                            {
                                List<string> infoList = item.Value;
                                UpdateImgLink(infoList);
                                GetImg(depName, infoList);
                            }
                            Support.OKStatus(depName);
                            Console.WriteLine("********************************");
                            Console.WriteLine("Completed: " + depName + "  (" + DateTime.Now.ToString() + ")");
                            Console.WriteLine("********************************");
                        }
                        else
                        {

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

        

        
        private static void UpdateImgLink(List<string> list)
        {
            if (list.Count == 2)
            {
                string LastRunID = GetLatestRunID();
                list[1] = list[1].Trim() + LastRunID;
            }
        }

        private static string GetLatestRunID()
        {
            string sendDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            string query0 = @"select max(a.runid) from[ATDataBase].[dbo].[CI_LOG_RUN] a inner join ci_log_job b on a.runid=b.runid where b.casecount > 0 and a.RunType=0 and b.type=0 and Convert(nvarchar(10),a.GmtCreate,23)='" + sendDate + "'";
            //string query1 = @"select max(a.runid) from[ATDataBase].[dbo].[CI_LOG_RUN] a inner join ci_log_job b on a.runid=b.runid where b.casecount > 0 and a.RunType=0 and b.type=1 and Convert(nvarchar(10),a.GmtCreate,23)='" + sendDate + "'";


            DataTable dt0 = SQLHelper.GetDataTableBySql(query0);
            //DataTable dt1 = SQLHelper.GetDataTableBySql(query1);

            return dt0.Rows[0][0].ToString();

        } 
        private static Result CheckRunStatus(string dep)
        {
            string sendDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            string queryRunID = @"select max(a.runid) from [ATDataBase].[dbo].[CI_LOG_RUN] a inner join ci_log_job b on a.runid=b.runid where b.casecount > 0 and a.RunType=0 and b.type=0 and Convert(nvarchar(10),a.GmtCreate,23)='" + sendDate + "'";
            DataTable dtRunID = SQLHelper.GetDataTableBySql(queryRunID);
            string runID = dtRunID.Rows[0][0].ToString();
            string apiJobName = "API." + dep + "%";
            string uiJobName = "UI." + dep + "%";
            string queryResult = @"select COUNT(*) from [ATDataBase].[dbo].[ci_log_job] where runid = " + runID + " and ( Jobname like '" + apiJobName + "' or Jobname like '" + uiJobName + "') and GmtEnd is null";
            DataTable dtResult = SQLHelper.GetDataTableBySql(queryResult);

            if (DateTime.Now.Hour > DeadLine)
            {
                return Result.NotCompleteButSend;
            }
            else if (int.Parse(dtResult.Rows[0][0].ToString()) == 0)
            {
                return Result.Completed;
            }
            else if (int.Parse(dtResult.Rows[0][0].ToString()) > 0)
            {
                return Result.NotYet;
            }
            return Result.Default;
        }

        public enum Result
        { 
            NotYet = 0,
            Completed = 1,
            NotCompleteButSend = 2,
            Default = 3
        }

        public static void GetImg(string depName, List<string> linkList)
        {
            Console.WriteLine("Start getting img...");
            string phantomJSPath = ConfigurationManager.AppSettings["phantomJSPath"];
            string phantomJSArguments = ConfigurationManager.AppSettings["phantomJSArguments"];
            string serverPath = ConfigurationManager.AppSettings["serverPath"];


            string emailAccouont = linkList[0];
            linkList.Remove(linkList[0]);

            string fileName = "ScreenShot";
            string folderName = DateTime.Now.Date.ToString("yyyyMMdd");

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
            string emailTitle = ChangeToChinese(depName) + "自动化执行结果" + " （" + DateTime.Now.ToString() + ")";
            Email.SendEmail(depName, emailTitle, emailAccouont, dic);
        }


        private static string ChangeToChinese(string dep)
        { 
            switch(dep)
            {
                case "Hotel":
                    return "酒店";
                case "Flight":
                    return "机票";
                case "PF":
                    return "公共服务";
                case "Corp":
                    return "商旅";
                case "NB":
                    return "营销高端";
                case "Vacations":
                    return "度假";
                case "YOU":
                    return "攻略社区";
                default:
                    return "";

            }
        }
    }
}
