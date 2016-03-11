using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Control_Upload : System.Web.UI.UserControl
{
    public void ShowPanel()
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "Script", "<script type=\"text/javascript\">ShowMessage(\"" + "\",\"" + "\");</script>");
        //Label1.Text = msg;
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string GroupName = (string)Session["grpName"];
        string FolderName = (string)Session["Foldername"];
        string UserName = (string)Session["UserName"];

        if (FileUploader.HasFile)
            try
            {
                string path = Server.MapPath("~");
                string fileName = FileUploader.FileName;

                path += "FileStorage" + "\\" + GroupName + "\\" + FolderName + "\\" + fileName;
                string pathdb = "FileStorage" + "\\" + GroupName + "\\" + FolderName + "\\" + fileName;
                
                FileInfo file = new FileInfo(path);
                if (!file.Exists)
                {
                    FileUploader.SaveAs(path);
                    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();

                    SqlCommand unamecomm = new SqlCommand("select UserID from UserTable where UserName = '" + UserName + "'", conn);
                    SqlDataReader userNameReader = unamecomm.ExecuteReader();

                    userNameReader.Read();
                    string UserID = userNameReader["UserID"].ToString();
                    userNameReader.Close();

                    SqlCommand comm = new SqlCommand("Insert into File_Table(FileName,FilePath,UserID) Values('" + fileName + "','" + pathdb + "','" + UserID + "')", conn);
                    SqlDataReader reader = comm.ExecuteReader();
                    reader.Close();

                    Response.RedirectPermanent("InsideFolder.aspx?FolderName=" + FolderName + "");
                }
                else
                {
                    Label1.Text = "File Exists";
                    ShowPanel();
                }

            }
            catch(Exception ex)
            {

            }
        else
        {
            Label1.Text = "Choose a File";
            ShowPanel();
        }

    }
}