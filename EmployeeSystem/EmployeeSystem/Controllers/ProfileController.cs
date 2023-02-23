using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeSystem.Data;
using EmployeeSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Controllers
{
    public class ProfileController : Controller
    {

       private  UserManager<ApplicationUser> _userManager;
        private  ApplicationDbContext _context;
        private readonly EmployeeSystemContext _empContext;
        private IHostingEnvironment _hostingEnvironment;


        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, EmployeeSystemContext empContext, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _empContext = empContext;
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<IActionResult> Index(string id)
        {
            var user = _context.Users.Include(g => g.Department).Include(g => g.Profile).FirstOrDefault(m => m.Id == id);


         


            return View(user);
        }

        public IActionResult Edit(string id)
        {
            var profile = _empContext.Profile.FirstOrDefault(g => g.ApplicationuserId == id);

            var user = _context.Users.FirstOrDefault(g => g.Id == id);

            try
            {
                if (!User.Identity.Name.Equals(user.UserName))
                {
                    return NotFound();
                }
            } catch(NullReferenceException)
            {
                return NotFound();
            }

         



            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ProfileId, [Bind(include: "ProfileId,ProfileDescription")] Profile profile, IList<IFormFile> files)
        {

            Console.WriteLine(profile.ProfileImageLink);
           
            var profileToUpdate = _empContext.Profile.FirstOrDefault(g => g.ProfileId == ProfileId);

          

            if(!profile.ProfileDescription.Equals(profileToUpdate.ProfileDescription))
            {
                profileToUpdate.ProfileDescription = profile.ProfileDescription;
            }


            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.Combine("",_hostingEnvironment.WebRootPath + "/images/" + "/" + profileToUpdate.ApplicationuserId + "/", "profilepic.jpg");

            FileInfo dir = new FileInfo(filePath);
            dir.Directory.Create();
         

            foreach (var formFile in files)
            {
                
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {

                        await formFile.CopyToAsync(stream);
                    }
                    profileToUpdate.ProfileImageLink = "/images/" + "/" + profileToUpdate.ApplicationuserId + "/" + "profilepic.jpg";
                }
            }






            _empContext.Update(profileToUpdate);
                    await _empContext.SaveChangesAsync();
             
            
            
            return View(profile);
        }
      

    }
}