<%@ Page Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="RegistrarOrdenTrabajo.aspx.cs"
    Inherits="sublimacion.RegistrarOrdenTrabajo" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <br />
          <asp:Label ID="LblTituloGral" runat="server" Font-Bold="true" Font-Names="calibri" Font-Underline="True" Text="Planes de Trabajos Pendientes"></asp:Label>
        <br />
        <asp:GridView ID="GridViewPlanif" Font-Names="calibri" runat="server" AutoGenerateColumns="False"
            GridLines="None" AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20"
            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            OnPageIndexChanging="GridViewPlanif_PageIndexChanging" OnRowCommand="GridViewPlanif_RowCommand">
            <PagerSettings PageButtonCount="5" />
            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                        <asp:Label ID="LblId" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha Inicio Planificada">
                    <ItemTemplate>
                        <asp:Label ID="LblFechaInicio" runat="server" Text='<%#Eval("Fecha_inicio_str") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Fecha Fin Planificada">
                    <ItemTemplate>
                        <asp:Label ID="LblFechaFin" runat="server" Text='<%#Eval("Fecha_fin_str") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnSeleccionar" Width="16px" Height="16px" ImageUrl="~/Images/File-Open-icon.png"
                            runat="server" ToolTip="Seleccionar" CommandName="Seleccionar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>

     <br />
    <br />
    <asp:Label ID="LblTitulo" runat="server" Text="" Font-Bold="true" Font-Names="calibri" Font-Underline="True"></asp:Label>
    <br />
    <div>
        <asp:GridView ID="GridViewPedidos" runat="server" AutoGenerateColumns="False" GridLines="None"
            AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20" CssClass="mGrid"
            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  OnPageIndexChanging="GridViewPedidos_PageIndexChanging">
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
     <asp:Label ID="LblComentario" runat="server" Font-Bold="true" Font-Names="calibri"
        ForeColor="Red"></asp:Label> 
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Fecha real de Inicio: "></asp:Label>
    
    <asp:TextBox ID="TxtFechaInicio" runat="server" Width="70px"></asp:TextBox>
    <act:CalendarExtender ID="CalendarExtender1" TargetControlID="TxtFechaInicio" runat="server" Format="dd/MM/yyyy" />
    
    <asp:TextBox ID="TxtHoraInicio" runat="server" Width="40px"></asp:TextBox>
    <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99:99" MaskType="Time" TargetControlID="TxtHoraInicio" />
    
    
    <asp:Label ID="Label4" runat="server" Text=" Fecha real de Fin: "></asp:Label>
    <asp:TextBox ID="TxtFechaFin" runat="server"  Width="70px"></asp:TextBox>
    <act:CalendarExtender ID="CalendarExtender2" TargetControlID="TxtFechaFin" runat="server" Format="dd/MM/yyyy" />

    <asp:TextBox ID="TxtHoraFin" runat="server" Width="40px"></asp:TextBox>
      <act:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99:99" MaskType="Time" TargetControlID="TxtHoraFin" />
    <asp:ImageButton ID="BtnRegistrarTiempoReal" runat="server" Width="16px" Height="16px"
        ImageUrl="~/Images/calculator.png" ToolTip="Registrar Tiempo Real" OnClick="RegistrarTiempoReal_Click" />
    <br />


    <br />
     <br />
    <br />

</asp:Content>
