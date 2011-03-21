<%@ Page Language="C#" MasterPageFile="~/Master.Master"  AutoEventWireup="true" CodeBehind="PedidoVer.aspx.cs" Inherits="sublimacion.PedidoVer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Button ID="BtnEditar" runat="server"  
        Text="Editar" onclick="BtnEditar_Click"  />
    <asp:Button ID="BtnNuevo" runat="server" 
        Text="Nuevo" onclick="BtnNuevo_Click" />
    <asp:Button ID="BtnBorrar" runat="server" 
        Text="Borrar" onclick="BtnBorrar_Click"  />

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:Label ID="LblEstado" runat="server" Text="Estado" Visible="false"></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="CmbEstados" runat="server" Height="16px" Width="162px" Visible="false">
    </asp:DropDownList>
     <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" 
                GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid" 
                BorderWidth="2px" HorizontalAlign="Center" Width="100%" Font-Size="Small" 
                Font-Names="Frutiger-Roman" PageSize="20"  
                AutoGenerateSelectButton="True" 
            onpageindexchanging="GridView1_PageIndexChanging">
                <PagerSettings PageButtonCount="5" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                    <asp:BoundField DataField="IdPedido" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" />
                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" SortExpression="Comentario" />
                    <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" SortExpression="Prioridad" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                    <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                    <asp:BoundField DataField="UserNombre" HeaderText="Usuario" SortExpression="Usuario" />
                    <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" SortExpression="Cliente" />
                
                </Columns>
               
                <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
               
                <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
               
            </asp:GridView>
        </div>
            
       
         <br />
    <br />
       

    </asp:Content>