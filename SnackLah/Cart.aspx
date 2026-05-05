<%@ Page Title="Cart" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="SnackLah.Cart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/cart.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="cart-container" style="font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
        <h2 style="text-align: center;">Your Shopping Cart</h2>

        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="grid" EmptyDataText="Your cart is empty.">
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Price" HeaderText="Unit Price (RM)" DataFormatString="{0:F2}" />
                <asp:BoundField DataField="Total" HeaderText="Total (RM)" DataFormatString="{0:F2}" />
            </Columns>
        </asp:GridView>

        <div class="cart-summary" style="display: flex; flex-direction: column; align-items: center; gap: 10px; margin-top: 20px;">
            <h3>Total: RM <asp:Label ID="lblTotal" runat="server" /></h3>

            <div>
                <asp:TextBox ID="txtPromoCode" runat="server" Placeholder="Enter Promo Code" CssClass="promo-input" />
                <asp:Button ID="btnApplyPromo" runat="server" Text="Apply" OnClick="btnApplyPromo_Click" CssClass="btn" />
            </div>

            <asp:Label ID="lblDiscountInfo" runat="server" ForeColor="Green" Font-Bold="True" />
            <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" CssClass="btn" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style="text-align: center; display: block;" />
        </div>
    </div>
</asp:Content>
