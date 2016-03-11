using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

public partial class Dynamic_Box : System.Web.UI.Page
{
    public static bool GroupNameTest, AdminIDTest;
    protected void Page_Load(object sender, EventArgs e)
    {
        GroupNameTest = AdminIDTest = false;
    }
    protected void CreateButton_Click(object sender, EventArgs e)
    {
        if ((GroupNameDupCheck.Text == GroupNameBox.Text + " Available") && (AdminIDDupCheck.Text == AdminIDBox.Text + " Available"))
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand comm = new SqlCommand("Insert into GroupTable(GroupName,AdminName,AdminPass) Values('" + GroupNameBox.Text + "','" + AdminIDBox.Text + "','" + AdminPassBox.Text + "')", conn);
            SqlDataReader reader = comm.ExecuteReader();
            reader.Close();
            conn.Close();

            string path = Server.MapPath("~");
            path += "FileStorage" + "\\" + GroupNameBox.Text;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            Label1.Text = "Group Created Successfully";
        }
        else
        {
            Label1.Text = "Group Name or Admin ID Taken";
        }
    }
    protected void GroupNameBox_TextChanged(object sender, System.EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        SqlCommand comm = new SqlCommand("Select GroupID from GroupTable where GroupName = '"+ GroupNameBox.Text +"'", conn);
        SqlDataReader reader = comm.ExecuteReader();

        try
        {
            reader.Read();
            string s = reader["GroupID"].ToString();
            GroupNameDupCheck.Visible = true;
            GroupNameDupCheck.Text = GroupNameBox.Text + " Already Exists!";
        }
        catch
        {
            GroupNameTest = true;
            GroupNameDupCheck.Visible = true;
            GroupNameDupCheck.Text = GroupNameBox.Text + " Available";
        }

        reader.Close();
        conn.Close();
        
    }
    protected void AdminIDBox_TextChanged(object sender, System.EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        SqlCommand comm = new SqlCommand("Select GroupID from GroupTable where AdminName = '" + AdminIDBox.Text + "'", conn);
        SqlDataReader reader = comm.ExecuteReader();

        try
        {
            reader.Read();
            string s = reader["GroupID"].ToString();
            AdminIDDupCheck.Visible = true;
            AdminIDDupCheck.Text = AdminIDBox.Text + " Already Exists!";
        }
        catch
        {
            AdminIDTest = true;
            AdminIDDupCheck.Visible = true;
            AdminIDDupCheck.Text = AdminIDBox.Text + " Available";
        }

        reader.Close();
        conn.Close();
    }
}