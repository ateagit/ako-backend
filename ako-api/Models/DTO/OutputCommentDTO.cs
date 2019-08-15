using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ako_api.Models.DTO
{
    public class OutputCommentDTO
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public string CreatorName { get; set; }
    }
}
