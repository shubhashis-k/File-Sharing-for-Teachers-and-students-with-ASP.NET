<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Message.ascx.cs" Inherits="User_Control_Message" %>

    <script type="text/javascript">
        var msgBoxTimeout;
        var timeToShow = 3000; //The message will hide after this time interval. Currently it is 10 seconds
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

<div id="divMessageBody" class="message-box">
    <a class="close-btn" onclick="HideMessage();">x</a>
    <div id="divMessage" class="message"></div>
</div>