using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Report.Email
{
    class Program
    {
        // Image address: \\VDST-W7-056\CIFiles\Pictures\
        // Phantomjs driver address: D:\phantomjs-driver\phantomjs.exe
        // JS file address: D:\phantomjs-driver\Scripts\Capture.js
        static void Main(string[] args)
        {

            Hashtable ScreenShotAddress = new Hashtable();
            ScreenShotAddress.Add("ApiCaseView", @"\\VDST-W7-056\CIFiles\Pictures\APICase.png");
            ScreenShotAddress.Add("UICaseView", @"\\VDST-W7-056\CIFiles\Pictures\UICase.png");
            ScreenShotAddress.Add("ApiInterFaceView", @"\\VDST-W7-056\CIFiles\Pictures\ApiInterFace.png");


            Hashtable ht = Email.GetDifferEmail();

            Thread getIMG = new Thread(delegate()
            {
                GetScreenShot();

            });
            getIMG.IsBackground = true;
            getIMG.Start();


            foreach (System.Collections.DictionaryEntry kv in ht)
            {
                string receiver = kv.Key.ToString();
                List<string> bodyImgAddress = new List<string>();
                List<string> bodyImg = (List<string>)kv.Value;

                string sendTime = bodyImg[bodyImg.Count - 1];
                bodyImg.Remove(sendTime);

                foreach (string eachImg in bodyImg)
                {
                    string address = ScreenShotAddress[eachImg].ToString();
                    bodyImgAddress.Add(address);
                }

                if (bodyImgAddress != null)
                {
                    string[] body = getList(bodyImgAddress);
                    Console.WriteLine("准备发送邮件");
                    Thread t = new Thread(delegate()
                    {
                        SM(body, receiver, null, "自动化测试报告订阅", "", sendTime);
                    });
                    t.IsBackground = true;
                    t.Start();

                }
            }
            Console.ReadLine();

        }

        public static void SM(string[] body, string receiver, string something, string textTitle, string nullthing, string sendTime)
        {
            Email.SendEmail(body, receiver, null, "自动化测试报告订阅", "", sendTime);
        }


        public static string[] getList(List<string> list)
        {
            string[] result = list.ToArray();

            return result;
        }

        public static void GetScreenShot()
        {
            while (true)
            {
                string[] linkAddress = { "http://autotest.sh.ctriptravel.com/Pages/Reports/CaseSummary.aspx?type=1", "http://autotest.sh.ctriptravel.com/Pages/Reports/CaseSummary.aspx?type=0", "http://autotest.sh.ctriptravel.com/Pages/Reports/APIRequestSummary.aspx" };
                string[] fileName = { "APICase.png", "UICase.png", "ApiInterFace.png" };
                for (int i = 0; i < linkAddress.Length; i++)
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = @"D:\phantomjs-driver\phantomjs.exe",
                        Arguments = @" D:\phantomjs-driver\Scripts\Capture.js " + linkAddress[i] + " " + fileName[i],
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
                    //Read the Error:
                    string error = p.StandardError.ReadToEnd();
                    //Read the Output:
                    string output = p.StandardOutput.ReadToEnd();
                }
                Console.WriteLine("截图完成! " + "时间： " + DateTime.Now);
                CopyFileToServer();
                Thread.Sleep(3600000);
            }
        }

        public static void CopyFileToServer()
        {
            if (File.Exists("APICase.png"))
            {
                File.Copy("APICase.png", @"\\VDST-W7-056\CIFiles\Pictures\APICase.png", true);
            }
            if (File.Exists("UICase.png"))
            {
                File.Copy("UICase.png", @"\\VDST-W7-056\CIFiles\Pictures\UICase.png", true);
            }
            if (File.Exists("ApiInterFace.png"))
            {
                File.Copy("ApiInterFace.png", @"\\VDST-W7-056\CIFiles\Pictures\ApiInterFace.png", true);
            }

        }
    }
}
