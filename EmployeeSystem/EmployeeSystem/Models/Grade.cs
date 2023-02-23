using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystem.Models
{
    public class Grade
    {
        public int GradeID { get; set; }
        public int GradeNumber { get; set; }

        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}
