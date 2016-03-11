using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DateChecker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dt = "12/12/2015";
        DateTime qdate = DateTime.ParseExact(dt , "dd/MM/yyyy", null);
        var curDate = DateTime.Now;
        var dateString = curDate.Day.ToString();

        int x = DateTime.Compare(qdate, DateTime.Now);
        Label1.Text = x.ToString();
    }
}