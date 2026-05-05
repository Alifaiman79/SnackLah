<%@ Page Title="Login" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SnackLah.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/login.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="login-container">
        <h2>Login to SnackLah</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="message-label" />
        
        <asp:TextBox ID="txtUsername" runat="server" Placeholder="Username" CssClass="form-control" /><br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Password" CssClass="form-control" /><br />

        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn" />

        <p style="margin-top:15px; font-size: 14px;">
            Don't have an account? 
            <a href="Register.aspx" style="color:#007bff; text-decoration:none;">Register here</a>.
        </p>
    </div>

</asp:Content>
