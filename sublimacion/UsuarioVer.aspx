<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="UsuarioVer.aspx.cs" Inherits="sublimacion.UsuarioVer" %>
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
                <asp:BoundField DataField="Id" HeaderText="ID" HeaderStyle-Font-Names="calibri" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-Font-Names="calibri" SortExpression="nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" HeaderStyle-Font-Names="calibri" SortExpression="apellido" />
                <asp:BoundField DataField="User" HeaderText="Usuario" HeaderStyle-Font-Names="calibri" SortExpression="usuario" />
                <asp:BoundField DataField="Password" HeaderText="Password" HeaderStyle-Font-Names="calibri" SortExpression="password" />
              <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Font-Names="calibri" SortExpression="mail" />
               <asp:BoundField DataField="Perfil" HeaderText="Perfil" HeaderStyle-Font-Names="calibri" SortExpression="perfil" />
                <asp:TemplateField  ItemStyle-Width="25px">
                 <ItemTemplate>  
                    <a href="UsuarioABM.aspx?id=<%# Eval("Id") %>" >
                        <img alt="Abrir" src="../images/File-Open-icon.png" border="0"  width="16px" height="16px"/>
                      </a>
                  </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
           
        </asp:GridView>

</asp:Content>
