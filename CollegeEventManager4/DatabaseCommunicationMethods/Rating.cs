using System;

namespace DatabaseCommunicationMethods
{
    public class Rating
    {
        public Rating()
        {
            EventID = 0;
            UserID = Guid.Empty;
            EventRatingID = 0;
            RatingValue = 0;
        }

        public int EventID
        {
            get;
            set;
        }

        public Guid UserID
        {
            get;
            set;
        }

        public int EventRatingID
        {
            get;
            set;
        }

        public int RatingValue
        {
            get;
            set;
        }
    }
}
