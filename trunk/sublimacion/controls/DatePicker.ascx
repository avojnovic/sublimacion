<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="GestPro.controls.DatePicker" %>
<asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlDatePicker">
            <asp:TextBox runat="server" ID="txtDay" Text="" MaxLength="2" ReadOnly="true" Width="15px" />-
            <asp:TextBox runat="server" ID="txtMonth" Text="" MaxLength="2" ReadOnly="true" Width="15px"/>-
            <asp:TextBox runat="server" ID="txtYear" Text="" MaxLength="4" ReadOnly="true" Width="30px"/>
            <asp:Button runat="server" ID="btnShowCalendar" Text="..." OnClick="btnShowCalendar_Click"  Width="22px"/>
            <%--<input type="button" value="..." onclick="javascript:ShowCalendar();" />--%>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlCalendar" style="position:absolute;">
            <asp:Calendar runat="server" ID="calDate" BackColor="Gray" OnSelectionChanged="calDate_OnSelectionChanged" />
            <div style="background-color:Gray; text-align:center;"><asp:LinkButton runat="server" ID="lnkClear" Text="Limpiar" OnClick="lnkClear_Click" /></div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript">
    function ShowCalendar()
    {
        var div = $get("<%= pnlCalendar.ClientID %>");
        if (div)
        {
            if (div.style.display=='none')
            {
                div.style.display = 'block';
            }   
            else
            {
                div.style.display = 'none';
            }
        }
    }
</script>