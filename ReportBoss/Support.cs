using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ReportBoss
{
    public class Support
    {
        public static Dictionary<string, List<string>> list = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> GetInfo()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            foreach (XmlNode node in doc.SelectNodes("//dep"))
            {
                List<string> temp = new List<string>();
                string depName = node.SelectSingleNode("name").InnerText;
                string emailAccount = node.SelectSingleNode("email").InnerText;
                temp.Add(emailAccount);
                foreach (XmlNode tempNode in node.SelectNodes("website/link"))
                {
                    string tempLink = tempNode.InnerText;
                    temp.Add(tempLink);
                }
                list.Add(depName, temp);
            }
            return list;
        }
    }
}
