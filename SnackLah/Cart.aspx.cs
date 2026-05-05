using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace SnackLah
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCart();
        }

        private void LoadCart()
        {
            var cart = Session["Cart"] as List<(int ProductID, int Quantity)>;
            if (cart == null || cart.Count == 0)
            {
                lblMessage.Text = "Cart is empty.";
                gvCart.DataSource = null;
                gvCart.DataBind();
                lblTotal.Text = "0.00";
                return;
            }

            string conStr = WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Price");
            dt.Columns.Add("Total");

            decimal grandTotal = 0;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                foreach (var item in cart)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Name, Price FROM Products WHERE ProductID = @pid", con);
                    cmd.Parameters.AddWithValue("@pid", item.ProductID);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string name = dr["Name"].ToString();
                        decimal price = Convert.ToDecimal(dr["Price"]);
                        decimal total = price * item.Quantity;
                        grandTotal += total;

                        dt.Rows.Add(name, item.Quantity, price.ToString("0.00"), total.ToString("0.00"));
                    }
                    dr.Close();
                }
            }

            // Apply discount if promo code was used
            decimal discountPercent = Session["PromoDiscount"] != null ? (decimal)Session["PromoDiscount"] : 0m;
            decimal discountAmount = grandTotal * discountPercent;
            decimal finalTotal = grandTotal - discountAmount;

            if (discountPercent > 0)
            {
                lblDiscountInfo.Text = $"Promo code applied: -RM {discountAmount:0.00} ({discountPercent * 100}% off)";
            }
            else
            {
                lblDiscountInfo.Text = "";
            }

            gvCart.DataSource = dt;
            gvCart.DataBind();
            lblTotal.Text = finalTotal.ToString("0.00");
        }

        protected void btnApplyPromo_Click(object sender, EventArgs e)
        {
            string enteredCode = txtPromoCode.Text.Trim().ToUpper();

            if (enteredCode == "SNACK10")
            {
                Session["PromoDiscount"] = 0.10m;
                lblMessage.Text = "";
            }
            else if (enteredCode == "SNACK20")
            {
                Session["PromoDiscount"] = 0.20m;
                lblMessage.Text = "";
            }
            else
            {
                Session["PromoDiscount"] = null;
                lblMessage.Text = "Invalid promo code.";
            }

            LoadCart(); // Refresh cart to show discount
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblMessage.Text = "You must log in to checkout.";
                return;
            }

            var cart = Session["Cart"] as List<(int ProductID, int Quantity)>;
            if (cart == null || cart.Count == 0)
            {
                lblMessage.Text = "Cart is empty.";
                return;
            }

            string conStr = WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Orders (UserID, OrderDate) OUTPUT INSERTED.OrderID VALUES (@uid, @date)", con);
                cmd.Parameters.AddWithValue("@uid", Session["UserID"]);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                int orderId = (int)cmd.ExecuteScalar();

                foreach (var item in cart)
                {
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES (@oid, @pid, @qty)", con);
                    cmd2.Parameters.AddWithValue("@oid", orderId);
                    cmd2.Parameters.AddWithValue("@pid", item.ProductID);
                    cmd2.Parameters.AddWithValue("@qty", item.Quantity);
                    cmd2.ExecuteNonQuery();
                }

                // Optionally: save the discount in DB (Orders table) if needed

                // Clear session data
                Session["Cart"] = null;
                Session["LastOrderID"] = orderId;

                Response.Redirect("ThankYou.aspx");
            }
        }
    }
}
