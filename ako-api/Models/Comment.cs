using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ako_api.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? CourseId { get; set; }
        [StringLength(255)]
        public string Message { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("CourseId")]
        [InverseProperty("Comment")]
        public virtual Course Course { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Comment")]
        public virtual User User { get; set; }
    }
}
