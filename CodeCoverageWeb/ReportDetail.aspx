<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportDetail.aspx.cs" Inherits="CodeCoverageWeb.ReportDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Code Coverage Report</title>
    <link rel="stylesheet" href="../Style/CodeCoverage.css" />
    <link rel="stylesheet" href="http://svn.ui.sh.ctripcorp.com/istyle/code/istyle.30626.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <div class="navbar-inner">
                <div style="color:white;font-size:24px;width:500px;margin-left:auto;margin-right:auto;margin-top:13px;">黑盒代码覆盖率统计报告<span class="preview">Preview 1.0</span></div>
                <div style="position:absolute;right:40px;top:20px; color:white;">
                    <a  style="color:white;" target="_blank" href="Help.html">Help</a>
                </div>
            </div>  
        </div>
        <div>
            <div id="ReportDiv" style="margin: 10px; margin-left: -50px; margin-top: 20px;">
                        <asp:GridView ID="ReportGridView" runat="server" BackColor="White" 
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Names="Microsoft YaHei"
                            Font-Size="Medium" Font-Strikeout="False" Font-Underline="False"
                            HorizontalAlign="Center" AutoGenerateColumns="False" 
                            onrowdatabound="ReportData_RowDataBound" >      
                            <Columns>
                                <asp:TemplateField HeaderText="DLL Name">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="hpLink" runat="server" 
                                    Font-Bold="True" Font-Underline="True" ForeColor="#3366FF" 
                                             Text='<%# bind("ModuleName") %>' ></asp:HyperLink>
                                             <asp:Label ID="hlable" runat="server" Text = '<%# bind("ModuleName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:BoundField  DataField="LinesCovered" HeaderText="LinesCovered" >
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                          
                                <asp:BoundField  DataField="LinesNotCovered" HeaderText="LinesNotCovered">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Lines%">
                                    <ItemTemplate>
                                         <asp:Label ID="Lines100Label" runat="server" Font-Bold="True" ForeColor="#3366FF"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BlocksCovered" HeaderText="BlocksCovered">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField  DataField="BlocksNotCovered" HeaderText="BlocksNotCovered">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Blocks%">
                                    <ItemTemplate>
                                         <asp:Label ID="Blocks100Label" runat="server" Font-Bold="True" ForeColor="#3366FF"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                        
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
            <div id="ClassDiv" style="margin: 10px; margin-left: -50px; margin-top: 20px;">
                <asp:GridView ID="ClassGridView" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        Font-Bold="False" Font-Italic="False" Font-Names="Microsoft YaHei" Font-Overline="False"
                        Font-Size="Medium" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" AutoGenerateColumns="False" 
                        onrowdatabound="ClassData_RowDataBound" >      
                        <Columns>
                            <asp:TemplateField HeaderText="Class Name">
                                <ItemTemplate>
                                        <asp:HyperLink ID="hpLink" runat="server" 
                                Font-Bold="True" Font-Underline="True" ForeColor="#3366FF" 
                                            Text='<%# bind("ClassName") %>' ></asp:HyperLink>
                                            <asp:Label ID="hlable" runat="server" Text = '<%# bind("ClassKeyName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField  DataField="LinesCovered" HeaderText="LinesCovered" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField  DataField="LinesNotCovered" HeaderText="LinesNotCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Lines%">
                                <ItemTemplate>
                                        <asp:Label ID="Lines100Class" runat="server" Font-Bold="True" ForeColor="#3366FF"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField  DataField="BlocksCovered" HeaderText="BlocksCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField  DataField="BlocksNotCovered" HeaderText="BlocksNotCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Blocks%">
                                <ItemTemplate>
                                        <asp:Label ID="Blocks100Class" runat="server" Font-Bold="True" ForeColor="#3366FF"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
            </div>
            <div id="NameSpaceDiv" style="margin: 10px; margin-left: -50px; margin-top: 20px;">
                    <asp:GridView ID="NameSpaceGridView" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        Font-Bold="False" Font-Italic="False" Font-Names="Microsoft YaHei" Font-Overline="False"
                        Font-Size="Medium" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" AutoGenerateColumns="False" OnRowDataBound="NameSpaceData_RowDataBound" >      
                        <Columns>
                             <asp:TemplateField HeaderText="NameSpaceKeyName">
                                <ItemTemplate>
                                        <asp:HyperLink ID="hpLink" runat="server" 
                                Font-Bold="True" Font-Underline="True" ForeColor="#3366FF" 
                                            Text='<%# bind("NameSpaceKeyName") %>' ></asp:HyperLink>
                                            <asp:Label ID="hlable" runat="server" Text = '<%# bind("NameSpaceKeyName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField  DataField="LinesCovered" HeaderText="LinesCovered" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField  DataField="LinesNotCovered" HeaderText="LinesNotCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Lines%">
                                <ItemTemplate>
                                        <asp:Label ID="Lines100Method" runat="server" Font-Bold="True" ForeColor="#3366FF"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField  DataField="BlocksCovered" HeaderText="BlocksCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField  DataField="BlocksNotCovered" HeaderText="BlocksNotCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Blocks%">
                                <ItemTemplate>
                                        <asp:Label ID="Blocks100Method" runat="server" Font-Bold="True" ForeColor="#3366FF"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </div>

            <div id="MethodDiv" style="margin: 10px; margin-left: -50px; margin-top: 20px;">
                    <asp:GridView ID="MethodGridView" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        Font-Bold="False" Font-Italic="False" Font-Names="Microsoft YaHei" Font-Overline="False"
                        Font-Size="Medium" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" AutoGenerateColumns="False" OnRowDataBound="MethodData_RowDataBound"  >      
                        <Columns> 
                            <asp:BoundField  DataField="MethodName" HeaderText="MethodName" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                         
                            <asp:BoundField  DataField="LinesCovered" HeaderText="LinesCovered" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField  DataField="LinesNotCovered" HeaderText="LinesNotCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Lines%">
                                <ItemTemplate>
                                        <asp:Label ID="Lines100Method" runat="server" Font-Bold="True" ForeColor="#3366FF"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField  DataField="BlocksCovered" HeaderText="BlocksCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField  DataField="BlocksNotCovered" HeaderText="BlocksNotCovered">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Blocks%">
                                <ItemTemplate>
                                        <asp:Label ID="Blocks100Method" runat="server" Font-Bold="True" ForeColor="#3366FF"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </div>
        </div>
        <div class="CenterPart">
            <asp:Label ID="NoReportLabel" runat="server" Text ="没有执行相关DLL的测试，没有生成内容！"></asp:Label>
            <asp:Button runat="server" ID="BackBtn" Text="返回" CssClass="btn btn-success" OnClick="BackBtn_Click" />
        </div>
    </form>
</body>
</html>
