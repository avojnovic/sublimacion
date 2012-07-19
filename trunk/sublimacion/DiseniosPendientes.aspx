<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DiseniosPendientes.aspx.cs" Inherits="sublimacion.DiseniosPendientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <br />

    <div>
        <asp:GridView ID="GridViewPedidos"  Font-Names="calibri" runat="server" AutoGenerateColumns="False" GridLines="None"
            AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20" CssClass="mGrid"
            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="GridViewPedidos_PageIndexChanging">
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
                        <a href="RegistrarDisenio.aspx?id=<%# Eval("IdPedido") %>">
                            <img alt="Abrir" src="../images/File-Open-icon.png" border="0" width="16px" height="16px" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
</asp:Content>
