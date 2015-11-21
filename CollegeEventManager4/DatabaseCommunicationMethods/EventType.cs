namespace DatabaseCommunicationMethods
{
    public class EventType
    {
        public EventType()
        {
            EventTypeID = 0;
            TypeName = string.Empty;
        }

        public int EventTypeID
        {
            get;
            set;
        }

        public string TypeName
        {
            get;
            set;
        }
    }
}