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
using Helper;

public partial class Log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["AdminLoginCookie"] != null)
        {
            Session["Adminloginstate"] = "1";
            string AdminName = Request.Cookies["AdminLoginCookie"]["AdminName"];
            Session["AdminName"] = AdminName;
            Response.RedirectPermanent("AdminPage.aspx");
        }
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        SqlCommand comm = new SqlCommand("SELECT AdminPass from GroupTable where AdminName = " + "'" + AdminNameBox.Text.ToString() + "'", conn);
        SqlDataReader reader = comm.ExecuteReader();

        reader.Read();

        try
        {
            string password = reader["AdminPass"].ToString();
            string pass2 = AdminPasswordBox.Text.ToString();

            if (password == pass2)
            {
                if (RememberMe.Checked == true)
                {
                    HttpCookie logincookie = new HttpCookie("AdminLoginCookie");
                    logincookie["AdminName"] = AdminNameBox.Text;
                    logincookie["AdminPassword"] = AdminPasswordBox.Text;
                    logincookie.Expires = DateTime.Now.AddDays(7d);
                    Response.Cookies.Add(logincookie);

                }
                Session["AdminName"] = AdminNameBox.Text;
                Session["Adminloginstate"] = "1";
                Response.RedirectPermanent("AdminPage.aspx");
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
