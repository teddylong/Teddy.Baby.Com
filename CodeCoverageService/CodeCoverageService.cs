using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Coverage.Analysis;

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
            Process.Start(StartProgram); 
        }

        protected override void OnStop()
        {
            Process[] proc = Process.GetProcessesByName("CodeCoverage");
            proc[0].Kill();
        }
    }
}
