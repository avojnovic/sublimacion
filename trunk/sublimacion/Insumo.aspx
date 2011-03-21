<%@ Page Language="C#" MasterPageFile="~/Master.Master"  AutoEventWireup="true" CodeBehind="Insumo.aspx.cs" Inherits="sublimacion.Insumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="margin-right: 0px">

                       

    
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="LblMensaje" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Nombre"></asp:Label>
                    :</td>
                <td>
                    <asp:TextBox ID="TxtNombre" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Nombre Fabricante:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtFabricante" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Costo:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtCosto" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Stock:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Txtstock" runat="server" ></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Fecha Actualizacion:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TxtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="BtnGuardar" runat="server" onclick="Button1_Click" 
                        Text="Guardar" />
                </td>
                <td align="center">
                    <asp:Button ID="BtnSalir" runat="server" Text="Salir" 
                        onclick="BtnSalir_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

                       

    
    </div>
        
    
</asp:Content>