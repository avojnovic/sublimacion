<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DescuentoABM.aspx.cs" Inherits="sublimacion.DescuentoABM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div style="margin-right: 0px">
<br/>
<br/> 
<center>      
<asp:Label ID="LblMensaje" Font-Names="Calibri" Font-Size="Small" ForeColor="Red" runat="server" ></asp:Label>
<br/> 

<asp:Panel runat="server" ID="panel1" Visible="true"  Width="600px" style="background-color:#DDDDDD">

<table style="width:500px;"  border="0" cellspacing="0">

<tr>
            <td align="left">
                <asp:Label ID="Label1" runat="server" Text="Producto" Font-Names="Calibri"></asp:Label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="CmbProducto" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
            </td>
            <td>
                <asp:DropDownList ID="CmbProducto" runat="server" Width="100%"/>
            </td>
        </tr>

 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label2" runat="server" Text="Cantidad:" Font-Names="Calibri" ></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtCantidad" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtCantidad" Width="100%" runat="server"></asp:TextBox>
        <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99999" MaskType="Number" TargetControlID="TxtCantidad"></act:MaskedEditExtender>
    </td>
</tr>


 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label3" runat="server" Text="Descuento:" Font-Names="Calibri"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtDescuento" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtDescuento" Width="100%" runat="server"></asp:TextBox>
        <act:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99.99" MaskType="Number" TargetControlID="TxtDescuento"></act:MaskedEditExtender>
    </td>
</tr>

 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label4" runat="server" Text="Fecha:" Font-Names="Calibri" ></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtFecha" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
        
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtFecha" Width="100%" runat="server"></asp:TextBox>
        
    </td>
</tr>

 <tr>
    <td align="center" colspan="2">
        <br/>
        <asp:ImageButton ID="BtnGuardar" Width="32px" Height="32px" ImageUrl="~/Images/Save.png" runat="server" ValidationGroup="add" onclick="BtnGuardar_Click" ToolTip="Guardar" />
        <asp:ImageButton ID="BtnSalir" Width="32px" Height="32px" ImageUrl="~/Images/return.png" runat="server" onclick="BtnSalir_Click" ToolTip="Salir" />
    </td>
</tr>

</table>

</asp:Panel>
<act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1" Radius="8" Color="#DDDDDD" Corners="All" Enabled="true"/>


</div>
</center>  
</asp:Content>
