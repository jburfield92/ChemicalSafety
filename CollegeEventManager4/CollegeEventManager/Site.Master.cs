using DatabaseCommunicationMethods;
using System;
using System.Web.UI;

namespace CollegeEventManager
{
    public partial class SiteMaster : MasterPage
    {
        /// <summary> Handles the page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                LoggedInTemplate.Visible = true;
                AnonymousTemplate.Visible = false;
                EventsNav.Visible = true;
                RsoNav.Visible = true;
                ProfileNav.Visible = true;
                CalendarNav.Visible = true;
                profileLink.InnerText = "Hello " + ((User)Session["User"]).UserName;
                if (((User)Session["User"]).IsSuperAdmin)
                {
                    UniversityNav.Visible = true;
                }
                else
                {
                    UniversityNav.Visible = false;
                }
            }
            else
            {
                EventsNav.Visible = false;
                RsoNav.Visible = false;
                ProfileNav.Visible = false;
                CalendarNav.Visible = false;
                UniversityNav.Visible = false;
                LoggedInTemplate.Visible = false;
                AnonymousTemplate.Visible = true;
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session["User"] = null;

            Response.Redirect("~/Default.aspx");
        }
    }
}