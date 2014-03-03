using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.Coverage.Analysis;
using System.Configuration;

namespace CodeCoverage
{
    class Program
    {
        private static List<Job> jobList = new List<Job>();
        private static List<Job> jobList2 = new List<Job>();
        private static string VSINSTR = ConfigurationManager.AppSettings["VSINSTR"];
        private static string VSPerfMon = ConfigurationManager.AppSettings["VSPerfMon"];
        private static string VSPerfMonStop = ConfigurationManager.AppSettings["VSPerfMonStop"];
        private static string SourceMachine = ConfigurationManager.AppSettings["SourceMachine"];
        private static string StartVSperfBAT = ConfigurationManager.AppSettings["StartVSperfBAT"];
        private static string BaseFolder = ConfigurationManager.AppSettings["BaseFolder"];
        private static string outPutFolder = ConfigurationManager.AppSettings["outPutFolder"];
        static void Main(string[] args)
        {
            while (true)
            {
                Object thislock = new object();
                lock (thislock)
                {
                    CheckDB();
                    CheckDB2();
                    Thread.Sleep(5 * 1000);
                    Console.WriteLine("idle...");
                }
            } 
        }
        // Status ='On'
        public static void CheckDB()
        {
            string querySQL = "select * FROM [ATDataBase].[dbo].[CodeCoverage] where Alive = 'YES' and Status ='On' and Machine = '" + SourceMachine + "'";
            DataTable dt = SQLHelper.GetDataTableBySql(querySQL);
            foreach (DataRow dr in dt.Rows)
            {
                Job job = new Job();
                job.ID = int.Parse(dr["ID"].ToString());
                job.Status = dr["Status"].ToString();
                job.StartTime = dr["StartTime"].ToString();
                job.EndTime = dr["EndTime"].ToString();
                job.FileName = dr["FileName"].ToString();
                job.Alive = dr["Alive"].ToString();
                job.Machine = dr["Machine"].ToString();
                jobList.Add(job);
            }
            if (dt.Rows.Count > 0)
            {
                LogService.WriteLog("Start Instrument...",EventLogEntryType.Information);
                Instrument(jobList);
                
            }
        }
        // Status ='Off'
        public static void CheckDB2()
        {
            string querySQL = "select * FROM [ATDataBase].[dbo].[CodeCoverage] where Alive = 'YES' and Status ='Off' and Machine = '" + SourceMachine + "'";
            DataTable dt = SQLHelper.GetDataTableBySql(querySQL);
            foreach (DataRow dr in dt.Rows)
            {
                Job job = new Job();
                job.ID = int.Parse(dr["ID"].ToString());
                job.Status = dr["Status"].ToString();
                job.StartTime = dr["StartTime"].ToString();
                job.EndTime = dr["EndTime"].ToString();
                job.FileName = dr["FileName"].ToString();
                job.Alive = dr["Alive"].ToString();
                job.Machine = dr["Machine"].ToString();
                jobList2.Add(job);
                UpdateDB2(job.ID);
            }
            if (dt.Rows.Count > 0)
            {
                StopIISAndExpress();
                Thread.Sleep(2000);
                StopVSperf();
                Thread.Sleep(2000);
                StartIIS();
                Thread.Sleep(2000);
                CovToXMLFile();
                Thread.Sleep(3000);
                RestoredFile(dt);
            }
        }

        // vsinstr -coverage someCode.dll
        public static void Instrument(List<Job> list)
        {
            foreach (Job j in list)
            {
                string[] dllName = j.FileName.Split(';');
                
                
                for (int i = 0; i < dllName.Length; i++)
                {
                    if (!dllName[i].Equals(string.Empty))
                    {
                        var startInfo = new ProcessStartInfo
                        {
                            FileName = VSINSTR,
                            Arguments = " -coverage " + dllName[i],
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            RedirectStandardInput = true,
                        };
                        var p = new Process();
                        p.StartInfo = startInfo;
                        p.Start();
                        p.WaitForExit();
                        if (!p.StandardError.ReadToEnd().Equals(String.Empty))
                        {
                            LogService.WriteLog(p.StandardError.ReadToEnd(), EventLogEntryType.Error);
                        }
                        LogService.WriteLog(p.StandardOutput.ReadToEnd(),EventLogEntryType.Information);
                        
                    }
                }
                UpdateDB(j.ID);
                StartVSperf();
            }    
        }
        private static void UpdateDB(int id)
        {
            string updateSQL = "UPDATE [ATDataBase].[dbo].[CodeCoverage] SET [Alive] = 'NO' WHERE ID = " + id;
            SQLHelper.ExecSQLCmd(updateSQL);
        }
        private static void UpdateDB2(int id)
        {
            string updateSQL = "UPDATE [ATDataBase].[dbo].[CodeCoverage] SET [Status] = 'Off',[Alive] = 'NO' WHERE ID = " + id;
            SQLHelper.ExecSQLCmd(updateSQL);
        }

