using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ako_api.Models
{
    public partial class Course
    {
        public Course()
        {
            Comment = new HashSet<Comment>();
            CoursePrerequisiteMainCourse = new HashSet<CoursePrerequisite>();
            CoursePrerequisitePrerequisiteCourse = new HashSet<CoursePrerequisite>();
            UserCoursePlanner = new HashSet<UserCoursePlanner>();
        }

        public int CourseId { get; set; }
        public int? SubjectId { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public string Content { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("Course")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<Comment> Comment { get; set; }
        [InverseProperty("MainCourse")]
        public virtual ICollection<CoursePrerequisite> CoursePrerequisiteMainCourse { get; set; }
        [InverseProperty("PrerequisiteCourse")]
        public virtual ICollection<CoursePrerequisite> CoursePrerequisitePrerequisiteCourse { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<UserCoursePlanner> UserCoursePlanner { get; set; }
    }
}
