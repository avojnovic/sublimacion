<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="CatalogoABM.aspx.cs" Inherits="sublimacion.CatalogoABM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-right: 0px">
        <br />
        <br />
        <asp:Label ID="LblMensaje" Font-Names="Calibri" Font-Size="Small" ForeColor="Red"
            runat="server"></asp:Label>
        <br />
        <center>
            <asp:Panel runat="server" ID="panel1" Visible="true" Width="600px" Style="background-color: #DDDDDD">
                <table style="width: 500px;" border="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label1" runat="server" Text="Nombre:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtNombre"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtNombre" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label2" runat="server" Text="Fecha:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtFecha"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtFecha" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label3" runat="server" Text="Producto:" Font-Names="Calibri"></asp:Label>
                        </td>
                        <td style="width: 200px;">
                            <asp:DropDownList ID="ComboProducto" Width="100%" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridViewPlantillas" runat="server" GridLines="None" AllowPaging="true"
                                Font-Names="Calibri" Width="400px" HorizontalAlign="Center" PageSize="40" CssClass="mGrid"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                                OnPageIndexChanging="GridViewPlantillas_PageIndexChanging">
                                <PagerSettings PageButtonCount="5" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="IdPlantilla">
                                        <ItemTemplate>
                                            <asp:Label ID="LblIdPlantilla" runat="server" Text='<%#Eval("IdPlantilla") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plantilla">
                                        <ItemTemplate>
                                            <asp:Label ID="LblNombrePlantilla" runat="server" Text='<%#Eval("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agregar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="checkBoxPlantilla" runat="server" ></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <br />
                            <asp:ImageButton ID="BtnGuardar" Width="32px" Height="32px" ImageUrl="~/Images/Save.png"
                                runat="server" ValidationGroup="add" OnClick="BtnGuardar_Click" ToolTip="Guardar" />
                            <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png"
                                runat="server" OnClick="BtnBorrar_Click" ToolTip="Borrar" />
                            <asp:ImageButton ID="BtnSalir" Width="32px" Height="32px" ImageUrl="~/Images/return.png"
                                runat="server" OnClick="BtnSalir_Click" ToolTip="Salir" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </center>
        <act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1"
            Radius="8" Color="#DDDDDD" Corners="All" Enabled="true" />
    </div>
</asp:Content>
