<%@ Page Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LogisticaProduccion.aspx.cs"
    Inherits="sublimacion.LogisticaProduccion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="LblFecha" runat="server" Font-Bold="true" Font-Names="calibri" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Pedidos Aceptados" Font-Bold="true" Font-Names="calibri"
        Font-Underline="True"></asp:Label>
    <br />
    <div>
        <asp:GridView ID="GridViewPedidos" runat="server" AutoGenerateColumns="False" GridLines="None"
            AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20" CssClass="mGrid"
            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="GridViewPedidos_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                        <asp:Label ID="LblIdPedido" runat="server" Text='<%#Eval("IdPedido") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FechaVer" HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" />
                <asp:BoundField DataField="Comentario" HeaderText="Comentario" SortExpression="Comentario" />
                <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" SortExpression="Prioridad" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" SortExpression="Cliente" />
                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                <asp:BoundField DataField="UserNombre" HeaderText="Usuario" SortExpression="Usuario" />
                <asp:BoundField DataField="CostoTotalTiempo" HeaderText="Tiempo Estimado" SortExpression="CostoTotalTiempo" />
                <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" SortExpression="PrecioTotal" />
                <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" SortExpression="CostoTotal" />
                <asp:TemplateField HeaderText="Seleccionar">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBox" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
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
    </div>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Fecha Inicio: "></asp:Label>
    <asp:TextBox ID="TxtFechaInicio" runat="server" Width="70px"></asp:TextBox>
    <act:CalendarExtender ID="CalendarExtender2" TargetControlID="TxtFechaInicio" runat="server"
        Format="dd/MM/yyyy" />
    <asp:TextBox ID="TxtHoraInicio" runat="server" Width="40px"></asp:TextBox>
    <act:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99:99" MaskType="Time"
        TargetControlID="TxtHoraInicio" />
    <asp:Label ID="Label4" runat="server" Text="    Fecha Fin: "></asp:Label>
    <asp:TextBox ID="TxtFechaFin" runat="server" ReadOnly="True" Width="70px"></asp:TextBox>
    <asp:TextBox ID="TxtHoraFin" runat="server" ReadOnly="True" Width="40px"></asp:TextBox>
    <asp:ImageButton ID="BtnCalcularTiempo" runat="server" Width="16px" Height="16px"
        ImageUrl="~/Images/calculator.png" ToolTip="Calcular Tiempo" OnClick="BtnCalcularTiempo_Click" />
    <br />
    <br />
    <asp:Label ID="LblComentario" runat="server" Font-Bold="true" Font-Names="calibri"
        ForeColor="Red"></asp:Label>
    <br />
    <asp:ImageButton ID="BtnAgregarAPlan" runat="server" Width="32px" Height="32px" ToolTip="Crear Plan de Producción" ImageUrl="~/Images/Generate.png"
        OnClick="BtnAgregarAPlan_Click" />
    <br />
     <asp:Label ID="LblComentarioPlan" runat="server" Font-Bold="true" Font-Names="calibri"
        ForeColor="Red"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Pedidos en Plan de Producción" Font-Bold="True"
        Font-Underline="True"></asp:Label>
    <br />
    <div>
        <asp:GridView ID="GridViewPlanif" runat="server" AutoGenerateColumns="False" GridLines="None"
            AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20" CssClass="mGrid"
            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="GridViewPlanif_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
              <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                        <asp:Label ID="LblIdPedido" runat="server" Text='<%#Eval("IdPedido") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="PlanId" HeaderText="Nro Plan" ReadOnly="True" SortExpression="Nro Plan" />
                <asp:BoundField DataField="FechaVer" HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" />
                <asp:BoundField DataField="Comentario" HeaderText="Comentario" SortExpression="Comentario" />
                <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" SortExpression="Prioridad" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" SortExpression="Cliente" />
                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                <asp:BoundField DataField="UserNombre" HeaderText="Usuario" SortExpression="Usuario" />
                <asp:BoundField DataField="CostoTotalTiempo" HeaderText="Tiempo Estimado" SortExpression="CostoTotalTiempo" />
                <asp:BoundField DataField="PrecioTotal" HeaderText="Precio Total" SortExpression="PrecioTotal" />
                <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" SortExpression="CostoTotal" />
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
    </div>
   
</asp:Content>
