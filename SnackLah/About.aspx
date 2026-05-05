<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="SnackLah.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/about.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="about-container">
        <h2>About <span class="highlight">SnackLah</span></h2>

        <div class="image-box">
            <img src="Images/snack-family.jpeg" alt="SnackLah Story" />
        </div>

        <h3><i class="fas fa-seedling"></i> Our Story</h3>
        <p>Founded with a passion for delicious meals,SnackLah brings satisfying food favorites to your doorstep. We aim to make every meal a celebration of flavor and comfort.</p>

        <h3><i class="fas fa-bullseye"></i> Our Mission</h3>
        <p>To deliver happiness through quality ingredients, convenient service, and diverse menu options — so everyone can enjoy great food, anytime, anywhere.</p>

        <div class="about-columns">
            <div class="column">
                <h3><i class="fas fa-heart"></i> Our Values</h3>
                <p>We stand for freshness, local sourcing, and delightful service with every pack delivered.</p>
            </div>
            <div class="column">
                <h3><i class="fas fa-users"></i> Our Community</h3>
                <p>SnackLah supports local food culture and strives to bring people together through shared meals and good times.</p>
            </div>
        </div>

        <div class="about-highlight">
            <strong>Fun Fact:</strong> Our best-selling pizza🍕 has been enjoyed over 20,000 times in March 2025!
        </div>
    </div>
</asp:Content>