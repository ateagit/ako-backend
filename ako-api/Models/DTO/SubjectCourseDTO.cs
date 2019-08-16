using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ako_api.Models.DTO
{
    public class SubjectCourseDTO
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }

        public List<OutputBasicCourseDTO> Course { get; set; }
    }
}
