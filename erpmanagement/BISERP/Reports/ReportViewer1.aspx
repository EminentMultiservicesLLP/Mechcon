<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer1.aspx.cs" Inherits="BISERP.Views.Shared.ReportViewer1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../aspnet_client/system_web/4_0_30319/crystalreportviewers13/js/crviewer/crv.js"></script>
    <link href="../aspnet_client/system_web/4_0_30319/crystalreportviewers13/js/crviewer/images/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    </div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
    </form>
</body>
</html>
