using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Control_popUp : System.Web.UI.UserControl
{
    public void ShowMessage()
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "Script", "<script type=\"text/javascript\">ShowMessage(\"" + "\",\"" + "\");</script>");
        //Label1.Text = msg;
    }
    protected void CreateFileButton_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        string FolderName = FileBox.Text;
        string ExpireDate = ExpireDateBox.Text;
        string grpName = (string)Session["grpName"];
        string grpID = (string)Session["grpID"];

        string path = Server.MapPath("~");

        if (RequiredFieldValidator1.IsValid && RequiredFieldValidator2.IsValid && RegularExpressionValidator1.IsValid)
        {
            path += "FileStorage" + "\\" + grpName + "\\" + FolderName;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);

                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);

                conn.Open();
                SqlCommand comm = new SqlCommand("Insert into FolderTable(FolderName,TimeOut,GroupID) Values('" + FolderName + "','" + ExpireDate + "','" + grpID + "')", conn);
                SqlDataReader reader = comm.ExecuteReader();

                reader.Close();
                conn.Close();

                Session["grpName"] = grpName;
                Response.RedirectPermanent("ShowGroupFolder.aspx");
            }
            else
            {
                Label1.Text = "Error!Duplicate File Name";
                ShowMessage();
            }
        }
         
    }
    protected void FileBox_TextChanged(object sender, EventArgs e)
    {
        string FolderName = FileBox.Text;
        string grpName = (string)Session["grpName"];
        string grpID = (string)Session["grpID"];
        string path = Server.MapPath("~");
        path += "FileStorage" + "\\" + grpName + "\\" + FolderName;

        if (Directory.Exists(path))
            Label1.Text = "Error Duplicate File";
        else
            Label1.Text = "";
    }
    protected void ExpireDateBox_TextChanged(object sender, EventArgs e)
    {

    }
}