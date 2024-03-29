﻿using System;
using System.Collections.Generic;

namespace ako_api.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? CourseId { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
