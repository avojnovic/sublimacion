<%@ Page Language="C#"  MasterPageFile="~/Master.Master"   AutoEventWireup="true" CodeBehind="VerLogistica.aspx.cs" Inherits="sublimacion.VerLogistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div>
        <br />
        <br />
        <asp:GridView ID="GridViewPlanif"  Font-Names="calibri" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="true" HorizontalAlign="Center" Width="100%" PageSize="20"
          CssClass="mGrid"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" onpageindexchanging="GridViewPlanif_PageIndexChanging">
                <PagerSettings PageButtonCount="5" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                   <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True"  />
                    <asp:BoundField DataField="Fecha_inicio_str" HeaderText="Fecha Inicio" ReadOnly="True"  />
                    <asp:BoundField DataField="Fecha_fin_str" HeaderText="Fecha Fin"  />
                </Columns>
               <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
</div>
</asp:Content>