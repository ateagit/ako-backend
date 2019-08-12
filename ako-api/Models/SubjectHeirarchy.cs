using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ako_api.Models
{
    public partial class SubjectHeirarchy
    {
        public int SubjectHeirarchyId { get; set; }
        public int? ParentSubjectId { get; set; }
        public int? ChildSubjectId { get; set; }

        [ForeignKey("ChildSubjectId")]
        [InverseProperty("SubjectHeirarchyChildSubject")]
        public virtual Subject ChildSubject { get; set; }
        [ForeignKey("ParentSubjectId")]
        [InverseProperty("SubjectHeirarchyParentSubject")]
        public virtual Subject ParentSubject { get; set; }
    }
}
