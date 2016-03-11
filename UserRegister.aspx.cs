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

public partial class UserRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in RadioButtonList1.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "Teacher")
                {
                    RollBox.Visible = false;
                    RollLabel.Visible = false;
                    DesignationBox.Visible = true;
                    DesignationLabel.Visible = true;
                }
                else
                {
                    RollBox.Visible = true;
                    RollLabel.Visible = true;
                    DesignationBox.Visible = false;
                    DesignationLabel.Visible = false;
                }
            }
        }
    }
    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        if (UserNameCheckLabel.Text != UsernameBox.Text + " Already exists!")
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                if (RadioButtonList1.SelectedValue == "Teacher")
                {
                    SqlCommand comm = new SqlCommand("Insert into UserTable(UserName,UserPass,ID_type,Designation,Batch,Department) Values('" + UsernameBox.Text + "','" + UserPassBox.Text + "','" + "Teacher" + "','" + DesignationBox.Text + "','" + BatchBox.Text + "','" + DeptDropDown.SelectedValue + "')", conn);
                    SqlDataReader reader = comm.ExecuteReader();
                    reader.Close();
                    conn.Close();
                }
                else
                {
                    SqlCommand comm = new SqlCommand("Insert into UserTable(UserName,UserPass,ID_type,Roll,Batch,Department) Values('" + UsernameBox.Text + "','" + UserPassBox.Text + "','" + "Student" + "','" + RollBox.Text + "','" + BatchBox.Text + "','" + DeptDropDown.SelectedValue + "')", conn);
                    SqlDataReader reader = comm.ExecuteReader();
                    reader.Close();
                    conn.Close();
                }

                Label1.Text = "Registration Successful";
            }
            catch
            {

            }
        }
        else
            Label1.Text = "UserName Already Taken. Please Choose Another!";
    }
    protected void UsernameBox_TextChanged(object sender, System.EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        SqlCommand comm = new SqlCommand("Select UserID from UserTable where Username = '" + UsernameBox.Text + "'", conn);
        SqlDataReader reader = comm.ExecuteReader();

        try
        {
            reader.Read();
            string s = reader["UserID"].ToString();
            UserNameCheckLabel.Visible = true;
            UserNameCheckLabel.Text = UsernameBox.Text + " Already exists!";
        }
        catch
        {
            UserNameCheckLabel.Visible = true;
            UserNameCheckLabel.Text = UsernameBox.Text + " Available!";
        }

        reader.Close();
        conn.Close();
    }
}