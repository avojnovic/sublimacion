<%@ Page Language="C#" MasterPageFile="~/Master.Master"   AutoEventWireup="true" CodeBehind="Pedido.aspx.cs" Inherits="sublimacion.Pedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 210px;
        }
        .style2
        {
            width: 274px;
        }
        .style3
        {
            width: 160px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <table style="width:100%;">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                <asp:Label ID="LblComentario" runat="server"></asp:Label>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="Fecha"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="TxtFecha" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label9" runat="server" Text="Estado"></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="CmbEstado" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label3" runat="server" Text="Comentario"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="TxtComentario" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label2" runat="server" Text="Prioridad"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="TxtPrioridad" runat="server"  Width="100%" ></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label4" runat="server" Text="Ubicacion"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="TxtUbicacion" runat="server"  Width="100%"></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label6" runat="server" Text="Cliente"></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="CmbCliente" runat="server"  Width="100%">
                </asp:DropDownList>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label7" runat="server" Text="Orden de Trabajo"></asp:Label>
            </td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label8" runat="server" Text="Plan de Produccion"></asp:Label>
            </td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label5" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="TxtUsuario" runat="server"  Width="100%" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                <asp:Label ID="LblValidarAgregarProducto0" runat="server">Productos en Pedido:</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                <asp:Label ID="LblProductosDisp" runat="server" Text="Productos Disponibles"></asp:Label>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Cantidad:   "></asp:Label>
                <asp:TextBox ID="TxtCantidad" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                <asp:ListBox ID="ListBoxProductos" runat="server" Height="100px" Width="270px">
                </asp:ListBox>
            </td>
            <td class="style3">
                <asp:Button ID="BtnAgregarProducto" runat="server"
                    Text="Agregar Producto =&gt;" onclick="BtnAgregarProducto_Click" />
            </td>
            <td>
                <asp:ListBox ID="ListBoxProductosAgregados" runat="server" Height="98px" 
                    Width="261px" AutoPostBack="True" 
                    onselectedindexchanged="ListBoxProductosAgregados_SelectedIndexChanged"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                <asp:Label ID="LblValidarAgregarProducto" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" 
                    onclick="BtnGuardar_Click" />
            </td>
            <td class="style3">
                <asp:Button ID="BtnSalir" runat="server" Text="Salir" onclick="BtnSalir_Click" 
                    style="height: 26px" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    
</asp:Content>