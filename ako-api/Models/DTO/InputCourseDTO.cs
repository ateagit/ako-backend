using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ako_api.Models.DTO
{
    public class InputCourseDTO
    {
        public int SubjectId { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public dynamic Content { get; set; }
        public int Difficulty { get; set; }
        public List<int> PrerequisiteCourseId { get; set; }
    }
}
