<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="SnackLah.Products" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/product.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="products-wrapper">
        <!-- Title -->
        <h2 class="page-heading">Menu Selection</h2>

        <!-- Filter Buttons -->
        <div class="filter-buttons">
            <asp:Button ID="btnAll" runat="server" Text="All" CssClass="btn filter-btn" OnClick="btnAll_Click" />
            <asp:Button ID="btnMain" runat="server" Text="Main" CssClass="btn filter-btn" OnClick="btnMain_Click" />
            <asp:Button ID="btnDessert" runat="server" Text="Dessert" CssClass="btn filter-btn" OnClick="btnDessert_Click" />
            <asp:Button ID="btnBeverage" runat="server" Text="Beverage" CssClass="btn filter-btn" OnClick="btnBeverage_Click" />
        </div>

        <!-- Message -->
        <div class="message-container">
            <asp:Label ID="lblMessage" runat="server" Style="font-weight:bold;" CssClass="message-label" />
        </div>

        <!-- Product Grid -->
        <div class="product-grid-modern">
            <asp:Repeater ID="rptProducts" runat="server">
                <ItemTemplate>
                    <div class="product-card">
                        <img src='<%# "Images/" + Eval("Image") %>' alt='<%# Eval("Name") %>' class="product-image" />
                        <div class="product-info">
                            <h3 class="product-name"><%# Eval("Name") %></h3>
                            <p class="product-price">RM <%# Eval("Price", "{0:F2}") %></p>
                            <div class="product-interact">
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                <label class="qty-label">Qty:
                                    <asp:TextBox ID="txtQty" runat="server" CssClass="qty-input" />
                                </label>
                            </div>
                            <asp:HiddenField ID="hfProductID" runat="server" Value='<%# Eval("ProductID") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Add to Cart -->
        <div class="cart-action">
            <asp:Button ID="btnAddToCart" runat="server" Text="🛒 Add to Cart" CssClass="btn cart-btn" OnClick="btnAddToCart_Click" />
        </div>
    </div>
</asp:Content>
