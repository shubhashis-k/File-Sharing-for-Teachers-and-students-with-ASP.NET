<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadPopUp.ascx.cs" Inherits="User_Control_Upload" %>

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

<div id="divMessageBody" class="message-box" style="background-color:lightblue">
    <a class="close-btn" onclick="HideMessage();">Close</a>
    <asp:FileUpload ID="FileUploader" runat="server" />
    <br />
    <asp:Button ID="UploadButton" runat="server" Class="info" Height="27px" Text="Upload" Width="61px" OnClick="UploadButton_Click" />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <br />
    
</div>