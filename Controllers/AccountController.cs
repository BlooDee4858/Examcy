using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examcy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Examcy.ViewModels;
using Examcy.Data.Interfaces;
using Examcy.Data.Repository;

namespace Examcy.Controllers
{
    public class AccountController : Controller
    {
        //AppDBContent db;
        //public AccountController(AppDBContent context)
        //{
        //    db = context;
        //}
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
    
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserRepository u = new UserRepository();
                var response = await _accountService.Register(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    int r = u.GetRoleByLogin(response.Data.Name);
                    string role = "Home";
                    if (r == 1)
                        role = "THome";
                    if (r == 2)
                    {
                        role = "StHome";
                        u.addStudent(u.GetUserIdByLogin(response.Data.Name));
                    }
                    if (r == 3)
                    {
                        role = "AHome";
                        return RedirectToAction("Users", role);
                    }

                    return RedirectToAction("Index", role);
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserRepository u = new UserRepository();
                var response = await _accountService.Login(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    int r = u.GetRoleByLogin(response.Data.Name);
                    string role = "Home";
                    if (r == 1)
                        role = "THome";
                    if (r == 2)
                        role = "StHome";
                    if (r == 3)
                    {
                        role = "AHome";
                        return RedirectToAction("Users", role);
                    }

                    return RedirectToAction("Index", role);
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            int a = 10;
            a = 1 + 1;
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/Home/Index");
        }
    }
}
