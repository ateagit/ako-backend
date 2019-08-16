using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ako_api.Models.DTO
{
    public class OutputBasicCourseDTO
    {

        public int CourseId { get; set; }
        public string SubjectName { get; set; }
        public int SubjectId { get; set; }
        public string CreatorName { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }

    }
}
