using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace UATService
{
    public class JsonHelper
    {
        public static string GetJsonString(string url)
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            client.Encoding = Encoding.UTF8;

            return client.DownloadString(url);
        }

        public static void Request(string url)
        {
            RequestByPostWithJson(url, "");
        }
        public static string RequestByPostWithJson(String url, String param)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.KeepAlive = true;
            req.Timeout = 10000;
            req.ContentType = "application/json;charset=utf-8";
            //req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            //req.ContentType = "application/json";

            byte[] postData = Encoding.UTF8.GetBytes(param);
            req.ContentLength = postData.Length;
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(rsp.GetResponseStream());//, Encoding.GetEncoding(rsp.CharacterSet)
            String result = reader.ReadToEnd();

            if (reader != null) reader.Close();
            if (rsp != null) rsp.Close();
            return result;
        }
    }
}
