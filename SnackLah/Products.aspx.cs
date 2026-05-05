using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace SnackLah
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts(); // Load all products initially
            }
        }

        // Load products optionally filtered by category
        private void LoadProducts(string category = "")
        {
            string conStr = WebConfigurationManager.ConnectionStrings["SnackLahDBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT * FROM Products";
                if (!string.IsNullOrEmpty(category))
                {
                    query += " WHERE Category = @Category";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(category))
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptProducts.DataSource = dt;
                    rptProducts.DataBind();
                }
            }
        }

        // Filter button handlers
        protected void btnAll_Click(object sender, EventArgs e)
        {
            LoadProducts(); // No filter
        }

        protected void btnMain_Click(object sender, EventArgs e)
        {
            LoadProducts("Main");
        }

        protected void btnDessert_Click(object sender, EventArgs e)
        {
            LoadProducts("Dessert");
        }

        protected void btnBeverage_Click(object sender, EventArgs e)
        {
            LoadProducts("Beverage");
        }

        // Add selected items to cart
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            var cart = Session["Cart"] as List<(int ProductID, int Quantity)> ?? new List<(int, int)>();

            foreach (RepeaterItem item in rptProducts.Items)
            {
                var chk = (CheckBox)item.FindControl("chkSelect");
                var txt = (TextBox)item.FindControl("txtQty");
                var hf = (HiddenField)item.FindControl("hfProductID");

                if (chk != null && chk.Checked && int.TryParse(txt.Text, out int qty) && qty > 0)
                {
                    int productId = int.Parse(hf.Value);
                    cart.Add((productId, qty));
                }
            }

            if (cart.Count > 0)
            {
                Session["Cart"] = cart;
                lblMessage.Text = "Items added to cart!";
            }
            else
            {
                lblMessage.Text = "No items selected.";
            }
        }
    }
}
