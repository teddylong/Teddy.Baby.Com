<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeCoverage.aspx.cs" Inherits="CodeCoverageWeb.CodeCoverage" SmartNavigation="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Code Coverage Tool</title>
    <link rel="stylesheet" href="../Style/CodeCoverage.css" />
    <link rel="stylesheet" href="http://svn.ui.sh.ctripcorp.com/istyle/code/istyle.30626.css" />
    
    <script type="text/javascript" language="javascript" src="http://svn.ui.sh.ctripcorp.com/libs/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="http://svn.ui.sh.ctripcorp.com/istyle/code/istyle.30626.js"></script>
    <script type="text/javascript" language="javascript" src="JS/JavaScript.js"></script>
</head>
<body>
    <form id="form2" runat="server" >
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">    
              </asp:ScriptManager>
        </div>
        <div class="navbar">
            <div class="navbar-inner">
                <div style="color:white;font-size:24px;width:500px;margin-left:auto;margin-right:auto;margin-top:13px;">黑盒代码覆盖率统计工具<span class="preview">Preview 1.0</span></div>
                <div style="position:absolute;right:40px;top:20px; color:white;">
                    <a  style="color:white;" target="_blank" href="Help.html">Help</a>
                </div>
            </div>  
        </div>
        <div>
            <asp:UpdatePanel ID="uid"  runat="server">
                <ContentTemplate>
                <div style="margin-left: 50px; margin-top:20px;">
                    <div id="ShowFolderDiv" style="width: 420px; overflow: auto; height: 750px;">
                        <div style="margin: 10px;">
                            <img alt="Number One" src="../Images/CodeCoverage/Numbers1.png" height="42" width="42" />
                            <span style="font-size: 14px; color: #3366FF; font-weight: bold;">输入机器名，获取DLL文件: （例如, FAT46-WEB1）</span>
                            <asp:TextBox ID="serverName" runat="server" CssClass="TextBoxStyle" />
                            <asp:Button runat="server" ID="GetBtn" CssClass="btn btn-info" Text="获取" OnClick="Unnamed1_Click" /> 
                            <div style="margin-left: 120px; padding-bottom:10px;"> 
                                <div class="preload" style="visibility:hidden"><img alt="Loading" src="Images/CodeCoverage/Loading.gif" height="20" width="20" /></div> 
                            </div>
                            <asp:Label runat="server" ID="ServerNotFound" CssClass="ServerNotFound">服务器没有找到!</asp:Label>
                        </div>
                        <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer"
                            NodeIndent="15" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" OnTreeNodeExpanded="TreeView1_TreeNodeExpanded" >
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black"
                                HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                            
                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False"
                                HorizontalPadding="0px" VerticalPadding="0px" />
                        </asp:TreeView>
                    </div>
                   
                </div>   
                             
                <div id="ControlPanel" style="width: 600px;margin-top:-750px; margin-left:480px; height: auto;">
                     <div>
                        <table height="800" style="border-color:black;border-left-style:dashed;border-width:2px"><tr><td></td></tr></table>
                    </div>   
                    <div style="margin-top:-800px;margin-left:20px;">
                        <img style="margin-left: 10px; margin-top: 10px;" alt="Number Two" src="../Images/CodeCoverage/Numbers2.png" height="42" width="42" />
                        <div style="margin-top: -23px; margin-left: 60px;">
                            <label style="font-size: 14px; color: #3366FF; font-weight: bold;">启动 & 停止:</label>
                        </div>
                        <p style="margin-left: 13px;">
                            <asp:ListBox ID="CheckedFileName" runat="server" Width="550px" Height="100px"></asp:ListBox>
                        </p>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="margin-left: 513px;">
                <asp:Button ID="ResetBtn" CssClass="btn btn-primary" runat="server"
                    Text="重置" OnClick="ResetBtn_Click" />
                <asp:Button ID="StartBtn" CssClass="btn btn-primary" runat="server"
                    Text="开始" OnClick="StartBtn_Click" />
                <asp:Button ID="StopBtn" CssClass="btn btn-primary" runat="server" Text="停止"
                    OnClick="StopBtn_Click" />
            </div>
            <div style="margin-left: 510px; margin-top: 10px;">
                <asp:Label ID="MessageLabel" Style="font: menu;" runat="server" Height="20px" Width="500px"></asp:Label>
            </div>
            <div style="margin-left:500px;margin-top:10px;">
                <div>
                    <img style="margin-left: 10px; margin-top: 15px;" alt="Number Three" src="../Images/CodeCoverage/Numbers3.png" height="42" width="42" />
                    <div style="margin-top: -30px; margin-left: 60px;">
                        <label style="font-size: 14px; color: #3366FF; font-weight: bold; margin-top:5px;">选取报表文件:</label>  
                    </div>
                     <p style="margin-left: 13px;">
                        <asp:Listbox ID="ReportFileList" runat="server" Height="100px" Width="550px"></asp:Listbox>
                    </p>
                     <div style="margin-left: 13px;">
                        <asp:Button ID="RefreshReportBtn" CssClass="btn btn-primary" runat="server" Text="刷新" OnClick="RefreshReportBtn_Click"  />
                        <asp:Button ID="ShowTotalReport" CssClass="btn btn-primary" runat="server" Text="显示报表" OnClick="ShowTotalReport_Click" />
                    </div>
                </div>
            </div>
            
        </div>
           
            
        <asp:HiddenField ID="hddata" runat="server" />
       
        
    </form>
</body>
</html>
