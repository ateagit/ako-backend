using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [StringLength(255)]
        public string Name { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<Course> Course { get; set; }
        [InverseProperty("ChildSubject")]
        public virtual ICollection<SubjectHeirarchy> SubjectHeirarchyChildSubject { get; set; }
        [InverseProperty("ParentSubject")]
        public virtual ICollection<SubjectHeirarchy> SubjectHeirarchyParentSubject { get; set; }
    }
}
