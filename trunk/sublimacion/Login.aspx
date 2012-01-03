<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="sublimacion.Login" %>

<html>
<head runat="server">
    <title>Login Sublimación</title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <div>
        <center>
            <asp:Image ID="Image1" runat="server" ImageAlign="Middle" 
                ImageUrl="~/Images/logosuperior.PNG" Width= "100%" />
        </center>
        </div>
    <center>
    <br />
    <br />
    <br />
        <asp:Login ID="LoginCmp" runat="server" DestinationPageUrl="~/Default.aspx" 
            DisplayRememberMe="False" onauthenticate="LoginCmp_Authenticate" 
             TextBoxStyle-Width="150"  BackColor="#DDDDDD" BorderColor="#DDDDDD"   
            onloggingin="LoginCmp_LoggingIn" 
            FailureText="Usuario o Constraseña incorrectos, intentelo nuevamente.">
            <TitleTextStyle Font-Bold="True" />
        </asp:Login>
       
    </center>
    </div>
    </form>
</body>
</html>
