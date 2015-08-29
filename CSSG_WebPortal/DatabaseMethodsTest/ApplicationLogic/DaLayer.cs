using System;
using System.Data.SqlClient;
using System.Text;

namespace CSSG_DatabaseMethods
{
    public class DaLayer
    {
        /// <summary> Adds a user to the DB
        /// </summary>
        /// <param name="sensitiveInfo"></param>
        /// <param name="user"></param>
        public static void AddUser(UserPassword sensitiveInfo, User user)
        {
            using (SqlConnection connection = new SqlConnection(BuLayer.GetNewConnection()))
            using (SqlCommand command = new SqlCommand(null, connection))
            {
                connection.Open();

                // setup the membership row
                command.CommandText =
                    @"INSERT INTO Memberships 
                        (Password, PasswordSalt, PasswordQuestion, PasswordAnswer, CreatedDate, PasswordAttemptCount, PasswordAnswerAttemptCount, LockedOut)
                    OUTPUT INSERTED.MembershipId
                    VALUES (@password, @passwordSalt, @passwordQuestion, @passwordAnswer, @createdDate, @passwordAttemptCount, @passwordAnswerAttemptCount, @lockedOut)";

                command.Parameters.AddWithValue("@password", sensitiveInfo.Password);
                command.Parameters.AddWithValue("@passwordSalt", sensitiveInfo.Salt);
                command.Parameters.AddWithValue("@passwordQuestion", sensitiveInfo.PasswordQuestion);
                command.Parameters.AddWithValue("@passwordAnswer", sensitiveInfo.PasswordAnswer);
                command.Parameters.AddWithValue("@createdDate", sensitiveInfo.CreatedDate);
                command.Parameters.AddWithValue("@passwordAttemptCount", sensitiveInfo.PasswordAttemptCount);
                command.Parameters.AddWithValue("@passwordAnswerAttemptCount", sensitiveInfo.PasswordAnswerAttemptCount);
                command.Parameters.AddWithValue("@lockedOut", sensitiveInfo.IsLockedOut);

                int membershipId = (int)command.ExecuteScalar();

                command.Parameters.Clear();

                // setup the user row
                command.CommandText =
                    @"INSERT INTO Users (UserId, UserName, LastLoginDate, FirstName, LastName, Email, MembershipId) 
                    VALUES (@userId, @userName, @lastLoginDate, @firstName, @lastName, @email, @membershipId)";

                command.Parameters.AddWithValue("@userId", user.UserId);
                command.Parameters.AddWithValue("@userName", user.UserName);
                command.Parameters.AddWithValue("@lastLoginDate", user.LastLoginDate);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@membershipId", membershipId);

                command.ExecuteNonQuery();
            }
        }

        /// <summary> Gets a user from the DB
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User GetUserByUserName(string username)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(BuLayer.GetNewConnection()))
            using (SqlCommand command = new SqlCommand(null, connection))
            {
                connection.Open();

                command.CommandText =
                    @"SELECT Users.UserName, Users.LastLoginDate, Users.FirstName, Users.LastName, Users.Email
                    FROM Users
                    WHERE Users.UserName = @userName";
                command.Parameters.AddWithValue("@userName", username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User()
                        {
                            UserName = reader.GetString(0),
                            LastLoginDate = reader.GetDateTime(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            Email = reader.GetString(4),
                        };
                    }
                }
            }

            return user;
        }

        /// <summary> Gets the user by their email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User GetUserByEmail(string email)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(BuLayer.GetNewConnection()))
            using (SqlCommand command = new SqlCommand(null, connection))
            {
                connection.Open();

                command.CommandText =
                    @"SELECT Users.UserName, Users.LastLoginDate, Users.FirstName, Users.LastName, Users.Email
                    FROM Users
                    WHERE Users.Email = @email";
                command.Parameters.AddWithValue("@email", email);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User()
                        {
                            UserName = reader.GetString(0),
                            LastLoginDate = reader.GetDateTime(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            Email = reader.GetString(4),
                        };
                    }
                }
            }

            return user;
        }

        /// <summary> Deletes the user by their username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool DeleteUserByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(BuLayer.GetNewConnection()))
            using (SqlCommand command = new SqlCommand(null, connection))
            {
                connection.Open();

                command.CommandText =
                    @"DELETE FROM Users WHERE Users.UserName = @username";
                command.Parameters.AddWithValue("@username", username);

                if (command.ExecuteNonQuery() >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary> Deletes the user by their email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool DeleteUserByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(BuLayer.GetNewConnection()))
            using (SqlCommand command = new SqlCommand(null, connection))
            {
                connection.Open();

                command.CommandText =
                    @"DELETE FROM Users WHERE Users.Email = @email";
                command.Parameters.AddWithValue("@email", email);

                if (command.ExecuteNonQuery() >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary> Gets the Membership class for the user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static UserPassword GetUserCreditials(string username)
        {
            UserPassword membership = null;

            using (SqlConnection connection = new SqlConnection(BuLayer.GetNewConnection()))
            using (SqlCommand command = new SqlCommand(null, connection))
            {
                connection.Open();

                command.CommandText =
                    @"SELECT Memberships.Password, Memberships.PasswordSalt, Memberships.PasswordQuestion,
                    Memberships.PasswordAnswer, Memberships.createdDate, Memberships.PasswordAttemptCount,
                    Memberships.PasswordAnswerAttemptCount, Memberships.LockedOut
                    FROM Memberships
                    INNER JOIN Users ON Users.MembershipId = Memberships.MembershipId
                    WHERE Users.UserName = @username";

                command.Parameters.AddWithValue("@username", username);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        membership = new UserPassword()
                        {
                            Password = Encoding.ASCII.GetBytes(reader.GetString(0)),
                            Salt = reader.GetString(1),
                            PasswordQuestion = reader.GetString(2),
                            PasswordAnswer = reader.GetString(3),
                            CreatedDate = reader.GetDateTime(4),
                            PasswordAttemptCount = reader.GetInt32(5),
                            PasswordAnswerAttemptCount = reader.GetInt32(6),
                            IsLockedOut = reader.GetBoolean(7)
                        };
                    }
                }

                return membership;
            }
        } 
    }
}
