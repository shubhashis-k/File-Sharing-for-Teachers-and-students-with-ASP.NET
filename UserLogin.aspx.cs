using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helper;

public partial class UserLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["UserLoginCookie"] != null)
        {
            Session["loginstate"] = "1";
            string UserName = Request.Cookies["UserLoginCookie"]["UserName"];
            Session["UserName"] = UserName;
            Response.RedirectPermanent("UserPage.aspx");
        }

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        SqlCommand comm = new SqlCommand("SELECT UserPass from UserTable where UserName = " + "'" + UserNameBox.Text.ToString() + "'", conn);
        SqlDataReader reader = comm.ExecuteReader();

        reader.Read();

        try
        {
            string password = reader["UserPass"].ToString();
            string pass2 = UserPasswordBox.Text.ToString();

            if (password == pass2)
            {
                if(RememberMe.Checked == true)
                {
                    HttpCookie logincookie = new HttpCookie("UserLoginCookie");
                    logincookie["UserName"] = UserNameBox.Text;
                    logincookie["Password"] = UserPasswordBox.Text;
                    logincookie.Expires = DateTime.Now.AddDays(7d);
                    Response.Cookies.Add(logincookie);

                }
                Session["UserName"] = UserNameBox.Text;
                Session["loginstate"] = "1";
                Response.RedirectPermanent("UserPage.aspx");
            }
            else
                ucMessage.ShowMessage(Message.Text.ERROR_SERVER, Message.Type.error.ToString());

            reader.Close();
            conn.Close();
        }
        catch(Exception ex)
        {
            ucMessage.ShowMessage(Message.Text.ERROR_SERVER, Message.Type.error.ToString());
        }
    }
}