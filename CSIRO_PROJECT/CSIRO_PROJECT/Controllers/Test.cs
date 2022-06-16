using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSIRO_PROJECT.Controllers
{
    public class Test : Controller
    {

        /// <summary>
        ///  Geting session information from HomeController
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            // This token will be availabe to every page  

           // if (HttpContext.Session.GetString('userid') == null) RedirectToAction("Login", "Account");
            string uid = HttpContext.Session.GetString("UserID");// Testing purpose 
            
            return View();
        }
    }
}
