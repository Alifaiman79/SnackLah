<%@ Page Title="Register" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SnackLah.Register" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/register.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container">
        <h2>Create Your Account</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="message-label" />
        <asp:TextBox ID="txtUsername" runat="server" Placeholder="Username" CssClass="form-control" /><br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Password" CssClass="form-control" /><br />
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn" />

        <p style="margin-top:15px; font-size: 14px;">
            Already have an account? 
           <a href="Login.aspx" style="color:#007bff; text-decoration:none;">Login here</a>.
       </p>
    </div>
</asp:Content>
