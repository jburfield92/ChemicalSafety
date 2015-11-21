using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeEventManager
{
    public partial class _Default : Page
    {
        /// <summary> Handles the page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                registerButtonInJumbotron.Visible = false;
                loginButtonInJumbotron.Visible = false;
            }
        }
    }
}