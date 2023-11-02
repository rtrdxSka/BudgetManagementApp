using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using BudgetManagment.Models;
using BudgetManagment.ViewModels;

namespace BudgetManagment.Controllers
{
    public class AccountController : Controller
    {
        private readonly BudgetManagmentContext _context;

        public AccountController(BudgetManagmentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //return view here
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetails = await _context.UserDetails
                .SingleOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password);
                if (userdetails == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    //retunr view again
                    return Ok("Index");
                }
                HttpContext.Session.SetString("userId", userdetails.Name);

            }
            else
            {
                //return view again
                return Ok("Index");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<ActionResult> Registar(RegistrationViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new UserDetails()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile

                };
                _context.Add(user);
                await _context.SaveChangesAsync();

            }
            else
            {
                //return view here
                return Ok("Registration");
            }
            return RedirectToAction("Index", "Account");
        }
        // registration Page load
        public IActionResult Registration()
        {
            ViewData["Message"] = "Registration Page";
            //return view here
            return Ok();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //return view here
            return Ok("Index");
        }
        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }

        }
    }
}
