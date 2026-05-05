using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace SnackLah
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Trim input to remove leading/trailing whitespace
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Check if input fields are empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please fill in both Username and Password.";
                return; // Stop execution if fields are empty
            }

            string conStr = WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                SqlCommand checkUser = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username=@username", con);
                checkUser.Parameters.AddWithValue("@username", username);
                int userExist = (int)checkUser.ExecuteScalar();

                if (userExist == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password) VALUES (@username, @password)", con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Registered successfully!";
                }
                else
                {
                    lblMessage.Text = "Username already exists.";
                }
            }
        }

    }
}
