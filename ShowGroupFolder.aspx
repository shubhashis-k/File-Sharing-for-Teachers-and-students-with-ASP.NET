<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowGroupFolder.aspx.cs" Inherits="ShowGroupFolder" %>
<%@ Register Src="~/User_Control/popUp.ascx" TagPrefix="uc1" TagName="popUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <uc1:popUp runat="server" ID="popUp1" />
                        <asp:Button ID="CreateFile" runat="server" class="info" Text="Create File" Width="179px" OnClick="CreateFile_Click" />
                        <br />
                        <asp:Button ID="DeleteFile" runat="server" class="info" Text="Delete Selected Files" Width="179px" OnClick="DeleteFile_Click" />
                    </li>
                </ul>
            </nav>

            <asp:Label ID="AbilityLabel" runat="server"></asp:Label>
        </div>

        <div style="position:absolute; margin-top:-200px; margin-left:250px; ">
            <h2>
                Files in the Group : 
                <asp:Label ID="GroupNameLabel" runat="server" Text="Label"></asp:Label>
            </h2>
            
            <asp:GridView ID="FolderGridView" runat="server"
            AutoGenerateColumns = "False" 
            AllowPaging ="True"  OnPageIndexChanging ="FolderGridView_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" 
            >
                <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="FolderCheckBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                

                <asp:TemplateField HeaderText="Folder Name">
                    <ItemTemplate>
                        <asp:LinkButton Text='<%#Eval("File Name")%>' PostBackUrl='<%# String.Format("InsideFolder.aspx?FolderName={0}",  Eval("File Name")   ) %>' runat="server" ID="FolderLink" />
                    </ItemTemplate>
                </asp:TemplateField>

                

                <asp:BoundField 
                 DataField = "Expire Date" ItemStyle-Width = "150px" HeaderText = "Expire Date" >
                <ItemStyle Width="150px"></ItemStyle>
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

        <div style="position:absolute; margin-top:200px; margin-left:250px;">
            <asp:Label ID="ConfirmationLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>

