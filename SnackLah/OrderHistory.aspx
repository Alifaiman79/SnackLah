<%@ Page Title="Order History" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="SnackLah.OrderHistory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/orderhistory.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="text-align: center; font-family: 'Segoe UI', sans-serif; margin-bottom: 30px;">Your Order History</h2>

    <asp:Repeater ID="rptHistory" runat="server">
        <ItemTemplate>
            <div class="order-card">
                <div class="order-header">
                    <span class="order-id">Order #: <%# Eval("OrderID") %></span>
                    <span class="order-date">Date: <%# Eval("OrderDate", "{0:dd MMM yyyy}") %></span>
                </div>
                <div class="order-details">
                    <%# Eval("Details") %> <%%>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>