using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SnackLah
{
    public partial class Admin : System.Web.UI.Page
    {
        string conStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = @"
                    SELECT 
                        o.OrderID,
                        u.Username,
                        o.OrderDate,
                        STRING_AGG(p.Name + ' (x' + CAST(od.Quantity AS VARCHAR) + ')', ', ') AS Details
                    FROM Orders o
                    JOIN Users u ON o.UserID = u.UserID
                    JOIN OrderDetails od ON o.OrderID = od.OrderID
                    JOIN Products p ON od.ProductID = p.ProductID
                    GROUP BY o.OrderID, u.Username, o.OrderDate
                    ORDER BY o.OrderDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int orderId = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                // Delete order details first
                SqlCommand deleteDetails = new SqlCommand("DELETE FROM OrderDetails WHERE OrderID=@orderId", con);
                deleteDetails.Parameters.AddWithValue("@orderId", orderId);

                // Then delete order
                SqlCommand deleteOrder = new SqlCommand("DELETE FROM Orders WHERE OrderID=@orderId", con);
                deleteOrder.Parameters.AddWithValue("@orderId", orderId);

                con.Open();
                deleteDetails.ExecuteNonQuery();
                deleteOrder.ExecuteNonQuery();
            }

            LoadOrders();
        }
    }
}
