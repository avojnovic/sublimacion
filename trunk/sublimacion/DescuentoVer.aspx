<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DescuentoVer.aspx.cs" Inherits="sublimacion.DescuentoVer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<br/>
    <br/>   
    
     <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/New.png" runat="server" onclick="BtnNuevo_Click" ToolTip="Nuevo" />
  
    <asp:GridView ID="GridView1"  runat="server" AutoGenerateColumns="False" GridLines="None" 
      AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20"
      CssClass="mGrid"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  
      onpageindexchanging="GridView1_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="productoId" HeaderText="ID Producto" HeaderStyle-Font-Names="calibri" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="productoNombre" HeaderText="Nombre Producto" HeaderStyle-Font-Names="calibri" SortExpression="nombre" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" HeaderStyle-Font-Names="calibri" SortExpression="cantidad" />
                <asp:BoundField DataField="Descuento1" HeaderText="Descuento" HeaderStyle-Font-Names="calibri" SortExpression="descuento" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" HeaderStyle-Font-Names="calibri" SortExpression="fecha" />
                
                <asp:TemplateField  ItemStyle-Width="25px">
                 <ItemTemplate>  
                    <a href="DescuentoABM.aspx?productoId=<%# Eval("productoId") %>&cantidad=<%# Eval("Cantidad") %> " >
                        <img alt="Abrir" src="../images/File-Open-icon.png" border="0"  width="16px" height="16px"/>
                      </a>
                  </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
           
        </asp:GridView>

</asp:Content>
