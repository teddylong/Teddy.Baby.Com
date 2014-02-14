using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MonitorCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Report Email
        private BackgroundWorker outputWorker = new BackgroundWorker();
        private TextReader outputReader;
        public delegate void ProcessEventHanlder(object sender, ProcessEventArgs args);
        public event ProcessEventHanlder OnProcessOutput;
        public Process proc;
        public event ProcessEventHanlder OnProcessExit;
        //UAT
        private BackgroundWorker UAToutputWorker = new BackgroundWorker();
        private TextReader UAToutputReader;
        public delegate void UATProcessEventHanlder(object sender, ProcessEventArgs args);
        public event ProcessEventHanlder UATOnProcessOutput;
        public Process UATproc;
        public event ProcessEventHanlder UATOnProcessExit;
        //CI
        private BackgroundWorker CIoutputWorker = new BackgroundWorker();
        private TextReader CIoutputReader;
        public delegate void CIProcessEventHanlder(object sender, ProcessEventArgs args);
        public event ProcessEventHanlder CIOnProcessOutput;
        public Process CIproc;
        public event ProcessEventHanlder CIOnProcessExit;

        //Support --- Timer 
        public Timer reportTimer;
        public Timer uatTimer;
        public Timer ciTimer;
        public MainWindow()
        {
            InitializeComponent();
            ReportTimeBorder.Visibility = System.Windows.Visibility.Hidden;
            UATTimeBorder.Visibility = System.Windows.Visibility.Hidden;
            CITimeBorder.Visibility = System.Windows.Visibility.Hidden;
        }

        public void moveWindows(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ReportEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ReportEmailBtn.Content.ToString().Contains("Start"))
            {
                RunApp("ReportEmail");
                this.ReportEmailBtn.Content = "Stop Service";
                ReportTimeBorder.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            ReportTimeBorder.Visibility = System.Windows.Visibility.Visible;
                        }
                        ));
                reportTimer = new Timer(1000);
                reportTimer.AutoReset = true;
                reportTimer.Enabled = true;
                reportTimer.Elapsed += new ElapsedEventHandler(reportTimer_Elapsed);
            }
            else
            {
                if (IsProcessRunning)
                { 
                    this.ReportEmailBtn.Content = "Start Service";
                    ReportTimeBorder.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            ReportTimeBorder.Visibility = System.Windows.Visibility.Hidden;
                        }
                        ));
                    
                    //reportTimer.Enabled = false;
                    reportTimer.Stop();
                    reportTimer.Dispose();
                    ReportTimeText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            ReportTimeText.Text = "0";
                        }
                        ));
                    while(IsProcessRunning)
                    {
                        try
                        {
                            proc.Kill();
                        }
                        catch (Exception ex)
                        { 
                            
                        }
                    }
                    
                }
            }
        }
        public void reportTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            ReportTimeText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            ReportTimeText.Text = (int.Parse(ReportTimeText.Text) + 1).ToString();
                        }
                        ));
        }

        private void UATBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UATBtn.Content.ToString().Contains("Start"))
            {
                RunApp("UAT");
                this.UATBtn.Content = "Stop Service";
                UATTimeBorder.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            UATTimeBorder.Visibility = System.Windows.Visibility.Visible;
                        }
                        ));
                uatTimer = new Timer(1000);
                uatTimer.AutoReset = true;
                uatTimer.Enabled = true;
                uatTimer.Elapsed += new ElapsedEventHandler(uatTimer_Elapsed);
            }
            else
            {
                if (IsUATProcessRunning)
                { 
                    this.UATBtn.Content = "Start Service";
                    UATTimeBorder.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            UATTimeBorder.Visibility = System.Windows.Visibility.Hidden;
                        }
                        ));

                    //reportTimer.Enabled = false;
                    uatTimer.Stop();
                    uatTimer.Dispose();
                    UATTimeText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            UATTimeText.Text = "0";
                        }
                        ));
                    UATproc.Kill();
                }
            }
        }
        public void uatTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            UATTimeText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            UATTimeText.Text = (int.Parse(UATTimeText.Text) + 1).ToString();
                        }
                        ));
        }
        private void CIBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CIBtn.Content.ToString().Contains("Start"))
            {
                RunApp("CI");
                this.CIBtn.Content = "Stop Service";
                CITimeBorder.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            CITimeBorder.Visibility = System.Windows.Visibility.Visible;
                        }
                        ));
                ciTimer = new Timer(1000);
                ciTimer.AutoReset = true;
                ciTimer.Enabled = true;
                ciTimer.Elapsed += new ElapsedEventHandler(ciTimer_Elapsed);
            }
            else
            {
                if (IsCIProcessRunning)
                {
                    
                    this.CIBtn.Content = "Start Service";
                    CITimeBorder.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            CITimeBorder.Visibility = System.Windows.Visibility.Hidden;
                        }
                        ));

                    
                    ciTimer.Stop();
                    ciTimer.Dispose();
                    CITimeText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            CITimeText.Text = "0";
                        }
                        ));
                    CIproc.Kill();
                }
            }
        }
        public void ciTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            CITimeText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            CITimeText.Text = (int.Parse(CITimeText.Text) + 1).ToString();
                        }
                        ));
        }
        public void RunApp(string name)
        {
            string address = ConfigurationManager.AppSettings[name];
             
            if(name == "ReportEmail")
            {
                RunWithRedirect(address);
            }
            if (name == "UAT")
            {
                UATRunWithRedirect(address);
            }
            if (name == "CI")
            {
                CIRunWithRedirect(address);
            }
            
        }


        #region Report Email
        public void RunWithRedirect(string cmdPath)
        {
            ReceivedText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            ReceivedText.Text = "";
                        }
                        ));
            proc = new Process();
            proc.StartInfo.FileName = cmdPath;
            proc.StartInfo.RedirectStandardInput = true;
 
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            
            proc.EnableRaisingEvents = true;

            try
            {
                bool processStarted = proc.Start();
            }
            catch (Exception e)
            {
                ReceivedText.Dispatcher.BeginInvoke(new Action(() => ReceivedText.Text += e.Data));
                return;
            }

            outputReader = TextReader.Synchronized(proc.StandardOutput);

            outputWorker.WorkerReportsProgress = true;
            outputWorker.WorkerSupportsCancellation = true;
            outputWorker.DoWork += new DoWorkEventHandler(outputWorker_DoWork);
            outputWorker.ProgressChanged += new ProgressChangedEventHandler(outputWorker_ProgressChanged);
            proc.Exited += new EventHandler(currentProcess_Exited);
            try
            {
                outputWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            { }

            

        }

        void outputWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (outputWorker.CancellationPending == false)
            {
                //  Any lines to read?
                int count = 0;
                char[] buffer = new char[1024];
                do
                {
                    StringBuilder builder = new StringBuilder();
                    count = outputReader.Read(buffer, 0, 1024);
                    builder.Append(buffer, 0, count);
                    outputWorker.ReportProgress(0, builder.ToString());
                    ReceivedText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            ReceivedText.Text += builder.ToString();
                        }
                        ));
                } while (count > 0);

                System.Threading.Thread.Sleep(200);
            }
        }
        public void outputWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            if (e.UserState is string)
            {
                
                FireProcessOutputEvent(e.UserState as string);
            }
        }

        private void FireProcessOutputEvent(string content)
        {
            //  Get the event and fire it.
            var theEvent = OnProcessOutput;
            if (theEvent != null)
            {
                theEvent(this, new ProcessEventArgs(content));
            }
        }

        public bool IsProcessRunning
        { 
            get
            {
                try
                {
                    return (proc != null && proc.HasExited == false);
                }
                catch
                {
                    return false;
                }
            }
        }

        void currentProcess_Exited(object sender, EventArgs e)
        {
            //  Fire process exited.
            FireProcessExitEvent(proc.ExitCode);

            //  Disable the threads.
            outputWorker.CancelAsync();
            outputReader = null;
            proc = null;  
        }

        private void FireProcessExitEvent(int code)
        {
            //  Get the event and fire it.
            var theEvent = OnProcessExit;
            if (theEvent != null)
                theEvent(this, new ProcessEventArgs(code));
        }
        #endregion

        #region UAT
        public void UATRunWithRedirect(string cmdPath)
        {
            UATReceivedText.Dispatcher.BeginInvoke
                       (DispatcherPriority.Normal, (Action)(() =>
                       {
                           UATReceivedText.Text = "";
                       }
                       ));
            
            
            UATproc = new Process();
            UATproc.StartInfo.FileName = cmdPath;

            UATproc.StartInfo.RedirectStandardOutput = true;
            UATproc.StartInfo.UseShellExecute = false;
            UATproc.StartInfo.CreateNoWindow = true;

            UATproc.EnableRaisingEvents = true;

            try
            {
                bool processStarted = UATproc.Start();
            }
            catch (Exception e)
            {
                UATReceivedText.Dispatcher.BeginInvoke(new Action(() => UATReceivedText.Text += e.Data));
                return;
            }

            UAToutputReader = TextReader.Synchronized(UATproc.StandardOutput);

            UAToutputWorker.WorkerReportsProgress = true;
            UAToutputWorker.WorkerSupportsCancellation = true;
            UAToutputWorker.DoWork += new DoWorkEventHandler(UAToutputWorker_DoWork);

            UAToutputWorker.ProgressChanged += new ProgressChangedEventHandler(UAToutputWorker_ProgressChanged);
            UATproc.Exited += new EventHandler(UATcurrentProcess_Exited);
            UAToutputWorker.RunWorkerAsync();
        }
        public void UAToutputWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (UAToutputWorker.CancellationPending == false)
            {
                //  Any lines to read?
                int count = 0;
                char[] buffer = new char[1024];
                do
                {
                    StringBuilder builder = new StringBuilder();
                    count = UAToutputReader.Read(buffer, 0, 1024);
                    builder.Append(buffer, 0, count);
                    UAToutputWorker.ReportProgress(0, builder.ToString());
                    UATReceivedText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            UATReceivedText.Text += builder.ToString();
                        }
                        ));
                } while (count > 0);

                System.Threading.Thread.Sleep(200);
            }
        }
        public void UAToutputWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (e.UserState is string)
            {

                UATFireProcessOutputEvent(e.UserState as string);
            }
        }

        private void UATFireProcessOutputEvent(string content)
        {
            //  Get the event and fire it.
            var theEvent = UATOnProcessOutput;
            if (theEvent != null)
            {
                theEvent(this, new ProcessEventArgs(content));
            }
        }
        void UATcurrentProcess_Exited(object sender, EventArgs e)
        {
            //  Fire process exited.
            UATFireProcessExitEvent(UATproc.ExitCode);

            //  Disable the threads.
            UAToutputWorker.CancelAsync();
            UAToutputReader = null;
            UATproc = null;
        }

        private void UATFireProcessExitEvent(int code)
        {
            //  Get the event and fire it.
            var theEvent = UATOnProcessExit;
            if (theEvent != null)
                theEvent(this, new ProcessEventArgs(code));
        }

        public bool IsUATProcessRunning
        {
            get
            {
                try
                {
                    return (UATproc != null && UATproc.HasExited == false);
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion

        #region CI
        public void CIRunWithRedirect(string cmdPath)
        {
            CIReceivedText.Dispatcher.BeginInvoke
                       (DispatcherPriority.Normal, (Action)(() =>
                       {
                           CIReceivedText.Text = "";
                       }
                       ));

            CIproc = new Process();
            CIproc.StartInfo.FileName = cmdPath;

            CIproc.StartInfo.RedirectStandardOutput = true;
            CIproc.StartInfo.UseShellExecute = false;
            CIproc.StartInfo.CreateNoWindow = true;

            CIproc.EnableRaisingEvents = true;

            try
            {
                bool processStarted = CIproc.Start();
            }
            catch (Exception e)
            {
                CIReceivedText.Dispatcher.BeginInvoke(new Action(() => CIReceivedText.Text += e.Data));
                return;
            }

            CIoutputReader = TextReader.Synchronized(CIproc.StandardOutput);

            CIoutputWorker.WorkerReportsProgress = true;
            CIoutputWorker.WorkerSupportsCancellation = true;
            CIoutputWorker.DoWork += new DoWorkEventHandler(CIoutputWorker_DoWork);

            CIoutputWorker.ProgressChanged += new ProgressChangedEventHandler(CIoutputWorker_ProgressChanged);
            CIproc.Exited += new EventHandler(CIcurrentProcess_Exited);
            CIoutputWorker.RunWorkerAsync();
        }
        public void CIoutputWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (CIoutputWorker.CancellationPending == false)
            {
                //  Any lines to read?
                int count = 0;
                char[] buffer = new char[1024];
                do
                {
                    StringBuilder builder = new StringBuilder();
                    count = CIoutputReader.Read(buffer, 0, 1024);
                    builder.Append(buffer, 0, count);
                    CIoutputWorker.ReportProgress(0, builder.ToString());
                    CIReceivedText.Dispatcher.BeginInvoke
                        (DispatcherPriority.Normal, (Action)(() =>
                        {
                            CIReceivedText.Text += builder.ToString();
                        }
                        ));
                } while (count > 0);

                System.Threading.Thread.Sleep(200);
            }
        }
        public void CIoutputWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (e.UserState is string)
            {

                CIFireProcessOutputEvent(e.UserState as string);
            }
        }

        private void CIFireProcessOutputEvent(string content)
        {
            //  Get the event and fire it.
            var theEvent = CIOnProcessOutput;
            if (theEvent != null)
            {
                theEvent(this, new ProcessEventArgs(content));
            }
        }
        void CIcurrentProcess_Exited(object sender, EventArgs e)
        {
            //  Fire process exited.
            CIFireProcessExitEvent(CIproc.ExitCode);

            //  Disable the threads.
            CIoutputWorker.CancelAsync();
            CIoutputReader = null;
            CIproc = null;
        }

        private void CIFireProcessExitEvent(int code)
        {
            //  Get the event and fire it.
            var theEvent = CIOnProcessExit;
            if (theEvent != null)
                theEvent(this, new ProcessEventArgs(code));
        }

        public bool IsCIProcessRunning
        {
            get
            {
                try
                {
                    return (CIproc != null && CIproc.HasExited == false);
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion

        // Kill all the processs when close the Application
        public void windows_Close(object sender, CancelEventArgs e)
        {
            if (IsProcessRunning)
            {
                while (IsProcessRunning)
                {
                    try
                    {
                        proc.Kill();
                    }
                    catch (Exception ex) { }
                }
            }
            if (IsUATProcessRunning)
            {
                while (IsUATProcessRunning)
                {
                    try
                    {
                        UATproc.Kill();
                    }
                    catch (Exception ex) { }
                }  
            }
            if (IsCIProcessRunning)
            {
                while (IsCIProcessRunning)
                {
                    try
                    {
                        CIproc.Kill();
                    }
                    catch (Exception ex) { }
                }
            }

        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != System.Windows.WindowState.Minimized)
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != System.Windows.WindowState.Minimized)
            {

                this.Close();
            }
        }

      


    }
}
