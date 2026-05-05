<%@ Page Title="Admin Panel" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SnackLah.Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/admin.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" 
          integrity="sha512-uXfWwzIfh6+7MdqxEaWZZXqLaS5Y3OjZuCeyOMVqBvcD92x7ZJDJzkJGfAv2bYvZyqGLvDaKL1pADNdD1rkzWQ==" 
          crossorigin="anonymous" referrerpolicy="no-referrer" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Admin Navigation Bar -->
    <div class="admin-navbar">
        <a href="Admin.aspx"><i class="fas fa-receipt"></i> User Order Details</a>
        <a href="AdminManageUsers.aspx"><i class="fas fa-users-cog"></i> Manage Users</a>
    </div>

    <div class="admin-panel">
        <h2 class="admin-title">All Orders </h2>

        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderID"
            CssClass="admin-grid" GridLines="None" OnRowDeleting="gvOrders_RowDeleting"
            HeaderStyle-CssClass="grid-header"
            RowStyle-CssClass="grid-row" AlternatingRowStyle-CssClass="grid-alt-row">

            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" ReadOnly="True" 
                    ItemStyle-CssClass="col-id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Username" HeaderText="Username" 
                    ItemStyle-CssClass="col-user" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:dd MMM yyyy}" />
                <asp:BoundField DataField="Details" HeaderText="Order Details" HtmlEncode="false" />
                <asp:CommandField ShowDeleteButton="True" ButtonType="Link" DeleteText="Delete" 
                    ItemStyle-CssClass="col-delete" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
