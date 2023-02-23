using System;
using System.Collections.Generic;
using System.Text;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<EmployeeSystem.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<EmployeeSystem.Models.Department> Department { get; set; }
       
    }
}
