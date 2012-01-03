<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateComboPicker.ascx.cs" 
Inherits="GestPro.controls.DateComboPicker" %>
<table width="100px" border="0">
    <tr>
        <td>
            <asp:DropDownList runat="server" ID="ddlDay" Width="40px" />
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlMonth" Width="40px" />
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlYear" Width="60px" />
        </td>
    </tr>
</table>
