using DatabaseCommunicationMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeEventManager
{
    public partial class RSOs : Page
    {
        private static List<RSO> FilteredRsos;
        private static List<RSO> AllRso;

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
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(SearchBox);
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(FilterByRadioBtnGroup);

                List<RSO> rsos = Sql.GetRSOs();
                AllRso = rsos;
                FilteredRsos = rsos;
                BindItemsList(rsos);
            }
        }

        /// <summary> Handles an event when we databound one of the rows in the repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RptRSOs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // if there are no items
            if (RptRSOs.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label emptyLbl = (Label)e.Item.FindControl("EmptyLbl");
                    emptyLbl.Visible = true;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button joinRsoBtn = (Button)e.Item.FindControl("JoinRSOBtn");

                List<User> members = Sql.GetRSOMembers((int)DataBinder.Eval(e.Item.DataItem, "RSOID"));

                if (members.Any(x => x.UserID == loggedInUser.UserID))
                {
                    joinRsoBtn.Enabled = false;
                }
                else
                {
                    joinRsoBtn.Enabled = true;
                }
            }
        }

        /// <summary> gets the image out of the byte array for each event
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string GetImage(object img)
        {
            if (img == null)
            {
                return "~/Content/NoImage.png";
            }

            return "data:image/jpg;base64, " + Convert.ToBase64String((byte[])img);
        }

        #region Pagination methods

        /// <summary> The current page in the pagination
        /// </summary>
        private int CurrentPage
        {
            get
            {
                object objPage = ViewState["_CurrentPage"];
                int _CurrentPage = 0;

                if (objPage == null)
                {
                    _CurrentPage = 0;
                }
                else
                {
                    _CurrentPage = (int)objPage;
                }

                return _CurrentPage;
            }
            set { ViewState["_CurrentPage"] = value; }
        }

        /// <summary> The first index in the pagination
        /// </summary>
        private int fistIndex
        {
            get
            {
                int _FirstIndex = 0;

                if (ViewState["_FirstIndex"] == null)
                {
                    _FirstIndex = 0;
                }
                else
                {
                    _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
                }

                return _FirstIndex;
            }
            set { ViewState["_FirstIndex"] = value; }
        }

        /// <summary> the last index in the pagination
        /// </summary>
        private int lastIndex
        {
            get
            {
                int _LastIndex = 0;

                if (ViewState["_LastIndex"] == null)
                {
                    _LastIndex = 0;
                }
                else
                {
                    _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
                }

                return _LastIndex;
            }
            set { ViewState["_LastIndex"] = value; }
        }

        /// <summary> The datasource for the pages
        /// </summary>
        private PagedDataSource _PageDataSource = new PagedDataSource();

        /// <summary> Binds the items in the repeater
        /// </summary>
        private void BindItemsList(List<RSO> rsos)
        {
            _PageDataSource.DataSource = rsos;
            _PageDataSource.AllowPaging = true;
            _PageDataSource.PageSize = 10;
            _PageDataSource.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = _PageDataSource.PageCount;

            lbtnPrevious.Enabled = !_PageDataSource.IsFirstPage;
            lbtnNext.Enabled = !_PageDataSource.IsLastPage;
            lbtnFirst.Enabled = !_PageDataSource.IsFirstPage;
            lbtnLast.Enabled = !_PageDataSource.IsLastPage;

            RptRSOs.DataSource = _PageDataSource;
            RptRSOs.DataBind();
            doPaging();
        }

        /// <summary> sets up the pages
        /// </summary>
        private void doPaging()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");

            fistIndex = CurrentPage - 5;

            if (CurrentPage > 5)
            {
                lastIndex = CurrentPage + 5;
            }
            else
            {
                lastIndex = 10;
            }
            if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                fistIndex = lastIndex - 10;
            }

            if (fistIndex < 0)
            {
                fistIndex = 0;
            }

            for (int i = fistIndex; i < lastIndex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            dlPaging.DataSource = dt;
            dlPaging.DataBind();
        }

        /// <summary> Handles the Next Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindItemsList(FilteredRsos);
        }

        /// <summary> Handles the Previous Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindItemsList(FilteredRsos);
        }

        /// <summary> Handles the Last Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindItemsList(FilteredRsos);
        }

        /// <summary> Handles the First Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindItemsList(FilteredRsos);
        }

        /// <summary> Sets up the paging command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("Paging"))
            {
                CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
                BindItemsList(FilteredRsos);
            }
        }

        /// <summary> Sets up the page buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");

            if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkbtnPage.Enabled = false;
                lnkbtnPage.Font.Bold = true;
            }
        }

        #endregion

        /// <summary> Saves the RSO event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            RSO rso = new RSO
            {
                AdminID = loggedInUser.UserID,
                RSOName = ModalEditRSOTitleTextBox.Text,
                Approved = true,
                CreatedDate = DateTime.Now,
                Description = ModalEditRSODescriptionTextBox.Text,
                UniversityID = loggedInUser.UniversityID
            };

            if (ModalEditRSOPictureUploader.PostedFile != null)
            {
                string filePath = ModalEditRSOPictureUploader.PostedFile.FileName;
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
                    Stream fs = ModalEditRSOPictureUploader.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    rso.Picture = bytes;
                }
            }

            Sql.UpdateRSO(rso, ModalEditRSOPictureUploader.PostedFile != null);

            Response.Redirect("~/RSO.aspx");
        }

        /// <summary> User leaving the RSO event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LeaveBtn_Click(object sender, EventArgs e)
        {
            Sql.DeleteMember(loggedInUser.UserID, Convert.ToInt32(ModalDetailsRSOId.Value));
            Response.Redirect("~/RSO.aspx");
        }

        /// <summary> Creates an RSO event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveCreateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ModalCreateRSOUserOneTextBox.Text) || string.IsNullOrEmpty(ModalCreateRSOUserTwoTextBox.Text) 
                || string.IsNullOrEmpty(ModalCreateRSOUserThreeTextBox.Text) || string.IsNullOrEmpty(ModalCreateRSOUserFourTextBox.Text))
            {
                // display error
                return;
            }

            User userOne = Sql.GetUserByEmail(ModalCreateRSOUserOneTextBox.Text);
            User userTwo = Sql.GetUserByEmail(ModalCreateRSOUserTwoTextBox.Text);
            User userThree = Sql.GetUserByEmail(ModalCreateRSOUserThreeTextBox.Text);
            User userFour = Sql.GetUserByEmail(ModalCreateRSOUserFourTextBox.Text);

            if (userOne == null || userTwo == null || userThree == null || userFour == null)
            {
                // display error
                return;
            }

            RSO rso = new RSO
            {
                AdminID = loggedInUser.UserID,
                RSOName = ModalCreateRSOTitleTextBox.Text,
                Approved = true,
                CreatedDate = DateTime.Now,
                Description = ModalCreateRSODescriptionTextBox.Text,
                UniversityID = loggedInUser.UniversityID
            };

            if (ModalCreateRSOPictureUploader.PostedFile != null)
            {
                string filePath = ModalCreateRSOPictureUploader.PostedFile.FileName;
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
                    Stream fs = ModalCreateRSOPictureUploader.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    rso.Picture = bytes;
                }
            }

            Sql.AddRSOMember(rso.RSOID, loggedInUser.UserID);
            Sql.AddRSOMember(rso.RSOID, userOne.UserID);
            Sql.AddRSOMember(rso.RSOID, userTwo.UserID);
            Sql.AddRSOMember(rso.RSOID, userThree.UserID);
            Sql.AddRSOMember(rso.RSOID, userFour.UserID);

            Sql.AddRSO(rso, ModalCreateRSOPictureUploader.PostedFile != null);

            Response.Redirect("~/RSO.aspx");
        }

        /// <summary> Edits an RSO event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditBtn_Click(object sender, EventArgs e)
        {
            ModalEditRSOId.Value = ModalDetailsRSOId.Value;
            ModalEditRSOTitleTextBox.Text = RSONameTextLabel.Text;
            ModalEditRSODescriptionTextBox.Text = ModalDetailsRSODescriptionText.Text;

            ScriptManager.RegisterStartupScript(this, GetType(), "Show", "<script> $('#EditRSOModal').modal('toggle');</script>", false);
            UpdatePanel1.Update();
        }

        /// <summary> Handles the JoinRSO button click event for the repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void JoinRSOBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem rptItem = (RepeaterItem)btn.NamingContainer;
            HiddenField rsoID = (HiddenField)rptItem.FindControl("RSOIDField");

            Sql.AddRSOMember(Convert.ToInt32(rsoID.Value), loggedInUser.UserID);

            BindItemsList(FilteredRsos);
        }

        protected void DetailsBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem rptItem = (RepeaterItem)btn.NamingContainer;
            HiddenField rsoId = (HiddenField)rptItem.FindControl("RSOIDField");
            ModalDetailsRSOId.Value = rsoId.Value;
            RSO detailRSO = Sql.GetRSOById(Convert.ToInt32(rsoId.Value));
            User admin = Sql.GetUserById(detailRSO.AdminID);
            List<User> members = Sql.GetRSOMembers(detailRSO.RSOID);

            ModalRSOImage.ImageUrl = GetImage(detailRSO.Picture);

            RSONameTextLabel.Text = detailRSO.RSOName;
            ModalDetailsRSOAdminText.Text = admin.FirstName + " " + admin.LastName;
            ModalDetailsRSOMemberCountText.Text = members.Count.ToString();

            ModalDetailsRSOMemberListBox.DataSource = members;
            ModalDetailsRSOMemberListBox.DataTextField = "UserName";
            ModalDetailsRSOMemberListBox.DataValueField = "UserID";
            ModalDetailsRSOMemberListBox.DataBind();

            University univ = Sql.GetUniversityById(detailRSO.UniversityID);

            ModalDetailsRSOUniversityText.Text = univ.Name;
            ModalDetailsRSODateText.Text = detailRSO.CreatedDate.ToString();
            ModalDetailsRSODescriptionText.Text = detailRSO.Description;

            List<Event> eventsForRSO = Sql.GetEventsForRSOByID(detailRSO.RSOID);

            ModalDetailsRSOEventsListBox.DataSource = eventsForRSO;
            ModalDetailsRSOEventsListBox.DataTextField = "EventName";
            ModalDetailsRSOEventsListBox.DataValueField = "EventID";
            ModalDetailsRSOEventsListBox.DataBind();

            if (members.Any(x => x.UserID == loggedInUser.UserID))
            {
                JoinBtn.Visible = false;
                LeaveBtn.Visible = true;
            }
            else
            {
                JoinBtn.Visible = true;
                LeaveBtn.Visible = false;
            }

            if (loggedInUser.UserID == admin.UserID)
            {
                EditBtn.Visible = true;
            }
            else
            {
                EditBtn.Visible = false;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "Show", "<script> $('#detailsModal').modal('toggle');</script>", false);
            UpdatePanel1.Update();
        }

        protected void FilterChanged(object sender, EventArgs e)
        {
            int filterIndex = FilterByRadioBtnGroup.SelectedIndex;

            switch (filterIndex)
            {
                case 0:
                    FilteredRsos = AllRso;
                    break;
                case 1:
                    FilteredRsos = AllRso.Where(x => x.UniversityID == loggedInUser.UniversityID).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                FilteredRsos = FilteredRsos.FindAll(x => x.RSOName.ToLower().Contains(SearchBox.Text.ToLower()));
            }

            BindItemsList(FilteredRsos);
        }
    }
}