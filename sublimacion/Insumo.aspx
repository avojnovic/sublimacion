<%@ Page Language="C#" MasterPageFile="~/Master.Master"  AutoEventWireup="true" CodeBehind="Insumo.aspx.cs" Inherits="sublimacion.Insumo" %>
<%@ Register Src="~/controls/DatePicker.ascx" TagPrefix="ctrol" TagName="DatePicker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="margin-right: 0px">

        <br/>
         <br/>              

      <asp:Label ID="LblMensaje" ForeColor="Red" runat="server"></asp:Label>
       <br/>  
       <center>
        <asp:Panel runat="server" ID="panel1" Visible="true"  Width="600px" style="background-color:#DDDDDD">
        <table style="width:500px;"  border="0" cellspacing="0"   >
             <tr>
                 <td align="left" style="width:100px;" >
                    <asp:Label ID="Label6" runat="server" Text="Nombre:"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtNombre" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                  </td>
                <td style="width:200px;" >
                    <asp:TextBox ID="TxtNombre" Width="100%" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label2" runat="server" Text="Nombre Fabricante:"></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFabricante" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                </td>
                <td>
                    <asp:TextBox ID="TxtFabricante" Width="100%" runat="server"></asp:TextBox>
                </td>
              
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label3" runat="server" Text="Costo:"></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtCosto" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                </td>
                <td>
                    <asp:TextBox ID="TxtCosto" Width="100%" runat="server"></asp:TextBox>
                     <act:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999.99" MaskType="Number" TargetControlID="TxtCosto"></act:MaskedEditExtender>
                </td>
                
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label4" runat="server" Text="Stock:"></asp:Label>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txtstock" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                </td>
                <td>
                    <asp:TextBox ID="Txtstock" Width="100%" runat="server" ></asp:TextBox>
                    <act:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="9999" MaskType="Number" TargetControlID="Txtstock"></act:MaskedEditExtender>
                </td>
              
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label5" runat="server" Text="Fecha Actualizacion:"></asp:Label>
                     
                </td>
                <td>
                    <asp:TextBox ID="TxtFecha" Width="100%" runat="server" ReadOnly="True"></asp:TextBox>

                </td>
               
            </tr>
            <tr>
                <td align="center" colspan="2">
                  <br/>
                    <asp:ImageButton ID="ImageButton1" Width="32px" Height="32px" ImageUrl="~/Images/Save.png" runat="server" ValidationGroup="add" onclick="Button1_Click" ToolTip="Guardar" />
                     <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png" runat="server" onclick="BtnBorrar_Click" ToolTip="Borrar" />
                     <asp:ImageButton ID="ImageButton2" Width="32px" Height="32px" ImageUrl="~/Images/return.png" runat="server" onclick="BtnSalir_Click" ToolTip="Salir" />
                </td>

            </tr>
        </table>
          </asp:Panel>
         <act:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="panel1" Radius="8" Color="#DDDDDD" Corners="All" Enabled="true"/>
                       

    
    </div>
        
    
</asp:Content>