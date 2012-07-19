<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="ReporteRanking.aspx.cs" Inherits="sublimacion.ReporteRanking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />

    <asp:Label ID="LbComentario" runat="server" Font-Bold="true" Font-Names="calibri" ForeColor="Red"/> 

     <asp:Panel ID="Panel1" DefaultButton="BtnBuscar" runat="server">
        
        <br />
        <table>
        <tr>
            <td align="left">
                <asp:Label ID="Label1" runat="server" Text="Fecha desde:" Font-Names="calibri"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtFechaDesde" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFechaDesde"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                <act:CalendarExtender ID="CalendarExtender1" TargetControlID="TxtFechaDesde" runat="server" Format="dd/MM/yyyy" />
            </td>
            <td align="left">
                <asp:Label ID="Label2" runat="server" Text="Fecha Hasta:" Font-Names="calibri"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtFechaHasta" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtFechaHasta"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                <act:CalendarExtender ID="CalendarExtender2" TargetControlID="TxtFechaHasta" runat="server" Format="dd/MM/yyyy"  />
            </td>
            <td align="left">
                <asp:Label ID="Label3" runat="server" Text="Estados:" Font-Names="calibri"></asp:Label>
            </td>
        
            <td>
                <asp:DropDownList ID="CmbEstados" runat="server" Width="200px" Visible="true"  
                    AutoPostBack="true" onselectedindexchanged="CmbEstados_SelectedIndexChanged" />
            </td>
            <td>
                <asp:ImageButton ID="BtnBuscar" Width="16px" Height="16px" ImageUrl="~/Images/search.png"
                    runat="server" OnClick="BtnBuscar_Click" ToolTip="Buscar" ValidationGroup="add"/>

                <asp:ImageButton ID="BtnLimpiar" Width="16px" Height="16px" ImageUrl="~/Images/clear.png"
                    runat="server" OnClick="Btnlimpiar_Click" ToolTip="Limpiar" />
            </td>
            </tr>
        </table>


        <asp:GridView ID="GridViewReporte" Font-Names="calibri" runat="server" AutoGenerateColumns="False"
            GridLines="None" AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20"
            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            OnPageIndexChanging="GridViewReporte_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="IdPedido" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="FechaVer" HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" />
                <asp:BoundField DataField="Comentario" HeaderText="Comentario" SortExpression="Comentario" />
                <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" SortExpression="Prioridad" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                <asp:BoundField DataField="UserNombre" HeaderText="Usuario" SortExpression="Usuario" />
                <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" SortExpression="Cliente" />
                <asp:TemplateField ItemStyle-Width="25px">
                    <ItemTemplate>
                        <a href="PedidoABM.aspx?id=<%# Eval("IdPedido") %>">
                            <img alt="Abrir" src="../images/File-Open-icon.png" border="0" width="16px" height="16px" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                            </Columns>
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
