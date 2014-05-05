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

namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Send("autotest@ctrip.com", "Testing", "This Email comes from our server ---> 192.168.43.202");
        }

        private static void SendMailNoAttach(string postData)
        {
            try
            {
                byte[] buffer = Encoding.GetEncoding("utf-8").GetBytes(postData);
                string url = @"http://192.168.43.202:8888/SendMail.asmx";
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
            Console.WriteLine("Sent..." + DateTime.Now);

        }
    }
}
