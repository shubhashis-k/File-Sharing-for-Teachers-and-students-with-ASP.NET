<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateGroup.aspx.cs" Inherits="Dynamic_Box" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="Content/metro-bootstrap.css" rel="stylesheet" />
    <link href="Content/metro-bootstrap-responsive.css" rel="stylesheet" />
    <script src="Scripts/Metro/metro-dropdown.js"></script>
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery.widget.min.js"></script>
    <script src="Scripts/metro.min.js"></script>
    <link href="loginStyle.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 147px;
        }
        .auto-style3 {
            width: 29px;
        }
        .auto-style4 {
            width: 319px;
        }
    </style>
</head>
<body class="metro">

    <form id="form1" runat="server">
        <asp:ScriptManager EnablePartialRendering="true"
        ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
        
        <br />
        <br />
        <h1 style="text-align:center">Fill up the Form</h1>
        
        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style2">Enter Group Name</td>
                <td class="auto-style4">
                    <asp:UpdatePanel ID="GroupNameCheckPanel" runat="server"
                     UpdateMode="Conditional">
                        <ContentTemplate>
                        <div class="input-control text">
                        <asp:TextBox ID="GroupNameBox" runat="server" AutoPostBack="True" OnTextChanged="GroupNameBox_TextChanged"></asp:TextBox> 
                        </div>
                        <asp:Label ID="GroupNameDupCheck" runat="server" Visible="False"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="GroupNameBox" ErrorMessage="Field must not be Empty"></asp:RequiredFieldValidator>
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style2">Enter Admin ID</td>
                <td class="auto-style4">
                    <asp:UpdatePanel ID="AdminIDCheckPanel" runat="server"
                     UpdateMode="Conditional">
                        <ContentTemplate>
                        <div class="input-control text">
                        <asp:TextBox ID="AdminIDBox" runat="server" AutoPostBack="True" OnTextChanged="AdminIDBox_TextChanged"></asp:TextBox>
                        <asp:Label ID="AdminIDDupCheck" runat="server" Visible="False"></asp:Label>    
                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="AdminIDBox" ErrorMessage="Field must not be Empty"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style2">Enter Admin Password</td>
                <td class="auto-style4">
                    <div class="input-control text">
                    <asp:TextBox ID="AdminPassBox" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="AdminPassBox" ErrorMessage="Field must not be Empty"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style4">
                    <asp:Button ID="CreateButton" runat="server" Height="50px" Text="Create Group" class="info" Width="189px" OnClick="CreateButton_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />

        
        <asp:Label ID="Label1" runat="server"></asp:Label>

        
        <asp:Label ID="Success" runat="server" Visible="False"></asp:Label>

        
    </form>


</body>
</html>