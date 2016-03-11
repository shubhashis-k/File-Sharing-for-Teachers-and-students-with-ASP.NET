<%@ Control Language="C#" AutoEventWireup="true" CodeFile="popUp.ascx.cs" Inherits="User_Control_popUp" %>

    <script type="text/javascript">
        var msgBoxTimeout;
        var msgBoxRight = -320; //this the message box right value. This is used as Style attribute

        //Call this function from anywhere with a message and Message Type which is your CSS Class name from enum
        function ShowMessage(msg, type) {
            clearInterval(msgBoxTimeout);
            $("#divMessageBody").css("right", msgBoxRight);

            var classAttr = "message-box " + type;
            $("#divMessage").html(msg);
            $("#divMessageBody").attr("class", classAttr);

            $("#divMessageBody").stop().animate({
                right: "0px"
            }, 700, "easeInOutCirc");

            msgBoxTimeout = setTimeout(function () {
                HideMessage();
            }, timeToShow);
        }

        function HideMessage() {
            $("#divMessageBody").stop().animate({
                right: msgBoxRight
            }, 900, "easeInOutCirc");

            clearInterval(msgBoxTimeout);
        }

    </script>

<div id="divMessageBody" class="message-box" style="background-color:lightblue; width:300px" >
    <a class="close-btn" onclick="HideMessage();">Close</a>
    <br />
    <asp:ScriptManager EnablePartialRendering="true"
     ID="ScriptManager1" runat="server">
     </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server"
     UpdateMode="Conditional">
         <ContentTemplate>
            <asp:TextBox ID="FileBox" runat="server" Width="80px" AutoPostBack="True" CausesValidation="True" OnTextChanged="FileBox_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="File Name Required" ControlToValidate="FileBox" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="ExpireDateBox" runat="server" Width="80px" AutoPostBack="True" CausesValidation="True" OnTextChanged="ExpireDateBox_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ExpireDateBox" Display="Dynamic" EnableClientScript="False" ErrorMessage="RequiredFieldValidator">Date Required</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="ExpireDateBox" Display="Dynamic" EnableClientScript="False" ErrorMessage="RegularExpressionValidator" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$">dd/mm/yy required</asp:RegularExpressionValidator>
            <br />
            <asp:Button ID="CreateFileButton" class="info" runat="server" Text="Create" OnClick="CreateFileButton_Click"/>
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>