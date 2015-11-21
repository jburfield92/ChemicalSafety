using System;

namespace DatabaseCommunicationMethods
{
    public class Comment
    {
        public Comment()
        {
            EventID = 0;
            UserID = Guid.Empty;
            CommentID = 0;
            Text = string.Empty;
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

        public int CommentID
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public DateTime CommentDate
        {
            get;
            set;
        }
    }
}
