<%@ Page Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PedidoABM.aspx.cs" Inherits="sublimacion.PedidoABM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <br/>
         <br/> 

    <asp:Label ID="LblComentario" runat="server"></asp:Label>
      <center>
        <asp:Panel runat="server" ID="panel1" Visible="true"  Width="800px" style="background-color:#DDDDDD">
    <table>
        <tr>
            <td align="left">
                <asp:Label ID="Label1" runat="server" Text="Fecha"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtFecha" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label9" runat="server" Text="Estado"></asp:Label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CmbEstado" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
            </td>
            <td>
                <asp:DropDownList ID="CmbEstado" runat="server" Width="100%"/>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label3" runat="server" Text="Comentario"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtComentario" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
            </td>
            <td>
                <asp:TextBox ID="TxtComentario" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label2" runat="server" Text="Prioridad"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtPrioridad" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
            </td>
            <td>
                <asp:TextBox ID="TxtPrioridad" runat="server" Width="100%"></asp:TextBox>
                 <act:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="999" MaskType="Number" TargetControlID="TxtPrioridad"></act:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label4" runat="server" Text="Ubicacion"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtUbicacion" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
            </td>
            <td>
                <asp:TextBox ID="TxtUbicacion" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label6" runat="server" Text="Cliente"></asp:Label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="CmbCliente" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
            </td>
            <td>
                <asp:DropDownList ID="CmbCliente" runat="server" Width="100%"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label7" runat="server" Text="Orden de Trabajo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label8" runat="server" Text="Plan de Produccion"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label5" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtUsuario" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <table border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LblProductosDisp" runat="server" Text="Productos Disponibles"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Cantidad:   "></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtCantidad" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="prod" />
                            <asp:TextBox ID="TxtCantidad" runat="server"></asp:TextBox>
                             <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999" MaskType="Number" TargetControlID="TxtCantidad"></act:MaskedEditExtender>
                        </td>
                        <td align="left">
                            <asp:Label ID="LblValidarAgregarProducto0" runat="server">Productos en Pedido:</asp:Label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ListBoxProductosAgregados"  Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:ListBox ID="ListBoxProductos" runat="server" Height="100px" Width="270px" />
                        </td>
                        <td>
                            <asp:Button ID="BtnAgregarProducto" runat="server" ValidationGroup="prod" Text="Agregar Producto =&gt;" OnClick="BtnAgregarProducto_Click" />
                        </td>
                        <td >
                            <asp:ListBox ID="ListBoxProductosAgregados" runat="server" Height="98px" Width="261px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

         <tr>
                <td align="center" colspan="2">
                  <br/>
                    <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/Save.png" runat="server" ValidationGroup="add" onclick="BtnGuardar_Click" ToolTip="Guardar" />
                     <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png" runat="server" onclick="BtnBorrar_Click" ToolTip="Borrar" />
                     <asp:ImageButton ID="ImageButton2" Width="32px" Height="32px" ImageUrl="~/Images/return.png" runat="server" onclick="BtnSalir_Click" ToolTip="Salir" />
                </td>

            </tr>
       
    </table>
    </center>
      </asp:Panel>
         <act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1" Radius="8" Color="#DDDDDD" Corners="All" Enabled="true"/>
  
</asp:Content>
