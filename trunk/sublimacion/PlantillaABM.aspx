<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="PlantillaABM.aspx.cs" Inherits="sublimacion.PlantillaABM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-right: 0px">
        <br />
        <br />
        <asp:Label ID="LblMensaje" ForeColor="Red" runat="server"></asp:Label>
        <br />
        <center>
            <asp:Panel runat="server" ID="panel1" Visible="true" Width="600px" Style="background-color: #DDDDDD">
                <table style="width: 500px;" border="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label6" runat="server" Text="Nombre:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtNombre"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtNombre" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label2" runat="server" Text="Medida Largo:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtLargo"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td>
                            <asp:TextBox ID="TxtLargo" Width="100%" runat="server"></asp:TextBox>
                            <act:maskededitextender id="MaskedEditExtender1" runat="server" mask="9999.99" masktype="Number"
                                targetcontrolid="TxtLargo"></act:maskededitextender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label3" runat="server" Text="Medida Ancho:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtAncho"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td>
                            <asp:TextBox ID="TxtAncho" Width="100%" runat="server"></asp:TextBox>
                            <act:maskededitextender id="MaskedEditExtender2" runat="server" mask="9999.99" masktype="Number"
                                targetcontrolid="TxtAncho"></act:maskededitextender>
                        </td>
                    </tr>
                       <tr>
                <td align="center" colspan="2">
                  <br/>
                    <asp:ImageButton ID="BtnGuardar" Width="32px" Height="32px" ImageUrl="~/Images/Save.png" runat="server" ValidationGroup="add" onclick="BtnGuardar_Click" ToolTip="Guardar" />
                     <asp:ImageButton ID="BtnBorrar" Width="32px" Height="32px" ImageUrl="~/Images/Trash.png" runat="server" onclick="BtnBorrar_Click" ToolTip="Borrar" />
                     <asp:ImageButton ID="BtnSalir" Width="32px" Height="32px" ImageUrl="~/Images/return.png" runat="server" onclick="BtnSalir_Click" ToolTip="Salir" />
                </td>

            </tr>
                  
                </table>
            </asp:Panel>
            <act:roundedcornersextender id="RoundedCornersExtender2" runat="server" targetcontrolid="panel1"
                radius="8" color="#DDDDDD" corners="All" enabled="true" />
                  </center>
    </div>
</asp:Content>
