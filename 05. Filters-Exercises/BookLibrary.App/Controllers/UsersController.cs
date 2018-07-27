using BookLibrary.App.Filters;
using BookLibrary.App.Helpers;
using BookLibrary.App.Models.BindingModels;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.App.Controllers
{
    public class UsersController : Controller
    {
        private UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Login(string returnUrl = null)
        {
            if (this.HttpContext.Session.GetString("_$CurrentUserSessionKey$_") != null)
            {
                return this.RedirectToPage("/index");
            }

            if (returnUrl != null)
            {
                TempData["message"] = "Login to access this page";
                TempData["returnUrl"] = returnUrl;
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string passwordHash = PasswordHasher.GetPasswordHash(model.Password);

            bool userExists = this.usersService.CheckIfUserExists(model.Username, passwordHash);

            if (!userExists)
            {
                return this.View();
            }

            this.HttpContext.Session.SetString("_$CurrentUserSessionKey$_", model.Username);

            if (TempData.ContainsKey("returnUrl"))
            {
                return this.LocalRedirect(TempData["returnUrl"].ToString());
            }

            return this.RedirectToPage("/Index");
        }

        public IActionResult Logout()
        {
            this.HttpContext.Session.Clear();

            return this.RedirectToPage("/Index");
        }
    }
}