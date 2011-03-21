<%@ Page Language="C#"  MasterPageFile="~/Master.Master"   AutoEventWireup="true" CodeBehind="VerLogistica.aspx.cs" Inherits="sublimacion.VerLogistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div>
        
        <asp:GridView ID="GridViewPlanif" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" 
                GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid" 
                BorderWidth="2px" HorizontalAlign="Center" Width="100%" Font-Size="Small" 
                Font-Names="Frutiger-Roman" PageSize="8" onpageindexchanging="GridViewPlanif_PageIndexChanging"  
            >
                <PagerSettings PageButtonCount="5" />
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                   <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True"  />
                    <asp:BoundField DataField="Fecha_inicio_str" HeaderText="Fecha Inicio" ReadOnly="True"  />
                    <asp:BoundField DataField="Fecha_fin_str" HeaderText="Fecha Fin"  />
                  
                </Columns>
               
                <SelectedRowStyle BackColor="Silver" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
               
                <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
               
            </asp:GridView>
        </div>
 </asp:Content>