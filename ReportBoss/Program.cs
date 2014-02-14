using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ReportBoss
{
    class Program
    {
        public static Dictionary<string, List<string>> list = new Dictionary<string, List<string>>();
        static void Main(string[] args)
        {
            
            list = Support.GetInfo();
            foreach (var item in list)
            {
                string depName = item.Key;
                List<string> infoList = item.Value;
                UpdateImgLink(infoList);
                GetImg(depName, infoList);
                Thread.Sleep(2 * 60 * 1000);
            }
            Console.ReadLine();
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
            string query1 = @"select max(a.runid) from[ATDataBase].[dbo].[CI_LOG_RUN] a inner join ci_log_job b on a.runid=b.runid where b.casecount > 0 and a.RunType=0 and b.type=1 and Convert(nvarchar(10),a.GmtCreate,23)='" + sendDate + "'";


            DataTable dt0 = SQLHelper.GetDataTableBySql(query0);
            DataTable dt1 = SQLHelper.GetDataTableBySql(query1);

            return dt0.Rows[0][0].ToString() + "/" + dt1.Rows[0][0].ToString();
            
        }

        public static void GetImg(string depName,List<string> linkList)
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
                Arguments =" " + phantomJSArguments + " " + linkList[0] + " " + folderName + "/" + depName + "/" + fileName + ".png",
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
    