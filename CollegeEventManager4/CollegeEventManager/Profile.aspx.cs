using DatabaseCommunicationMethods;
using System;
using System.IO;

namespace CollegeEventManager
{
    public partial class Profile : System.Web.UI.Page
    {
        private User loggedInUser;

        /// <summary> Handles the page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            loggedInUser = (User)Session["User"];

            if (loggedInUser == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
                if (loggedInUser.Picture != null)
                {
                    ProfileImage.ImageUrl = "data:image/jpg;base64, " + Convert.ToBase64String(loggedInUser.Picture);
                }
                else
                {
                    ProfileImage.ImageUrl = "~/Content/NoImage.png";
                }

                UserNameText.Text = loggedInUser.UserName;
                FirstNameText.Text = loggedInUser.FirstName;
                LastNameText.Text = loggedInUser.LastName;
                EmailText.Text = loggedInUser.Email;
                UniversityText.Text = Sql.GetUniversityById(loggedInUser.UniversityID).Name;
                RSOsListBox.DataSource = Sql.GetRSOByUserId(loggedInUser.UserID);
                RSOsListBox.DataTextField = "RSOName";
                RSOsListBox.DataValueField = "RSOID";
                RSOsListBox.DataBind();

                EventsListBox.DataSource = Sql.GetEventsByUserId(loggedInUser.UserID);
                RSOsListBox.DataTextField = "EventName";
                RSOsListBox.DataValueField = "EventID";
                EventsListBox.DataBind();
            }
        }

        protected void NewImage_Click(object sender, EventArgs e)
        {
            if (ProfileImageUploader.PostedFile != null)
            {
                byte[] bytes;
                string filePath = ProfileImageUploader.PostedFile.FileName;
                string filename = Path.GetFileName(filePath);
                string ext = Path.GetExtension(filename);
                string contenttype = string.Empty;

                switch (ext)
                {
                    case ".jpg":
                        contenttype = "image/jpg";
                        break;
                    case ".png":
                        contenttype = "image/png";
                        break;
                    case ".gif":
                        contenttype = "image/gif";
                        break;
                }

                if (contenttype != string.Empty)
                {
                    Stream fs = ProfileImageUploader.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((int)fs.Length);
                    loggedInUser.Picture = bytes;
                    Sql.UpdateUser(loggedInUser, true);
                    Session["User"] = loggedInUser;
                }
            }
        }
    }
}