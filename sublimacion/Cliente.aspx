<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="sublimacion.Cliente" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div style="margin-right: 0px">
<br/>
<br/>         
<asp:Label ID="LblMensaje" ForeColor="Red" runat="server"></asp:Label>
<br/> 
<center>
<asp:Panel runat="server" ID="panel1" Visible="true"  Width="600px" style="background-color:#DDDDDD">

<table style="width:500px;"  border="0" cellspacing="0">
 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label6" runat="server" Text="Nombre:"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtNombre" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtNombre" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>
<%--agregar items para dar de alta--%>

<tr>
    <td align="center" colspan="2">
        <br/>
        <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/Save.png" runat="server" ValidationGroup="add" onclick="BtnGuardar_Click" ToolTip="Guardar" />
        <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png" runat="server" onclick="BtnBorrar_Click" ToolTip="Borrar" />
        <asp:ImageButton ID="ImageButton2" Width="32px" Height="32px" ImageUrl="~/Images/return.png" runat="server" onclick="BtnSalir_Click" ToolTip="Salir" />
    </td>
</tr>

</table>
</asp:Panel>
<act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1" Radius="8" Color="#DDDDDD" Corners="All" Enabled="true"/>


</div>
        
    
</asp:Content>