<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CollegeEventManager.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="form-horizontal">
        <div class="form-group">
            <div class="page-header col-md-6 col-md-offset-3">
                <h1>Register</h1>
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
            <asp:Label ID="EmailLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="EmailTextBox" Text="Email:"/>
            <div class="col-md-3">
                <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control" TextMode="Email" MaxLength="100"/>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="FirstNameLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="FirstNameTextBox" Text="First Name:"/>
            <div class="col-md-3">
                <asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="form-control" MaxLength="64"/>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="LastNameLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="LastNameTextBox" Text="Last Name:"/>
            <div class="col-md-3">
                <asp:TextBox ID="LastNameTextBox" runat="server" CssClass="form-control" MaxLength="64"/>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="UniversityLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="UniversityDdl" Text="University:"/>
            <div class="col-md-3">
                <asp:DropDownList ID="UniversityDdl" runat="server" CssClass="form-control" OnLoad="UniversityDdl_Load" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="PictureLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="PictureUploader" Text="Picture:"/>
            <div class="col-md-3">
                <asp:FileUpload ID="PictureUploader" runat="server" CssClass="form-control"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-2 col-md-offset-3">
                <asp:Image ID="RegisterImage" CssClass="img-rounded" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-1 col-md-offset-5">
                <asp:button ID="RegisterBtn" runat="server" CssClass="btn btn-success" Text="Register" Onclick="RegisterBtn_Click" AutoPostBack="true" />
            </div>
            <div class="col-md-1">
                <asp:button ID="LoginBtn" runat="server" CssClass="btn btn-primary" Text="Login" PostBackUrl="~/Login.aspx"/>
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
