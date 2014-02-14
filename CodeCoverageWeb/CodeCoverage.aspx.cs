using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace CodeCoverageWeb
{
    public partial class CodeCoverage : System.Web.UI.Page
    {

        public static Dictionary<string, string> dic = new Dictionary<string, string>();
        public static string XMLFILE = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ServerNotFound.Visible = false;
            if (!Page.IsPostBack)
            {
                GetReportXmlFile();
            }
            
        }
        private static void getDirectories(string path, TreeNode tn)
        {
            string[] fileNames = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            foreach (string dir in directories)
            {
                TreeNode subtn = new TreeNode();
                subtn.Text = GetShorterFileName(dir);
                subtn.ToolTip = dir;
                //subtn.Expanded = false;
                //subtn.NavigateUrl = "#";
                subtn.SelectAction = TreeNodeSelectAction.Expand;
                //getDirectories(dir, subtn);
                tn.ChildNodes.Add(subtn);
                subtn.ImageUrl = "~/Images/CodeCoverage/dir.png";
                
            }

            foreach (string file in fileNames)
            {
                TreeNode subtn = new TreeNode();
                subtn.Text = GetShorterFileName(file);
                subtn.ToolTip = file;
                if (subtn.Text.EndsWith("dll"))
                {
                    subtn.ImageUrl = "~/Images/CodeCoverage/file.png";
                    subtn.SelectAction = TreeNodeSelectAction.Select;
                    tn.ChildNodes.Add(subtn);
                }
            }
        }
        private static string GetShorterFileName(string filename)
        {
            return filename.Substring(filename.LastIndexOf("\\") + 1);
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            
            if (!TreeView1.SelectedNode.ToolTip.Equals(string.Empty))
            {
                string name = TreeView1.SelectedNode.ToolTip.Substring(TreeView1.SelectedNode.ToolTip.IndexOf("d\\WebSites\\")).Replace("d\\WebSites\\", "");
                CheckedFileName.Items.Add(name);
            }
        }

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            CheckedFileName.Items.Clear();
        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = false;
            string result = "";
            string returnID = "";
            if (CheckedFileName.Items.Count > 0)
            {
                string machineName = serverName.Text;
                string fileName = "";
                foreach (ListItem item in CheckedFileName.Items)
                {
                    fileName = @"D:\WebSites\" + item.Text;
                    result += fileName + ";";
                }
                returnID = SendInsertSQL(machineName, result);
            }
            if (!result.Equals(String.Empty))
            {
                hddata.Value = returnID;
                MessageLabel.Text = "开始统计中...请运行你的相关DLL的黑盒测试用例。当测试结束后点击“停止”按钮。";
            }
        }

        private string SendInsertSQL(string machineName, string fileName)
        {
            string insertSQL = "INSERT INTO [ATDataBase].[dbo].[CodeCoverage] VALUES ('On',getDate()" + ",null,'" + fileName + "','YES','" + machineName + "')";
            string result = SQLHelper.InsertSQL(insertSQL);
            return result;
        }

        protected void StopBtn_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = true;
            if (!hddata.Value.Equals(string.Empty))
            {
                string id = hddata.Value;
                string updateSQL = "UPDATE [ATDataBase].[dbo].[CodeCoverage] SET [Alive] = 'YES',[Status] = 'Off',[EndTime] = getdate() WHERE ID = " + id;
                int result = SQLHelper.ExecSQLCmd(updateSQL);
                if (result != 0)
                {
                    MessageLabel.Text = "已经停止，5秒后生成报告。点击下方的刷新按钮获取。";
                }
            }
        }

        protected void RefreshReportBtn_Click(object sender, EventArgs e)
        {
            GetReportXmlFile();
        }

        private void GetReportXmlFile()
        {
            ReportFileList.Items.Clear();
            string rootPath = ConfigurationManager.AppSettings["ReportAddress"];
            if (pathExists(rootPath))
            {
                string[] fileName = Directory.GetFiles(rootPath, "*.xml");
                for (int i = 0; i < fileName.Length; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = GetShorterFileName(fileName[i]);
                    item.Value = fileName[i];
                    ReportFileList.Items.Add(item);
                }
            }
                
            
        }

        protected void ShowTotalReport_Click(object sender, EventArgs e)
        {
            if (ReportFileList.SelectedItem != null)
            {
                XMLFILE = ReportFileList.SelectedItem.Value;

                Response.Redirect(@"ReportDetail.aspx?ReportPath=" + XMLFILE);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "ShowMessage('No XML File Selected!');", true);
            }
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "displayLoading();", true);
            string rootPath = @"\\" + serverName.Text + @"\d\WebSites";
            CheckedFileName.Items.Clear();
            TreeView1.Nodes.Clear();
            TreeNode tn = new TreeNode();
            tn.Text = serverName.Text.ToUpper();

            
            if (pathExists(rootPath))
            {
                getDirectories(rootPath, tn);
                TreeView1.Nodes.Add(tn);
                tn.ImageUrl = "~/Images/CodeCoverage/disc_drive.png";
                tn.SelectAction = TreeNodeSelectAction.None;
            }
            else
            {
                ServerNotFound.Visible = true;
            }
           
        }

        public bool pathExists(string path)
        {
            bool exists = true;
            Thread t = new Thread(new ThreadStart(delegate()
                {
                    exists = Directory.Exists(path);
                })
                );
            t.Start();
            bool completed = t.Join(3000);
            if (!completed)
            {
                exists = false;
                t.Abort();
            }
            return exists;
        }

        protected void TreeView1_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            string name = e.Node.ToolTip;
            if (name != "" && e.Node.ChildNodes.Count == 0)
            {

                getDirectories(name, e.Node);
                e.Node.Expand();
            }

            
        }
    }

}