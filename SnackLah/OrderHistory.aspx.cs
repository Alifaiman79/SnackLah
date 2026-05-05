using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;

namespace SnackLah
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["UserID"] != null)
                LoadHistory();
        }

        private void LoadHistory()
        {
            string conStr = WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            dt.Columns.Add("OrderID");
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("Details");

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                // Get list of orders
                SqlCommand cmd = new SqlCommand("SELECT OrderID, OrderDate FROM Orders WHERE UserID = @uid", con);
                cmd.Parameters.AddWithValue("@uid", Session["UserID"]);

                List<(int OrderID, string OrderDate)> orders = new List<(int, string)>();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int orderId = Convert.ToInt32(dr["OrderID"]);
                        string orderDate = Convert.ToDateTime(dr["OrderDate"]).ToString("yyyy-MM-dd");
                        orders.Add((orderId, orderDate));
                    }
                }

                // Now fetch details for each order one by one
                foreach (var order in orders)
                {
                    StringBuilder details = new StringBuilder();

                    SqlCommand detailCmd = new SqlCommand(@"
                SELECT p.Name, od.Quantity 
                FROM OrderDetails od
                JOIN Products p ON od.ProductID = p.ProductID
                WHERE od.OrderID = @oid", con);

                    detailCmd.Parameters.AddWithValue("@oid", order.OrderID);

                    using (SqlDataReader dr2 = detailCmd.ExecuteReader())
                    {
                        while (dr2.Read())
                        {
                            details.Append("<li>" + dr2["Name"] + " x" + dr2["Quantity"] + "</li>");
                        }
                    }

                    dt.Rows.Add(order.OrderID, order.OrderDate, details.ToString());
                }
            }

            rptHistory.DataSource = dt;
            rptHistory.DataBind();
        }

    }
}