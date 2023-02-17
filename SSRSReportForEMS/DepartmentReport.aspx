<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentReport.aspx.cs" Inherits="EMSSSRS.DepartmentReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="RDLCDepartment" runat="server" Height="695px" Width="1012px">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
