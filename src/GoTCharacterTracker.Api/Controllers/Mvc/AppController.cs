using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GoTCharacterTracker.Controllers.Mvc
{
    [Authorize]
    public class AppController : Controller
    {
        private readonly IConfiguration m_config;
        private string m_user;
        private string m_pwd;

        public AppController(IConfiguration config)
        {
            m_config = config;
            m_user = m_config["MasterLoginUsername"];
            m_pwd = m_config["MasterLoginPassword"];
        }

        // Base page to serve non-auth, public SPA app
        [AllowAnonymous]
        [Route("/")]
        public IActionResult PublicBase()
        {
            ViewData["Message"] = "Base app page that will serve the app.";

            return View("~/Views/Public/PublicAppBase.cshtml");
        }

        // Non-auth page using default routing convention
        [AllowAnonymous]
        [Route("/about")]
        public IActionResult About()
        {
            ViewData["Message"] = "About page served from shared.";

            return View();
        }


        // Base admin page behind auth to serve admin SPA app - requires authorization
        [Route("/admin")]
        public IActionResult AdminBase()
        {
            ViewData["Message"] = "Base app page that will serve the app.";

            return View("~/Views/Admin/AdminAppBase.cshtml");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/admin/login")]
        public IActionResult AdminLoginGet(LoginDTO login, string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/admin");
            }

            return View("~/Views/Admin/Login.cshtml");

        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/login")]
        public async Task<IActionResult> AdminLoginPostAsync(LoginDTO login)
        {
            if (login.Username == m_user && login.Password == m_pwd)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Username)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Redirect("/admin");
            }

            return View("~/Views/Admin/Login.cshtml");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/admin/logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/admin/login");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/admin");
        }

    }

    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
