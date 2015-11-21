using System;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseCommunicationMethods
{
    public class User
    {
        public User()
        {
            UserID = Guid.Empty;
            UserName = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PasswordSalt = string.Empty;
            IsSuperAdmin = false;
            UniversityID = int.MinValue;
            Picture = null;
        }

        public Guid UserID
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {

            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public string PasswordSalt
        {
            get;
            set;
        }

        public bool IsSuperAdmin
        {
            get;
            set;
        }

        public int UniversityID
        {
            get;
            set;
        }

        public byte[] Picture
        {
            get;
            set;
        }

        public string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[32];
            rng.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public string CreatePasswordHash(string password, string salt)
        {
            string saltedPassword = string.Concat(password, salt);

            byte[] passwordBytes = Encoding.ASCII.GetBytes(saltedPassword);
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] hashedPassword = sha1.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashedPassword);
        }
    }
}
