using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public string RoleName { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("ProfileId")]
        public int? ProfileId { get; set; }

        public Profile Profile { get; set; }

    }
}
