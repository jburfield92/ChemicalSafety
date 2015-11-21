using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseCommunicationMethods;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace CollegeEventManager
{
    public partial class Events : Page
    {
        private static List<Event> FilteredEvents;
        private static List<Event> AllEvents;

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

                List<Event> events = Sql.GetEvents();
                AllEvents = events;
                FilteredEvents = events;
                BindItemsList(events);
                ModalCreateEventCategoryDdl.DataSource = Sql.GetCategories();
                ModalCreateEventCategoryDdl.DataTextField = "CategoryName";
                ModalCreateEventCategoryDdl.DataValueField = "EventCategoryID";
                ModalCreateEventCategoryDdl.DataBind();

                ModalEditEventCategoryDdl.DataSource = Sql.GetCategories();
                ModalEditEventCategoryDdl.DataTextField = "CategoryName";
                ModalEditEventCategoryDdl.DataValueField = "EventCategoryID";
                ModalEditEventCategoryDdl.DataBind();

                ModalCreateEventTypeDdl.DataSource = Sql.GetEventTypes();
                ModalCreateEventTypeDdl.DataTextField = "TypeName";
                ModalCreateEventTypeDdl.DataValueField = "EventTypeID";
                ModalCreateEventTypeDdl.DataBind();

                ModalEditEventTypeDdl.DataSource = Sql.GetEventTypes();
                ModalEditEventTypeDdl.DataTextField = "TypeName";
                ModalEditEventTypeDdl.DataValueField = "EventTypeID";
                ModalEditEventTypeDdl.DataBind();

                ModalCreateEventRSODdl.DataSource = Sql.GetRSOs();
                ModalCreateEventRSODdl.DataTextField = "RSOName";
                ModalCreateEventRSODdl.DataValueField = "RSOID";
                ModalCreateEventRSODdl.DataBind();

                ModalEditEventRSODdl.DataSource = Sql.GetRSOs();
                ModalEditEventRSODdl.DataTextField = "RSOName";
                ModalEditEventRSODdl.DataValueField = "RSOID";
                ModalEditEventRSODdl.DataBind();
            }
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
        private void BindItemsList(List<Event> events)
        {
            _PageDataSource.DataSource = events;
            _PageDataSource.AllowPaging = true;
            _PageDataSource.PageSize = 10;
            _PageDataSource.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = _PageDataSource.PageCount;

            lbtnPrevious.Enabled = !_PageDataSource.IsFirstPage;
            lbtnNext.Enabled = !_PageDataSource.IsLastPage;
            lbtnFirst.Enabled = !_PageDataSource.IsFirstPage;
            lbtnLast.Enabled = !_PageDataSource.IsLastPage;

            RptEvents.DataSource = _PageDataSource;
            RptEvents.DataBind();
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
            BindItemsList(FilteredEvents);
        }

        /// <summary> Handles the Previous Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindItemsList(FilteredEvents);
        }

        /// <summary> Handles the Last Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindItemsList(FilteredEvents);
        }

        /// <summary> Handles the First Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindItemsList(FilteredEvents);
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
                BindItemsList(FilteredEvents);
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

        /// <summary> Handles an event when we databound one of the rows in the repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RptEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // if there are no items
            if (RptEvents.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label emptyLbl = (Label)e.Item.FindControl("EmptyLbl");
                    emptyLbl.Visible = true;
                }
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button joinEventBtn = (Button)e.Item.FindControl("JoinEventBtn");
                Button publicBtn = (Button)e.Item.FindControl("PublicBtn");
                Button privateBtn = (Button)e.Item.FindControl("PrivateBtn");
                Button rsoBtn = (Button)e.Item.FindControl("RSOBtn");
                Button fundraisingBtn = (Button)e.Item.FindControl("FundraisingBtn");
                Button socialBtn = (Button)e.Item.FindControl("SocialBtn");
                Button educationalBtn = (Button)e.Item.FindControl("EdcuationalBtn");
                Button entertainmentBtn = (Button)e.Item.FindControl("EntertainmentBtn");
                Label eventRatingLbl = (Label)e.Item.FindControl("EventRatingLbl");

                string categoryName = Sql.GetCategoryById((int)DataBinder.Eval(e.Item.DataItem, "EventCategoryID"));
                string typeName = Sql.GetTypeById((int)DataBinder.Eval(e.Item.DataItem, "EventTypeID"));

                if (typeName == "Public")
                {
                    publicBtn.CssClass = "btn btn-info";
                }
                else if (typeName == "Private")
                {
                    privateBtn.CssClass = "btn btn-info";
                }
                else if (typeName == "RSO")
                {
                    rsoBtn.CssClass = "btn btn-info";
                }

                if (categoryName == "Fundraising")
                {
                    fundraisingBtn.CssClass = "btn btn-info";
                }
                else if (categoryName == "Social")
                {
                    socialBtn.CssClass = "btn btn-info";
                }
                else if (categoryName == "Educational")
                {
                    educationalBtn.CssClass = "btn btn-info";
                }
                else if (categoryName == "Entertainment")
                {
                    entertainmentBtn.CssClass = "btn btn-info";
                }

                List<User> attendees = Sql.GetAttendeesByEventID((int)DataBinder.Eval(e.Item.DataItem, "EventID"));

                if (attendees.Any(x => x.UserID == loggedInUser.UserID))
                {
                    joinEventBtn.Enabled = false;
                }
                else
                {
                    joinEventBtn.Enabled = true;
                }

                List<Rating> ratings = Sql.GetRatingsByEventId((int)DataBinder.Eval(e.Item.DataItem, "EventID"));

                int average = 0;

                if (ratings.Count > 0)
                {
                    int total = 0;

                    for (int i = 0; i < ratings.Count; i++)
                    {
                        total += ratings[i].RatingValue;
                    }

                    average = total / ratings.Count;
                }

                eventRatingLbl.Text = average.ToString();
            }
        }

        /// <summary> gets the image out of the byte array for each event's image 
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

        /// <summary> Handles the JoinEvent button click event for the repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void JoinEventBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem rptItem = (RepeaterItem)btn.NamingContainer;
            HiddenField eventId = (HiddenField)rptItem.FindControl("EventIDField");

            Event evt = Sql.GetEventByID(Convert.ToInt32(eventId.Value));

            if (Sql.GetTypeById(evt.EventTypeID) == "RSO")
            {
                if (!Sql.GetRSOByUserId(loggedInUser.UserID).Any(x => x.RSOID == evt.RSOID))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "scriptkey", "<script>alert('This event is an RSO event. You must be a part of the same RSO as the RSO the event is associated with.');</script>", false);
                    return;
                }
            }

            if (Sql.GetTypeById(evt.EventTypeID) == "Private")
            {
                if (loggedInUser.UniversityID != evt.UniversityID)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "scriptkey", "<script>alert('This event is Private event. You must be a part of the same university as the university the event is associated with.');</script>", false);
                    return;
                }
            }

            Sql.AddAttendee(loggedInUser.UserID, Convert.ToInt32(eventId.Value));

            BindItemsList(FilteredEvents);
        }

        /// <summary> Handles the LeaveEvent button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LeaveBtn_Click(object sender, EventArgs e)
        {
            Sql.DeleteAttendee(loggedInUser.UserID, Convert.ToInt32(ModalDetailsEventId.Value));
            Response.Redirect("~/Events.aspx");
        }

        /// <summary> Handles the SaveEvent button click event for the modal event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            //  TODO: validate address via google maps
            // if valid continue, else return and display error

            verifyAddress.Value = ModalEditEventAddressTextBox.Text;

            Page.ClientScript.RegisterClientScriptInclude("contentpg2", ResolveUrl(@"Scripts\mapVerifyAddress.js"));
            if (!Master.Page.ClientScript.IsStartupScriptRegistered("mapVerifyAddress"))
            {
                Master.Page.ClientScript.RegisterStartupScript(this.GetType(), "mapVerifyAddress", "callAlert();", true);
            }

            // if invalid address generate error here
            if (verifyResult.Equals("bad"))
            {


            }

            int locationID = Sql.GetLocationIdByAddress(ModalEditEventAddressTextBox.Text);

            if (locationID == -1)
            {
                Sql.AddLocation(ModalEditEventAddressTextBox.Text);
                locationID = Sql.GetLocationIdByAddress(ModalEditEventAddressTextBox.Text);
            }

            List<Event> otherEvents = Sql.GetEvents();

            // checks if this event already exists for given datetime and location
            foreach (Event otherEvt in otherEvents)
            {
                if (otherEvt.Address == ModalEditEventAddressTextBox.Text && otherEvt.EventDate == Convert.ToDateTime(ModalEditEventDateTextBox.Text))
                {
                    // TODO: Figure out way to show error message in bootstrap modal/update panel?
                    return;
                }
            }

            string date = ModalEditEventDateTextBox.Text + " " + ModalEditEventTimeHourDdl.SelectedItem.Text + ":" + ModalEditEventTimeMinuteDdl.SelectedItem.Text + " " + ModalEditEventTimeOfDayDdl.SelectedItem.Text;

            Event evt = new Event
            {
                Address = ModalEditEventAddressTextBox.Text,
                AdminID = loggedInUser.UserID,
                Approved = true,
                ContactEmail = ModalEditEventContactEmailLbl.Text,
                ContactPhone = ModalEditEventContactPhoneLbl.Text,
                DatePublished = DateTime.Now,
                EventDate = DateTime.Parse(date),
                EventName = ModalEditEventTitleTextBox.Text,
                Description = ModalEditEventDescriptionTextBox.Text,
                EventCategoryID = Convert.ToInt32(ModalEditEventCategoryDdl.SelectedValue),
                EventTypeID = Convert.ToInt32(ModalEditEventTypeDdl.SelectedValue),
                LocationID = locationID,
                UniversityID = loggedInUser.UniversityID,
                RSOID = ModalEditEventRSODdl.SelectedValue == string.Empty ? 0 : Convert.ToInt32(ModalEditEventRSODdl.SelectedValue)
            };

            if (ModalEditEventPictureUploader.PostedFile != null)
            {
                string filePath = ModalEditEventPictureUploader.PostedFile.FileName;
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
                    Stream fs = ModalEditEventPictureUploader.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    evt.Picture = bytes;
                }
            }

            Sql.UpdateEvent(evt, ModalEditEventPictureUploader.PostedFile != null, !string.IsNullOrEmpty(ModalEditEventRSODdl.SelectedValue), Convert.ToInt32(ModalEditEventId.Value));

            Response.Redirect("~/Events.aspx");
        }

        /// <summary> Handles the CreateEvent button click event for the modal event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveCreateBtn_Click(object sender, EventArgs e)
        {
            //  TODO: validate address via google maps
            // if valid continue, else return and display error

            verifyAddress.Value = ModalCreateEventLocationAddressTextBox.Text;

            Page.ClientScript.RegisterClientScriptInclude("contentpg2", ResolveUrl(@"Scripts\mapVerifyAddress.js"));
            if (!Master.Page.ClientScript.IsStartupScriptRegistered("mapVerifyAddress"))
            {
                Master.Page.ClientScript.RegisterStartupScript(this.GetType(), "mapVerifyAddress", "callAlert();", true);
            }

            // if invalid address generate error here
            if (verifyResult.Equals("bad"))
            {


            }

            int locationID = Sql.GetLocationIdByAddress(ModalCreateEventLocationAddressTextBox.Text);

            if (locationID == -1)
            {
                Sql.AddLocation(ModalCreateEventLocationAddressTextBox.Text);
                locationID = Sql.GetLocationIdByAddress(ModalCreateEventLocationAddressTextBox.Text);
            }

            List<Event> otherEvents = Sql.GetEvents();

            // checks if this event already exists for given datetime and location
            foreach (Event otherEvt in otherEvents)
            {
                if (otherEvt.Address == ModalCreateEventLocationAddressTextBox.Text && otherEvt.EventDate == Convert.ToDateTime(ModalCreateEventDateTextBox.Text))
                {
                    // TODO: Figure out way to show error message in bootstrap modal?
                    return;
                }
            }

            string date = ModalCreateEventDateTextBox.Text + " " + ModalCreateEventTimeHourDdl.SelectedItem.Text + ":" + ModalCreateEventTimeMinuteDdl.SelectedItem.Text + " " + ModalCreateEventTimeOfDayDdl.SelectedItem.Text;

            Event evt = new Event
            {
                Address = ModalCreateEventLocationAddressTextBox.Text,
                AdminID = loggedInUser.UserID,
                Approved = true,
                ContactEmail = ModalCreateEventContactEmailLbl.Text,
                ContactPhone = ModalCreateEventContactPhoneLbl.Text,
                DatePublished = DateTime.Now,
                EventDate = DateTime.Parse(date),
                EventName = ModalCreateEventTitleTextBox.Text,
                Description = ModalCreateEventDescriptionTextBox.Text,
                EventCategoryID = Convert.ToInt32(ModalCreateEventCategoryDdl.SelectedValue),
                EventTypeID = Convert.ToInt32(ModalCreateEventTypeDdl.SelectedValue),
                LocationID = locationID,
                UniversityID = loggedInUser.UniversityID,
                RSOID = ModalCreateEventRSODdl.SelectedValue == string.Empty ? 0 : Convert.ToInt32(ModalCreateEventRSODdl.SelectedValue)
            };

            if (ModalCreateEventPictureUploader.PostedFile != null)
            {
                string filePath = ModalCreateEventPictureUploader.PostedFile.FileName;
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
                    Stream fs = ModalCreateEventPictureUploader.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((int)fs.Length);
                    evt.Picture = bytes;
                }
                else
                {
                    // TODO: Figure out way to show error message in bootstrap modal?
                    return;
                }
            }

            Sql.AddEvent(evt, ModalCreateEventPictureUploader.PostedFile != null, !string.IsNullOrEmpty(ModalCreateEventRSODdl.SelectedValue));

            Response.Redirect("~/Events.aspx");
        }

        /// <summary> Handles the rating change control event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModalDetailsEventRatingControl_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            int eventID = Convert.ToInt32(ModalDetailsEventId.Value);

            Rating rating = Sql.GetRatingByUserIdAndEventId(loggedInUser.UserID, eventID);
            
            if (rating == null)
            {
                Sql.AddRating(loggedInUser.UserID, eventID, Convert.ToInt32(e.Value));
            }
            else
            {
                Sql.UpdateRating(loggedInUser.UserID, eventID, Convert.ToInt32(e.Value));
            }

            List<Rating> ratings = Sql.GetRatingsByEventId(eventID);

            int average = 0;

            if (ratings.Count > 0)
            {
                int total = 0;

                for (int i = 0; i < ratings.Count; i++)
                {
                    total += ratings[i].RatingValue;
                }

                average = total / ratings.Count;
            }

            ModalDetailsEventRatingControl.CurrentRating = average;
            ModalDetailsEventRatingCount.Text = ratings.Count + " users have rated this event";
            ModalDetailsEventRatingAverage.Text = "Average rating for this event is " + average;
        }

        /// <summary> Handles the Details button click event for populating the DetailsModal with the event information from the repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DetailsBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem rptItem = (RepeaterItem)btn.NamingContainer;
            HiddenField eventId = (HiddenField)rptItem.FindControl("EventIDField");
            ModalDetailsEventId.Value = eventId.Value;
            Event detailEvent = Sql.GetEventByID(Convert.ToInt32(eventId.Value));
            User admin = Sql.GetUserById(detailEvent.AdminID);
            List<User> attendees = Sql.GetAttendeesByEventID(detailEvent.EventID);

            ModalEventImage.ImageUrl = GetImage(detailEvent.Picture);

            EventNameTextLabel.Text = detailEvent.EventName;
            CommentTextBox.Text = string.Empty;
            ModalDetailsEventAdminText.Text = admin.FirstName + " " +  admin.LastName;
            ModalDetailsEventAteendeeCountText.Text = attendees.Count.ToString();

            ModalDetailsEventAttendeeListBox.DataSource = attendees;
            ModalDetailsEventAttendeeListBox.DataTextField = "UserName";
            ModalDetailsEventAttendeeListBox.DataValueField = "UserID";
            ModalDetailsEventAttendeeListBox.DataBind();

            ModalDetailsEventCategoryText.Text = Sql.GetCategoryById(detailEvent.EventCategoryID);
            ModalDetailsEventTypeText.Text = Sql.GetTypeById(detailEvent.EventTypeID);

            RSO rso = Sql.GetRSOById(detailEvent.RSOID);

            if (rso == null)
            {
                ModalDetailsEventRSOText.Text = "";
            }
            else
            {
                ModalDetailsEventRSOText.Text = rso.RSOName;
            }
            ModalDetailsEventLocationText.Text = Sql.GetLocationById(detailEvent.LocationID).Address;
            ModalDetailsEventDescriptionText.Text = detailEvent.Description;
            ModalDetailsEventDateText.Text = detailEvent.EventDate.ToString();
            ModalDetailsEventDatePublishedText.Text = detailEvent.DatePublished.ToString();
            ModalDetailsEventContactPhoneText.Text = detailEvent.ContactPhone.ToString();
            ModalDetailsEventContactEmailText.Text = detailEvent.ContactEmail;

            List<Comment> comments = Sql.GetCommentsByEventId(detailEvent.EventID);
            rptComments.DataSource = comments;
            rptComments.DataBind();

            List<Rating> ratings = Sql.GetRatingsByEventId(detailEvent.EventID);

            int average = 0;

            if (ratings.Count > 0)
            {
                int total = 0;

                for (int i = 0; i < ratings.Count; i++)
                {
                    total += ratings[i].RatingValue;
                }

                average = total / ratings.Count;
            }

            ModalDetailsEventRatingControl.CurrentRating = average;
            ModalDetailsEventRatingCount.Text = ratings.Count + " users have rated this event";
            ModalDetailsEventRatingAverage.Text = "Average rating for this event is " + average;

            if (attendees.Any(x => x.UserID == loggedInUser.UserID))
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

            // add code to setup map?
            Page.ClientScript.RegisterClientScriptInclude("contentpg", ResolveUrl(@"Scripts\detailsMap.js"));
            if (!Master.Page.ClientScript.IsStartupScriptRegistered("detailsMap"))
            {
                Master.Page.ClientScript.RegisterStartupScript(this.GetType(), "detailsMap", "callAlert();", true);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "Show", "<script> $('#detailsModal').modal('toggle');</script>", false);
            UpdatePanel1.Update();
        }

        /// <summary> Handles the Edit button click event for populating the EditModal with the event information from the DetailsModal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditBtn_Click(object sender, EventArgs e)
        {
            ModalEditEventId.Value = ModalDetailsEventId.Value;
            ModalEditEventTitleTextBox.Text = EventNameTextLabel.Text;
            ModalEditEventAddressTextBox.Text = ModalDetailsEventLocationText.Text;

            foreach (ListItem li in ModalEditEventRSODdl.Items)
            {
                if (li.Text == ModalDetailsEventRSOText.Text)
                {
                    ModalEditEventRSODdl.SelectedValue = li.Value;
                }
            }

            foreach (ListItem li in ModalEditEventCategoryDdl.Items)
            {
                if (li.Text == ModalDetailsEventCategoryText.Text)
                {
                    ModalEditEventCategoryDdl.SelectedValue = li.Value;
                }
            }

            foreach (ListItem li in ModalEditEventTypeDdl.Items)
            {
                if (li.Text == ModalDetailsEventTypeText.Text)
                {
                    ModalEditEventTypeDdl.SelectedValue = li.Value;
                }
            }

            DateTime date = Convert.ToDateTime(ModalDetailsEventDateText.Text);

            ModalEditEventDateTextBox.Text = date.Month + "/" + date.Day + "/" + date.Year;
            ModalEditEventTimeHourDdl.SelectedValue = string.Format("{0:00}", date.Hour.ToString() == "0" ? "12" : date.Hour.ToString());
            ModalEditEventTimeMinuteDdl.SelectedValue = string.Format("{0:00}", date.Minute.ToString());
            ModalEditEventTimeOfDayDdl.SelectedValue = date.ToString("tt");

            ModalEditEventDescriptionTextBox.Text = ModalDetailsEventDescriptionText.Text;
            ModalEditEventContactPhoneLbl.Text = ModalDetailsEventContactPhoneText.Text;
            ModalEditEventContactEmailLbl.Text = ModalDetailsEventContactEmailText.Text;

            ScriptManager.RegisterStartupScript(this, GetType(), "Show", "<script> $('#EditEventModal').modal('toggle');</script>", false);
            UpdatePanel1.Update();
        }

        /// <summary> Handles the EditComment button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditButton_Click(object sender, EventArgs e)
        {
            CommentsMessage.Text = string.Empty;

            LinkButton btn = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btn.NamingContainer;
            HiddenField userIdField = (HiddenField)rptItem.FindControl("UserId");

            if (loggedInUser.UserID != Guid.Parse(userIdField.Value))
            {
                CommentsMessage.Text = "You can only edit comments you posted!";
                return;
            }

            Label commentLabel = (Label)rptItem.FindControl("Comment");
            TextBox commentTextBox = (TextBox)rptItem.FindControl("EditCommentTextBox");
            Button confirmButton = (Button)rptItem.FindControl("EditCommentConfirm");
            Button cancelButton = (Button)rptItem.FindControl("EditCommentCancel");

            LinkButton editButton = (LinkButton)rptItem.FindControl("EditButton");
            LinkButton deleteButton = (LinkButton)rptItem.FindControl("DeleteButton");

            commentLabel.Visible = false;
            editButton.Visible = false;
            deleteButton.Visible = false;

            commentTextBox.Visible = true;
            confirmButton.Visible = true;
            cancelButton.Visible = true;
        }

        /// <summary> Handles the DeleteComment button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            CommentsMessage.Text = string.Empty;

            LinkButton btn = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btn.NamingContainer;
            HiddenField userIdField = (HiddenField)rptItem.FindControl("UserId");

            if (loggedInUser.UserID != Guid.Parse(userIdField.Value))
            {
                CommentsMessage.Text = "You can only delete comments you posted!";
                return;
            }

            HiddenField commentIdField = (HiddenField)rptItem.FindControl("CommentId");

            Sql.DeleteComment(Convert.ToInt32(commentIdField.Value));
            List<Comment> comments = Sql.GetCommentsByEventId(Convert.ToInt32(ModalDetailsEventId.Value));
            rptComments.DataSource = comments;
            rptComments.DataBind();
        }

        /// <summary> Handles the EditCommentConfirm button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditCommentConfirm_Click(object sender, EventArgs e)
        {
            CommentsMessage.Text = string.Empty;

            Button btn = (Button)sender;
            RepeaterItem rptItem = (RepeaterItem)btn.NamingContainer;
            HiddenField userIdField = (HiddenField)rptItem.FindControl("UserId");
            HiddenField commentIdField = (HiddenField)rptItem.FindControl("CommentId");
            TextBox commentTextBox = (TextBox)rptItem.FindControl("EditCommentTextBox");

            List<Comment> comments;

            // don't want blank comments
            if (string.IsNullOrEmpty(commentTextBox.Text))
            {
                Sql.DeleteComment(Convert.ToInt32(commentIdField.Value));
                comments = Sql.GetCommentsByEventId(Convert.ToInt32(ModalDetailsEventId.Value));
                rptComments.DataSource = comments;
                rptComments.DataBind();
                return;
            }

            Sql.UpdateComment(commentTextBox.Text, Convert.ToInt32(commentIdField.Value));

            comments = Sql.GetCommentsByEventId(Convert.ToInt32(ModalDetailsEventId.Value));
            rptComments.DataSource = comments;
            rptComments.DataBind();
        }

        /// <summary> Handles the EditCommentCancel button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditCommentCancel_Click(object sender, EventArgs e)
        {
            CommentsMessage.Text = string.Empty;

            Button btn = (Button)sender;
            RepeaterItem rptItem = (RepeaterItem)btn.NamingContainer;
            Label commentLabel = (Label)rptItem.FindControl("Comment");
            TextBox commentTextBox = (TextBox)rptItem.FindControl("EditCommentTextBox");
            Button confirmButton = (Button)rptItem.FindControl("EditCommentConfirm");
            Button cancelButton = (Button)rptItem.FindControl("EditCommentCancel");
            LinkButton editButton = (LinkButton)rptItem.FindControl("EditButton");
            LinkButton deleteButton = (LinkButton)rptItem.FindControl("DeleteButton");

            commentLabel.Visible = true;
            editButton.Visible = true;
            deleteButton.Visible = true;

            commentTextBox.Visible = false;
            confirmButton.Visible = false;
            cancelButton.Visible = false;
        }

        /// <summary> Handles the AddComment button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddCommentBtn_Click(object sender, EventArgs e)
        {
            CommentsMessage.Text = string.Empty;

            if (!string.IsNullOrEmpty(CommentTextBox.Text))
            { 
                Comment comment = new Comment
                {
                    CommentDate = DateTime.Now,
                    Text = CommentTextBox.Text,
                    UserID = loggedInUser.UserID,
                    EventID = Convert.ToInt32(ModalDetailsEventId.Value)
                };

                Sql.AddComment(comment);

                List<Comment> comments = Sql.GetCommentsByEventId(Convert.ToInt32(ModalDetailsEventId.Value));
                rptComments.DataSource = comments;
                rptComments.DataBind();
            }
            else
            {
                CommentsMessage.Text = "You cannot add a empty comment!";
            }
        }

        /// <summary> Handles the Filter/Search changes for the event repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FilterChange(object sender, EventArgs e)
        {
            int filterIndex = FilterByRadioBtnGroup.SelectedIndex;
            List<RSO> rsos = Sql.GetRSOByUserId(loggedInUser.UserID);

            switch (filterIndex)
            {
                case 0:
                    FilteredEvents = AllEvents;
                    break;
                case 1:
                    FilteredEvents = AllEvents.Where(x => rsos.Any(y => y.RSOID == x.RSOID)).ToList();
                    break;
                case 2:
                    FilteredEvents = AllEvents.Where(x => Sql.GetTypeById(x.EventTypeID) == "Public").ToList();
                    break;
                case 3:
                    FilteredEvents = AllEvents.Where(x => Sql.GetTypeById(x.EventTypeID) == "Private" && x.UniversityID == loggedInUser.UniversityID).ToList();
                    break;

            }

            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                FilteredEvents = FilteredEvents.FindAll(x => x.EventName.ToLower().Contains(SearchBox.Text.ToLower()));
            }

            BindItemsList(FilteredEvents);
        }
    }
}