using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ako_api.Models
{
    public partial class CoursePrerequisite
    {
        public int PrerequisiteId { get; set; }
        public int? MainCourseId { get; set; }
        public int? PrerequisiteCourseId { get; set; }

        [ForeignKey("MainCourseId")]
        [InverseProperty("CoursePrerequisiteMainCourse")]
        public virtual Course MainCourse { get; set; }
        [ForeignKey("PrerequisiteCourseId")]
        [InverseProperty("CoursePrerequisitePrerequisiteCourse")]
        public virtual Course PrerequisiteCourse { get; set; }
    }
}
