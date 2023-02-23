using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeSystem.Models;

namespace EmployeeSystem.Models
{
    public class EmployeeSystemContext : DbContext
    {
        public EmployeeSystemContext (DbContextOptions<EmployeeSystemContext> options)
            : base(options)
        {
        }
        public DbSet<EmployeeSystem.Models.Student> Student { get; set; }
        public DbSet<EmployeeSystem.Models.Grade> Grade { get; set; }
        public DbSet<EmployeeSystem.Models.Profile> Profile { get; set; }

       
    }
}
