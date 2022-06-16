using CSIRO_PROJECT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
// This controller handle the user account 
namespace CSIRO_PROJECT.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager { get; }
        private SignInManager<IdentityUser> signInManager { get; }
        

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
            
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
         
        }

        // Get request for the Registration Form Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // Post request to create the new registration 
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel userRequest)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = userRequest.Email, Email = userRequest.Email };
                var result = await  userManager.CreateAsync(user, userRequest.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    // Confirmaton yet to send to the user email 
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId =user.Id,token=token},Request.Scheme);
                    // Displaying  the link mass
                    ViewBag.ErrorTitle = "Registration is success";
                    ViewBag.ErrorMessage = "Please check you mail";
                    
                    

                    return View("Error");
                    
                }

                // If threre is errror we need to collect those error

                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View();
        }


        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token == null)
            {
                RedirectToAction("Register");
            }
            

                var user = await userManager.FindByIdAsync(userId); // User Manager will handle the userID from AspNetUser table 

            if(user == null)
            {
                ViewBag.ErrorTitle = " ";
                ViewBag.ErrorMessage = userId + "Not Found";
                return View("Error");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.ErrorTitle = "";
                ViewBag.ErrorMessage = "Registration is successfult";
                return View("Login");
            }
            ViewBag.ErrorMessage = " Registration not success"; 
            return View();
            
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View( new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel userDetail)
        {
            if (ModelState.IsValid)
            {
               var result = await signInManager.PasswordSignInAsync(userDetail.Email, userDetail.Password,userDetail.RememberMe,false);

                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(userDetail.Email);
                    var userID = user.Id;
                    
                    // Creating User Id as the session name userId of type value is userID
                    HttpContext.Session.SetString("userId", userID);


                   

                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "UserModels");
                    }

                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "UserModels");
                    }

                    return RedirectToAction("Create", "UserModels");
                    



                    // This should be going to home page later on. 
                }
               

                    ModelState.AddModelError("", "Invalid Attempt");
                               
            }

            return View(userDetail);
        }



        public async Task<IActionResult> Logout()
        {
           await signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
