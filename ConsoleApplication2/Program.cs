using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GetFileSystem
{
    class Program
    {
        private static string path = ConfigurationManager.AppSettings["XMLFilePath"];
        static void Main(string[] args)
        {
            XmlDocument xml = new XmlDocument();
            
            GetXML(@"\\VDST-W7-060\share", xml,"dll");
        }

        private static void GetXML(string path, XmlDocument xml, string fileFormat)
        {
            string[] fileNames = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);
            XmlElement root = xml.CreateElement("Root");
            foreach (string file in fileNames)
            {
                if (file.EndsWith(fileFormat))
                {
                    XmlElement element = xml.CreateElement("File");
                    element.SetAttribute("name", getShortName(file.ToString()));
                    root.AppendChild(element);
                }
            }
            foreach (string dir in directories)
            {
                XmlElement element = xml.CreateElement("Folder");
                element.SetAttribute("path", dir.ToString());
                root.AppendChild(element);
                getDirFile(dir, element, xml, fileFormat);
            }
            //root.AppendChild(xml);
            xml.AppendChild(root);
            xml.Save("test.xml");
        }

        private static void getDirFile(string dir, XmlElement element, XmlDocument xml, string fileFormat)
        {
            string[] fileNames = Directory.GetFiles(dir);
            foreach (string file in fileNames)
            {
                if (file.EndsWith(fileFormat))
                {
                    XmlElement SubElement = xml.CreateElement("File");
                    SubElement.SetAttribute("name", getShortName(file.ToString()));
                    
                    element.AppendChild(SubElement);
                }

            }
            string[] sunDir = Directory.GetDirectories(dir);
            foreach (string subsubDir in sunDir)
            {
                XmlElement SubElement = xml.CreateElement("Folder");
                SubElement.SetAttribute("path", subsubDir.ToString());
                
                element.AppendChild(SubElement);
                getDirFile(subsubDir, SubElement, xml, fileFormat);
            }

        }
        private static string getShortName(string name)
        {
            return name.Substring(name.LastIndexOf("\\") + 1);
        }


    }
}
