using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace EmailServer
{
    public class Mail
    {
        private static string GetClinetIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
        /// <summary>
        /// 邮件发送,调用者需要向技术运行申请相关权限。
        /// </summary>
        /// <param name="serverHost">指定SMTP服务名 例如携程内网smtp:appmail.sh.ctriptravel.com</param>
        /// <param name="port">端口号 如果不需要传入参数为000</param>
        /// <param name="fromMailAddress">发件人邮箱地址 如：lvxl@ctrip.com 或者自定义名字：AutoTest@ctrip.com</param>
        /// <param name="fromMailPwd">发件人密码(当前是通过公司内网发送，任意字符即可)</param>
        /// <param name="toMailAddressList">收件人地址列表 可以进行群发邮件</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="mailContent">邮件内容</param>
        /// <param name="pathList">附件列表集合 可以添加多个附件;也可以不添加附件</param>
        public static string SendToEmail(string serverHost, int port, List<string> toMailAddressList, string mailTitle, string mailContent, List<string> pathList)
        {
            //mailTitle = mailTitle + string.Format("(From {0})", GetClinetIP());
            mailContent = "<span style='font-family:\"微软雅黑\";font-size:10.0pt;''>" + mailContent + "</span>";
            //检测附件是否存在以及附件的大小
            FileStream FileStream_my = null;
            if (pathList.Count > 0)
            {
                for (int i = 0; i < pathList.Count; i++)
                {
                    try
                    {
                        if (!File.Exists(pathList[i]))
                        {
                            return "File not exist... \n " + pathList[i];
                        }
                        else
                        {
                            FileStream_my = new FileStream(pathList[i], FileMode.Open);//附件文件流
                            string name = FileStream_my.Name;
                            long fileSize = FileStream_my.Length;
                            int size = (int)(fileSize / 1024 / 1024);
                            FileStream_my.Close();
                            FileStream_my.Dispose();
                            //控制文件大小不大于5Ｍ                
                            if (size > 5)
                            {
                                return ("文件长度不能大于5M！你选择的文件大小为{0}M");
                            }
                        }
                    }
                    catch (IOException Ex)
                    {
                        return Ex.Message;
                    }
                }
            }
            MailAddress MailAddress_from = null; //设置发信人地址  
            //邮件发送验证
            SmtpClient smtpCilent = new SmtpClient();//设置SMTP协议
            try
            {
                //指定SMTP服务名  
                //例如QQ邮箱为smtp.qq.com新浪cn邮箱为 smtp.sina.cn等
                smtpCilent.Host = serverHost;
                if (port != 000)
                {
                    smtpCilent.Port = port;//指定端口号
                }
                //smtpCilent.Timeout = 0; //超时时间
            }
            catch (Exception Ex)
            {
                return ("邮件发送失败,请确定SMTP服务名是否正确 \n" + Ex.ToString());
            }
            try
            {
                //验证发件人
                //创建发件人的服务器认证
                NetworkCredential networkCredential_My = new NetworkCredential("appmail051", "b9{jkJ8^A\"iJ)B^wxem!");
                //实例化发件人地址     
                MailAddress_from = new System.Net.Mail.MailAddress("appmail051@ctrip.com", "AutoTest");
                //指定发件人信息  包括邮箱地址和邮箱密码            
                smtpCilent.Credentials = new System.Net.NetworkCredential("appmail051", "b9{jkJ8^A\"iJ)B^wxem!");
            }
            catch (Exception Ex)
            {
                return ("邮件发送失败,请确定发件邮箱地址和密码的正确性！\n " + Ex.Message);
            }
            MailMessage MailMessage_Mai = new MailMessage();
            //清空历史发送信息，以防发送时收件人收到的错误信息(收件人列表会不断重复)
            MailMessage_Mai.To.Clear();
            ////添加收件人邮箱地址 ,可以添加多个收件人地址
            for (int i = 0; i < toMailAddressList.Count; i++)
            {
                // //设置收信人地址  不需要密码 
                try
                {
                    MailMessage_Mai.To.Add(new MailAddress(toMailAddressList[i].ToString()));
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            MailMessage_Mai.From = MailAddress_from;//发件人邮箱 
            //邮件主题                
            MailMessage_Mai.Subject = mailTitle;//邮件标题
            MailMessage_Mai.SubjectEncoding = System.Text.Encoding.UTF8;//文本格式
            MailMessage_Mai.Body = mailContent;//邮件正文  
            MailMessage_Mai.IsBodyHtml = true;
            MailMessage_Mai.BodyEncoding = System.Text.Encoding.UTF8;//文本格式
            MailMessage_Mai.IsBodyHtml = true;
            //清空历史附件  以防附件重复发送               
            MailMessage_Mai.Attachments.Clear();
            if (pathList.Count > 0)
            {
                for (int i = 0; i < pathList.Count; i++)
                {
                    try
                    {
                        //构造一个附件对象
                        Attachment attach = new Attachment(pathList[i]);
                        //得到文件的信息
                        //System.Net.Mime.ContentDisposition disposition = attach.ContentDisposition;
                        //disposition.CreationDate = System.IO.File.GetCreationTime(pathList[i]);
                        //disposition.ModificationDate = System.IO.File.GetLastWriteTime(pathList[i]);
                        //disposition.ReadDate = System.IO.File.GetLastAccessTime(pathList[i]);
                        //向邮件添加附件
                        MailMessage_Mai.Attachments.Add(attach);
                    }
                    catch (IOException Ex)
                    {
                        return Ex.Message;
                    }
                }
            }
            //注册邮件发送完毕后的处理事件，必须先注册SendCompletedEventHandler事件  
            try
            {
                //smtpCilent.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                //smtpCilent.SendAsync(MailMessage_Mai, "000000000");
                smtpCilent.Send(MailMessage_Mai);
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
            finally
            {
                foreach (Attachment item in MailMessage_Mai.Attachments)
                {
                    item.Dispose();   //一定要释放该对象,否则无法删除附件
                }
            }
            return "OK";
        }


        public static string SendHTMLEmail(string serverHost, int port, List<string> toMailAddressList, string mailTitle, string mailContent)
        {     
            MailAddress MailAddress_from = null;  
            SmtpClient smtpCilent = new SmtpClient();
            try
            { 
                smtpCilent.Host = serverHost;
                if (port != 000)
                {
                    smtpCilent.Port = port;
                } 
            }
            catch (Exception Ex)
            {
                return ("邮件发送失败,请确定SMTP服务名是否正确 \n" + Ex.ToString());
            }
            try
            {
                NetworkCredential networkCredential_My = new NetworkCredential("appmail051", "b9{jkJ8^A\"iJ)B^wxem!"); 
                MailAddress_from = new System.Net.Mail.MailAddress("appmail051@ctrip.com", "AutoTest");         
                smtpCilent.Credentials = new System.Net.NetworkCredential("appmail051", "b9{jkJ8^A\"iJ)B^wxem!");
            }
            catch (Exception Ex)
            {
                return ("邮件发送失败,请确定发件邮箱地址和密码的正确性！\n " + Ex.Message);
            }
            MailMessage MailMessage_Mai = new MailMessage();
            MailMessage_Mai.To.Clear();
            for (int i = 0; i < toMailAddressList.Count; i++)
            {
                try
                {
                    MailMessage_Mai.To.Add(new MailAddress(toMailAddressList[i].ToString()));
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            MailMessage_Mai.From = MailAddress_from;              
            MailMessage_Mai.Subject = mailTitle;
            MailMessage_Mai.SubjectEncoding = System.Text.Encoding.UTF8;
            MailMessage_Mai.Body = mailContent;
            MailMessage_Mai.IsBodyHtml = true;
            MailMessage_Mai.BodyEncoding = System.Text.Encoding.UTF8;
            MailMessage_Mai.IsBodyHtml = true;
             
            MailMessage_Mai.Attachments.Clear();
           
            try
            {
                smtpCilent.Send(MailMessage_Mai);
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
            finally
            {
                foreach (Attachment item in MailMessage_Mai.Attachments)
                {
                    item.Dispose();
                }
            }
            return "OK";
        }
    }
}