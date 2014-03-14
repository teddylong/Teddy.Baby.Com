using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using System.Web;
using System.Collections;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;

namespace Report.Hotel
{
    public class Email
    {
        private static void SendMailNoAttach(string postData)
        {
            try
            {
                byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(postData);
                string url = @"http://192.168.81.123/SendMail/SendMail.asmx";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.Host = "192.168.81.123";
                req.ContentType = "application/soap+xml; charset=utf-8";
                //application/x-www-form-urlencoded
                req.ContentLength = buffer.Length;
                Console.WriteLine(req.ContentLength);
                req.Headers.Add("SOAPAction: \"http://tempuri.org/SendMailWithHtml\"");
                req.Timeout = 5000;

                Stream reqst = req.GetRequestStream();
                reqst.Write(buffer, 0, buffer.Length);
                reqst.Flush();
                HttpWebResponse response = req.GetResponse() as HttpWebResponse;
                response.Close();
                reqst.Close();

            }
            catch
            {
                throw;
            }

        }

        public static void Send(string receiver, string title, string body)
        {
            Console.WriteLine("当前线程邮件接收者： " + receiver + " 时间： " + DateTime.Now);
            title = HttpUtility.HtmlEncode(title);
            body = HttpUtility.HtmlEncode(body);
            string soaphdr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\">";
            string template = "<soap:Body><SendMailWithHtml xmlns=\"http://tempuri.org/\"><AddressList xsi:type=\"xsd:string\">{0}</AddressList><title xsi:type=\"xsd:string\">{1}</title><body xsi:type=\"xsd:string\">{2}</body></SendMailWithHtml></soap:Body>";


            string postData = string.Format(template, receiver, title, body);
            postData = soaphdr + postData + "</soap:Envelope>";
            SendMailNoAttach(postData);
            Console.WriteLine("Sent..." + DateTime.Now);

        }

        public static bool SendEmail(string depName, string title, string emailAccount, Dictionary<string, string> dic)
        {
            try
            {
                List<int> data = new List<int>();
                if (!depName.Equals(String.Empty))
                {
                    data = getDataBase(depName);
                }
                MailMessage Email = new MailMessage();
                Email.IsBodyHtml = true;
                Email.BodyEncoding = System.Text.Encoding.UTF8;
                Email.Body = "<div style=\"background-color:#3385FF; color:White; text-align:center; margin-left:auto; margin-right:auto; font-size:large; font-family:@微软雅黑; font-weight:bold;\">" + title + "</div>";

                Email.Body += "<div style=\"color:red;font-size:13px;\">(点击图片可link到Portal Site)</div>";
                Email.Body += "<div style=\"font-size:16px; color:blue;\">成功运行Job数/总Job数 ---> " + data[0] + "/" + data[1] + "</div>";

                foreach (var item in dic)
                {
                    Email.Body += "<a style=\"text-align:center; margin-left:auto; margin-right:auto;\" href = " + item.Key + ">" + "<img src=\"" + item.Value + "\"/></a><br/><br/>";
                }

                Email.Priority = MailPriority.Low;
                Email.Subject = title;
                Email.SubjectEncoding = Encoding.UTF8;
                Send(emailAccount, Email.Subject, Email.Body);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something wrong happened in Email.SendEmail...");
                Console.WriteLine(e.ToString());
                Console.WriteLine(DateTime.Now.ToString());
                return false;
            }
        }

        public static List<int> getDataBase(string depName)
        {
            int depNumber = 0;
            switch (depName)
            {
                case "Flight":
                    depNumber = 10003;
                    return getData(depNumber);
                case "Hotel":
                    depNumber = 10004;
                    return getData(depNumber);
                case "Corp":
                    depNumber = 10006;
                    return getData(depNumber);
                case "NB":
                    depNumber = 10008;
                    return getData(depNumber);
                case "PF":
                    depNumber = 10005;
                    return getData(depNumber);
                case "Vacations":
                    depNumber = 10009;
                    return getData(depNumber);
                case "YOU":
                    depNumber = 10010;
                    return getData(depNumber);
                default:
                    return null;
            }


        }
        public static List<int> getData(int depNumber)
        {
            List<int> result = new List<int>();
            string querySuccess = @"select COUNT(*) from [ATDataBase].[dbo].[CI_Log_RunDetail] t1, (select JobName,max(CreateTime) as Time from [ATDataBase].[dbo].[CI_Log_RunDetail] t2 group by JobName) t2 where t1.JobName = t2.Jobname and t1.CreateTime = t2.Time and Status =2 and t1.JobName in (SELECT [ProjectName] FROM [ATDataBase].[dbo].[CI_Tfs_Project] where DepID = " + depNumber + ")";
            DataTable dtSuccess = SQLHelper.GetDataTableBySqlNoParm(querySuccess);
            result.Add(int.Parse(dtSuccess.Rows[0][0].ToString()));
            string queryTotal = @"select COUNT(*) from [ATDataBase].[dbo].[CI_Log_RunDetail] t1, (select JobName,max(CreateTime) as Time from [ATDataBase].[dbo].[CI_Log_RunDetail] t2 group by JobName) t2 where t1.JobName = t2.Jobname and t1.CreateTime = t2.Time and t1.JobName in (SELECT [ProjectName] FROM [ATDataBase].[dbo].[CI_Tfs_Project] where DepID = " + depNumber + ")";
            DataTable dtTotal = SQLHelper.GetDataTableBySqlNoParm(queryTotal);
            result.Add(int.Parse(dtTotal.Rows[0][0].ToString()));
            return result;
        }

    }
}
