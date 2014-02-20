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
        private static string depName = ConfigurationManager.AppSettings["DepName"];
        private static string emailList = ConfigurationManager.AppSettings["EmailList"];
        private static int DeadLine = int.Parse(ConfigurationManager.AppSettings["DeadLine"]);
        static void Main(string[] args)
        {
            while (true)
            {
                Result result = CheckRunStatus(depName);
                if (result == Result.NotYet)
                {
                    Thread.Sleep(3 * 60 * 1000);
                    break;
                }
                else if (result == Result.Completed)
                {
                    List<string> list = ProcessEmailList(emailList);
                    GetImg(depName, list);
                }
                else if (result == Result.NotCompleteButSend)
                {

                }
                else
                { 
                    
                }
            }
        }

        private static List<string> ProcessEmailList(string emaillist)
        {
            List<string> result = new List<string>();
            string[] temp = emailList.Split(';');
            for (int i = 0; i < temp.Length; i++)
            {
                result.Add(temp[i]);
            }
            return result;
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

            if (DateTime.Now.Hour > 17)
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
            string emailTitle = depName + "自动化执行结果" + " （" + DateTime.Now.ToString() + ")";
            Email.SendEmail(depName, emailTitle, emailAccouont, dic);
        }
    }
}
