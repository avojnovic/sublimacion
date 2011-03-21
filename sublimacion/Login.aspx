<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="sublimacion.Login" %>

<html>
<head runat="server">
    <title>Login Sublimación</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
    <center>
        <asp:Login ID="LoginCmp" runat="server" DestinationPageUrl="~/Default.aspx" 
            DisplayRememberMe="False" onauthenticate="LoginCmp_Authenticate" 
            onloggingin="LoginCmp_LoggingIn" 
            FailureText="Usuario o Constraseña incorrectos, intentelo nuevamente.">
            <TitleTextStyle Font-Bold="True" />
        </asp:Login>
    </center>
    </div>
    </form>
</body>
</html>
