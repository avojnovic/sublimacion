<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="ProductoABM.aspx.cs" Inherits="sublimacion.ProductoABM" %>

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
                            <asp:Label ID="Label2" runat="server" Text="Precio:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtPrecio"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtPrecio" Width="100%" runat="server"></asp:TextBox>
                            <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999.99" MaskType="Number"
                                TargetControlID="TxtPrecio">
                            </act:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label3" runat="server" Text="Tiempo (en minutos):" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtTiempo"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtTiempo" Width="100%" runat="server"></asp:TextBox>
                            <act:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="999" MaskType="Number"
                                TargetControlID="TxtTiempo">
                            </act:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridViewInsumos" runat="server" GridLines="None" AllowPaging="true" Font-Names="Calibri"
                                Width="400px" HorizontalAlign="Center" PageSize="40" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" AutoGenerateEditButton="true"
                                OnRowEditing="GridViewInsumos_RowEditing" OnPageIndexChanging="GridViewInsumos_PageIndexChanging"
                                OnRowUpdating="GridViewInsumos_OnRowUpdating" OnRowCancelingEdit="GridViewInsumos_OnRowCancelingEdit">
                                <PagerSettings PageButtonCount="5" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Idinsumo">
                                        <ItemTemplate>
                                            <asp:Label ID="LblIdinsumo" runat="server" Text='<%#Eval("Idinsumo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Insumo">
                                        <ItemTemplate>
                                            <asp:Label ID="LblNombreInsumo" runat="server" Text='<%#Eval("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cantidad">
                                        <ItemTemplate>
                                            <%# Eval("Cantidad") %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:TextBox>
                                            <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99999" MaskType="Number"
                                                TargetControlID="TxtCantidad">
                                            </act:MaskedEditExtender>
                                        </EditItemTemplate>
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
                                runat="server" ValidationGroup="add" ToolTip="Guardar" OnClick="BtnGuardar_Click" />
                            <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png"
                                runat="server" ToolTip="Borrar" OnClick="BtnBorrar_Click" />
                            <asp:ImageButton ID="BtnSalir" Width="32px" Height="32px" ImageUrl="~/Images/return.png"
                                runat="server" ToolTip="Salir" OnClick="BtnSalir_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1"
                Radius="8" Color="#DDDDDD" Corners="All" Enabled="true" />
        </center>
    </div>
</asp:Content>
