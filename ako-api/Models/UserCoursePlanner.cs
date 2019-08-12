using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ako_api.Models
{
    public partial class UserCoursePlanner
    {
        public int UserCoursePlannerId { get; set; }
        public int? CourseId { get; set; }
        public int? UserId { get; set; }
        public int? Rating { get; set; }

        [ForeignKey("CourseId")]
        [InverseProperty("UserCoursePlanner")]
        public virtual Course Course { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserCoursePlanner")]
        public virtual User User { get; set; }
    }
}
