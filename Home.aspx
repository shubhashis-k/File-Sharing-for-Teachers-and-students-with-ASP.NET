<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="navigation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/metro-bootstrap.css" rel="stylesheet" />
    <link href="Content/metro-bootstrap-responsive.css" rel="stylesheet" />
    <script src="Scripts/Metro/metro-dropdown.js"></script>
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery.widget.min.js"></script>
    <script src="Scripts/metro.min.js"></script>
    <link href="loginStyle.css" rel="stylesheet" />
    <style>
        body {background-image:url("HomeImage.jpg");}
    </style>

    <title></title>
</head>
<body class="metro" >
    <form id="form1" runat="server">
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

     <h1 style="text-align:center">Welcome To Homework Manager</h1> <br /><br />

     <p style="text-align:center; font-size:30px; font-family:Segoe UI Light;">Managing homework and assignment will be easy with this website.</p>

        <br /><br />
    <p style="text-align:center; font-size:30px; font-family:Segoe UI Light;">Register and Have a look around!!!</p>

    </form>
</body>
</html>
