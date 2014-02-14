using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.IO;
using System.Diagnostics;

namespace RemoteCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //ConnectionOptions opt = new ConnectionOptions();
            //string host = "192.168.43.210";
            //opt.Username = @"192.168.43.210\autotest";
            //opt.Password = @"1qaz@WSX";
            //ManagementPath mngPath = new ManagementPath(@"\\" + host + @"\root\cimv2:Win32_Process");
            //ManagementScope scope = new ManagementScope(mngPath, opt);
            //scope.Connect();
            //ObjectGetOptions objOption = new ObjectGetOptions();
            

            //ManagementPath path = new ManagementPath("Win32_Process");
            //ManagementClass processClass = new ManagementClass(scope, path, objOption);
            //ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

            //inParams["CommandLine"] = "calc.exe";
            //ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null);
            //Console.WriteLine(outParams.GetText(TextFormat.Mof));

            string remoteDiskPath = @"\\VDST-W7-060\publish";
            SearchRemoteDisk(remoteDiskPath);

            Console.ReadLine();
        }

        public static void SearchRemoteDisk(string path)
        {
            string[] directories = Directory.GetDirectories(path);        
        }
        


    }

}
