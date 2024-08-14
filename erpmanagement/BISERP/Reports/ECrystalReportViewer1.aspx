<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ECrystalReportViewer.aspx.cs" Inherits="ECrystalReportViewer" %>
<%--<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>--%>
<%@ Register tagPrefix="cr1" namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crystal Report Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="100%" Height="700px">
                <cr1:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                <cr1:CrystalReportViewer ID="CrystalReportViewer2" runat="server" AutoDataBind="true" />
            </asp:Panel>
    </div>
    </form>
</body>
</html>
