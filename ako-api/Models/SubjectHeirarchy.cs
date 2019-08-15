using System;
using System.Collections.Generic;

namespace ako_api.Models
{
    public partial class SubjectHeirarchy
    {
        public int SubjectHeirarchyId { get; set; }
        public int? ParentSubjectId { get; set; }
        public int? ChildSubjectId { get; set; }

        public virtual Subject ChildSubject { get; set; }
        public virtual Subject ParentSubject { get; set; }
    }
}
