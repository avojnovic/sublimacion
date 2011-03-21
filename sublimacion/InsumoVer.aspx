<%@ Page Language="C#" MasterPageFile="~/Master.Master"  AutoEventWireup="true" CodeBehind="InsumoVer.aspx.cs" Inherits="sublimacion.InsumoVer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="BtnEditar" runat="server"  
        Text="Editar" onclick="BtnEditar_Click" />
    <asp:Button ID="BtnNuevo" runat="server" 
        Text="Nuevo" onclick="BtnNuevo_Click" />
    <asp:Button ID="BtnBorrar" runat="server" 
        Text="Borrar" onclick="BtnBorrar_Click" />


    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" 
            GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
            BorderWidth="1px" HorizontalAlign="Center" Width="100%" Font-Size="Small" 
            Font-Names="Frutiger-Roman" PageSize="20" onpageindexchanging="GridView1_PageIndexChanging" 
            AutoGenerateSelectButton="True" 
           >
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="IdInsumoStr" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" SortExpression="nombre" />
                <asp:BoundField DataField="NombreFab" HeaderText="Fabricante" SortExpression="fabricante" />
                <asp:BoundField DataField="CostoStr" HeaderText="Costo" SortExpression="costo" />
                <asp:BoundField DataField="StockStr" HeaderText="Stock" SortExpression="stock" />
                <asp:BoundField DataField="FechaActStr" HeaderText="Fecha Actualizacion" SortExpression="fechaAct" />
            </Columns>
           
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" 
                VerticalAlign="Middle" />
           
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
           
        </asp:GridView>


 </asp:Content>