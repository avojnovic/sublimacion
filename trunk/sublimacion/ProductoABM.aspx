<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductoABM.aspx.cs" Inherits="sublimacion.ProductoABM" %>
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
        <asp:Label ID="Label2" runat="server" Text="Precio:" Font-Names="Calibri"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtPrecio" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtPrecio" Width="100%" runat="server"></asp:TextBox>
          </td>
</tr>

 <tr>
    <td align="left" style="width:100px;" >
        <asp:Label ID="Label3" runat="server" Text="Tiempo:" Font-Names="Calibri" ></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtTiempo" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
        
    </td>
    <td style="width:200px;" >
        <asp:TextBox ID="TxtTiempo" Width="100%" runat="server"></asp:TextBox>
       
    </td>
</tr>


<table border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LblInsumosDis" runat="server" Text="Insumos Disponibles" Font-Names="calibri"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Cantidad Requerida:" Font-Names="calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtCantidadReq" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="prod" />
                            <asp:TextBox ID="TextCantidadReq" runat="server"></asp:TextBox>
                             <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="TxtCantidadReq"></act:MaskedEditExtender>
                        </td>
                        <td align="left">
                            <asp:Label ID="LblInsumosReq" runat="server" Text="Insumos Requeridos" Font-Names="calibri"></asp:Label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ListBoxInsumosAgregados"  Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:ListBox ID="ListBoxInsumos" runat="server" Height="100px" Width="270px" />
                        </td>
                        <td>
                            <asp:Button ID="BtnAgregarInsumo" runat="server" ValidationGroup="prod" Text="Agregar Insumo =&gt;" OnClick="BtnAgregarProducto_Click" />
                        </td>
                        <td >
                            <asp:ListBox ID="ListBoxInsumosAgregados" runat="server" Height="98px" Width="261px" />
                        </td>
                    </tr>
                </table>


</center>
<tr>
    <td align="center" colspan="2">
        <br/>
        <asp:ImageButton ID="ImageButton1"  Width="32px" Height="32px" ImageUrl="~/Images/Save.png" runat="server" ValidationGroup="add" onclick="BtnGuardar_Click" ToolTip="Guardar" />
        <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png" runat="server" onclick="BtnBorrar_Click" ToolTip="Borrar" />
        <asp:ImageButton ID="ImageButton2" Width="32px" Height="32px" ImageUrl="~/Images/return.png" runat="server" onclick="BtnSalir_Click" ToolTip="Salir" />
    </td>
</tr>

</table>
</asp:Panel>
<act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1" Radius="8" Color="#DDDDDD" Corners="All" Enabled="true"/>


</div>
        
    

</asp:Content>
