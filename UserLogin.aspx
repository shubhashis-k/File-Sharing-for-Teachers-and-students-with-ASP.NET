<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="UserLogin" %>
<%@ Register Src="~/User_Control/Message.ascx" TagPrefix="uc1" TagName="Message" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="Content/metro-bootstrap.css" rel="stylesheet" />
    <link href="Content/metro-bootstrap-responsive.css" rel="stylesheet" />
    <script src="Scripts/Metro/metro-dropdown.js"></script>
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery.widget.min.js"></script>
    <script src="Scripts/metro.min.js"></script>
    <script src="Scripts/jquery.ui.effect.js"></script>
    <link href="errorMessageLayout.css" rel="stylesheet" />
    <link href="loginStyle.css" rel="stylesheet" />
    <title></title>
</head>

<body class="metro">

    <form id="form1" runat="server">

        <uc1:Message ID="ucMessage" runat="server" />
        <div>
        <nav class="navigation-bar">
            <nav class="navigation-bar-content">

                <a class="element brand" href="#"><span class="icon-home"></span> Home</a>

                 <a class="element brand" href="#"><span class="icon-help"></span> Help</a>

                <span class="element-divider"></span>

                <span class="element-divider place-right"></span>
                <div class="element place-right">
                    <a class="dropdown dropdown-toggle" href="#"><span class="icon-safari"></span> Register</a>
                    <ul class="dropdown-menu place-right" data-role="dropdown">
                        <li><a href="CreateGroup.aspx">Create Group</a></li>
                        <li><a href="UserRegister.aspx">Register User</a></li>
                    </ul>
                </div>
                <span class="element-divider place-right"></span>
                <div class="element place-right">
                    <a class="dropdown dropdown-toggle" href="#"><span class="icon-key"></span> Login</a>
                    <ul class="dropdown-menu place-right" data-role="dropdown">
                        <li><a href="AdminLogin.aspx">Admin Login</a></li>
                        <li><a href="UserLogin.aspx">User Login</a></li>
                    </ul>
                </div>

            </nav>
        </nav>
        </div>
        <h1 style="text-align:center">Log in as User</h1>
        <div class="loginholder">
            <div class="input-control text" style="margin-top:140px">
                <asp:TextBox ID="UserNameBox" runat="server" value="" placeholder="Enter Username" ></asp:TextBox>
                <asp:TextBox ID="UserPasswordBox" runat="server" placeholder="Enter Password" TextMode="Password"></asp:TextBox>    
                              
            </div>
            <div style="margin-top:10px">
                <asp:CheckBox ID="RememberMe" runat="server" Text="" /><asp:Label ID="Label1" runat="server" Text="Remember Me"></asp:Label>
            </div>
            <div style="margin-top:15px">
                <asp:Button ID="LoginButton" runat="server" Text="LOGIN" class="info" style="height:40px;width:350px" OnClick="LoginButton_Click"/>
                
            </div>
        </div>    

    </form>


</body>
</html>

