using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystem.Models
{
    public class Department
    {
       
       public int DepartmentId { get; set; }

       public string Departmentname { get; set; }


        public virtual List<ApplicationUser> ApplicationUser { get; set; }


    }
}
