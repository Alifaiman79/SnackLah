using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace SnackLah
{
    public partial class ThankYou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["LastOrderID"] != null)
            {
                int orderId = (int)Session["LastOrderID"];
                LoadReceipt(orderId);
            }
        }

        private void LoadReceipt(int orderId)
        {
            string conStr = WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Price");
            dt.Columns.Add("Total");

            decimal originalTotal = 0;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                // Get order date
                SqlCommand cmdOrder = new SqlCommand("SELECT OrderDate FROM Orders WHERE OrderID = @OrderID", con);
                cmdOrder.Parameters.AddWithValue("@OrderID", orderId);
                SqlDataReader drOrder = cmdOrder.ExecuteReader();
                if (drOrder.Read())
                {
                    lblOrderID.Text = orderId.ToString();
                    lblOrderDate.Text = Convert.ToDateTime(drOrder["OrderDate"]).ToString("dd MMM yyyy hh:mm tt");
                }
                drOrder.Close();

                // Get order details
                SqlCommand cmdDetails = new SqlCommand(@"
                    SELECT p.Name, od.Quantity, p.Price
                    FROM OrderDetails od
                    JOIN Products p ON od.ProductID = p.ProductID
                    WHERE od.OrderID = @OrderID", con);
                cmdDetails.Parameters.AddWithValue("@OrderID", orderId);
                SqlDataReader dr = cmdDetails.ExecuteReader();

                while (dr.Read())
                {
                    string name = dr["Name"].ToString();
                    int qty = Convert.ToInt32(dr["Quantity"]);
                    decimal price = Convert.ToDecimal(dr["Price"]);
                    decimal total = price * qty;
                    originalTotal += total;

                    dt.Rows.Add(name, qty, price.ToString("0.00"), total.ToString("0.00"));
                }
                dr.Close();
            }

            gvOrderDetails.DataSource = dt;
            gvOrderDetails.DataBind();

            // Handle discount
            decimal discountPercent = Session["PromoDiscount"] != null ? (decimal)Session["PromoDiscount"] : 0m;
            decimal discountAmount = originalTotal * discountPercent;
            decimal finalTotal = originalTotal - discountAmount;

            if (discountPercent > 0)
            {
                phDiscount.Visible = true;
                lblOriginalTotal.Text = originalTotal.ToString("0.00");
                lblDiscountAmount.Text = discountAmount.ToString("0.00");
            }

            lblTotal.Text = finalTotal.ToString("0.00");

            // Clear discount session after use
            Session["PromoDiscount"] = null;
        }
    }
}
