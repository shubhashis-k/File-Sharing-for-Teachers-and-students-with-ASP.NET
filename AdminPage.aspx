<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

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
                        <asp:Label ID="WelcomeLabel" runat="server" Text="Welcome Admin"></asp:Label>
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
                            <asp:TextBox ID="FilterBox" runat="server" placeholder="Enter Text"></asp:TextBox>
                        </div>
                    </li>

                    <li>
                        <div>
                            Filter By
                            <asp:DropDownList ID="FilterByList" runat="server">
                                <asp:ListItem>UserName</asp:ListItem>
                                <asp:ListItem>Designation</asp:ListItem>
                                <asp:ListItem>Roll</asp:ListItem>
                                <asp:ListItem>Batch</asp:ListItem>
                                <asp:ListItem>Department</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li>
                        <asp:Button ID="FilterButton" class="info" runat="server" Text="Filter" Height="44px" Width="145px" OnClick="FilterButton_Click" /></li>
                    <li>
                        <a href="AdminPage.aspx">Requests</a>
                    </li>
                </ul>
            </nav>
        </div>

        <div style="position:absolute; margin-top:-200px; margin-left:250px; ">
            <h2>
                Pending Requests to Your Group : <asp:Label ID="GroupNameLabel" runat="server"></asp:Label>
            </h2>

            <asp:GridView ID="RequestGridView" runat="server" AutoGenerateColumns = "False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="true" PageSize ="10"  OnPageIndexChanging ="RequestGridView_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                
                    <asp:TemplateField HeaderText="Check">
                        <ItemTemplate>
                            <asp:CheckBox ID="SelectRequest" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                
                <asp:BoundField ItemStyle-Width = "150px"
                 DataField = "User Name" HeaderText = "User Name"> <ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField ItemStyle-Width = "150px"
                 DataField = "ID Type" HeaderText = "Will Join As"> <ItemStyle Width="150px"></ItemStyle>
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

        <div style="position:absolute; margin-top:210px; margin-left:250px;">
            <asp:Label ID="ConfirmationLabel" runat="server" Text=""></asp:Label>
        </div>


        <div style="position:absolute; margin-top:-230px; margin-left:250px;">
            <asp:Button ID="ApproveButton" runat="server" class="info" Text="Approve Selected" Width="162px" OnClick="ApproveButton_Click" />
        </div>
    </form>
</body>
</html>
