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

public partial class FindGroup : System.Web.UI.Page
{
    public ArrayList GroupNameID = new ArrayList();
    public ArrayList GroupNameList = new ArrayList();
    public string UserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginstate"] != "1")
            Response.RedirectPermanent("Home.aspx");

        string SearchGroup = (string)Session["SearchGroup"];
        UserID = (string)Session["UserID"];

        Session["SearchGroup"] = null;

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);

        conn.Open();
        SqlCommand getGroupName = new SqlCommand("SELECT GroupName FROM GroupTable WHERE GroupName like '%" + SearchGroup + "%'", conn);
        SqlDataReader GroupNamereader = getGroupName.ExecuteReader();

        while (GroupNamereader.Read())
        {
            GroupNameList.Add(GroupNamereader["GroupName"].ToString());
        }

        GroupNamereader.Close();
        for (int i = 0; i < GroupNameID.Count; i++)
        {
            SqlCommand getGroupID = new SqlCommand("SELECT GroupID FROM GroupTable WHERE GroupName =" + "'" + GroupNameList[i] + "'", conn);
            SqlDataReader GroupIDreader = getGroupID.ExecuteReader();

            GroupIDreader.Read();

            GroupNameID.Add(GroupIDreader["GroupID"].ToString());

            GroupIDreader.Close();
        }


        if (!Page.IsPostBack)
        {
            GroupGridView.DataSource = GetDataTable();
            GroupGridView.DataBind();
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
    protected void RequestButton_Click(object sender, EventArgs e)
    {
        if (Session["loginstate"] != "1")
            Response.RedirectPermanent("Home.aspx");

        ConfirmationLabel.Text = "";
        for (int i = 0; i < GroupGridView.Rows.Count;i++ )
        {
            CheckBox chk = (CheckBox)GroupGridView.Rows[i].FindControl("GroupCheckBox");
            if (chk.Checked == true)
            {
                 string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                 SqlConnection conn = new SqlConnection(connectionString);
                 conn.Open();

                 string grp = GroupGridView.Rows[i].Cells[1].Text.ToString();

                 SqlCommand getGroupID = new SqlCommand("SELECT GroupID FROM GroupTable WHERE GroupName =" + "'" + grp + "'", conn);
                 SqlDataReader grpIDreader = getGroupID.ExecuteReader();

                 grpIDreader.Read();

                 string gid = grpIDreader["GroupID"].ToString();

                 grpIDreader.Close();

                 SqlCommand getIDtype = new SqlCommand("SELECT ID_type FROM UserTable WHERE UserID=" + "'" + UserID + "'", conn);
                 SqlDataReader IDtypereader = getIDtype.ExecuteReader();

                 IDtypereader.Read();

                 string IDtype = IDtypereader["ID_type"].ToString();

                 IDtypereader.Close();
                 try
                 {
                     SqlCommand relcomm = new SqlCommand("select RelationID from RelationTable where GroupID = '" + gid + "' and UserID = '" + UserID + "'", conn);
                     SqlDataReader findrel = relcomm.ExecuteReader();
                     findrel.Read();
                     try
                     {
                         string st = findrel["RelationID"].ToString();
                         ConfirmationLabel.Text += "Already a member or Already sent Request for " + grp + "<br/>" ;
                         findrel.Close();
                     }
                     catch
                     {
                         findrel.Close();
                         SqlCommand comm = new SqlCommand("Insert into RelationTable(UserID,GroupID,ID_type,Status) Values('" + UserID + "','" + gid + "','" + IDtype + "','" + "0" + "')", conn);
                         SqlDataReader reader = comm.ExecuteReader();
                         reader.Close();
                         conn.Close();
                         ConfirmationLabel.Text += "Join Requests Sent for " + grp + "<br/>";
                     }
                 }
                 catch
                 {

                 }
            }

        }
    }

    protected void GroupGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GroupGridView.PageIndex = e.NewPageIndex;
        GroupGridView.DataSource = GetDataTable();
        GroupGridView.DataBind();
    }


    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        if (Session["loginstate"] != "1")
            Response.RedirectPermanent("Home.aspx");
        Session.Abandon();
        Response.RedirectPermanent("Home.aspx");
    }
}