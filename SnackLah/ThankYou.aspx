<%@ Page Title="Thank You" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="SnackLah.ThankYou" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="thankyou-container" style="font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif; max-width: 800px; margin: auto; padding: 40px; background-color: #fff; border: 1px solid #ccc; border-radius: 8px; box-shadow: 0 0 10px rgba(0,0,0,0.1);">

        <h2 style="text-align: center; color: #4CAF50;">✅ Thank You for Your Purchase!</h2>
        <p style="text-align: center;">Your order has been successfully placed. Below is your receipt.</p>

        <hr style="margin: 20px 0;" />

        <div style="margin-bottom: 20px; font-size: 16px;">
            <strong>Order ID:</strong> <asp:Label ID="lblOrderID" runat="server" /><br />
            <strong>Order Date:</strong> <asp:Label ID="lblOrderDate" runat="server" />
        </div>

        <asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="False" CssClass="grid"
            HeaderStyle-BackColor="#f5f5f5" HeaderStyle-Font-Bold="True" GridLines="Horizontal"
            BorderStyle="Solid" BorderWidth="1px" CellPadding="8" Width="100%">
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Price" HeaderText="Unit Price (RM)" />
                <asp:BoundField DataField="Total" HeaderText="Total (RM)" />
            </Columns>
        </asp:GridView>

        <asp:PlaceHolder ID="phDiscount" runat="server" Visible="false">
            <h4 style="text-align: center; margin-top: 20px;">Total Price: RM <asp:Label ID="lblOriginalTotal" runat="server" /></h4>
            <h4 style="text-align: center; color: green;">Discount Applied: -RM <asp:Label ID="lblDiscountAmount" runat="server" /></h4>
        </asp:PlaceHolder>

        <h3 style="text-align: center; margin-top: 20px;">Total Price: RM <asp:Label ID="lblTotal" runat="server" /></h3>

        <div style="margin-top: 30px; text-align: center;">
            <a href="OrderHistory.aspx" class="btn" style="margin-right: 10px;">📄 View Order History</a>
            <a href="Products.aspx" class="btn">🛒 Continue Shopping</a>
        </div>
    </div>
</asp:Content>
