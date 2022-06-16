using CSIRO_PROJECT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSIRO_PROJECT.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdministrationController : Controller
    {
        private RoleManager<IdentityRole> roleManager{ get; }
        private UserManager<IdentityUser> userManager { get; }

        private List<string> userList;

        public AdministrationController(RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager)
        {
            this.roleManager = _roleManager;
            this.userManager = _userManager;
            userList = new List<string>();

            var users = userManager.Users;

            foreach (var user in users)
            {
                userList.Add(user.Email);
            }
        }

        [HttpGet]
    
        
        public IActionResult CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName };


                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return View("Display", roleManager.Roles);
                }

                foreach (IdentityError err in result.Errors)
                {

                    ModelState.AddModelError("", err.Description);
                    return View(err);
                }

               
            }
            return View("Display", roleManager.Roles);


        }

        [HttpGet]
        
        public async Task<IActionResult> Delete(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            IdentityResult result = await roleManager.DeleteAsync(role);


            foreach(IdentityError e in result.Errors)
            {
                ModelState.AddModelError(" ", e.Description);

            }

            return View("Display", roleManager.Roles);
        }

        private void FillArray(ManageRole mrole)
        {

            var users = userManager.Users;
            mrole.userList = new List<SelectListItem>();
            
            foreach(var user in users)
            {
                SelectListItem item = new SelectListItem();
                item.Text = user.UserName;
                item.Value = user.Id;

                mrole.userList.Add(item);
            }



            var roles = roleManager.Roles;
            mrole.roleList = new List<SelectListItem>();

            foreach (var role in roles)
            {
                SelectListItem item = new SelectListItem();
                item.Text = role.Name;
                item.Value = role.Id;

                mrole.roleList.Add(item);
            }
        }

        [HttpGet]
     
        public IActionResult ManageRole()
        {
            ManageRole mrole = new ManageRole();
            FillArray(mrole);

            return View(mrole);
        }



        [HttpPost]

        public async Task<IActionResult> ManageRole(ManageRole mrole)
        {
            
            var role = await roleManager.FindByIdAsync(mrole.roleId);
            var user = await userManager.FindByIdAsync(mrole.userId);

            if(role == null || user == null)
            {
                return View("Error");
            }

            var roleId = role.Id;
            
            if(!(await userManager.IsInRoleAsync(user, role.Name)))
            {
                
                var result = await userManager.AddToRoleAsync(user,role.Name);
            }
            return View("Display",roleManager.Roles);
        }


        [HttpGet]
       
        public async Task<IActionResult> GetRole()
        {
            

            foreach( var email in userList)
            {
                var u = await userManager.FindByEmailAsync(email);
                var r = await userManager.GetRolesAsync(u);
                
            }

            return View();
        }

        


       public IActionResult Display()
       {
           return View();
        }

        }
       
    }

