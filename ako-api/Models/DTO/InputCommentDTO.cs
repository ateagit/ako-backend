using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ako_api.Models.DTO
{
    public class InputCommentDTO
    {
        public int? CourseId { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
    }
}
