using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCommunicationMethods
{
    public class Sql
    {
        public static string GetConnection()
        {
            //return "Data Source=Joe-PC\\SQLEXPRESS;Initial Catalog=CEM_Database;Integrated Security=True"; //DESKTOP
            return "Data Source=joes-surface;Initial Catalog=CEM_Database;Integrated Security=True"; //laptop
        }

        #region DB Add Methods

        public static void AddUser(User user, bool pic)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"INSERT INTO dbo.[User] (UserID, UserName, FirstName, LastName, PasswordSalt, PasswordHash, IsSuperAdmin, Email, UniversityID) 
                    VALUES (@userId, @userName, @firstName, @lastName, @passwordSalt, @passwordHash, @isSuperAdmin, @email, @universityID)";

                command.Parameters.AddWithValue("@userId", user.UserID);
                command.Parameters.AddWithValue("@userName", user.UserName);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@passwordSalt", user.PasswordSalt);
                command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@isSuperAdmin", user.IsSuperAdmin);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@universityID", user.UniversityID);

                command.ExecuteNonQuery();

                if (pic)
                {
                    command.CommandText =
                        @"UPDATE dbo.[User]
                        SET Picture = @picture
                        WHERE UserID = @userID";

                    command.Parameters.Add("@picture", SqlDbType.VarBinary, user.Picture.Length).Value = user.Picture;

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddEvent(Event newEvent, bool pic, bool rso)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();
                command.CommandText =
                    @"INSERT INTO Event (EventName, DatePublished, Approved, ContactPhone, ContactEmail, Description, EventDate, LocationID, EventTypeID, EventCategoryID, AdminID, UniversityID) 
                    VALUES (@eventName, @datePublished, @approved, @contactPhone, @contactEmail, @description, @date, @locationID, @eventTypeID, @eventCategoryID, @adminID, @universityID)";

                command.Parameters.AddWithValue("@eventName", newEvent.EventName);
                command.Parameters.AddWithValue("@datePublished", newEvent.DatePublished);
                command.Parameters.AddWithValue("@approved", newEvent.Approved);
                command.Parameters.AddWithValue("@contactPhone", newEvent.ContactPhone);
                command.Parameters.AddWithValue("@contactEmail", newEvent.ContactEmail);
                command.Parameters.AddWithValue("@description", newEvent.Description);
                command.Parameters.AddWithValue("@date", newEvent.EventDate);
                command.Parameters.AddWithValue("@locationID", newEvent.LocationID);
                command.Parameters.AddWithValue("@eventTypeID", newEvent.EventTypeID);
                command.Parameters.AddWithValue("@eventCategoryID", newEvent.EventCategoryID);
                command.Parameters.AddWithValue("@adminID", newEvent.AdminID);
                command.Parameters.AddWithValue("@universityID", newEvent.UniversityID);

                command.ExecuteNonQuery();

                if (pic)
                {
                    command.CommandText =
                        @"UPDATE Event
                        SET Picture = @picture
                        WHERE EventName = @eventName";

                    command.Parameters.Add("@picture", SqlDbType.VarBinary, newEvent.Picture.Length).Value = newEvent.Picture;

                    command.ExecuteNonQuery();
                }

                if (rso)
                {
                    command.CommandText =
                        @"UPDATE Event
                        WHERE EventName = @eventName
                        SET RSOID = @rsoId";

                    command.Parameters.AddWithValue("@rsoId", newEvent.RSOID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddRSO(RSO newRso, bool pic)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {

                conn.Open();

                command.CommandText =
                    @"INSERT INTO RSO (RSOName, Approved, Description, CreatedDate, AdminID, UniversityID, Picture) 
                    VALUES (@name, @approved, @description, @createdDate, @adminID, @universityID, @picture)";

                command.Parameters.AddWithValue("@name", newRso.RSOName);
                command.Parameters.AddWithValue("@approved", newRso.Approved);
                command.Parameters.AddWithValue("@description", newRso.Description);
                command.Parameters.AddWithValue("@createdDate", newRso.CreatedDate);
                command.Parameters.AddWithValue("@adminID", newRso.AdminID);
                command.Parameters.AddWithValue("@universityID", newRso.UniversityID);

                command.ExecuteNonQuery();

                if (pic)
                {
                    command.CommandText =
                        @"UPDATE RSO 
                        SET Picture = @picture
                        WHERE RSOName = @name";

                    command.Parameters.Add("@picture", SqlDbType.VarBinary, newRso.Picture.Length).Value = newRso.Picture;

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddUniversity(University university, bool pic)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"INSERT INTO University (UniversityName, Description, CreatedDate, SuperAdminID, LocationID, EmailSuffix) 
                    VALUES (@name, @description, @createdDate, @superAdminID, @locationID, @suffix)";

                command.Parameters.AddWithValue("@name", university.Name);
                command.Parameters.AddWithValue("@description", university.Description);
                command.Parameters.AddWithValue("@createdDate", university.CreatedDate);
                command.Parameters.AddWithValue("@locationID", university.LocationID);
                command.Parameters.AddWithValue("@superAdminID", university.SuperAdminID);
                command.Parameters.AddWithValue("@suffix", university.Suffix);

                command.ExecuteNonQuery();

                if (pic)
                {
                    command.CommandText =
                        @"UPDATE University
                        SET Picture = @picture
                        WHERE University.UniversityName = @name";

                    command.Parameters.Add("@picture", SqlDbType.VarBinary, university.Picture.Length).Value = university.Picture;

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddComment(Comment commentToAdd)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();
                command.CommandText =
                    @"INSERT INTO EventComment (EventID, UserID, Comment, CommentDate)
                    VALUES (@eventID, @userID, @comment, @commentDate)";

                command.Parameters.AddWithValue("@eventID", commentToAdd.EventID);
                command.Parameters.AddWithValue("@userID", commentToAdd.UserID);
                command.Parameters.AddWithValue("@comment", commentToAdd.Text);
                command.Parameters.AddWithValue("@commentDate", DateTime.Now);

                command.ExecuteNonQuery();
            }
        }

        public static void AddAttendee(Guid userID, int eventID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"INSERT INTO Attends (UserID, EventID)
                    VALUES (@userID, @eventID)";

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@eventID", eventID);

                command.ExecuteNonQuery();
            }
        }

        public static void AddRating(Guid userID, int eventID, int rating)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"INSERT INTO EventRating (UserID, EventID, Rating)
                    VALUES (@userID, @eventID, @rating)";

                command.Parameters.AddWithValue("@eventID", eventID);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@rating", rating);

                command.ExecuteNonQuery();
            }
        }

        public static void AddRSOMember(int rsoID, Guid userID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"INSERT INTO Member (RSOID, UserID)
                    VALUES (@rsoID, @userID)";

                command.Parameters.AddWithValue("@rsoID", rsoID);
                command.Parameters.AddWithValue("@userID", userID);

                command.ExecuteNonQuery();
            }
        }

        public static void AddEventCategory(string name)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"INSERT INTO EventCategory (CategoryName)
                    VALUES (@name)";

                command.Parameters.AddWithValue("@name", name);

                command.ExecuteNonQuery();
            }
        }

        public static void AddEventType(string name)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"INSERT INTO EventType (TypeName)
                    VALUES (@name)";

                command.Parameters.AddWithValue("@name", name);

                command.ExecuteNonQuery();
            }
        }

        public static void AddLocation(string address)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"INSERT INTO Location (Address)
                    VALUES (@address)";

                command.Parameters.AddWithValue("@address", address);

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region DB Get Methods

        public static User GetUserByUserName(string userName)
        {
            User requestedUser = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT UserID, UserName, FirstName, LastName, PasswordSalt, PasswordHash, Email, IsSuperAdmin, Picture, UniversityID
                    FROM dbo.[User]
                    WHERE UserName = @userName";
                command.Parameters.AddWithValue("@userName", userName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedUser = new User
                        {
                            UserID = reader.GetGuid(0),
                            UserName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PasswordSalt = reader.GetString(4),
                            PasswordHash = reader.GetString(5),
                            Email = reader.GetString(6),
                            IsSuperAdmin = reader.GetBoolean(7),
                            Picture = !reader.IsDBNull(8) ? (byte[])reader.GetValue(8) : null,
                            UniversityID = reader.GetInt32(9)
                        };
                    }
                }
            }

            return requestedUser;
        }

        public static User GetUserByEmail(string email)
        {
            User requestedUser = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT UserID, UserName, FirstName, LastName, PasswordSalt, PasswordHash, Email, IsSuperAdmin, Picture, UniversityID
                    FROM dbo.[User]
                    WHERE Email = @email";

                command.Parameters.AddWithValue("@email", email);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedUser = new User
                        {
                            UserID = reader.GetGuid(0),
                            UserName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PasswordSalt = reader.GetString(4),
                            PasswordHash = reader.GetString(5),
                            Email = reader.GetString(6),
                            IsSuperAdmin = reader.GetBoolean(7),
                            Picture = !reader.IsDBNull(8) ? (byte[])reader.GetValue(8) : null,
                            UniversityID = reader.GetInt32(9)
                        };
                    }
                }
            }

            return requestedUser;
        }

        public static User GetUserById(Guid userID)
        {
            User requestedUser = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT UserID, UserName, FirstName, LastName, PasswordSalt, PasswordHash, Email, IsSuperAdmin, Picture, UniversityID
                    FROM dbo.[User]
                    WHERE userID = @userID";

                command.Parameters.AddWithValue("@userID", userID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedUser = new User
                        {
                            UserID = reader.GetGuid(0),
                            UserName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PasswordSalt = reader.GetString(4),
                            PasswordHash = reader.GetString(5),
                            Email = reader.GetString(6),
                            IsSuperAdmin = reader.GetBoolean(7),
                            Picture = !reader.IsDBNull(8) ? (byte[])reader.GetValue(8) : null,
                            UniversityID = reader.GetInt32(9)
                        };
                    }
                }
            }

            return requestedUser;
        }

        public static Event GetEventByID(int eventID)
        {
            Event FindEvent = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventID, EventName, DatePublished, Approved, ContactPhone, ContactEmail, Description, EventDate, LocationID, EventTypeID, EventCategoryID, Picture, AdminID, UniversityID, RSOID
                    FROM Event
                    WHERE EventID = @eventID";

                command.Parameters.AddWithValue("@eventID", eventID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FindEvent = new Event
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            DatePublished = reader.GetDateTime(2),
                            Approved = reader.GetBoolean(3),
                            ContactPhone = reader.GetString(4),
                            ContactEmail = reader.GetString(5),
                            Description = reader.GetString(6),
                            EventDate = reader.GetDateTime(7),
                            LocationID = reader.GetInt32(8),
                            EventTypeID = reader.GetInt32(9),
                            EventCategoryID = reader.GetInt32(10),
                            Picture = reader.IsDBNull(11) ? null : (byte[])reader.GetValue(11),
                            AdminID = reader.GetGuid(12),
                            UniversityID = reader.GetInt32(13),
                            RSOID = reader.IsDBNull(14) ? -1 : reader.GetInt32(14)
                        };
                    }
                }
            }

            return FindEvent;
        }

        public static List<Event> GetEvents()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventID
                    FROM Event";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(GetEventByID(reader.GetInt32(0)));
                    }
                }
            }

            return events;
        }

        public static List<Event> GetPublicEvents()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventID
                    FROM Event, EventType
                    WHERE Event.EventTypeID = EventType.EventTypeID AND EventType.TypeName = 'Public'";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(GetEventByID(reader.GetInt32(0)));
                    }
                }
            }

            return events;
        }

        public static List<Event> GetPrivateEvents()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventID
                    FROM Event, EventType
                    WHERE Event.EventTypeID = EventType.EventTypeID AND EventType.TypeName = 'Private'";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(GetEventByID(reader.GetInt32(0)));
                    }
                }
            }

            return events;
        }

        public static List<Event> GetRSOEvents()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventID
                    FROM Event, EventType
                    WHERE Event.EventTypeID = EventType.EventTypeID AND EventType.TypeName = 'RSO'";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(GetEventByID(reader.GetInt32(0)));
                    }
                }
            }

            return events;
        }

        public static List<Event> GetEventsByUserId(Guid userID)
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventID
                    FROM Attends 
                    WHERE UserID = @userID";

                command.Parameters.AddWithValue("@userID", userID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(GetEventByID(reader.GetInt32(0)));
                    }
                }
            }

            return events;
        }

        public static List<Event> GetEventsForRSOByID(int rsoID)
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventID
                    FROM Event 
                    WHERE RSOID = @rsoID";

                command.Parameters.AddWithValue("@rsoID", rsoID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(GetEventByID(reader.GetInt32(0)));
                    }
                }
            }

            return events;
        }

        public static Comment GetCommentById(int commentID)
        {
            Comment requestedComment = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT CommentID, EventID, UserID, Comment, CommentDate
                    FROM EventComment
                    WHERE CommentID = @commentID";

                command.Parameters.AddWithValue("@commentID", commentID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedComment = new Comment
                        {
                            CommentID = reader.GetInt32(0),
                            EventID = reader.GetInt32(1),
                            UserID = reader.GetGuid(2),
                            Text = reader.GetString(3),
                            CommentDate = reader.GetDateTime(4)
                        };
                    }
                }
            }

            return requestedComment;
        }
        
        public static List<Comment> GetCommentsByEventId(int eventID)
        {
            List<Comment> comments = new List<Comment>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT CommentID
                    FROM EventComment
                    WHERE EventID = @eventID";

                command.Parameters.AddWithValue("@eventID", eventID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(GetCommentById(reader.GetInt32(0)));
                    }
                }
            }

            return comments;
        }

        public static Rating GetRatingById(int ratingID)
        {
            Rating requestedRating = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventRatingID, EventID, UserID, Rating
                    FROM EventRating
                    WHERE EventRatingID = @ratingID";

                command.Parameters.AddWithValue("@ratingID", ratingID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedRating = new Rating
                        {
                            EventRatingID = reader.GetInt32(0),
                            EventID = reader.GetInt32(1),
                            UserID = reader.GetGuid(2),
                            RatingValue = reader.GetInt32(3)
                        };
                    }
                }
            }

            return requestedRating;
        }

        public static Rating GetRatingByUserIdAndEventId(Guid userID, int eventID)
        {
            Rating requestedRating = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventRatingID, EventID, UserID, Rating
                    FROM EventRating
                    WHERE UserID = @userID AND EventID = @eventID";

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@eventID", eventID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedRating = new Rating
                        {
                            EventRatingID = reader.GetInt32(0),
                            EventID = reader.GetInt32(1),
                            UserID = reader.GetGuid(2),
                            RatingValue = reader.GetInt32(3)
                        };
                    }
                }
            }

            return requestedRating;
        }

        public static List<Rating> GetRatingsByEventId(int eventID)
        {
            List<Rating> ratings = new List<Rating>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventRatingID
                    FROM EventRating
                    WHERE EventID = @eventID";

                command.Parameters.AddWithValue("@eventID", eventID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ratings.Add(GetRatingById(reader.GetInt32(0)));
                    }
                }
            }

            return ratings;
        }

        public static List<User> GetAttendeesByEventID(int eventID)
        {
            List<User> attendees = new List<User>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT UserID
                    FROM Attends
                    WHERE EventID = @eventID";

                command.Parameters.AddWithValue("@eventID", eventID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        attendees.Add(GetUserById(reader.GetGuid(0)));
                    }
                }
            }

            return attendees;
        }

        public static University GetUniversityById(int universityID)
        {
            University requestedUnivesrity = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT UniversityID, UniversityName, Description, CreatedDate, SuperAdminID, LocationID, Picture, EmailSuffix
                    FROM University
                    WHERE UniversityID = @universityID";

                command.Parameters.AddWithValue("@universityID", universityID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedUnivesrity = new University
                        {
                            UniversityID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            CreatedDate = reader.GetDateTime(3),
                            SuperAdminID = reader.GetGuid(4),
                            LocationID = reader.GetInt32(5),
                            Picture = reader.IsDBNull(6) ? null : (byte[])reader.GetValue(6),
                            Suffix = reader.GetString(7)
                        };
                    }
                }
            }

            return requestedUnivesrity;
        }

        public static List<University> GetUniversities()
        {
            List<University> universities = new List<University>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT UniversityID
                    FROM University";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        universities.Add(GetUniversityById(reader.GetInt32(0)));
                    }
                }
            }

            return universities;
        }

        public static RSO GetRSOById(int rsoID)
        {
            RSO requestedRso = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT RSOID, RSOName, Approved, CreatedDate, Description, Picture, AdminID, UniversityID
                    FROM RSO
                    WHERE RSOID = @rsoID";

                command.Parameters.AddWithValue("@rsoID", rsoID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedRso = new RSO
                        {
                            RSOID = reader.GetInt32(0),
                            RSOName = reader.GetString(1),
                            Approved = reader.GetBoolean(2),
                            CreatedDate = reader.GetDateTime(3),
                            Description = reader.GetString(4),
                            Picture = !reader.IsDBNull(5) ? (byte[])reader.GetValue(5) : null,
                            AdminID = reader.GetGuid(6),
                            UniversityID = reader.GetInt32(7),
                        };
                    }
                }
            }

            return requestedRso;
        }

        public static List<RSO> GetRSOs()
        {
            List<RSO> rsos = new List<RSO>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT RSOID
                    FROM RSO";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rsos.Add(GetRSOById(reader.GetInt32(0)));
                    }
                }
            }

            return rsos;
        }

        public static List<User> GetRSOMembers(int rsoID)
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT UserID
                    FROM Users, RSO
                    WHERE RSO.RSOID = @rsoID";

                command.Parameters.AddWithValue("@rsoID", rsoID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(GetUserById(reader.GetGuid(0)));
                    }
                }
            }

            return users;
        }

        public static List<RSO> GetRSOByUserId(Guid userID)
        {
            List<RSO> rsos = new List<RSO>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT RSOID
                    FROM Member
                    WHERE UserID = @userID";

                command.Parameters.AddWithValue("@userID", userID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rsos.Add(GetRSOById(reader.GetInt32(0)));
                    }
                }
            }

            return rsos;
        }

        public static Location GetLocationById(int LocationID)
        {
            Location requestedLocation = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT LocationID, Address
                    FROM Location
                    WHERE LocationID = @locationID";

                command.Parameters.AddWithValue("@locationID", LocationID);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requestedLocation = new Location
                        {
                            LocationID = reader.GetInt32(0),
                            Address = reader.GetString(1)
                        };
                    }
                }
            }

            return requestedLocation;
        }

        public static string GetCategoryById(int categoryID)
        {
            string requestedCategory = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT CategoryName
                    FROM EventCategory
                    WHERE EventCategoryID = @categoryID";

                command.Parameters.AddWithValue("@categoryID", categoryID);

                requestedCategory = command.ExecuteScalar().ToString();
            }

            return requestedCategory;
        }

        public static string GetTypeById(int typeID)
        {
            string requestedType = null;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT TypeName
                    FROM EventType
                    WHERE EventTypeID = @typeID";

                command.Parameters.AddWithValue("@typeID", typeID);

                requestedType = command.ExecuteScalar().ToString();
            }

            return requestedType;
        }

        public static int GetLocationIdByAddress(string address)
        {
            object result;

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT LocationID
                    FROM Location
                    WHERE Address = @address";

                command.Parameters.AddWithValue("@address", address);

                result = command.ExecuteScalar();
            }

            return result == null ? -1 : Convert.ToInt32(result);
        }
        
        public static List<EventCategory> GetCategories()
        { 
            List<EventCategory> categories = new List<EventCategory>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventCategoryID, CategoryName
                    FROM EventCategory";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new EventCategory { EventCategoryID = reader.GetInt32(0), CategoryName = reader.GetString(1) });
                    }
                }
            }

            return categories;
        }

        public static List<EventType> GetEventTypes()
        {
            List<EventType> types = new List<EventType>();

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"SELECT EventTypeID, TypeName
                    FROM EventType";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        types.Add(new EventType { EventTypeID = reader.GetInt32(0), TypeName = reader.GetString(1) });
                    }
                }
            }

            return types;
        }

        #endregion

        #region DB Update Methods

        public static void UpdateUser(User user, bool pic)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"UPDATE dbo.[User] 
                    SET UserID = @userId, UserName = @userName, FirstName = @firstName, LastName = @lastName, Email = @email, UniversityID = @universityID
					WHERE UserID = @userID";

                command.Parameters.AddWithValue("@userID", user.UserID);
                command.Parameters.AddWithValue("@userName", user.UserName);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@universityID", user.UniversityID);

                command.ExecuteNonQuery();

                if (pic)
                {
                    command.CommandText =
                        @"UPDATE dbo.[User] 
                        SET Picture = @picture
                        WHERE UserID = @userID";

                    command.Parameters.AddWithValue("@picture", user.Picture);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateEvent(Event newEvent, bool pic, bool rso, int eventID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();
                command.CommandText =
                    @"UPDATE Event
                    SET EventName = @eventName, Approved = @approved, ContactPhone = @contactPhone, ContactEmail = @contactEmail, Description = @description, EventDate = @date
                    WHERE EventID = @eventID";

                command.Parameters.AddWithValue("@eventID", eventID);
                command.Parameters.AddWithValue("@eventName", newEvent.EventName);
                command.Parameters.AddWithValue("@approved", newEvent.Approved);
                command.Parameters.AddWithValue("@contactPhone", newEvent.ContactPhone);
                command.Parameters.AddWithValue("@contactEmail", newEvent.ContactEmail);
                command.Parameters.AddWithValue("@description", newEvent.Description);
                command.Parameters.AddWithValue("@date", newEvent.EventDate);

                command.ExecuteNonQuery();

                if (pic)
                {
                    command.CommandText =
                        @"UPDATE Event
                        SET Picture = @picture
                        WHERE EventName = @eventName";

                    command.Parameters.AddWithValue("@picture", newEvent.Picture);

                    command.ExecuteNonQuery();
                }


            }
        }

        public static void UpdateRSO(RSO newRso, bool pic)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {

                conn.Open();

                command.CommandText =
                    @"UPDATE RSO
                    SET RSOName = @name, Approved = @approved, Description = @description
                    WHERE (RSO.RSOID = rsoID)";

                command.Parameters.AddWithValue("@rsoID", newRso.RSOID);
                command.Parameters.AddWithValue("@name", newRso.RSOName);
                command.Parameters.AddWithValue("@approved", newRso.Approved);
                command.Parameters.AddWithValue("@description", newRso.Description);


                command.ExecuteNonQuery();

                if (pic)
                {
                    command.CommandText =
                        @"UPDATE RSO
                        SET Picture = @picture
                        WHERE RSO.RSOName = @name";

                    command.Parameters.Add("@picture", SqlDbType.VarBinary, newRso.Picture.Length).Value = newRso.Picture;

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateUniversity(University university, bool pic, int universityID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"UPDATE University
                    SET UniversityName = @name, Description = @description
                    WHERE University.UniversityID = @universityID";

                command.Parameters.AddWithValue("@universityID", universityID);
                command.Parameters.AddWithValue("@name", university.Name);
                command.Parameters.AddWithValue("@description", university.Description);

                command.ExecuteNonQuery();

                if (pic)
                {
                    command.CommandText =
                        @"UPDATE University
                        SET Picture = @picture
                        WHERE University.UniversityID = @universityID";

                    command.Parameters.Add("@picture", SqlDbType.VarBinary, university.Picture.Length).Value = university.Picture;

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateComment(string text, int commentID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"UPDATE EventComment
                    SET Comment = @comment, CommentDate = @commentDate
                    WHERE EventComment.commentID = @cID";

                command.Parameters.AddWithValue("@cID", commentID);
                command.Parameters.AddWithValue("@comment", text);
                command.Parameters.AddWithValue("@commentDate", DateTime.Now);

                command.ExecuteNonQuery();
            }
        }

        public static void UpdateRating(Guid userID, int eventID, int rating)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"UPDATE EventRating
                    SET Rating = @rating
                    WHERE eventID = @eventID AND UserID = @userID";

                command.Parameters.AddWithValue("@rating", rating);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@eventID", eventID);

                command.ExecuteNonQuery();
            }
        }

        public static void UpdateLocation(string address, int LocationID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"UPDATE Location
                    SET Address = @address
                    WHERE @locationID = LocationID";
               
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@locationID", LocationID);

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region DB Delete Methods

        public static void DeleteEvent(Event eventDelete)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"DELETE FROM Location
                    WHERE LocationID = @locationID;
                
                    DELETE FROM Attends 
                    WHERE EventID = @eventID;

                    DELETE FROM Event
                    WHERE EventID = @eventID;";

                command.Parameters.AddWithValue("@locationID", eventDelete.LocationID);
                command.Parameters.AddWithValue("@eventID", eventDelete.EventID);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteComment(int commentID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"DELETE FROM EventComment
                    WHERE CommentID = @commentID";

                command.Parameters.AddWithValue("@commentID", commentID);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteAttendee(Guid userID, int eventID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"DELETE FROM Attends 
                    WHERE EventID = @eventID AND UserID = @userID";

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@eventID", eventID);

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteMember(Guid userID, int rsoID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            using (SqlCommand command = new SqlCommand(null, conn))
            {
                conn.Open();

                command.CommandText =
                    @"DELETE FROM Member 
                    WHERE RSOID = @rsoID AND UserID = @userID";

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@rsoID", rsoID);

                command.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
