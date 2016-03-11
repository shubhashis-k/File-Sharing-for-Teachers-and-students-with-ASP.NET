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

public partial class UserPage : System.Web.UI.Page
{
    public ArrayList GroupNameID = new ArrayList();
    public ArrayList GroupNameList = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        string loginstate = (string)Session["loginstate"];

        if (loginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        string loggedUserName = (string)Session["UserName"];

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);

        conn.Open();

        SqlCommand getUserID = new SqlCommand("SELECT UserID FROM UserTable WHERE UserName = '" + loggedUserName + "'", conn);
        SqlDataReader UserIDreader = getUserID.ExecuteReader();

        UserIDreader.Read();

        string userID = UserIDreader["UserID"].ToString();

        Session["UserID"] = userID;

        UserIDreader.Close();

        SqlCommand getGroupID = new SqlCommand("SELECT GroupID FROM RelationTable WHERE UserID = '" + userID + "' and Status = '1'", conn);
        SqlDataReader GroupIDreader = getGroupID.ExecuteReader();

        while (GroupIDreader.Read())
        {
            GroupNameID.Add(GroupIDreader["GroupID"].ToString());
        }

        GroupIDreader.Close();
        for (int i = 0; i < GroupNameID.Count; i++)
        {
            SqlCommand getGroupName = new SqlCommand("SELECT GroupName FROM GroupTable WHERE GroupID =" + "'" + GroupNameID[i] +"'", conn);
            SqlDataReader GroupNamereader = getGroupName.ExecuteReader();

            GroupNamereader.Read();

            GroupNameList.Add(GroupNamereader["GroupName"].ToString());

            GroupNamereader.Close();
        }


        if (!Page.IsPostBack)
        {
            JoinedGroupGridView.DataSource = GetDataTable();
            JoinedGroupGridView.DataBind();
        }
    }

    public System.Data.DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Group Name");

        for (int i = 0; i < GroupNameList.Count; i++)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["Group Name"] = GroupNameList[i];

        }

        return dt;
    }
    protected void FindGroupButton_Click(object sender, EventArgs e)
    {
        if (Session["loginstate"] != "1")
            Response.RedirectPermanent("Home.aspx");
        Session["SearchGroup"] = GroupName.Text;
        Response.RedirectPermanent("FindGroup.aspx");
    }

    protected void JoinedGroupGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        JoinedGroupGridView.PageIndex = e.NewPageIndex;
        JoinedGroupGridView.DataSource = GetDataTable();
        JoinedGroupGridView.DataBind();
    }


    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        HttpCookie loginCookie = new HttpCookie("UserLoginCookie");
        loginCookie.Expires = DateTime.Now.AddDays(-1d);
        Response.Cookies.Add(loginCookie);
        Response.RedirectPermanent("Home.aspx");
    }
}