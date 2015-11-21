<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CollegeEventManager._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Welcome</h1>
        <p class="lead">Tired of being alone in your house? Do you need friends? Are you a college student? Then find an event near you or an RSO to join to meet new people and get outside for once!</p>
        <p class="lead">College Event Manager is the perfect place to find events with fellow college students.</p>
        <p><a id="registerButtonInJumbotron" runat="server" href="Register.aspx" class="btn btn-primary btn-lg">Register</a>&nbsp;<a id="loginButtonInJumbotron" runat="server" href="Login.aspx" class="btn btn-primary btn-lg">Login</a></p>
    </div>

    <div>
        <hr />
        <footer>
            <p class="text-center">&copy; <%: DateTime.Now.Year %> - College Event Manager</p>
        </footer>
    </div>
</asp:Content>
