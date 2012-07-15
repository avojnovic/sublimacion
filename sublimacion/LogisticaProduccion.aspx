﻿<%@ Page Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LogisticaProduccion.aspx.cs" Inherits="sublimacion.LogisticaProduccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="LblFecha" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Pedidos Aceptados" Font-Bold="True" Font-Underline="True"></asp:Label>
    <br />
    <asp:Button ID="BtnVerPedido" runat="server" OnClick="BtnVerPedido_Click" Text="Editar Pedido" />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20"
          CssClass="mGrid"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="GridView1_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="IdPedido" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="CostoTotalTiempo" HeaderText="Tiempo Total" ReadOnly="True" SortExpression="CostoTotalTiempo" />
                <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" SortExpression="CostoTotal" />
                <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" SortExpression="Prioridad" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
            </Columns>
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <br />
    <br />
    <br />
    <asp:Button ID="AgregarAPlan" runat="server" Text="Agregar a Plan de Producción" OnClick="AgregarAPlan_Click" />
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Fecha Inicio: "></asp:Label>
    <asp:TextBox ID="TxtFechaInicio" runat="server" Width="240px"></asp:TextBox>
    <asp:Label ID="Label4" runat="server" Text="Fecha Fin: "></asp:Label>
    <asp:TextBox ID="TxtFechaFin" runat="server" ReadOnly="True" Width="240px"></asp:TextBox>
    <asp:Label ID="LblComentario" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Pedidos en Plan de Producción" Font-Bold="True" Font-Underline="True"></asp:Label>
    <br />
    <div>
        <asp:GridView ID="GridViewPlanif"  runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20"
          CssClass="mGrid"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="GridViewPlanif_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="IdPedido" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="CostoTotalTiempo" HeaderText="Tiempo Total" ReadOnly="True" SortExpression="CostoTotalTiempo" />
                <asp:BoundField DataField="CostoTotal" HeaderText="Costo Total" SortExpression="CostoTotal" />
                <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" SortExpression="Prioridad" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
            </Columns>
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" OnClick="BtnLimpiar_Click" />
    <asp:Button ID="BtnGuardarLogistica" runat="server" Text="Guardar Logistica" OnClick="BtnGuardarLogistica_Click" />
</asp:Content>