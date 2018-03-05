using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Chat_App.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationSchemeProvider asp;

        public AuthController(IAuthenticationSchemeProvider a)
        {
            this.asp = a;
        }

        public async Task <IActionResult> Login()
        {
            var allSchemeProvider = (await asp.GetAllSchemesAsync())
                .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));
            return View(allSchemeProvider);
        }

        public IActionResult SignIn(String provider)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/"}, provider);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}