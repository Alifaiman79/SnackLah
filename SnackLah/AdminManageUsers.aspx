<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminManageUsers.aspx.cs" Inherits="SnackLah.AdminManageUsers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/manageuser.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css"
          integrity="sha512-uXfWwzIfh6+7MdqxEaWZZXqLaS5Y3OjZuCeyOMVqBvcD92x7ZJDJzkJGfAv2bYvZyqGLvDaKL1pADNdD1rkzWQ=="
          crossorigin="anonymous" referrerpolicy="no-referrer" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Admin Navigation Bar -->
    <div class="admin-navbar">
        <a href="Admin.aspx"><i class="fas fa-receipt"></i> User Order Details</a>
        <a href="AdminManageUsers.aspx" class="active"><i class="fas fa-users-cog"></i> Manage Users</a>
    </div>

    <!-- Admin Panel -->
    <div class="admin-panel">
        <h2 class="admin-title"><i class="fas fa-user-shield"></i> Manage Users</h2>

        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
            OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating"
            OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowDeleting="gvUsers_RowDeleting"
            CssClass="admin-grid">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" ItemStyle-CssClass="col-id" />
                <asp:BoundField DataField="Username" HeaderText="Username" ItemStyle-CssClass="col-user" />
                <asp:BoundField DataField="Role" HeaderText="Role" />

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Edit" CssClass="btn-edit" Text='<i class="fas fa-edit"></i> Edit' />
                        <asp:LinkButton runat="server" CommandName="Delete" CssClass="btn-delete" Text='<i class="fas fa-trash-alt"></i> Delete' OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Update" CssClass="btn-save" Text='<i class="fas fa-save"></i> Save' />
                        <asp:LinkButton runat="server" CommandName="Cancel" CssClass="btn-cancel" Text='<i class="fas fa-times"></i> Cancel' />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
    </div>
</asp:Content>
