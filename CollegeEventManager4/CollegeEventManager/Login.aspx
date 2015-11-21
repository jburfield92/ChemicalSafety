<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CollegeEventManager.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div class="form-horizontal">
        <div class="form-group">
            <div class="page-header col-md-6 col-md-offset-3">
                <h1>Login</h1>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="UserNameLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="UserNameTextBox" Text="UserName:"/>
            <div class="col-md-3">
                <asp:TextBox ID="UserNameTextBox" runat="server" CssClass="form-control" MaxLength="64"/>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="PasswordLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="PasswordTextBox" Text="Password:"/>
            <div class="col-md-3">
                <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="form-control" TextMode="Password" MaxLength="32"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-1 col-md-offset-5">
                <asp:button ID="LoginBtn" runat="server" CssClass="btn btn-success" Text="Login" OnClick="LoginBtn_Click" AutoPostBack="true"/>
            </div>
            <div class="col-md-1">
                <asp:button ID="RegisterBtn" runat="server" CssClass="btn btn-primary" Text="Register" PostBackUrl="~/Register.aspx" />
            </div>
        </div>
        <asp:Label ID="ErrorLabel" runat="server" CssClass="control-label text-danger"/>
    </div>
    <div>
        <hr />
        <footer>
            <p class="text-center">&copy; <%: DateTime.Now.Year %> - College Event Manager</p>
        </footer>
    </div>
</asp:Content>
