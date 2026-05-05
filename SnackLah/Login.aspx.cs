using System;
using System.Data.SqlClient;
using System.Web;

namespace SnackLah
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter both username and password.";
                return;
            }

            string conStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT UserID, Role FROM Users WHERE Username=@username AND Password=@password", con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password); // Consider hashing if stored hashed

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Session["UserID"] = reader["UserID"].ToString();
                    Session["Username"] = username;
                    Session["Role"] = reader["Role"].ToString();

                    string role = reader["Role"].ToString();

                    if (role == "Admin")
                    {
                        Response.Redirect("Admin.aspx");
                    }
                    else
                    {
                        Response.Redirect("Products.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid username or password.";
                }
            }
        }


    }
}