        // vsperfmon /coverage /output: output.coverage /cs /user:Everyone
        public static void StartVSperf()
        {
            Process.Start(StartVSperfBAT);
            LogService.WriteLog("Build Coverage File...", EventLogEntryType.Information);
        }
        // vsperfcmd -shutdown
        public static void StopVSperf()
        {
            var startVSPerfmonStopInfo = new ProcessStartInfo
            {
                FileName = VSPerfMonStop,
                Arguments = " -shutdown",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
            };
            var pVSPerfmonStop = new Process();
            pVSPerfmonStop.StartInfo = startVSPerfmonStopInfo;
            pVSPerfmonStop.Start();
            pVSPerfmonStop.WaitForExit();
            //Read the Error:
            string errorVSPerfmonStop = pVSPerfmonStop.StandardError.ReadToEnd();
            if (!errorVSPerfmonStop.Equals(String.Empty))
            {
                LogService.WriteLog(errorVSPerfmonStop, EventLogEntryType.Error);
            }
        
            //Read the Output:
            string outputVSPerfmonStop = pVSPerfmonStop.StandardOutput.ReadToEnd();
            if (!outputVSPerfmonStop.Equals(String.Empty))
            {
                LogService.WriteLog(outputVSPerfmonStop, EventLogEntryType.Information);
            }
        }
        // taskkill /im iisexpress.exe
        // iisreset /stop
        public static void StopIISAndExpress()
        { 
            
            var stopIISExpress = new ProcessStartInfo
            {
                FileName = "taskkill.exe",
                Arguments = " /im iisexpress.exe",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
            };
            var pStopIISExpress = new Process();
            pStopIISExpress.StartInfo = stopIISExpress;
            pStopIISExpress.Start();
            pStopIISExpress.WaitForExit();
            if (!pStopIISExpress.StandardOutput.ReadToEnd().Equals(String.Empty))
            {
                LogService.WriteLog(pStopIISExpress.StandardOutput.ReadToEnd(), EventLogEntryType.Information);
            }

            var stopIIS = new ProcessStartInfo
            {
                FileName = "iisreset.exe",
                Arguments = " /stop",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
            };
            var pStopIIS = new Process();
            pStopIIS.StartInfo = stopIIS;
            pStopIIS.Start();
            pStopIIS.WaitForExit();
            if (!pStopIIS.StandardOutput.ReadToEnd().Equals(String.Empty))
            {
                LogService.WriteLog(pStopIIS.StandardOutput.ReadToEnd(), EventLogEntryType.Information);
            }
        }
        // iisreset /start
        public static void StartIIS()
        {
            var startIIS = new ProcessStartInfo
            {
                FileName = "iisreset.exe",
                Arguments = " /start",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
            };
            var pStartIIS = new Process();
            pStartIIS.StartInfo = startIIS;
            pStartIIS.Start();
            pStartIIS.WaitForExit();
            if (!pStartIIS.StandardOutput.ReadToEnd().Equals(String.Empty))
            {
                LogService.WriteLog(pStartIIS.StandardOutput.ReadToEnd(), EventLogEntryType.Information);
            }
        }
        public static void CovToXMLFile()
        {
            string path = BaseFolder + @"\output.coverage";
            try
            {
                if (File.Exists(path))
                {
                    CoverageInfo ci = CoverageInfo.CreateFromFile(path);
                    CoverageDS data = ci.BuildDataSet(null);
                    string outputName = DateTime.Now.ToString("yyyyMMddHHmm") + ".xml";
                    data.ExportXml(outPutFolder + @"\" + outputName);
                    LogService.WriteLog("Build Coverage XML file...", EventLogEntryType.Information);
                }
            }
            catch (Exception e)
            {
                LogService.WriteLog("Build XML file failed..." + "\n" + e.ToString(),EventLogEntryType.Error);
            }
        }

        public static void RestoredFile(DataTable dt)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Job job = new Job();
                    job.ID = int.Parse(dr["ID"].ToString());
                    job.FileName = dr["FileName"].ToString();
                    job.Machine = dr["Machine"].ToString();
                    string[] files = job.FileName.Split(';');
                    for (int i = 0; i < files.Length; i++)
                    {

                        if (File.Exists(files[i]) && File.Exists(files[i] + ".orig"))
                        {
                            File.Delete(files[i]);
                            File.Move(files[i] + ".orig", files[i]);
                            if (File.Exists(files[i].Remove(files[i].IndexOf(".dll")) + ".instr.pdb"))
                            {
                                File.Delete(files[i].Remove(files[i].IndexOf(".dll")) + ".instr.pdb");
                            }

                        }
                    }
                }
                LogService.WriteLog("Restored File...", EventLogEntryType.Information);
            }
            catch (Exception e)
            {
                LogService.WriteLog("Restored File failed..." + "\n" + e.ToString(), EventLogEntryType.Error);
            }
        }
    }
}
