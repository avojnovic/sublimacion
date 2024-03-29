﻿<%@ Page Language="C#"  MasterPageFile="~/Master.Master"  AutoEventWireup="true" CodeBehind="ClienteVer.aspx.cs" Inherits="sublimacion.ClienteVer" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <br/>  
     
  <script type="text/Javascript">

 </script>
 <asp:Panel defaultbutton="BtnBuscar" runat="server">
     <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/New.png" runat="server" onclick="BtnNuevo_Click" ToolTip="Nuevo" />
  <br/>
  <asp:TextBox ID="txtBuscar" runat="server" Width="200px" ></asp:TextBox>
  <asp:ImageButton ID="BtnBuscar" Width="16px" Height="16px" ImageUrl="~/Images/search.png" runat="server" onclick="BtnBuscar_Click" ToolTip="Buscar" />
    <asp:ImageButton ID="BtnLimpiar" Width="16px" Height="16px" ImageUrl="~/Images/clear.png" runat="server" onclick="Btnlimpiar_Click" ToolTip="Limpiar" />
    <asp:GridView ID="GridView1"   Font-Names="calibri" runat="server" AutoGenerateColumns="False" GridLines="None" 
      AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20"
      CssClass="mGrid"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  
      onpageindexchanging="GridView1_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="IdClienteStr" HeaderText="ID" HeaderStyle-Font-Names="calibri" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-Font-Names="calibri" SortExpression="nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" HeaderStyle-Font-Names="calibri" SortExpression="apellido" />
                <asp:BoundField DataField="Dni" HeaderText="Dni" HeaderStyle-Font-Names="calibri" SortExpression="Dni" />
                <asp:BoundField DataField="Direccion" HeaderText="Direccion" HeaderStyle-Font-Names="calibri" SortExpression="Direccion" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" HeaderStyle-Font-Names="calibri" SortExpression="Telefono" />
               <asp:BoundField DataField="Mail" HeaderText="Mail" HeaderStyle-Font-Names="calibri" SortExpression="Mail" />
               <asp:BoundField DataField="FechaVer" HeaderText="Fecha" HeaderStyle-Font-Names="calibri" SortExpression="Fecha" />
                <asp:TemplateField  ItemStyle-Width="25px">
                 <ItemTemplate>  
                    <a href="ClienteABM.aspx?id=<%# Eval("IdCliente") %>" >
                        <img alt="Abrir" src="../images/File-Open-icon.png" border="0"  width="16px" height="16px"/>
                      </a>
                  </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
           
        </asp:GridView>
</asp:Panel>

</asp:Content>
