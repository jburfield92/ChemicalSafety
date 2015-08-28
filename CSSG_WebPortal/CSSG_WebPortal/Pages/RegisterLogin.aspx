<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterLogin.aspx.cs" Inherits="CSSG_WebPortal.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function RegisterValidate()
        {
            var username = document.getElementById('<%=RegisterUsernameTextbox.ClientID %>');
            var email = document.getElementById('<%=EmailTextbox.ClientID %>');
            var firstname = document.getElementById('<%=FirstNameTextbox.ClientID %>');
            var lastname = document.getElementById('<%=LastNameTextbox.ClientID %>');
            var passwordQuestion = document.getElementById('<%=PasswordQuestionTextbox.ClientID %>');
            var questionAnswer = document.getElementById('<%=QuestionAnswerTextbox.ClientID %>');

            if (username.value == "")
            {
                alert("Username field is blank");
                username.focus();
                return false;
            }
            if (email.value == "") {
                alert("Email field is blank");
                email.focus();
                return false;
            }
            if (firstname.value == "") {
                alert("First Name field is blank");
                firstname.focus();
                return false;
            }
            if (lastname.value == "") {
                alert("Last Name field is blank");
                lastname.focus();
                return false;
            }
            if (passwordQuestion.value == "") {
                alert("Password Question field is blank");
                passwordQuestion.focus();
                return false;
            }
            if (questionAnswer.value == "") {
                alert("Password Question Answer field is blank");
                questionAnswer.focus();
                return false;
            }

            var emailPat = /^(\".*\"|[A-Za-z]\w*)@(\[\d{1,3}(\.\d{1,3}){3}]|[A-Za-z]\w*(\.[A-Za-z]\w*)+)$/
            var EmailmatchArray = Email.match(emailPat);

            if (EmailmatchArray == null)
            {
                alert("Email wasn't entered correctly, try again.");
                email.focus();
                return false;
            }

            var passwordStrength = document.getElementById("passStrength");

            if (passwordStrength.value != "Good" && passwordStrength.value != "Strong" && passwordStrength.value != "Very Strong")
            {
                alert("Password Strength is Weak! Try a longer password with a combination of uppercase and lowercase letters, numbers and special characters.");
                return false;
            }
        }

        function CheckPasswordStrength(password) {
            var passwordStrength = document.getElementById("passStrength");

            //TextBox left blank.
            if (password.length == 0) {
                passwordStrength.innerHTML = "";
                return;
            }

            //Regular Expressions.
            var regex = new Array();
            regex.push("[A-Z]"); //Uppercase Alphabet.
            regex.push("[a-z]"); //Lowercase Alphabet.
            regex.push("[0-9]"); //Digit.
            regex.push("[$@$!%*#?&]"); //Special Character.

            var passed = 0;

            //Validate for each Regular Expression.
            for (var i = 0; i < regex.length; i++) {
                if (new RegExp(regex[i]).test(password)) {
                    passed++;
                }
            }

            //Validate for length of Password.
            if (passed > 2 && password.length > 8) {
                passed++;
            }

            //Display status.
            var color = "";
            var strength = "";
            switch (passed) {
                case 0:
                case 1:
                    strength = "Weak";
                    color = "red";
                    break;
                case 2:
                    strength = "Good";
                    color = "darkorange";
                    break;
                case 3:
                case 4:
                    strength = "Strong";
                    color = "green";
                    break;
                case 5:
                    strength = "Very Strong";
                    color = "darkgreen";
                    break;
            }
            passwordStrength.innerHTML = strength;
            passwordStrength.style.color = color;
        }

    </script>
    <div class="subpage">
        <div class="row">
            <div class="col-md-6 divider col-sm-6 col-xs-6">
                <h3>Register:</h3>
                
                <asp:textbox runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Username" id="RegisterUsernameTextbox"/>
                <asp:textbox runat="server" CssClass="form-control" TextMode="Email" placeholder="Email" id="EmailTextbox"/>
                <asp:textbox runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="First Name" id="FirstNameTextbox"/>
                <asp:textbox runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Last Name" id="LastNameTextbox"/>
                <asp:textbox runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Password Question" id="PasswordQuestionTextbox"/>
                <asp:textbox runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Question Answer" id="QuestionAnswerTextbox"/>
                <asp:dropdownlist runat="server" CssClass="form-control dropdownlist" id="OrganizationDdl">
                    <Items>
                       <asp:ListItem Text="Select Organization" Value="" />
                   </Items>
                </asp:dropdownlist>
                <asp:dropdownlist runat="server" CssClass="form-control dropdownlist" id="DepartmentDdl">
                    <Items>
                       <asp:ListItem Text="Select Department" Value="" />
                   </Items>
                </asp:dropdownlist>
                <asp:dropdownlist runat="server" CssClass="form-control dropdownlist" id="LocationDdl">
                    <Items>
                       <asp:ListItem Text="Select Location" Value="" />
                   </Items>
                </asp:dropdownlist>

                <asp:textbox runat="server" CssClass="form-control" ID="RegisterPasswordTextbox" TextMode="Password" placeholder="Password" onkeyup="CheckPasswordStrength(this.value)"/>
                <span id="passStrength" runat="server"></span>
                <asp:textbox runat="server" CssClass="form-control" ID="ConfirmPasswordTextbox" TextMode="Password" placeholder="Confirm Password"/>
                <asp:CompareValidator
                    runat="server"
                    ID="ConfirmPasswordComparisonValidator"
                    ControlToValidate="RegisterPasswordTextbox"
                    ControlToCompare="ConfirmPasswordTextbox"
                    CssClass="ValidationError"
                    ErrorMessage="Passwords must match"
                    >
                </asp:CompareValidator>
                <br />
                <asp:Button ID="RegisterButton" CssClass="btn btn-default subbutton" Text="Register" runat="server" OnClick="RegisterButton_Click" OnClientClick="return RegisterValidate()"/>
                <asp:Button ID="ResetButton" CssClass="btn btn-default subbutton" runat="server" Text="Reset Form" OnClick="ResetButton_Click" />
                <asp:Label id="ErrorTextRegister" runat="server" CssClass="ValidationError"></asp:Label>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-6">
                <h3>Login:</h3>
                <asp:textbox runat="server" CssClass="form-control" type="text" placeholder="Username" id="LoginUsernameTextbox" />
                <asp:textbox runat="server" CssClass="form-control" type="password" placeholder="Password" id="LoginPasswordTextbox"/>
                <br />
                <asp:Button ID="LoginButton" CssClass="btn btn-default subbutton" Text="Login" runat="server" OnClick="LoginButton_Click"/>
                <asp:Label id="ErrorTextLogin" runat="server" CssClass="ValidationError"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>
