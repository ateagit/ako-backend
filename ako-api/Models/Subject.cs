using System;
using System.Collections.Generic;

namespace ako_api.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Course = new HashSet<Course>();
            SubjectHeirarchyChildSubject = new HashSet<SubjectHeirarchy>();
            SubjectHeirarchyParentSubject = new HashSet<SubjectHeirarchy>();
        }

        public int SubjectId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<SubjectHeirarchy> SubjectHeirarchyChildSubject { get; set; }
        public virtual ICollection<SubjectHeirarchy> SubjectHeirarchyParentSubject { get; set; }
    }
}
