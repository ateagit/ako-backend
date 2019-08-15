using System;
using System.Collections.Generic;

namespace ako_api.Models
{
    public partial class UserCoursePlanner
    {
        public int UserCoursePlannerId { get; set; }
        public int? CourseId { get; set; }
        public int? UserId { get; set; }
        public int? Rating { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
