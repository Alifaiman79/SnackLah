using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
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

                SqlCommand cmd = new SqlCommand("SELECT UserID, Role, Password FROM Users WHERE Username=@username", con);
                cmd.Parameters.AddWithValue("@username", username);
                int userId = 0;
                string role = null;
                string storedPassword = null;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userId = Convert.ToInt32(reader["UserID"]);
                        role = reader["Role"].ToString();
                        storedPassword = reader["Password"].ToString();
                    }
                }

                bool legacyPassword = storedPassword != null &&
                    !storedPassword.StartsWith("$pbkdf2-sha256$", StringComparison.Ordinal);
                bool valid = legacyPassword
                    ? string.Equals(storedPassword, password, StringComparison.Ordinal)
                    : PasswordHasher.Verify(password, storedPassword);

                if (valid)
                {
                    if (legacyPassword)
                    {
                        using (SqlCommand upgrade = new SqlCommand(
                            "UPDATE Users SET Password=@hash WHERE UserID=@id AND Password=@old", con))
                        {
                            upgrade.Parameters.AddWithValue("@hash", PasswordHasher.Hash(password));
                            upgrade.Parameters.AddWithValue("@id", userId);
                            upgrade.Parameters.AddWithValue("@old", storedPassword);
                            upgrade.ExecuteNonQuery();
                        }
                    }

                    Session["UserID"] = userId.ToString();
                    Session["Username"] = username;
                    Session["Role"] = role;

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

    internal static class PasswordHasher
    {
        private const int Iterations = 100000;
        private const int SaltLength = 16;
        private const int HashLength = 32;

        internal static string Hash(string password)
        {
            byte[] salt = new byte[SaltLength];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
                random.GetBytes(salt);
            using (var derive = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                return "$pbkdf2-sha256$" + Iterations + "$" +
                    Convert.ToBase64String(salt) + "$" +
                    Convert.ToBase64String(derive.GetBytes(HashLength));
        }

        internal static bool Verify(string password, string stored)
        {
            if (string.IsNullOrEmpty(stored))
                return false;
            string[] parts = stored.Split('$');
            if (parts.Length != 5 || parts[1] != "pbkdf2-sha256" ||
                !int.TryParse(parts[2], out int iterations) || iterations < 10000 || iterations > 1000000)
                return false;

            try
            {
                byte[] salt = Convert.FromBase64String(parts[3]);
                byte[] expected = Convert.FromBase64String(parts[4]);
                if (salt.Length != SaltLength || expected.Length != HashLength)
                    return false;
                using (var derive = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                {
                    byte[] actual = derive.GetBytes(expected.Length);
                    int difference = 0;
                    for (int i = 0; i < actual.Length; i++)
                        difference |= actual[i] ^ expected[i];
                    return difference == 0;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
