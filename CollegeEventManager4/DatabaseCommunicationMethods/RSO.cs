using System;

namespace DatabaseCommunicationMethods
{
    public class RSO
    {
        public RSO()
        {
            RSOID = 0;
            RSOName = string.Empty;
            Approved = false;
            Description = string.Empty;
            CreatedDate = DateTime.MinValue;
            Picture = null;
            AdminID = Guid.Empty;
            UniversityID = 0;
        }
        public int RSOID
        {
            get;
            set;
        }
        public string RSOName
        {
            get;
            set;
        }

        public bool Approved
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

        public byte[] Picture
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
