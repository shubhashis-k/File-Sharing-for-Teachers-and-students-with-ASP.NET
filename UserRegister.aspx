<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRegister.aspx.cs" Inherits="UserRegister" %>

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
            width: 96px;
        }
        .auto-style3 {
            width: 200px;
        }
        .auto-style4 {
            width: 96px;
            height: 14px;
        }
        .auto-style5 {
            width: 200px;
            height: 14px;
        }
        .auto-style6 {
            height: 14px;
        }
        .auto-style7 {
            width: 266px;
        }
        .auto-style8 {
            height: 14px;
            width: 266px;
        }
    </style>
</head>
<body class="metro">

    <form id="form1" runat="server">
        <asp:ScriptManager EnablePartialRendering="true"
        ID="ScriptManager2" runat="server"></asp:ScriptManager>
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
        <h1 style="text-align:center">Fill up the User Registration Form</h1>
        
        <br />
        <br />
        <br />
        
        <br />
        <br />

         
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">Enter Username</td>
                <td class="auto-style7">
                    <asp:UpdatePanel ID="AdminIDCheckPanel" runat="server"
                     UpdateMode="Conditional">
                        <ContentTemplate>
                        <div class="input-control text">
                            <asp:TextBox ID="UsernameBox" runat="server" AutoPostBack="True" OnTextChanged="UsernameBox_TextChanged"></asp:TextBox>
                            <asp:Label ID="UserNameCheckLabel" runat="server" Visible="False"></asp:Label>
                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UsernameBox" ErrorMessage="Field Must Not be Empty"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">Enter Password</td>
                <td class="auto-style7">
                    <div class="input-control text">
                        <asp:TextBox ID="UserPassBox" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="UserPassBox" ErrorMessage="Field Must not be Empty"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style4"></td>
                <td class="auto-style5">Register As</td>
                <td class="auto-style8">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Width="35px">
                        <asp:ListItem>Teacher</asp:ListItem>
                        <asp:ListItem>Student</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="auto-style6">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadioButtonList1" ErrorMessage="Choose a Type"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Label ID="DesignationLabel" runat="server" Text="Designation" Visible="False"></asp:Label>
                    <asp:Label ID="RollLabel" runat="server" Text="Roll" Visible="False"></asp:Label>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="DesignationBox" runat="server" Width="112px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="RollBox" runat="server" Width="112px" Visible="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">Batch</td>
                <td class="auto-style7">
                    <div class="input-control text">
                        <asp:TextBox ID="BatchBox" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="BatchBox" ErrorMessage="Specify a Batch"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">Department</td>
                <td class="auto-style7">
                    <asp:DropDownList ID="DeptDropDown" runat="server">
                        <asp:ListItem>CSE</asp:ListItem>
                        <asp:ListItem>EEE</asp:ListItem>
                        <asp:ListItem>ECE</asp:ListItem>
                        <asp:ListItem>ME</asp:ListItem>
                        <asp:ListItem>CIVIL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DeptDropDown" ErrorMessage="Choose a Department"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style7">
                    <asp:Button ID="RegisterButton" runat="server" class="info" Text="Register" Height="45px" OnClick="RegisterButton_Click" Width="173px" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

         
        <asp:Label ID="Label1" runat="server"></asp:Label>

         
    </form>


</body>
</html>
