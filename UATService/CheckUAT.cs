using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Net;
using System.IO;

namespace UATService
{
    public class CheckUAT
    {
        // Main method
        public static bool CheckESB()
        {
            try
            {
                ServiceESB.ESBSoapClient client = new ServiceESB.ESBSoapClient();
                string requestXml = "<?xml version=\"1.0\"?><Request><Header UserID=\"SOA.WSUser\" RequestType=\"SOA.ESB.GetReferenceID\" /></Request>";
                string result = client.Request(requestXml);
                if (result.Contains("Success"))
                { 
                    return true;
                }
                else
                {   
                    return false;
                }
            }
            catch (Exception e)
            {
                
                return false;
            }
        }
        public static bool CheckABTesting()
        {
            string ABTestingUrl = "http://192.168.83.88:8080/ab/experiments";

            try
            {
                string resultABTesting = JsonHelper.GetJsonString(ABTestingUrl);

                Newtonsoft.Json.Linq.JArray arr = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(resultABTesting);
                if (arr.Count > 1)
                {
                    
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception e)
            {
                
                return false;
            }


        }
        public static bool CheckDashBoard()
        {
            try
            {
                string DashBoardUrl = @"http://192.168.82.16:8080/meta/namespaces/ns-null/metrics/esb.request.count/tagNames/hostip/tagValues";
                string resultDashBoard = JsonHelper.GetJsonString(DashBoardUrl);
                JObject o = (JObject)JsonConvert.DeserializeObject(resultDashBoard);
                if (o["tagValues"].HasValues)
                {
                    // DashBorad is OK.
                    return true;
                }
                else
                {  
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public static bool CheckSearchEngine()
        {
            string SearchEngineUrl = @"http://192.168.82.221:90/vacation/search?noLogging=true";
            string parmData = "<?xml version=\"1.0\"?><Request><query occur=\"should\" searchType=\"1\"><query occur=\"must\"><query occur=\"must\"><query occur=\"should\"><FirstStartCity>2</FirstStartCity></query><query occur=\"should\"><OtherStartCity>2</OtherStartCity>        </query>         </query> </query> </query> <sort> <price>desc</price> </sort> <stat> <SearchTabType />           <TagID /> <DestinationNavID /> <District /> <ProductLineID /> <TravelDays /> <ProductLevel /> <TransportationID /> <ProductCategoryID /> <ProductPatternID /><DestinationCityID /><VisaType /><VisaAddress /></stat><recommend>5</recommend><group>4</group> <return><ProductID/><ImageUrl/><CharacteristicDesc/><Comment/><ManagerRecommend/><SaleMode/>                   <TagID/>                   <ProductType/>       <ScenicSpotNames/>       <VisaAddressName/>              <PromoteSales/>          <VisaInterview/>     <VisaAddress/>        <Announcement/> <AdvanceBookingDays/>         <SearchTabTypeShow/> <ImageDesc/>          <Price/>   <Schedule/>     <Festival/>                <CustomTagName/>       <PackageID/> <PackageManagerRecommend/>          <PackageName/>    <PackagePromoteSales/>        <PackageSpecialDesc/> </return> <fromTo>1-2</fromTo> </Request>";
            try
            {
                string resultSearchEngine = PostWebRequest(SearchEngineUrl, parmData, Encoding.UTF8);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(resultSearchEngine);
                string value = doc.SelectSingleNode("//totalForPage").InnerText;
                if (Int32.Parse(value) > 0)
                {
                    //SearchEngine is OK.
                   
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception e)
            {
                
                return false;
            }
        }
        public static bool CheckMarkLand()
        {
            string MarkLandUrl = @"http://192.168.82.221:8070/markland/search?requestFormat=url&responseFormat=json&query0=Keyword:%E9%9D%92%E7%BE%8A%E5%8C%BA&return0=ID,word,type,CityName,provinceId,provinceName&section0=1-5&needTokens0=true";
            try
            {
                string resultMarkLand = JsonHelper.GetJsonString(MarkLandUrl);
                Newtonsoft.Json.Linq.JArray arr = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(resultMarkLand);
                if (arr.Count > 0)
                {
                    // MarkLand is OK.
                    
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception e)
            {
               
                return false;
            }

        }
        public static bool CheckScenicspot()
        {
            string ScenicspotUrl = @"http://192.168.82.15:8050/scenicspot/search?requestFormat=url&responseFormat=json&stat=ProvinceID&sort=Star&query=ScenicSpotName:%E4%BA%BA%E6%B0%91&section=1-10&return=scenicSpotId,scenicSpotName";
            try
            {
                string resultScenicspot = JsonHelper.GetJsonString(ScenicspotUrl);
                JObject objectScenicspot = (JObject)JsonConvert.DeserializeObject(resultScenicspot);
                if (Int32.Parse(objectScenicspot["total"].ToString()) > 0)
                {
                    // Scenicspot is OK.
                   
                    return true;
                }
                else
                {
                   
                    return false;
                }
            }
            catch (Exception e)
            {
                
                return false;
            }
        }
        public static bool CheckMemcached()
        {
            try
            {
                ServiceMemcached.ClientServiceSoapClient client = new ServiceMemcached.ClientServiceSoapClient();
                string requestXML = "<?xml version=\"1.0\"?><Request><Header UserID=\"900904\" RequestType=\"Arch.Cache.Service.GetClientConfigurationRequest\" AsyncRequest=\"false\" /><Client><ClusterName>Test</ClusterName><IP>127.0.0.1</IP><DomainBaseDictionary>D:</DomainBaseDictionary></Client></Request>";
                string resultMemcached = client.Request(requestXML);
                XmlDocument docMemcachedService = new XmlDocument();
                docMemcachedService.LoadXml(resultMemcached);
                if (docMemcachedService.SelectSingleNode("//Header").Attributes["ResultCode"].Value == "Success")
                {
                    // Memcached is OK.
                    
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception e)
            {
                
                return false;
            }
        }
        public static bool CheckCentralLogging()
        {
            try
            {
                CLoggingService.MonitorSoapClient client = new CLoggingService.MonitorSoapClient();
                CLoggingService.JobMsgEntity entity = new CLoggingService.JobMsgEntity();
                entity.JobClassFullName = "CLoggingAppMonitor.JobEntities.LogWriterMonitor,CLoggingAppMonitor";
                entity.Action = "getstatus";
                entity.JobInfo = "1356";
                CLoggingService.JobMsgEntity entityResult = client.Request(entity);
                if (entityResult.JobResult == CLoggingService.JobResult.Success)
                {
                    // CentralLogging is OK.
                    //CentralLoggingResultText.Value = entityResult.Action + "*****" + entityResult.HostIP + "*****" + entityResult.JobClassFullName + "*****" + entityResult.JobInfo + "*****" + entityResult.JobResult + "*****" + entityResult.JobStatus;
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception e)
            {
                
                return false;
            }
        }
        // Support method
        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return ret;
        }
    }
}
