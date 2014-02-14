using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace UATService
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

            title = HttpUtility.HtmlEncode(title);
            body = HttpUtility.HtmlEncode(body);
            string soaphdr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\">";
            string template = "<soap:Body><SendMailWithHtml xmlns=\"http://tempuri.org/\"><AddressList xsi:type=\"xsd:string\">{0}</AddressList><title xsi:type=\"xsd:string\">{1}</title><body xsi:type=\"xsd:string\">{2}</body></SendMailWithHtml></soap:Body>";


            string postData = string.Format(template, receiver, title, body);
            postData = soaphdr + postData + "</soap:Envelope>";

           

            SendMailNoAttach(postData);     
        }
 

        public static bool SendEmail(string toAddress, string mailSubject, string mailBody)
        {
            MailMessage Email = new MailMessage();
            Email.IsBodyHtml = true;
            Email.BodyEncoding = System.Text.Encoding.UTF8;
            Email.Body = "<div style=\"background-color:#3385FF; color:White; text-align:center; margin-left:auto; margin-right:auto; font-size:xx-large; font-family:@微软雅黑; font-weight:bold;\">UAT Services Status Error</div>";
            Email.Body += mailBody;
          
            Email.Priority = MailPriority.High;
            Email.Subject = mailSubject;
            Email.SubjectEncoding = Encoding.UTF8;

            Send(toAddress, mailSubject, Email.Body);

            return true;
        }

    }
}
