<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindGroup.aspx.cs" Inherits="FindGroup" %>

<!DOCTYPE html>

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
    <title></title>
</head>
<body class="metro">
    <form id="form1" runat="server">
        <div>
            <nav class="navigation-bar">
                <nav class="navigation-bar-content">

                    <a class="element brand" href="#"><span class="icon-home"></span> Home</a>

                     <a class="element brand" href="#"><span class="icon-help"></span> Help</a>

                    <span class="element-divider"></span>

                    <span class="element-divider place-right"></span>
                    <div class="element place-right">
                        <a class="dropdown-toggle" href="#">
                            <span class="icon-cog"></span>
                        </a>
                        <ul class="dropdown-menu place-right" data-role="dropdown">
                            <asp:Button ID="LogoutButton" runat="server" Height="40px" Width="230px" class="primary" Text="Logout" OnClick="LogoutButton_Click"/>
                        </ul>
                    </div>
                    <span class="element-divider place-right"></span>
                    <div class="element place-right">
                        <asp:Label ID="WelcomeLabel" runat="server" Text="Welcome User"></asp:Label>
                    </div>

                </nav>
            </nav>
        </div>

        <div style="width:200px; margin-top:100px">
            <nav class="vertical-menu">
                <ul>
                    <li class="title">Menu</li>
                    <li>
                        <div class="input-control text">
                            <asp:TextBox ID="GroupName" runat="server"></asp:TextBox>
                        </div>
                    </li>
                    <li>
                        <asp:Button ID="FindGroupButton" class="info" runat="server" Text="Find Group" Height="44px" Width="145px" OnClick="FindGroupButton_Click" /></li>
                    <li>
                        <a href="UserPage.aspx">Joined Groups</a>
                    </li>
                </ul>
            </nav>
        </div>

        <div style="position:absolute; margin-top:-200px; margin-left:250px; ">
            <h2>
                Search Results: 
            </h2>
            
            <asp:GridView ID="GroupGridView" runat="server"
            AutoGenerateColumns = "False" 
            AllowPaging ="True" CellPadding="4" ForeColor="#333333" GridLines="None"  OnPageIndexChanging ="GroupGridView_PageIndexChanging"
            >
                <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:TemplateField HeaderText="Check">
                    <ItemTemplate>
                        <asp:CheckBox ID="GroupCheckBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField ItemStyle-Width = "150px"
                 DataField = "Group Name" HeaderText = "Group Name" > <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>
                
            </Columns>
            
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            
        </asp:GridView>
        </div>

        <div style="position:absolute; margin-top:190px; margin-left:250px;">
            <asp:Label ID="ConfirmationLabel" runat="server" Text=""></asp:Label>
        </div>

        <div style="position:absolute; margin-top:-230px; margin-left:250px;">
            <asp:Button ID="RequestButton" runat="server" class="info" Text="Send Join Request" Width="162px" OnClick="RequestButton_Click" />
        </div>

    </form>
</body>
</html>

