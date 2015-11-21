using System;

namespace DatabaseCommunicationMethods
{
    public class Location
    {
        public Location()
        {
            LocationID = 0;
            Address = string.Empty;
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
    }
}
