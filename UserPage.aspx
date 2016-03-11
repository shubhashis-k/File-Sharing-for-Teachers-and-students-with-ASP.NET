<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserPage.aspx.cs" Inherits="UserPage" %>

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
                            <li>
                                <asp:Button ID="LogoutButton" runat="server" Height="40px" Width="230px" class="primary" Text="Logout" OnClick="LogoutButton_Click"/>
                            </li>
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
                You have access to following groups: 
            </h2>
            
            <asp:GridView ID="JoinedGroupGridView" runat="server"
            AutoGenerateColumns = "False" 
            AllowPaging ="True" CellPadding="4" ForeColor="#333333" GridLines="None"  PageSize ="10"  OnPageIndexChanging ="JoinedGroupGridView_PageIndexChanging"
            >
                <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:BoundField ItemStyle-Width = "150px"
                 DataField = "Group Name" HeaderText = "Group Name" DataFormatString="<a href= ShowGroupFolder.aspx?Name={0}>{0}</a>" HtmlEncodeFormatString="False"> <ItemStyle Width="150px"></ItemStyle>
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
    </form>
</body>
</html>
