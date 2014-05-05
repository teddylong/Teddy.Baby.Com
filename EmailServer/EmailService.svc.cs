using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace EmailServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class EmailService : IService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        
        public void SendMailWithNoAttach(string AddressList, string title, string body)
        {
            List<string> mailList = new List<string>();
            string[] mails = AddressList.Split(new Char[] { ';' });


            foreach (string mail in mails)
            {
                mailList.Add(mail);
            }

            List<string> fileList = new List<string>();
            Mail.SendToEmail("appmail.sh.ctriptravel.com", 000, mailList, title, body, fileList);
        }
        public string SendMail(string AddressList, string title, string body, string fileName)
        {
            List<string> mailList = new List<string>();
            string[] mails = AddressList.Split(new Char[] { ';' });
            foreach (string mail in mails)
            {
                mailList.Add(mail);
            }
            
            List<string> fileList = new List<string>();
            string[] files = fileName.Split(';');
            foreach (string file in files)
            {
                fileList.Add(file);
            }
            return Mail.SendToEmail("appmail.sh.ctriptravel.com", 000, mailList, title, body, fileList);
        }
        public void SendMailWithHtml(string AddressList, string title, string body)
        {
            List<string> mailList = new List<string>();
            string[] mails = AddressList.Split(new Char[] { ';' });

            title = HttpUtility.HtmlDecode(title);
            body = HttpUtility.HtmlDecode(body);

            foreach (string mail in mails)
            {
                mailList.Add(mail);
            }

            List<string> fileList = new List<string>();
            Mail.SendToEmail("appmail.sh.ctriptravel.com", 000, mailList, title, body, fileList);
        }
    }
}
