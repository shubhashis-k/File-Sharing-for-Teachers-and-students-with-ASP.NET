using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPage : System.Web.UI.Page
{
    public ArrayList UserIDList = new ArrayList();
    public ArrayList UserNameList = new ArrayList();
    public ArrayList IDTypeList = new ArrayList();

    SqlConnection conn;
    string GroupID;
    protected void Page_Load(object sender, EventArgs e)
    {
        string adminloginstate = (string)Session["Adminloginstate"];

        if (adminloginstate != "1")
            Response.RedirectPermanent("Home.aspx");
        string AdminName = (string)Session["AdminName"];

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        conn = new SqlConnection(connectionString);
        conn.Open();

        SqlCommand getGroupName = new SqlCommand("select GroupName from GroupTable where AdminName = '" + AdminName + "'", conn);
        SqlDataReader GroupNamereader = getGroupName.ExecuteReader();
        GroupNamereader.Read();

        GroupNameLabel.Text = GroupNamereader["GroupName"].ToString();
        GroupNamereader.Close();

        SqlCommand getGroupID = new SqlCommand("select GroupID from GroupTable where AdminName = '" + AdminName + "'", conn);
        SqlDataReader GroupIDreader = getGroupID.ExecuteReader();
        GroupIDreader.Read();

        GroupID = GroupIDreader["GroupID"].ToString();
        GroupIDreader.Close();

        SqlCommand getUserID = new SqlCommand("select UserID from RelationTable where GroupID = '" + GroupID + "' and Status = '0'" , conn);
        SqlDataReader UserIDreader = getUserID.ExecuteReader();

        while (UserIDreader.Read())
        {
            UserIDList.Add(UserIDreader["UserID"].ToString());
        }

        UserIDreader.Close();

        for(int i = 0 ; i < UserIDList.Count ; i++)
        {
            string userName = ConvertToUserName(UserIDList[i].ToString());
            string ID_type = ConvertToIDtype(UserIDList[i].ToString());

            UserNameList.Add(userName);
            IDTypeList.Add(ID_type);

            //Label1.Text += userName + " " + ID_type + "<br/>";
        }

        if (!Page.IsPostBack)
        {
            RequestGridView.DataSource = GetDataTable();
            RequestGridView.DataBind();
        }
    }

    public System.Data.DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("User Name");
        dt.Columns.Add("ID Type");

        for (int i = 0; i < UserNameList.Count; i++)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["User Name"] = UserNameList[i];
            dt.Rows[dt.Rows.Count - 1]["ID Type"] = IDTypeList[i];
        }

        return dt;
    }

    public string ConvertToUserName(string UserID)
    {
        SqlCommand getUserName = new SqlCommand("select UserName from UserTable where UserID = '" + UserID + "'", conn);
        SqlDataReader userIDreader = getUserName.ExecuteReader();
        userIDreader.Read();

        string UserName = userIDreader["UserName"].ToString();

        userIDreader.Close();

        return UserName;
    }

    public string ConvertToIDtype(string UserID)
    {
        SqlCommand getID_type = new SqlCommand("select ID_type from RelationTable where UserID = '" + UserID + "'", conn);
        SqlDataReader ID_typereader = getID_type.ExecuteReader();
        ID_typereader.Read();

        string ID_type = ID_typereader["ID_type"].ToString();

        ID_typereader.Close();

        return ID_type;
    }
    protected void FilterButton_Click(object sender, EventArgs e)
    {
        string adminloginstate = (string)Session["Adminloginstate"];
        if (adminloginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        Session["Group ID"] = GroupID;
        Session["FilterText"] = FilterBox.Text.ToString();
        Session["FilterBy"] = FilterByList.SelectedValue;

        Response.RedirectPermanent("FilterPage.aspx");
    }
    protected void ApproveButton_Click(object sender, EventArgs e)
    {
        string adminloginstate = (string)Session["Adminloginstate"];
        if (adminloginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        for (int i = 0; i < RequestGridView.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)RequestGridView.Rows[i].FindControl("SelectRequest");
            if (chk.Checked == true)
            {
                string UserName = RequestGridView.Rows[i].Cells[1].Text.ToString();
                SqlCommand getUserID = new SqlCommand("select UserID from UserTable where UserName = '" + UserName + "'", conn);
                SqlDataReader userIDreader = getUserID.ExecuteReader();
                userIDreader.Read();

                string UserID = userIDreader["UserID"].ToString();
                userIDreader.Close();

                SqlCommand updateRtable = new SqlCommand("update RelationTable set Status = '1' where UserID = '" + UserID +"' and GroupID = '" + GroupID +"'", conn);
                SqlDataReader updater = updateRtable.ExecuteReader();
            }
        }
        Response.RedirectPermanent("AdminPage.aspx");
    }
    protected void RequestGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RequestGridView.PageIndex = e.NewPageIndex;
        RequestGridView.DataSource = GetDataTable();
        RequestGridView.DataBind();
    }

    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        string adminloginstate = (string)Session["Adminloginstate"];
        if (adminloginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        HttpCookie loginCookie = new HttpCookie("AdminLoginCookie");
        loginCookie.Expires = DateTime.Now.AddDays(-1d);
        Response.Cookies.Add(loginCookie);

        Session.Abandon();
        Response.RedirectPermanent("Home.aspx");
    }
}