<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CatalogoVer.aspx.cs" Inherits="sublimacion.CatalogoVer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <br/>
    <br/>   
    
     <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/New.png" runat="server" onclick="BtnNuevo_Click" ToolTip="Nuevo" />
  
    <asp:GridView ID="GridView1" Font-Names="calibri" runat="server" AutoGenerateColumns="False" GridLines="None"
      AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="30"
      CssClass="mGrid"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  
      onpageindexchanging="GridView1_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="IdCatalogo" HeaderText="ID" HeaderStyle-Font-Names="calibri" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-Font-Names="calibri" SortExpression="nombre" />
                <asp:BoundField DataField="FechaVer" HeaderText="Fecha" HeaderStyle-Font-Names="calibri" SortExpression="fecha" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" HeaderStyle-Font-Names="calibri" SortExpression="producto" />
                

                <asp:TemplateField  ItemStyle-Width="25px">
                 <ItemTemplate>  
                    <a href="CatalogoABM.aspx?id=<%# Eval("IdCatalogo") %>" >
                        <img alt="Abrir" src="../images/File-Open-icon.png" border="0"  width="16px" height="16px"/>
                      </a>
                  </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
           
        </asp:GridView>
</asp:Content>
