using System;

namespace SnackLah
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    lblWelcome.Text = "Welcome, " + Session["Username"].ToString();
                    lnkLogout.Visible = true;
                }
                else
                {
                    lblWelcome.Text = "";
                    lnkLogout.Visible = false;
                }
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear(); // Removes all session data
            Response.Redirect("Login.aspx");
        }
    }
}
