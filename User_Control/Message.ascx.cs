using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Control_Message : System.Web.UI.UserControl
{
    public void ShowMessage(string msg, string type)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "Script", "<script type=\"text/javascript\">ShowMessage(\"" + msg + "\",\"" + type + "\");</script>");
    }
}
