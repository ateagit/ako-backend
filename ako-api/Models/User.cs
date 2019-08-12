using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ako_api.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            UserCoursePlanner = new HashSet<UserCoursePlanner>();
        }

        public int UserId { get; set; }
        [StringLength(255)]
        public string AuthProviderId { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Comment> Comment { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserCoursePlanner> UserCoursePlanner { get; set; }
    }
}
