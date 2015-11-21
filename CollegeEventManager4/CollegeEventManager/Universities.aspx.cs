using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollegeEventManager
{
    public partial class Universities : System.Web.UI.Page
    {
        // <summary> The current page in the pagination
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

        /// <summary> Handles the page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItemsList();
            }
        }

        /// <summary> Handles an event when we databound one of the rows in the repeater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RptUniversities_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // if there are no items
            if (RptUniversities.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label emptyLbl = (Label)e.Item.FindControl("EmptyLbl");
                    emptyLbl.Visible = true;
                }
            }
        }

        /// <summary> gets the image out of the byte array for each event
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string GetImage(object img)
        {
            return "data:image/jpg;base64, " + Convert.ToBase64String((byte[])img);
        }

        /// <summary> Gets the datatable for the repeater
        /// TODO: THIS WILL BE POPULATING USING A DB CALL
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            byte[] img = (new WebClient()).DownloadData("http://a5.mzstatic.com/us/r30/Purple5/v4/5a/2e/e9/5a2ee9b3-8f0e-4f8b-4043-dd3e3ea29766/icon128-2x.png");

            DataTable dt = new DataTable();

            dt.Columns.Add("UniversityId", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UniversityEmail", typeof(string));
            dt.Columns.Add("Image", typeof(byte[]));
            dt.Columns.Add("CreatedDate", typeof(byte[]));
            dt.Columns.Add("Location", typeof(byte[]));

            for (int i = 1; i <= 100; i++)
            {
                dt.Rows.Add(i, "Test University " + i, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec.", "knights.ucf.edu", img, DateTime.Now, "123 Fake Address");
            }

            return dt;
        }

        /// <summary> Binds the items in the repeater
        /// </summary>
        private void BindItemsList()
        {
            DataTable dataTable = GetDataTable();
            _PageDataSource.DataSource = dataTable.DefaultView;
            _PageDataSource.AllowPaging = true;
            _PageDataSource.PageSize = 10;
            _PageDataSource.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = _PageDataSource.PageCount;

            lbtnPrevious.Enabled = !_PageDataSource.IsFirstPage;
            lbtnNext.Enabled = !_PageDataSource.IsLastPage;
            lbtnFirst.Enabled = !_PageDataSource.IsFirstPage;
            lbtnLast.Enabled = !_PageDataSource.IsLastPage;

            RptUniversities.DataSource = _PageDataSource;
            RptUniversities.DataBind();
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
            BindItemsList();
        }

        /// <summary> Handles the Previous Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindItemsList();
        }

        /// <summary> Handles the Last Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindItemsList();
        }

        /// <summary> Handles the First Page click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindItemsList();
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
                BindItemsList();
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

        /// <summary> Creating a university event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveCreateBtn_Click(object sender, EventArgs e)
        {

        }

        /// <summary> Showing details of the university event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DetailsBtn_Click(object sender, EventArgs e)
        {

        }
    }
}