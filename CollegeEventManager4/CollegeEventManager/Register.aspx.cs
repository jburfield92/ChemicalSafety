using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;

namespace CollegeEventManager
{
    public partial class Register : System.Web.UI.Page
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

        /// <summary> Loads the universities into the UniversityDdl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UniversityDdl_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UniversityDdl.DataSource = DatabaseCommunicationMethods.Sql.GetUniversities();
                UniversityDdl.DataTextField = "Name";
                UniversityDdl.DataValueField = "UniversityID";
                UniversityDdl.DataBind();
            }
        }

        /// <summary> Registers the user and logs them in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Response.Redirect("~/Default.aspx");
            }

            DatabaseCommunicationMethods.User user = new DatabaseCommunicationMethods.User();

            string filePath = PictureUploader.PostedFile.FileName;
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
                case ".pdf":
                    contenttype = "application/pdf";
                    break;
            }
            if (contenttype != string.Empty)
            {

                Stream fs = PictureUploader.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                user.Picture = bytes;
            }
            else
            {
                /// error bad file type
            }

            user.UserID = Guid.NewGuid();
            user.UserName = UserNameTextBox.Text;
            user.FirstName = FirstNameTextBox.Text;
            user.LastName = LastNameTextBox.Text;
            user.UniversityID = Int32.Parse(UniversityDdl.SelectedValue);
            user.PasswordSalt = user.CreateSalt();
            user.PasswordHash = user.CreatePasswordHash(PasswordTextBox.Text, user.PasswordSalt);
            user.Email = EmailTextBox.Text;
            
            string check = EmailTextBox.Text;
            string suffix = null;
            suffix = check.Substring(check.IndexOf(@"@") + 1);

            if (DatabaseCommunicationMethods.Sql.GetUniversityById(Int32.Parse(UniversityDdl.SelectedValue)).Suffix == suffix)
            {
                if (DatabaseCommunicationMethods.Sql.GetUserByUserName(user.UserName) == null && DatabaseCommunicationMethods.Sql.GetUserByEmail(user.Email) == null)
                {
                    try
                    {
                        DatabaseCommunicationMethods.Sql.AddUser(user, true);
                    }
                    catch
                    {
                        DatabaseCommunicationMethods.Sql.AddUser(user, false);
                    }

                    Session["User"] = user;


                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }
}