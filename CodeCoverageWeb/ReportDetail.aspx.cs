using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeCoverageWeb
{
    public partial class ReportDetail : System.Web.UI.Page
    {
        public static string XMLFILE = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
            ViewState["ReportPath"] = Request.QueryString["ReportPath"];
            ViewState["ID"] = Request.QueryString["ID"];
            ViewState["NameSpaceID"] = Request.QueryString["NameSpaceID"];
            ViewState["ClassID"] = Request.QueryString["ClassID"];

            if (ViewState["ID"] != null)
            {
                var dd = ViewState["ID"].ToString();
                ShowNameSpaceReport(dd);
            }
            if (ViewState["NameSpaceID"] != null)
            {
                var dd = ViewState["NameSpaceID"].ToString();
                ShowCLassReport(dd);
            }
            if (ViewState["ClassID"] != null)
            {
                var dd = ViewState["ClassID"].ToString();
                ShowMethodReport(dd);
            }
            if (ViewState["ClassID"] == null && ViewState["ID"] == null && ViewState["NameSpaceID"] == null)
            {
                ShowTotalReport();
            }
        }
        protected void ShowMethodReport(string id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet myDataSet = new DataSet();
                myDataSet.ReadXml(XMLFILE);
                string query = string.Format("ClassKeyName='{0}'", id);
                DataTable dtv = myDataSet.Tables["Method"];
                DataRow[] result = myDataSet.Tables["Method"].Select(query);
                DataTable xxx = ToDataTable(result);
                GridView1.Visible = false;
                GridView2.Visible = false;
                NoReportLabel.Visible = false;

                MethodGridView.DataSource = xxx;
                MethodGridView.DataBind();

            }
            catch (Exception ex)
            {

            }

        }

        protected void ShowNameSpaceReport(string id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet myDataSet = new DataSet();
                myDataSet.ReadXml(XMLFILE);
                string query = string.Format("ModuleName='{0}'", id);
                DataTable dtv = myDataSet.Tables["NameSpaceTable"];
                DataRow[] result = myDataSet.Tables["NameSpaceTable"].Select(query);
                DataTable xxx = ToDataTable(result);
                GridView1.Visible = false;
                NoReportLabel.Visible = false;
                GridView2.DataSource = xxx;
                GridView2.DataBind();

            }
            catch (Exception ex)
            {

            }

        }


        protected void ShowTotalReport()
        {
            ReportData.Visible = true;
            GridView1.Visible = false;
            GridView2.Visible = false;
            ViewState["ID"] = null;
            ViewState["ClassID"] = null;
            XMLFILE = "";
            if (ViewState["ReportPath"] != null)
            {
                XMLFILE = ViewState["ReportPath"].ToString();

                try
                {
                    DataTable dt = new DataTable();
                    DataSet myDataSet = new DataSet();
                    myDataSet.ReadXml(XMLFILE);

                    DataTable total = myDataSet.Tables["Module"];
                    if (total.Columns["ModuleName"].Table.Rows.Count == 0)
                    {
                        NoReportLabel.Visible = true;
                    }
                    else
                    {
                        NoReportLabel.Visible = false;
                        ReportData.DataSource = total;
                        ReportData.DataBind();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "ShowMessage('No XML File Selected!');", true);
            }
        }

        protected void ShowCLassReport(string id)
        {

            try
            {
                DataTable dt = new DataTable();
                DataSet myDataSet = new DataSet();
                myDataSet.ReadXml(XMLFILE);
                string query = string.Format("NameSpaceKeyName='{0}'", id);
                DataTable dtv = myDataSet.Tables["Class"];
                DataRow[] result = myDataSet.Tables["Class"].Select(query);
                DataTable xxx = ToDataTable(result);
                NoReportLabel.Visible = false;
                ReportData.Visible = false;


                GridView1.DataSource = xxx;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }

        }
        public DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);
            return tmp;
        }
        protected void hpLink_Click(object sender, EventArgs e)
        {
            string ddd = ViewState["text"].ToString();
        }

        protected void ReportData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Block100 = (Label)e.Row.FindControl("Blocks100Label");
                var d1 = e.Row.Cells[4].Text;
                var d2=e.Row.Cells[5].Text;

                Block100.Text = (float.Parse(d1.ToString()) / (float.Parse(d1) + float.Parse(d2)) * 100).ToString("0.00") + "%";

                Label Line100 = (Label)e.Row.FindControl("Lines100Label");
                var d3 = e.Row.Cells[1].Text;
                var d4 = e.Row.Cells[2].Text;

                Line100.Text = (float.Parse(d3.ToString()) / (float.Parse(d3) + float.Parse(d4)) * 100).ToString("0.00") + "%"; 


                HyperLink hKLink = (HyperLink)e.Row.FindControl("hpLink");
                Label lID = (Label)e.Row.FindControl("hlable");
                string lName = hKLink.Text;

                hKLink.Attributes["href"] = @"ReportDetail.aspx?ID=" + lID.Text;
                lID.Visible = false;
                
            }

        }

        protected void ClassData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hKLink = (HyperLink)e.Row.FindControl("hpLink");
                Label lID = (Label)e.Row.FindControl("hlable");
                string lName = hKLink.Text;

                Label Block100 = (Label)e.Row.FindControl("Blocks100Class");
                var d1 = e.Row.Cells[4].Text;
                var d2 = e.Row.Cells[5].Text;

                string BlockResult = (float.Parse(d1.ToString()) / (float.Parse(d1) + float.Parse(d2)) * 100).ToString("0.00") + "%";
                if (BlockResult == "0.00%")
                {
                    Block100.Text = "---";
                    Block100.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    Block100.Text = BlockResult;
                }

                Label Line100 = (Label)e.Row.FindControl("Lines100Class");
                var d3 = e.Row.Cells[1].Text;
                var d4 = e.Row.Cells[2].Text;

                string LineResult = (float.Parse(d3.ToString()) / (float.Parse(d3) + float.Parse(d4)) * 100).ToString("0.00") + "%";
                if (LineResult == "0.00%")
                {
                    Line100.Text = "---";
                    Line100.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    Line100.Text = LineResult;
                }

                hKLink.Attributes["href"] = @"ReportDetail.aspx?ClassID=" + lID.Text;
                lID.Visible = false;
            }

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hKLink = (HyperLink)e.Row.FindControl("hpLink");
                Label lID = (Label)e.Row.FindControl("hlable");

                Label Block100 = (Label)e.Row.FindControl("Blocks100Method");
                var d1 = e.Row.Cells[4].Text;
                var d2 = e.Row.Cells[5].Text;

                string BlockResult = (float.Parse(d1.ToString()) / (float.Parse(d1) + float.Parse(d2)) * 100).ToString("0.00") + "%";
                if (BlockResult == "0.00%")
                {
                    Block100.Text = "---";
                    Block100.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    Block100.Text = BlockResult;
                }

                Label Line100 = (Label)e.Row.FindControl("Lines100Method");
                var d3 = e.Row.Cells[1].Text;
                var d4 = e.Row.Cells[2].Text;

                string LineResult = (float.Parse(d3.ToString()) / (float.Parse(d3) + float.Parse(d4)) * 100).ToString("0.00") + "%";
                if (LineResult == "0.00%")
                {
                    Line100.Text = "---";
                    Line100.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    Line100.Text = LineResult;
                }
                hKLink.Attributes["href"] = @"ReportDetail.aspx?NameSpaceID=" + lID.Text;
                lID.Visible = false;
                
            }
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }

        protected void MethodGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                Label Block100 = (Label)e.Row.FindControl("Blocks100Method");
                var d1 = e.Row.Cells[4].Text;
                var d2 = e.Row.Cells[5].Text;

                string BlockResult = (float.Parse(d1.ToString()) / (float.Parse(d1) + float.Parse(d2)) * 100).ToString("0.00") + "%";
                if (BlockResult == "0.00%")
                {
                    Block100.Text = "---";
                    Block100.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    Block100.Text = BlockResult;
                }

                Label Line100 = (Label)e.Row.FindControl("Lines100Method");
                var d3 = e.Row.Cells[1].Text;
                var d4 = e.Row.Cells[2].Text;

                string LineResult = (float.Parse(d3.ToString()) / (float.Parse(d3) + float.Parse(d4)) * 100).ToString("0.00") + "%";
                if (LineResult == "0.00%")
                {
                    Line100.Text = "---";
                    Line100.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    Line100.Text = LineResult;
                }

            }
        }
    }
}