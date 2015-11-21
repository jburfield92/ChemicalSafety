using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeEventManager
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary> Handles the page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            DatabaseCommunicationMethods.User user = DatabaseCommunicationMethods.Sql.GetUserByUserName(UserNameTextBox.Text);

            if (user != null)
            {
                string one = user.CreatePasswordHash(PasswordTextBox.Text, user.PasswordSalt);

                string two = user.PasswordHash;

                if (one == two)
                {
                    Session["User"] = user;
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }
}