using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CodeCoverageService
{
    public partial class CodeCoverageService : ServiceBase
    {
        private static string StartProgram = ConfigurationManager.AppSettings["StartProgram"];
        public CodeCoverageService()
        {
            InitializeComponent();
        }
        
        protected override void OnStart(string[] args)
        {
            if (ChangeConfigFile())
            {
                //Process.Start(StartProgram);
                Process p = new Process();
                p.StartInfo.FileName = StartProgram;
                p.StartInfo.Verb = "runas";
                p.Start();
                

                //string errorVSPerfmonStop = pStart.StandardError.ReadToEnd();
                //LogService.WriteLog(errorVSPerfmonStop, EventLogEntryType.Error); 
                LogService.WriteLog("Code Coverage Service Started...", EventLogEntryType.Warning);
            }
        }

        protected override void OnStop()
        {
            Process[] proc = Process.GetProcessesByName("CodeCoverage");
            proc[0].Kill();
            LogService.WriteLog("Code Coverage Service Stopped...", EventLogEntryType.Warning);
        }


        private bool ChangeConfigFile()
        {
            string filePath = @"D:\apps\CodeCoverage\CodeCoverage.exe.config";
            string machineName = System.Environment.MachineName;

            if (File.Exists(filePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNodeList list = doc.SelectNodes("configuration/appSettings/add");
                foreach (XmlNode node in list)
                {
                    if (node.Attributes["key"].Value == "SourceMachine")
                    {
                        node.Attributes["value"].Value = machineName;
                    }
                }
                doc.Save(filePath);
                LogService.WriteLog("Changed the Config file...", EventLogEntryType.Information);
                return true;
            }
            else
            {
                LogService.WriteLog("Change the Config file failed...",EventLogEntryType.Error);
                return false;
            }
        }
    }
}
