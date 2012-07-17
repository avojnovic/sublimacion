<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ClienteABM.aspx.cs" Inherits="sublimacion.ClienteABM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div style="margin-right: 0px">
<br/>
<br/>         
<asp:Label ID="LblMensaje" Font-Names="Calibri" Font-Size="Small" ForeColor="Red" runat="server" ></asp:Label>
<br/> 
<center>
<asp:Panel runat="server" ID="panel1" Visible="true"  Width="600px" style="background-color:#DDDDDD">

<table style="width:500px;"  border="0" cellspacing="0">
 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label1" runat="server" Text="Nombre:" Font-Names="Calibri" ></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtNombre" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtNombre" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>


 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label2" runat="server" Text="Apellido:" Font-Names="Calibri"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtApellido" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtApellido" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>

 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label3" runat="server" Text="DNI:" Font-Names="Calibri" ></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDni" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
        
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtDni" Width="100%" runat="server"></asp:TextBox>
        <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99999999" MaskType="Number" TargetControlID="TxtDni"></act:MaskedEditExtender>
    </td>
</tr>

 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label4" runat="server" Text="Direccion:" Font-Names="Calibri"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtDireccion" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtDireccion" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>

 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label5" runat="server" Text="Telefono:" Font-Names="Calibri"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtTelefono" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtTelefono" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>

 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label6" runat="server" Text="Mail:" Font-Names="Calibri"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtMail" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtMail" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>

 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label7" runat="server" Text="Fecha:" Font-Names="Calibri"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtFecha" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtFecha" Width="100%" runat="server"></asp:TextBox>
    </td>
</tr>


<tr>
    <td align="center" colspan="2">
        <br/>
        <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/Save.png" runat="server" ValidationGroup="add" onclick="BtnGuardar_Click" ToolTip="Guardar" />
        <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png" runat="server" onclick="BtnBorrar_Click" ToolTip="Borrar" />
        <asp:ImageButton ID="BtnSalir" Width="32px" Height="32px" ImageUrl="~/Images/return.png" runat="server" onclick="BtnSalir_Click" ToolTip="Salir" />
         <asp:ImageButton ID="BtnVerPedidos" Width="32px" Height="32px" ImageUrl="~/Images/jobs.png" runat="server"  onclick="BtnVerPedidos_Click" ToolTip="Ver Pedidos" />
       <asp:ImageButton ID="BtnPedidoNew" Width="32px" Height="32px" ImageUrl="~/Images/SaveAndNew.png" runat="server" ValidationGroup="add" onclick="BtnPedidoNew_Click" ToolTip="Guardar y Nuevo Pedido" />
         
    </td>
</tr>

</table>

</asp:Panel>
<act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1" Radius="8" Color="#DDDDDD" Corners="All" Enabled="true"/>
</center>

</div>
        
    
</asp:Content>