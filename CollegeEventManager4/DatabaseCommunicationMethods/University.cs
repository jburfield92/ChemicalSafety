using System;

namespace DatabaseCommunicationMethods
{
    public class University
    {
        public University()
        {
            UniversityID = 0;
            Name = string.Empty;
            Description = string.Empty;
            CreatedDate = DateTime.MinValue;
            SuperAdminID = Guid.Empty;
            LocationID = int.MinValue;
            Picture = null;
            Suffix = null;
        }

        public int UniversityID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public Guid SuperAdminID
        {
            get;
            set;
        }

        public int LocationID
        {
            get;
            set;
        }

        public byte[] Picture
        {
            get;
            set;
        }

        public string Suffix
        {
            get;
            set;
        }
    }
}
