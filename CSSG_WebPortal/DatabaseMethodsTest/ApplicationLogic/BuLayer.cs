using System.Configuration;

namespace CSSG_DatabaseMethods
{
    public class BuLayer
    {
        /// <summary> Gets the connection string
        /// </summary>
        /// <returns></returns>
        public static string GetNewConnection()
        {
            return ConfigurationManager.ConnectionStrings["CSSG"].ConnectionString;
        }

        /// <summary> Returns true if the given password is matches what is stored in the DB
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string username, string password)
        {
            UserPassword membership = DaLayer.GetUserCreditials(username);

            byte[] enteredPasswordHashed = membership.CreatePasswordHash(password, membership.Salt);

            return (membership.Password == enteredPasswordHashed);
        }

        public static bool VerifyUsername(string username)
        {

            return true;
        }
    }
}
