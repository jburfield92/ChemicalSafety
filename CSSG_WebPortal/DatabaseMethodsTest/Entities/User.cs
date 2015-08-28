using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSG_DatabaseMethods
{
    /// <summary> Class that stores the user information
    /// </summary>
    public class User
    {
        /// <summary> Constructor
        /// </summary>
        public User()
        {
            UserId = Guid.Empty;
            UserName = string.Empty;
            LastLoginDate = DateTime.MinValue;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        public Guid UserId
        {
            get;
            set;
        }

        /// <summary> the users name
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary> the last login date of the user
        /// </summary>
        public DateTime LastLoginDate
        {
            get;
            set;
        }

        /// <summary> the users first name
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary> the users last name
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary> the users email address
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary> the organization ID
        /// </summary>
        public int OrganizationId
        {
            get;
            set;
        }

        /// <summary> the department ID
        /// </summary>
        public int DepartmentId
        {
            get;
            set;
        }

        /// <summary> the location ID
        /// </summary>
        public int LocationId
        {
            get;
            set;
        }

        /// <summary> the membership ID
        /// </summary>
        public int MembershipId
        {
            get;
            set;
        }
    }
}
