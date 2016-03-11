using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InsideFolder : System.Web.UI.Page
{
    public ArrayList FilePath = new ArrayList();
    public ArrayList Uploader = new ArrayList();
    public ArrayList FileName = new ArrayList();
    string FolderName,GroupName,UserIDSession,GroupIDSession,IDtype,UserNameSession;
    protected void Page_Load(object sender, EventArgs e)
    {
        FolderName = "";

        string loginstate = (string)Session["loginstate"];
        if (loginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        if (Request.QueryString["FolderName"] != null)
        {
            FolderName = Request.QueryString["FolderName"].ToString();
        }
        else
            Response.RedirectPermanent("UserPage.aspx");

        GroupName = (string)Session["grpName"];
        UserIDSession = (string)Session["UserID"];
        GroupIDSession = (string)Session["grpID"];
        UserNameSession = (string)Session["UserName"];

        string Rootpath = Server.MapPath("~");
        Rootpath += "FileStorage" + "\\" + GroupName + "\\" + FolderName;


        if (Directory.Exists(Rootpath))
        {
            DirectoryInfo di = new DirectoryInfo(Rootpath);

            foreach (var fi in di.GetFiles())
            {
                FileName.Add(fi.Name.ToString());
                string pathdb = "FileStorage" + "\\" + GroupName + "\\" + FolderName + "\\" + fi.Name.ToString();
                FilePath.Add(pathdb);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();


            SqlCommand getIDtype = new SqlCommand("SELECT ID_type FROM RelationTable WHERE GroupID = '" + GroupIDSession + "' and UserID = '" + UserIDSession + "'", conn);
            SqlDataReader getIDtypereader = getIDtype.ExecuteReader();
            getIDtypereader.Read();
            if (getIDtypereader["ID_type"].ToString() == "Student")
            {
                IDtype = getIDtypereader["ID_type"].ToString();
            }
            getIDtypereader.Close();

            for (int i = 0; i < FilePath.Count; i++)
            {
                string fpath = FilePath[i] as string;
                SqlCommand getUserID = new SqlCommand("SELECT UserID FROM File_Table WHERE Filepath = " + "'" + fpath + "'", conn);
                SqlDataReader UID = getUserID.ExecuteReader();

                UID.Read();
                string UserID = UID["UserID"].ToString();
                UID.Close();

                SqlCommand getUserName = new SqlCommand("SELECT UserName FROM UserTable WHERE UserID = " + "'" + UserID + "'", conn);
                SqlDataReader UserNameReader = getUserName.ExecuteReader();

                UserNameReader.Read();

                Uploader.Add(UserNameReader["UserName"].ToString());

                UserNameReader.Close();
            }

            if (!Page.IsPostBack)
            {
                FileGridView.DataSource = GetDataTable();
                FileGridView.DataBind();
            }
        }
        else
            Response.RedirectPermanent("UserPage.aspx");
    }

    
    public System.Data.DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("File Name");
        dt.Columns.Add("Uploader");

        for (int i = 0; i < FileName.Count; i++)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["File Name"] = FileName[i];
            dt.Rows[dt.Rows.Count - 1]["Uploader"] = Uploader[i];

        }

        return dt;
    }

    protected void UploadFileButton_Click(object sender, EventArgs e)
    {
        string loginstate = (string)Session["loginstate"];
        if (loginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();


        SqlCommand getExpireDate = new SqlCommand("SELECT TimeOut FROM FolderTable WHERE FolderName = '" + FolderName + "' and GroupID = '" + GroupIDSession + "'", conn);
        SqlDataReader ExpDate = getExpireDate.ExecuteReader();
        ExpDate.Read();

        string exp = ExpDate["TimeOut"].ToString();
        DateTime TimeOut = DateTime.ParseExact(exp, "dd/MM/yyyy", null);

        int x = DateTime.Compare(TimeOut, DateTime.Now);
        if (x == 1)
        {
            Session["FolderName"] = FolderName;
            UploadPopUp.ShowPanel();
        }
        else
            AbilityLabel.Text = "Submission Time Over!";
    }
    protected void DownloadFileButton_Click(object sender, EventArgs e)
    {
        string loginstate = (string)Session["loginstate"];
        if (loginstate != "1")
            Response.RedirectPermanent("Home.aspx");

        for (int i = 0; i < FileGridView.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)FileGridView.Rows[i].FindControl("FileCheckBox");
            if (chk.Checked == true)
            {
                string FileName = FileGridView.Rows[i].Cells[1].Text.ToString();
                string Uploader = FileGridView.Rows[i].Cells[2].Text.ToString();

                string path = Server.MapPath("~");
                path += "FileStorage" + "\\" + GroupName + "\\" + FolderName + "\\" + FileName;

                FileInfo file = new FileInfo(path);
                if (IDtype == "Student" && UserNameSession != Uploader)
                    AbilityLabel.Text = "No Permission to Download";
                else if(file.Exists)
                {
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.Flush();
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }
            }
        }
    }
   
    protected void DeleteFilesButton_Click(object sender, EventArgs e)
    {
        if (IDtype == "Student")
            AbilityLabel.Text = "Sorry You Cant Delete!";
        else
        {
            string loginstate = (string)Session["loginstate"];
            if (loginstate != "1")
                Response.RedirectPermanent("Home.aspx");

            for (int i = 0; i < FileGridView.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)FileGridView.Rows[i].FindControl("FileCheckBox");
                if (chk.Checked == true)
                {
                    string FileName = FileGridView.Rows[i].Cells[1].Text.ToString();
                    string Uploader = FileGridView.Rows[i].Cells[2].Text.ToString();

                    string path = Server.MapPath("~");
                    path += "FileStorage" + "\\" + GroupName + "\\" + FolderName + "\\" + FileName;

                    string pathdb = "FileStorage" + "\\" + GroupName + "\\" + FolderName + "\\" + FileName;
                    FileInfo file = new FileInfo(path);

                    if (file.Exists)
                    {
                        file.Delete();

                        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        SqlConnection conn = new SqlConnection(connectionString);
                        conn.Open();

                        SqlCommand unamecomm = new SqlCommand("select UserID from UserTable where UserName = '" + Uploader + "'", conn);
                        SqlDataReader userNameReader = unamecomm.ExecuteReader();

                        userNameReader.Read();
                        string UserID = userNameReader["UserID"].ToString();
                        userNameReader.Close();

                        SqlCommand filedel = new SqlCommand("delete from File_Table where FilePath = '" + pathdb + "' and UserID = '" + UserID + "'", conn);
                        SqlDataReader del = filedel.ExecuteReader();

                        del.Close();
                    }
                }

            }

            Response.RedirectPermanent("InsideFolder.aspx?FolderName=" + FolderName + "");
        }
    }

    protected void FileGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FileGridView.PageIndex = e.NewPageIndex;
        FileGridView.DataSource = GetDataTable();
        FileGridView.DataBind();
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