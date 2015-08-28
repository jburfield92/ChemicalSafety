using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSSG_DatabaseMethods;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace CSSG_WebPortal
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary> Handles registering the user (first we do server side validation)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RegisterUsernameTextbox.Text))
            {
                ErrorTextRegister.Text = "Username field is blank";
                return;
            }
            if (string.IsNullOrEmpty(EmailTextbox.Text))
            {
                ErrorTextRegister.Text = "Email field is blank";
                return;
            }
            if (string.IsNullOrEmpty(FirstNameTextbox.Text))
            {
                ErrorTextRegister.Text = "First Name field is blank";
                return;
            }
            if (string.IsNullOrEmpty(LastNameTextbox.Text))
            {
                ErrorTextRegister.Text = "Last Name field is blank";
                return;
            }
            if (string.IsNullOrEmpty(PasswordQuestionTextbox.Text))
            {
                ErrorTextRegister.Text = "Password Question field is blank";
                return;
            }
            if (string.IsNullOrEmpty(QuestionAnswerTextbox.Text))
            {
                ErrorTextRegister.Text = "Password Question Answer field is blank";
                return;
            }

            bool EmailmatchArray = Regex.IsMatch(EmailTextbox.Text,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            if (!EmailmatchArray)
            {
                ErrorTextRegister.Text = "Email wasn't entered correctly, try again.";
                return;
            }

            var passwordStrength = passStrength.InnerText;

            if (passwordStrength != "Good" && passwordStrength != "Strong" && passwordStrength != "Very Strong")
            {
                ErrorTextRegister.Text = "Password Strength is Weak! Try a longer password with a combination of uppercase and lowercase letters, numbers and special characters.";
                return;
            }

            // if we get here, the form is good.
            RegisterUser();
        }

        /// <summary> Handles registering the user
        /// </summary>
        private void RegisterUser()
        {
            int organizationId = 0;
            int departmentId = 0;
            int locationId = 0;
            string password = null;
            string confirmPassword = null;

            password = Request.Form["RegisterPasswordTextbox"];
            confirmPassword = Request.Form["ConfirmPasswordTextbox"];

            if (password == confirmPassword)
            {
                UserPassword newUserPassword = new UserPassword()
                {
                    PasswordQuestion = Request.Form["PasswordQuestionTextbox"],
                    PasswordAnswer = Request.Form["QuestionAnswerTextbox"],
                    CreatedDate = DateTime.Now
                };

                newUserPassword.Salt = newUserPassword.CreateSalt(Request.Form["RegisterPasswordTextbox"].Length);
                newUserPassword.Password = newUserPassword.CreatePasswordHash(Request.Form["RegisterPasswordTextbox"], newUserPassword.Salt);

                if (OrganizationDdl.SelectedIndex > 0)
                {
                    organizationId = Convert.ToInt32(OrganizationDdl.SelectedItem.Value);
                }
                if (DepartmentDdl.SelectedIndex > 0)
                {
                    departmentId = Convert.ToInt32(DepartmentDdl.SelectedItem.Value);
                }
                if (LocationDdl.SelectedIndex > 0)
                {
                    locationId = Convert.ToInt32(LocationDdl.SelectedItem.Value);
                }

                User user = new User()
                {
                    UserId = Guid.NewGuid(),
                    UserName = Request.Form["RegisterUsernameTextbox"],
                    LastLoginDate = DateTime.Now,
                    FirstName = Request.Form["FirstNameTextbox"],
                    LastName = Request.Form["LastNameTextbox"],
                    Email = Request.Form["EmailTextbox"],
                    OrganizationId = organizationId,
                    DepartmentId = departmentId,
                    LocationId = locationId
                };

                DaLayer.AddUser(newUserPassword, user);
            }
            else
            {
                Page.SetFocus("ConfirmPasswordTextbox");
            }
        }

        /// <summary> Handles logging in the user (first  we do server side validation)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (LoginUser())
            {
                FormsAuthentication.SetAuthCookie("test", true);
                Response.Redirect("../Default.aspx");
            }
        }

        /// <summary> Logs in the user
        /// </summary>
        /// <returns></returns>
        private bool LoginUser()
        {
            return true;
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {

        }
    }
}