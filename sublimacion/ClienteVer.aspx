<%@ Page Language="C#"  MasterPageFile="~/Master.Master"  AutoEventWireup="true" CodeBehind="ClienteVer.aspx.cs" Inherits="sublimacion.Clientes" %>


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
                <asp:BoundField DataField="IdClienteStr" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" SortExpression="nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="apellido" />
                <asp:BoundField DataField="Dni" HeaderText="Dni" SortExpression="Dni" />
                <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
               <asp:BoundField DataField="Mail" HeaderText="Mail" SortExpression="Mail" />
               <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
                <asp:TemplateField  ItemStyle-Width="25px">
                 <ItemTemplate>  
                    <a href="Cliente.aspx?id=<%# Eval("IdCliente") %>" >
                        <img alt="Abrir" src="../images/File-Open-icon.png" border="0"  width="16px" height="16px"/>
                      </a>
                  </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
           
        </asp:GridView>


</asp:Content>
