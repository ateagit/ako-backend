using System;
using System.Collections.Generic;

namespace ako_api.Models
{
    public partial class CoursePrerequisite
    {
        public int PrerequisiteId { get; set; }
        public int? MainCourseId { get; set; }
        public int? PrerequisiteCourseId { get; set; }

        public virtual Course MainCourse { get; set; }
        public virtual Course PrerequisiteCourse { get; set; }
    }
}
