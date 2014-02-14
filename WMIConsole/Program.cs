using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.IO;
using System.Net;

namespace WMIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionOptions connection = new ConnectionOptions();
            connection.Username = @"192.168.43.210\autotest";
            connection.Password = @"1qaz@WSX";

            ManagementScope scope = new ManagementScope("\\\\192.168.43.210\\root\\CIMV2", connection);
            scope.Connect();

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Directory WHERE Name = 'e:\\teddy'");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            foreach (ManagementObject queryObj in searcher.Get())
            {

                Console.WriteLine("Name: {0}", queryObj["Name"]);
                
                
            }
            
            
    

            Console.ReadLine();
        }
    }
}
