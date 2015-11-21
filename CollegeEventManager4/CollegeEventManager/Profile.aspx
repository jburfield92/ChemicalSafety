<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="CollegeEventManager.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-7 col-md-offset-2">
                <div  class="page-header">
                    <h1>Your Profile</h1>
                </div>
            </div>
        </div>
        <div class="row">
           <div class="col-md-5 col-md-offset-3 text-center">
               <asp:Image ID="ProfileImage" runat="server" CssClass="img-rounded" width="300px" Height="300px"/>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-5 col-md-offset-3">
               <asp:FileUpload ID="ProfileImageUploader" runat="server" CssClass="form-control"/>
            </div> 
        </div>
        <br />
        <div class="row">
            <div class="col-md-2 col-md-offset-3">
               <asp:Button ID="NewImage" runat="server" CssClass="btn btn-primary" Text="Save Profile Picture" AutoPostBack="true" OnClick="NewImage_Click" />
           </div>
        </div>
        <br />
        <div class="row">
            <asp:Label ID="UserNameLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="UserNameText" Text="UserName:"/>
            <asp:Label ID="UserNameText" runat="server" CssClass="control-label col-md-3" Text="BlankUserName" />
        </div>
        <div class="row">
            <asp:Label ID="EmailLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="EmailText" Text="Email:"/>
            <asp:Label ID="EmailText" runat="server" CssClass="control-label col-md-3" Text="BlankEmail" />
        </div>
        <div class="row">
            <asp:Label ID="FirstNameLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="FirstNameText" Text="First Name:"/>
            <asp:Label ID="FirstNameText" runat="server" CssClass="control-label col-md-3" Text="BlankFirstName" />
        </div>
        <div class="row">
            <asp:Label ID="LastNameLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="LastNameText" Text="Last Name:"/>
            <asp:Label ID="LastNameText" runat="server" CssClass="control-label col-md-3" Text="BlankLastName"/>
        </div>
        <div class="row">
            <asp:Label ID="UniversityLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="UniversityText" Text="University:"/>
            <asp:Label ID="UniversityText" runat="server" CssClass="control-label col-md-3" Text="BlankUniversity" />
        </div>
        <br />
        <div class="row">
            <asp:Label ID="RSOsLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="RSOsListBox" Text="Your Current RSOs" />
        </div>
        <div class="row">
            <div class="col-md-5 col-md-offset-3">
                <asp:ListBox ID="RSOsListBox" runat="server" CssClass="form-control" />
            </div>
        </div>
        <br />
        <div class="row">
            <asp:Label ID="EventsLabel" runat="server" CssClass="control-label col-md-2 col-md-offset-3" AssociatedControlID="EventsListBox" Text="Your Current Events" />
        </div>
        <div class="row">
            <div class="col-md-5 col-md-offset-3">
                <asp:ListBox ID="EventsListBox" runat="server" CssClass="form-control" />
            </div>
        </div>
    </div>
    <div>
        <hr />
        <footer>
            <p class="text-center">&copy; <%: DateTime.Now.Year %> - College Event Manager</p>
        </footer>
    </div>
</asp:Content>
