<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="RegistrarDisenio.aspx.cs" Inherits="sublimacion.RegistrarDisenio" %>

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
                    </td>
                    <td>
                        <asp:TextBox ID="TxtCliente" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label3" runat="server" Text="Prioridad" Font-Names="calibri"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPrioridad" runat="server" Width="100%" Font-Names="calibri" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label4" runat="server" Text="Estado" Font-Names="calibri"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CmbEstado"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                    </td>
                    <td>
                        <asp:DropDownList ID="CmbEstado" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label5" runat="server" Text="Ubicacion" Font-Names="calibri"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtUbicacion" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label6" runat="server" Text="Usuario" Font-Names="calibri"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtUsuario" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label7" runat="server" Text="Comentario" Font-Names="calibri"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtComentario"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtComentario" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1"
            Radius="8" Color="#DDDDDD" Corners="All" Enabled="true" />
        <br />
        <center>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="GridViewLineaPedido" Font-Names="calibri" runat="server" GridLines="None"
                        AllowPaging="true" Width="800px" HorizontalAlign="Center" PageSize="20" CssClass="mGrid"
                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
                        OnPageIndexChanging="GridViewLineaPedido_PageIndexChanging" OnRowCommand="GridViewLineaPedido_RowCommand">
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
                                    <asp:Label ID="LblCata" Font-Names="calibri" runat="server" Text='<%#Eval("CatalogoNombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plantilla">
                                <ItemTemplate>
                                    <asp:Label ID="LblPlant" Font-Names="calibri" runat="server" Text='<%#Eval("PlantillaNombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cantidad">
                                <ItemTemplate>
                                    <asp:Label ID="LblCant" Font-Names="calibri" runat="server" Text='<%#Eval("Cantidad") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="BtnVerAdjunto" Width="16px" Height="16px" ImageUrl="~/Images/arrow_down.png"
                                            runat="server" ToolTip="Descargar archivo cliente" CommandName="VerAdjunto" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Archivo Cliente">
                                <ItemTemplate>
                                    <a href="VerImagen.aspx?Imagen=<%# Eval("ArchivoCliente") %>" target="_blank">
                                            <asp:Label ID="LblArchivo" Font-Names="calibri" runat="server" Text='<%#Eval("ArchivoClienteNombreMostrable") %>'></asp:Label>
                                        </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:ImageButton ID="BtnVerAdjuntoDisenio" Width="16px" Height="16px" ImageUrl="~/Images/arrow_down.png"
                                            runat="server" ToolTip="Descargar diseño" CommandName="VerAdjuntoDisenio" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Archivo Diseño">
                                <ItemTemplate>
                                  <a href="VerImagen.aspx?Imagen=<%# Eval("ArchivoDisenio") %>" target="_blank">
                                        <asp:Label ID="LblArchivoDisenio" Font-Names="calibri" runat="server" Text='<%#Eval("ArchivoDisenioNombreMostrable") %>'></asp:Label>
                                        </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField>
                                <ItemTemplate>
                                <asp:ImageButton ID="BtnSeleccionar" Width="16px" Height="16px" ImageUrl="~/Images/edit.png"
                                            runat="server" ToolTip="Seleccionar / Editar" CommandName="Seleccionar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            </table>
        </center>
        
        <br/>
           <asp:Panel runat="server" ID="panel3" Visible="true" Width="800px" Style="background-color: #DDDDDD">
            <table>
                <tr>
                    <td align="center">
                    <asp:Label ID="lblLineaSeleccionada" runat="server"></asp:Label>
                      <input type="file" runat="server" visible="false" id="FileDisenio" name="FileDisenio" runat="server" />
                       <asp:ImageButton ID="BtnAdjuntar" visible="false" Width="32px" Height="32px" ImageUrl="~/Images/attach.png"
                            runat="server" ValidationGroup="add" OnClick="BtnAdjuntar_Click" ToolTip="Adjuntar" />
                   </td>
                </tr>
            </table>
        </asp:Panel>
        <act:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" TargetControlID="panel3"
            Radius="8" Color="#DDDDDD" Corners="All" Enabled="true" />

        <br/>

        <asp:Panel runat="server" ID="panel2" Visible="true" Width="800px" Style="background-color: #DDDDDD">
            <table>
                <tr>
                    <td align="center" colspan="2">
                        <br />
                        <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/Save.png"
                            runat="server" ValidationGroup="add" OnClick="BtnGuardar_Click" ToolTip="Guardar" />
                        
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
