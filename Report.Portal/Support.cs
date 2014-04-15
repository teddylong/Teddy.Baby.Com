using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace Report.Portal
{
    public class Support
    {
        public static Dictionary<string, List<string>> list = new Dictionary<string, List<string>>();
        public static Dictionary<string, bool> status = new Dictionary<string, bool>();
        private static string XMLPath = ConfigurationManager.AppSettings["XMLPath"];
        public static Dictionary<string, List<string>> GetInfo(string dep)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XMLPath);
                foreach (XmlNode node in doc.SelectNodes("//dep"))
                {
                    string[] daySend = node.SelectSingleNode("day").InnerText.Split(',');
                    bool sendToday = IsSendToday(daySend);
                    if (sendToday)
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
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(DateTime.Now.ToString());
                return null;
            }
        }

        private static bool IsSendToday(string[] list)
        {
            bool result = false;
            DayOfWeek dow = DateTime.Now.DayOfWeek;
            foreach (string temp in list)
            {
                if (temp.ToLower() == dow.ToString().ToLower())
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool CheckStatus(string dep)
        {
            if (status.Count != 0)
            {
                return status[dep];
            }
            else
            {
                return true;
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
        public static bool SetupStatus()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XMLPath);
                foreach (XmlNode node in doc.SelectNodes("//dep/name"))
                {
                    status.Add(node.InnerText, false);
                }
                return true;
            }
            catch (Exception e)
            {
                status = null;
                Console.WriteLine(e.ToString());
                Console.WriteLine(DateTime.Now.ToString());
                return false;
            }
        }



        public static List<string> TotalDep()
        {
            try
            {
                List<string> result = new List<string>();
                XmlDocument doc = new XmlDocument();
                doc.Load(XMLPath);
                foreach (XmlNode node in doc.SelectNodes("//dep/name"))
                {
                    result.Add(node.InnerText);
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(DateTime.Now.ToString());
                return null;
            }
        }
    }
}
