using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace SnackLah
{
    public partial class AdminManageUsers : System.Web.UI.Page
    {
        string conStr = WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void gvUsers_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            LoadUsers();
        }

        protected void gvUsers_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            LoadUsers();
        }

        protected void gvUsers_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUsers.Rows[e.RowIndex];
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            string username = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text;
            string password = ((System.Web.UI.WebControls.TextBox)(row.Cells[2].Controls[0])).Text;
            string role = ((System.Web.UI.WebControls.TextBox)(row.Cells[3].Controls[0])).Text;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Users SET Username = @Username, Password = @Password, Role = @Role WHERE UserID = @UserID", con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@UserID", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            gvUsers.EditIndex = -1;
            LoadUsers();
            lblMessage.Text = "User updated successfully.";
        }

        protected void gvUsers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID = @UserID", con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadUsers();
            lblMessage.Text = "User deleted successfully.";
        }
    }
}