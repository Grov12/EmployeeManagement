using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystem.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string ProfileDescription { get; set; }
        public string ProfileImageLink { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationuserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
