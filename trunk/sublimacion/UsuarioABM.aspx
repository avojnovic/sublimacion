<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"
    CodeBehind="UsuarioABM.aspx.cs" Inherits="sublimacion.UsuarioABM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-right: 0px">
        <br />
        <br />
        <asp:Label ID="LblMensaje" Font-Names="Calibri" Font-Size="Small" ForeColor="Red"
            runat="server"></asp:Label>
        <br />
        <center>
            <asp:Panel runat="server" ID="panel1" Visible="true" Width="600px" Style="background-color: #DDDDDD">
                <table style="width: 500px;" border="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label1" runat="server" Text="Nombre:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtNombre"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtNombre" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label2" runat="server" Text="Apellido:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtApellido"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtApellido" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label3" runat="server" Text="Mail:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtMail"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtMail" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label5" runat="server" Text="Telefono:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtTelefono"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtTelefono" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label6" runat="server" Text="Usuario:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtUsuario"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtUsuario" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px;">
                            <asp:Label ID="Label7" runat="server" Text="Password:" Font-Names="Calibri"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtPassword"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
                        </td>
                        <td style="width: 200px;">
                            <asp:TextBox ID="TxtPassword" Width="100%" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
            <td align="left">
                <asp:Label ID="Label4" runat="server" Text="Perfil" Font-Names="Calibri"></asp:Label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="CmbPerfil" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="add" />
            </td>
            <td>
                <asp:DropDownList ID="CmbPerfil" runat="server" Width="100%"/>
            </td>
        </tr>

 <tr>

                    <td align="center" colspan="2">
                        <br />
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="32px" 
                            ImageUrl="~/Images/Save.png" OnClick="BtnGuardar_Click" ToolTip="Guardar" 
                            ValidationGroup="add" Width="32px" />
                        <asp:ImageButton ID="BtnBorrar" runat="server" Height="32px" 
                            ImageUrl="~/Images/Trash.png" OnClick="BtnBorrar_Click" ToolTip="Borrar" 
                            Width="32px" />
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="32px" 
                            ImageUrl="~/Images/return.png" OnClick="BtnSalir_Click" ToolTip="Salir" 
                            Width="32px" />
                    </td>
                </table>
            </asp:Panel>
            <act:roundedcornersextender id="RoundedCornersExtender2" runat="server" targetcontrolid="panel1"
                radius="8" color="#DDDDDD" corners="All" enabled="true" />
        </center>
    </div>
</asp:Content>
