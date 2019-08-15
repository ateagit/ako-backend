using System;
using System.Collections.Generic;

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
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Rating { get; set; }
        public int? UserId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<CoursePrerequisite> CoursePrerequisiteMainCourse { get; set; }
        public virtual ICollection<CoursePrerequisite> CoursePrerequisitePrerequisiteCourse { get; set; }
        public virtual ICollection<UserCoursePlanner> UserCoursePlanner { get; set; }
    }
}
