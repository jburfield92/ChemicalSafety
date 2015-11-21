using System;

namespace DatabaseCommunicationMethods
{
    public class EventCategory
    {
        public EventCategory()
        {
            EventCategoryID = 0;
            CategoryName = string.Empty;
        }

        public int EventCategoryID
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            set;
        }
    }
}
