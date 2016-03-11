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
using Helper;
using System.IO;
public partial class ShowGroupFolder : System.Web.UI.Page
{
    public ArrayList FolderName = new ArrayList();
    public ArrayList TimeOut = new ArrayList();
    string GroupName, GroupID, UserID, IDtype;
    protected void Page_Load(object sender, EventArgs e)
    {
        string loginstate = (string)Session["loginstate"];

        if (loginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        GroupName = "";
        if (Request.QueryString["Name"] != null)
        {
            GroupName = Request.QueryString["Name"].ToString();
        }
        else if (Session["grpName"] != null)
        {
            GroupName = (string)Session["grpName"];
        }

        UserID = (string)Session["UserID"];

        GroupNameLabel.Text = GroupName;
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        SqlCommand gcomm = new SqlCommand("SELECT GroupID FROM GroupTable WHERE GroupName = '" + GroupName + "'", conn);
        SqlDataReader GroupIDreader = gcomm.ExecuteReader();

        GroupIDreader.Read();
        GroupID = GroupIDreader["GroupID"].ToString();
        GroupIDreader.Close();

        
        SqlCommand verify = new SqlCommand("SELECT Status FROM RelationTable WHERE GroupID = '" + GroupID + "' and UserID = '" + UserID + "'", conn);
        SqlDataReader verifyreader = verify.ExecuteReader();

        try
        {
            verifyreader.Read();
            if (verifyreader["Status"].ToString() == "0")
            {
                Response.RedirectPermanent("UserPage.aspx");
            }
        }
        catch
        {
            Response.RedirectPermanent("UserPage.aspx");
        }
        verifyreader.Close();

        
        SqlCommand getIDtype = new SqlCommand("SELECT ID_type FROM RelationTable WHERE GroupID = '" + GroupID + "' and UserID = '" + UserID + "'", conn);
        SqlDataReader getIDtypereader = getIDtype.ExecuteReader();
        getIDtypereader.Read();
        if (getIDtypereader["ID_type"].ToString() == "Student")
        {
            IDtype = getIDtypereader["ID_type"].ToString();
        }
        getIDtypereader.Close();

        SqlCommand fcomm = new SqlCommand("SELECT FolderName FROM FolderTable WHERE GroupID = '" + GroupID + "'", conn);
        SqlDataReader readfoldername = fcomm.ExecuteReader();

        while (readfoldername.Read())
        {
            FolderName.Add(readfoldername["FolderName"]);
        }

        readfoldername.Close();

        SqlCommand tcomm = new SqlCommand("SELECT TimeOut FROM FolderTable WHERE GroupID = '" + GroupID + "'", conn);
        SqlDataReader readTimeOut = tcomm.ExecuteReader();

        while (readTimeOut.Read())
        {
            TimeOut.Add(readTimeOut["TimeOut"]);
        }

        readTimeOut.Close();
        conn.Close();

        Session["grpName"] = GroupName;
        Session["grpID"] = GroupID;

        if (!Page.IsPostBack)
        {
            FolderGridView.DataSource = GetDataTable();
            FolderGridView.DataBind();
        }
    }

    public System.Data.DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("File Name");
        dt.Columns.Add("Expire Date");

        for (int i = 0; i < FolderName.Count; i++)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["File Name"] = FolderName[i];
            dt.Rows[dt.Rows.Count - 1]["Expire Date"] = TimeOut[i];

        }

        return dt;
    }

    protected void FolderGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FolderGridView.PageIndex = e.NewPageIndex;
        FolderGridView.DataSource = GetDataTable();
        FolderGridView.DataBind();
    }
    protected void CreateFile_Click(object sender, EventArgs e)
    {
        if (IDtype == "Student")
            AbilityLabel.Text = "Sorry you Cannot Create File";
        else
        {
            Session["grpName"] = GroupName;
            Session["grpID"] = GroupID;
            popUp1.ShowMessage();
        }
    }

    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        HttpCookie loginCookie = new HttpCookie("UserLoginCookie");
        loginCookie.Expires = DateTime.Now.AddDays(-1d);
        Response.Cookies.Add(loginCookie);
        Response.RedirectPermanent("Home.aspx");
    }
    protected void DeleteFile_Click(object sender, EventArgs e)
    {
        if (IDtype == "Student")
            AbilityLabel.Text = "Sorry you Cannot Delete File";
        else
        {
            for (int i = 0; i < FolderGridView.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)FolderGridView.Rows[i].FindControl("FolderCheckBox");
                if (chk.Checked == true)
                {
                    string FileName = "";
                    foreach (Control ctl in FolderGridView.Rows[i].Cells[1].Controls)
                    {
                        if (ctl is LinkButton)
                        {
                            FileName = ((LinkButton)ctl).Text;
                        }
                    }

                    string path = Server.MapPath("~");

                    path += "FileStorage" + "\\" + GroupName + "\\" + FileName;

                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        SqlConnection conn = new SqlConnection(connectionString);
                        conn.Open();

                        SqlCommand filedel = new SqlCommand("delete from FolderTable where FolderName = '" + FileName + "' and GroupID = '" + GroupID + "'", conn);
                        SqlDataReader del = filedel.ExecuteReader();
                    }

                }
            }

            Session["grpName"] = GroupName;
            Response.RedirectPermanent("ShowGroupFolder.aspx");
        }
    }

    public string GetFileName(string LinkFile)
    {
        string FileName = ""; int a = 0;

        for (int i = 0; i < LinkFile.Count(); i++)
        {
            if (LinkFile[i] == '>' && a == 0)
                a++;
            else if (a == 1 && LinkFile[i] == '<')
                break;
            else if (a == 1)
                FileName += LinkFile[i];
        }

        return FileName;
    }
    
}