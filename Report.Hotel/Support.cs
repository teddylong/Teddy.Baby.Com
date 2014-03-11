using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Report.Hotel
{
    public class Support
    {
        public static Dictionary<string, List<string>> list = new Dictionary<string, List<string>>();
        public static Dictionary<string, bool> status = new Dictionary<string, bool>();
        public static Dictionary<string, List<string>> GetInfo(string dep)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            foreach (XmlNode node in doc.SelectNodes("//dep"))
            {

                if (node.SelectSingleNode("name").InnerText == dep)
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
            }
            return list;
        }

        public static bool CheckStatus(string dep)
        {
            if (status.Count != 0)
            {
                return status[dep];
            }
            else
            {
                return false;
            }
        }

        public static void OKStatus(string dep)
        {
            if (status.Count != 0)
            {
                status[dep] = true;
            }
        }

        public static void ReSetStatus()
        {
            if (status.Count != 0)
            {
                status.Clear();
                SetupStatus();
            }
        }
        public static void SetupStatus()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            foreach (XmlNode node in doc.SelectNodes("//dep/name"))
            {
                status.Add(node.InnerText, false);
            }
        }



        public static List<string> TotalDep()
        {
            List<string> result = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            foreach (XmlNode node in doc.SelectNodes("//dep/name"))
            {
                result.Add(node.InnerText);
            }
            return result;
        }
    }
}
