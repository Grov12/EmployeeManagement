using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EmployeeSystem.Data;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Controllers
{
    
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private  UserManager<ApplicationUser> _userManager;


        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.Include(g => g.Department).Include(g => g.Profile).ToListAsync();

        

         



            return View(users);
        }





        public async Task<IActionResult> EditUser(string id, bool error)
        {
            if(error)
            {
                ViewData["Error"] = "You need to be authorized as super-admin to do this.";
            }


            if(id == null)
            {
               
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            
        

            if(user == null)
            {
                Console.Write("Hello2");
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Departmentname");

            ViewData["RoleId"] = new SelectList(_context.Roles, "Name", "Name","User");

            return View(user);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>EditUser(string id, [Bind(include: "Id,Email,DepartmentId,RoleName")]ApplicationUser applicationUser)
        {
         

            bool x = User.IsInRole("SuperAdmin");
            Console.WriteLine(x);

            var user = await _context.Users
                   .FirstOrDefaultAsync(m => m.Id == id);

            user.Email = applicationUser.Email;
            user.DepartmentId = applicationUser.DepartmentId;

            if(applicationUser.RoleName.Equals("SuperAdmin") && !x)
            {
                return RedirectToAction("EditUser", "Admin", new {error = true});
            }


            else
            {
                user.RoleName = applicationUser.RoleName;
                await _userManager.AddToRoleAsync(user, user.RoleName);
            }
          







            Console.WriteLine(applicationUser.DepartmentId);
          

           
           


            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Users));
            }
            return View(applicationUser);


        }
          

        

        private bool ApplicationUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}