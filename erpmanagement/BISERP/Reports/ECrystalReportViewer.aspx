<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ECrystalReportViewer.aspx.cs" Inherits="BISERP.Reports.ECrystalReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript">
        debugger;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        ABC CCC
         <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="100%" Height="700px">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" Height="500px" Width="400px" BorderStyle="Dashed" Visible="True" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
