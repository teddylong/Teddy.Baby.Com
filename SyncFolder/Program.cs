using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace SyncFolder
{
    class Program
    {
        static string localPath = ConfigurationManager.AppSettings["localPath"];
        static string localMachinename = Dns.GetHostName();
        static string toPath = ConfigurationManager.AppSettings["toPath"] + localMachinename;
        static void Main(string[] args)
        {
            CopyAll(localPath, toPath);

            SyncFolder(localPath);
            Console.ReadLine();
        }

        private static void SyncFolder(string folderName)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = folderName;
            watcher.EnableRaisingEvents = true;
            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            Console.WriteLine("FileSystemWatcher ready and listening to changes in :\n\n" + watcher.Path);
        }
        static void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine(e.OldName + " is now: " + e.Name);
            CopyAll(localPath, toPath);
        }
        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name + " has changed");
            CopyAll(localPath, toPath);
        }
        static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name + " file has been deleted");
            CopyAll(localPath, toPath);
        }
        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name + " file has been created.");
            CopyAll(localPath, toPath);
        }

        private static void CopyAll(string sPath, string dPath)
        {
            try
            {
                if (Directory.Exists(dPath))
                {
                    Directory.Delete(dPath, true);
                }

                Directory.CreateDirectory(dPath);
                DirectoryInfo sDir = new DirectoryInfo(sPath);
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    file.CopyTo(dPath + "\\" + file.Name, true);
                }

                DirectoryInfo dDir = new DirectoryInfo(dPath);
                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    CopySubFolder(subDir.FullName, dPath + "//" + subDir.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private static void CopySubFolder(string sPath, string dPath)
        {
            try
            {
                Directory.CreateDirectory(dPath);
                DirectoryInfo sDir = new DirectoryInfo(sPath);
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    file.CopyTo(dPath + "\\" + file.Name, true);
                }

                DirectoryInfo dDir = new DirectoryInfo(dPath);
                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    CopySubFolder(subDir.FullName, dPath + "//" + subDir.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
  
}
