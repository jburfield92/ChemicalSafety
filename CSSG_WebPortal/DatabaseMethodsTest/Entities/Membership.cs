using System;
using System.Security.Cryptography;
using System.Text;

namespace CSSG_DatabaseMethods
{
    /// <summary> Class that stores the password information for a user
    /// </summary>
    public class UserPassword
    {
        /// <summary> constructor
        /// </summary>
        public UserPassword()
        {
            PasswordAttemptCount = 0;
            PasswordAnswerAttemptCount = 0;
            IsLockedOut = false;
        }

        /// <summary> the password
        /// </summary>
        public byte[] Password
        {
            get; set;
        }

        /// <summary> the security question to reset a password
        /// </summary>
        public string PasswordQuestion
        {
            get; set;
        }

        /// <summary> the answer to the security question
        /// </summary>
        public string PasswordAnswer
        {
            get; set;
        }

        public DateTime CreatedDate
        {
            get; set;
        }

        /// <summary> the number of attempts to guess a password
        /// </summary>
        public int PasswordAttemptCount
        {
            get; set;
        }

        /// <summary> the number of attempts to reset a password
        /// </summary>
        public int PasswordAnswerAttemptCount
        {
            get; set;
        }

        /// <summary> locks the user out
        /// </summary>
        public bool IsLockedOut
        {
            get; set;
        }

        /// <summary> The salt for the password
        /// </summary>
        public string Salt
        {
            get; set;
        }

        /// <summary> Creates the salt for the membership
        /// </summary>
        /// <param name="size"></param>
        public string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[size];
            rng.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public byte[] CreatePasswordHash(string password, string salt)
        {
            string saltedPassword = string.Concat(password, salt);

            byte[] passwordBytes = Encoding.ASCII.GetBytes(saltedPassword);
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] hashedPassword = sha1.ComputeHash(passwordBytes);

            return hashedPassword;
        }
    }
}
