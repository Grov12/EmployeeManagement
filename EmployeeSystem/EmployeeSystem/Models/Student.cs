using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystem.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public String StudentName { get; set; }
        public String StudentLastName { get; set; }


        public virtual List<Grade> Grades { get; set; }
    }
}
