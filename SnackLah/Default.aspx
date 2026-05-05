<%@ Page Title="Home" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SnackLah.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="welcome-container">
        <div class="welcome-card">
            <h1 class="welcome-title">Welcome to <span class="brand-name">SnackLah</span></h1>
            <p class="welcome-text">
                Your one-stop online food store.<br />
                Explore our delicious menu or login to place an order.
            </p>
            <div class="welcome-buttons">
                <a href="Products.aspx" class="welcome-btn menu-btn">Menu</a>
                <a href="Login.aspx" class="welcome-btn login-btn">Login</a>
            </div>
        </div>
    </div>
</asp:Content>