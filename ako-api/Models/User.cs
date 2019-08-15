using System;
using System.Collections.Generic;

namespace ako_api.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Course = new HashSet<Course>();
            UserCoursePlanner = new HashSet<UserCoursePlanner>();
        }

        public int UserId { get; set; }
        public string AuthProviderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<UserCoursePlanner> UserCoursePlanner { get; set; }
    }
}
