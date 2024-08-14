<%@ Page  Async="true" Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="BISERP.Views.Shared.ReportViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms,Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" SizeToReportContent="True" Width="99%" 
            BackColor="White" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        </rsweb:ReportViewer>   
    </div>
    </form>
</body>
</html>
