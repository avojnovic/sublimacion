<%@ Page Language="C#" MasterPageFile="~/Master.Master"  AutoEventWireup="true" CodeBehind="InsumoVer.aspx.cs" Inherits="sublimacion.InsumoVer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <br/>   
   <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/New.png" runat="server" onclick="BtnNuevo_Click" ToolTip="Nuevo" />
  
    <asp:GridView ID="GridView1"  runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20"
          CssClass="mGrid"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  onpageindexchanging="GridView1_PageIndexChanging">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="IdInsumoStr" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" SortExpression="nombre" />
                <asp:BoundField DataField="NombreFab" HeaderText="Fabricante" SortExpression="fabricante" />
                <asp:BoundField DataField="CostoStr" HeaderText="Costo" SortExpression="costo" />
                <asp:BoundField DataField="StockStr" HeaderText="Stock" SortExpression="stock" />
                <asp:BoundField DataField="FechaActStr" HeaderText="Fecha Actualizacion" SortExpression="fechaAct" />
               
                <asp:TemplateField  ItemStyle-Width="25px">
                 <ItemTemplate>  
                    <a href="Insumo.aspx?id=<%# Eval("Idinsumo") %>" >
                        <img alt="Abrir" src="../images/File-Open-icon.png" border="0"  width="16px" height="16px"/>
                      </a>
                  </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" 
                VerticalAlign="Middle" />
           
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
           
        </asp:GridView>


 </asp:Content>