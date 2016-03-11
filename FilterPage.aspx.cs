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

public partial class FilterPage : System.Web.UI.Page
{
    public ArrayList UserNameList = new ArrayList();
    string GroupID;
    SqlConnection conn;
    protected void Page_Load(object sender, EventArgs e)
    {
        string adminloginstate = (string)Session["Adminloginstate"];

        if (adminloginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        string FilterText = (string)Session["FilterText"];
        string FilterBy = (string)Session["FilterBy"];
        GroupID = (string)Session["Group ID"];

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        conn = new SqlConnection(connectionString);
        conn.Open();

        SqlCommand comm = new SqlCommand("select UserName from UserTable where "+ FilterBy +" like '%"+FilterText+"%'", conn);
        SqlDataReader reader = comm.ExecuteReader();

        while(reader.Read())
        {
            UserNameList.Add(reader["UserName"].ToString());
        }

        reader.Close();
        if (!Page.IsPostBack)
        {
            FilterGridView.DataSource = GetDataTable();
            FilterGridView.DataBind();
        }

    }

    protected void FilterButton_Click(object sender, EventArgs e)
    {
        string adminloginstate = (string)Session["Adminloginstate"];

        if (adminloginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        Session["FilterText"] = FilterBox.Text;
        Session["FilterBy"] = FilterByList.SelectedValue;

        Response.RedirectPermanent("FilterPage.aspx");
    }

    public System.Data.DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("User Name");

        for (int i = 0; i < UserNameList.Count; i++)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["User Name"] = UserNameList[i];
        }

        return dt;
    }
    protected void MakeTeacherButton_Click(object sender, EventArgs e)
    {
        string adminloginstate = (string)Session["Adminloginstate"];

        if (adminloginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        for (int i = 0; i < FilterGridView.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)FilterGridView.Rows[i].FindControl("UserCheckBox");
            if (chk.Checked == true)
            {
                string UserName = FilterGridView.Rows[i].Cells[1].Text.ToString();
                SqlCommand getUserID = new SqlCommand("select UserID from UserTable where UserName = '" + UserName + "'", conn);
                SqlDataReader userIDreader = getUserID.ExecuteReader();
                userIDreader.Read();
                
                string UserID = userIDreader["UserID"].ToString();
                userIDreader.Close();

                SqlCommand checkStatus = new SqlCommand("select RelationID from RelationTable where UserID = '" + UserID + "' and GroupID = '"+ GroupID +"'", conn);
                SqlDataReader checkStatusreader = checkStatus.ExecuteReader();
                checkStatusreader.Read();

                try
                {
                    string relid = checkStatusreader["RelationID"].ToString();
                    ConfirmationLabel.Text = UserName + " Already a Member of the Group. Or The Request is pending.";
                    checkStatusreader.Close();
                }
                catch
                {
                    checkStatusreader.Close();
                    SqlCommand comm = new SqlCommand("Insert into RelationTable(UserID,GroupID,ID_type,Status) Values('" + UserID + "','" + GroupID + "','" + "Teacher" + "','" + "1" + "')", conn);
                    SqlDataReader reader = comm.ExecuteReader();
                    reader.Close();
                    ConfirmationLabel.Text = UserName + " Made Teacher Successfully!";
                }
            }
        }

    }
    protected void MakeStudentButton_Click(object sender, EventArgs e)
    {
        string adminloginstate = (string)Session["Adminloginstate"];

        if (adminloginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        for (int i = 0; i < FilterGridView.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)FilterGridView.Rows[i].FindControl("UserCheckBox");
            if (chk.Checked == true)
            {
                string UserName = FilterGridView.Rows[i].Cells[1].Text.ToString();
                SqlCommand getUserID = new SqlCommand("select UserID from UserTable where UserName = '" + UserName + "'", conn);
                SqlDataReader userIDreader = getUserID.ExecuteReader();
                userIDreader.Read();

                string UserID = userIDreader["UserID"].ToString();
                userIDreader.Close();

                SqlCommand checkStatus = new SqlCommand("select RelationID from RelationTable where UserID = '" + UserID + "' and GroupID = '" + GroupID + "'", conn);
                SqlDataReader checkStatusreader = checkStatus.ExecuteReader();
                checkStatusreader.Read();

                try
                {
                    string relid = checkStatusreader["RelationID"].ToString();
                    ConfirmationLabel.Text = UserName + " Already a Member of the Group. Or The Request is pending.";
                    checkStatusreader.Close();
                }
                catch
                {
                    checkStatusreader.Close();
                    SqlCommand comm = new SqlCommand("Insert into RelationTable(UserID,GroupID,ID_type,Status) Values('" + UserID + "','" + GroupID + "','" + "Student" + "','" + "1" + "')", conn);
                    SqlDataReader reader = comm.ExecuteReader();
                    reader.Close();
                    ConfirmationLabel.Text = UserName + " Made Student Successfully!";
                }
            }
        }
    }

    protected void FilterGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FilterGridView.PageIndex = e.NewPageIndex;
        FilterGridView.DataSource = GetDataTable();
        FilterGridView.DataBind();
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