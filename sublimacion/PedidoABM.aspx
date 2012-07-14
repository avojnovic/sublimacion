<%@ Page Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PedidoABM.aspx.cs"
    Inherits="sublimacion.PedidoABM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <asp:Label ID="LblComentario" runat="server"></asp:Label>
    <center>
        <asp:Panel runat="server" ID="panel1" Visible="true" Width="800px" Style="background-color: #DDDDDD">
            <table>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label1" runat="server" Text="Fecha" Font-Names="calibri"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtFecha" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label2" runat="server" Text="Cliente" Font-Names="calibri"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="CmbCliente"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                    </td>
                    <td>
                        <asp:DropDownList ID="CmbCliente" runat="server" Width="100%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label3" runat="server" Text="Comentario" Font-Names="calibri"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtComentario"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtComentario" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label4" runat="server" Text="Prioridad" Font-Names="calibri"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtPrioridad"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPrioridad" runat="server" Width="100%" Font-Names="calibri"></asp:TextBox>
                        <act:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="999" MaskType="Number"
                            TargetControlID="TxtPrioridad">
                        </act:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label5" runat="server" Text="Estado" Font-Names="calibri"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CmbEstado"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                    </td>
                    <td>
                        <asp:DropDownList ID="CmbEstado" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label6" runat="server" Text="Ubicacion" Font-Names="calibri"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtUbicacion"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtUbicacion" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label9" runat="server" Text="Usuario" Font-Names="calibri"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtUsuario" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1"
            Radius="8" Color="#DDDDDD" Corners="All" Enabled="true" />
        <br />
        <table>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GridViewProductos" Font-Names="calibri" runat="server" GridLines="None"
                        AllowPaging="true" Width="400px" HorizontalAlign="Center" PageSize="20" CssClass="mGrid"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                        AutoGenerateEditButton="true" OnRowEditing="GridViewProductos_RowEditing" OnPageIndexChanging="GridViewProductos_PageIndexChanging"
                        OnRowUpdating="GridViewProductos_OnRowUpdating" 
                        OnRowCancelingEdit="GridViewProductos_OnRowCancelingEdit" 
                        onrowdatabound="GridViewProductos_RowDataBound">
                        <PagerSettings PageButtonCount="5" />
                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="LblIdproducto" Font-Names="calibri" runat="server" Text='<%#Eval("Idproducto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Producto">
                                <ItemTemplate>
                                    <asp:Label ID="LblNombreproducto" Font-Names="calibri" runat="server" Text='<%#Eval("Nombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Catalogo">
                                <ItemTemplate>
                                 <asp:Label ID="LblCata" Font-Names="calibri" runat="server"  Text='<%#Eval("CatalogoNombre") %>'></asp:Label>
                                    
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DDLCatalogo" DataTextField="Nombre" DataValueField="IdCatalogo" runat="server" AutoPostBack="true" 
                                                    OnSelectedIndexChanged="DDLCatalogo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plantilla">
                                <ItemTemplate>
                                       <asp:Label ID="LblPlant" Font-Names="calibri" runat="server" Text='<%#Eval("PlantillaNombre") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DDLPlantilla" DataTextField="Nombre" DataValueField="IdPlantilla" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cantidad">
                                <ItemTemplate>
                                    <%# Eval("Cantidad") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtCantidad" runat="server" Font-Names="calibri" Text='<%# Eval("Cantidad") %>'></asp:TextBox>
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
        </table>
        <asp:Panel runat="server" ID="panel2" Visible="true" Width="800px" Style="background-color: #DDDDDD">
            <table>
                <tr>
                    <td align="center" colspan="2">
                        <br />
                        <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/Save.png"
                            runat="server" ValidationGroup="add" OnClick="BtnGuardar_Click" ToolTip="Guardar" />
                        <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png"
                            runat="server" OnClick="BtnBorrar_Click" ToolTip="Borrar" />
                        <asp:ImageButton ID="ImageButton2" Width="32px" Height="32px" ImageUrl="~/Images/return.png"
                            runat="server" OnClick="BtnSalir_Click" ToolTip="Salir" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <act:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="panel2"
            Radius="8" Color="#DDDDDD" Corners="All" Enabled="true" />
    </center>
</asp:Content>
