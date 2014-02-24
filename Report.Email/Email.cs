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

namespace Report.Email
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

        public static void Send(string receiver, string title, string body, string sendTime)
        {

            String iHour = DateTime.Now.Hour.ToString();
            String iMin = DateTime.Now.Minute.ToString();
            int waitTime = 0;

            int waitHours = int.Parse(sendTime) - int.Parse(iHour);
            if (waitHours < 0)
            {
                waitHours = 24 - (-waitHours);
                waitTime = (waitHours * 3600000) - (int.Parse(iMin) * 60000);
            }
            else
            {
                if (waitHours == 0)
                {
                    waitTime = 3600000 - (int.Parse(iMin) * 60000);
                }
                else
                {
                    waitTime = (waitHours * 3600000) - (int.Parse(iMin) * 60000);
                }
            }
            Console.WriteLine("当前线程邮件接收者： " + receiver + " 时间： " + DateTime.Now);
            Console.WriteLine("需要等待： " + waitTime / 1000 + "秒。。。");

            Thread.Sleep(waitTime);
            Console.WriteLine("等待结束，发送邮件中。。。" + " 时间： " + DateTime.Now);

            title = HttpUtility.HtmlEncode(title);
            body = HttpUtility.HtmlEncode(body);
            string soaphdr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\">";
            string template = "<soap:Body><SendMailWithHtml xmlns=\"http://tempuri.org/\"><AddressList xsi:type=\"xsd:string\">{0}</AddressList><title xsi:type=\"xsd:string\">{1}</title><body xsi:type=\"xsd:string\">{2}</body></SendMailWithHtml></soap:Body>";


            string postData = string.Format(template, receiver, title, body);
            postData = soaphdr + postData + "</soap:Envelope>";

            while (true)
            {

                SendMailNoAttach(postData);
                Console.WriteLine("邮件发送完成( " + receiver + " )。。。等待下一次发送" + " 时间： " + DateTime.Now);
                Thread.Sleep(24 * 3600000);
            }
        }

        public static Hashtable GetDifferEmail()
        {
            String strSql = " select distinct email from [ATDataBase].[dbo].[CI_ReportEmail] ";
            var dtEmails = SQLHelper.GetDataTableBySql(strSql);
            String strChanel = " ";


            Hashtable htEmail = new Hashtable();


            for (int i = 0; i < dtEmails.Rows.Count; i++)
            {
                strChanel = "  select Channel from [ATDataBase].[dbo].[CI_ReportEmail] where Email=";
                strChanel = strChanel + "'" + dtEmails.Rows[i]["email"].ToString() + "'";
                var dtChanel = SQLHelper.GetDataTableBySql(strChanel);
                strChanel = "";
                List<string> ltChannel = new List<string>();

                for (int j = 0; j < dtChanel.Rows.Count; j++)
                {
                    ltChannel.Add(dtChanel.Rows[j]["Channel"].ToString());

                }

                string sendTimeSQL = "select distinct SendTime from [ATDataBase].[dbo].[CI_ReportEmail] where Email=";
                sendTimeSQL = sendTimeSQL + "'" + dtEmails.Rows[i]["email"].ToString() + "'";
                var dtSendTime = SQLHelper.GetDataTableBySql(sendTimeSQL);
                List<string> ltSendTime = new List<string>();

                for (int j = 0; j < dtSendTime.Rows.Count; j++)
                {
                    ltChannel.Add(dtSendTime.Rows[j]["SendTime"].ToString());

                }

                htEmail.Add(dtEmails.Rows[i]["email"].ToString(), ltChannel);
                ltChannel = null;
            }

            return htEmail;

        }

        public static bool SendEmail(string[] files, string toAddress, ArrayList arrFilePath, string mailSubject, string mailBody, string sendTime)
        {
            MailMessage Email = new MailMessage();
            Email.IsBodyHtml = true;
            Email.BodyEncoding = System.Text.Encoding.UTF8;
            mailBody = "<div style=\"background-color:#3385FF; color:White; text-align:center; margin-left:auto; margin-right:auto; font-size:xx-large; font-family:@微软雅黑; font-weight:bold;\">自动化Portal 邮件订阅</div>";
            Email.Body += mailBody;
            for (int i = 0; i < files.Length; i++)
            {
                string pattern = @"\\";
                string[] temp = Regex.Split(files[i], pattern);
                string fileName = temp[temp.Length - 1];
                fileName = fileName.Split('.')[0];

                string[] realName = nameProcess(fileName);
                Email.Body += "<br/>";
                Email.Body += "<div style=\"color:Red; font-family:Tahoma; font-size:30px\">" + realName[0] + ": <a style=\"font-size:20px;\" href=\"" + realName[1] + "\">点击可查看网页</a></div><br/>";
                Email.Body += "<img height=\"auto\" src=\"" + files[i] + "\"/><br/>";
            }
            Email.Priority = MailPriority.Low;
            Email.Subject = mailSubject;
            Email.SubjectEncoding = Encoding.UTF8;

            if (arrFilePath != null)
            {
                for (int i = 0; i < arrFilePath.Count; i++)
                {
                    string file = arrFilePath[i].ToString();
                    Attachment data = new Attachment(file, System.Net.Mime.MediaTypeNames.Application.Octet);
                    System.Net.Mime.ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(file);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                    Email.Attachments.Add(data);
                }
            }
            Send(toAddress, mailSubject, Email.Body, sendTime);

            return true;
        }

        public static string[] nameProcess(string name)
        {
            switch (name)
            {
                case "APICase":
                    string[] result1 = { "接口自动化Case报告", "http://autotest/Pages/Reports/CaseSummary.aspx?type=1" };
                    return result1;
                case "UICase":
                    string[] result2 = { "UI自动化Case报告", "http://autotest/Pages/Reports/CaseSummary.aspx?type=0" };
                    return result2;
                case "ApiInterFace":
                    string[] result3 = { "接口View报告", "http://autotest/Pages/Reports/APIRequestSummary.aspx" };
                    return result3;
                default:
                    return null;
            }

        }

        
    }
}
