using System;

namespace DatabaseCommunicationMethods
{
    public class Event
    {
        public Event()
        {
            EventID = 0;
            EventName = string.Empty;
            DatePublished = DateTime.MinValue;
            Approved = false;
            ContactPhone = string.Empty;
            ContactEmail = string.Empty;
            Description = string.Empty;
            EventDate = DateTime.MinValue;
            LocationID = 0;
            EventTypeID = 0;
            EventCategoryID = 0;
            Picture = null;
            RSOID = 0;
            AdminID = Guid.Empty;
            UniversityID = 0;
        }

        public int EventID
        {
            get;
            set;
        }

        public string EventName
        {
            get;
            set;
        }

        public DateTime DatePublished
        {
            get;
            set;
        }

        public bool Approved
        {
            get;
            set;
        }

        public string ContactPhone
        {
            get;
            set;
        }

        public string ContactEmail
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public DateTime EventDate
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public int EventTypeID
        {
            get;
            set;
        }

        public int EventCategoryID
        {
            get;
            set;
        }

        public byte[] Picture
        {
            get;
            set;
        }

        public int RSOID
        {
            get;
            set;
        }

        public Guid AdminID
        {
            get;
            set;
        }

        public int UniversityID
        {
            get;
            set;
        }
    }
}
