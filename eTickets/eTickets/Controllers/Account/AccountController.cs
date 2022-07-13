using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using System.Linq;
using System.Web;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using eTickets.Data;
using eTickets.Models.Account;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _db;

        public AccountController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _db.Register.Where(n => n.RoleName == "4").ToListAsync();
            return View(users);
        }

        //public async Task<IActionResult> Register()
        public IActionResult Register()
        {
            //var dropDownData = await GetDropdownValues();
            //ViewBag.data = new SelectList(dropDownData.RoleName, "id", "name"); 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel obj)
        {
            if (ModelState.IsValid)
            {
                obj.RoleName = "4";
                _db.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var data = _db.Register.Where(s => s.Email.Equals(obj.Email) && s.Password.Equals(obj.Password)).ToList();
                
                if (data.Count() > 0)
                {
                    ViewData["username"] = obj.Email;
                    HttpContext.Session.SetString("id", data[0].id.ToString());
                    HttpContext.Session.SetString("name", data[0].Name.ToString());
                    HttpContext.Session.SetInt32("id", data[0].id);
                    HttpContext.Session.SetString("Rid", data[0].RoleName);
                    TempData["id"] = data[0].id;
                    TempData["Uid"] = data[0].id.ToString();
                    TempData["Role"] = data[0].RoleName;
                    TempData["email"] = data[0].Email;
                    HttpContext.Session.SetString("Email", data[0].Email);
                    string script = string.Format("sessionStorage.id='{0}';", data[0].id);
                    return RedirectToAction("Index", "Movie");
                }
                else
                {
                    string msg = "Wrong credentials. Please,try again!";
                    TempData["ErrorMessage"] = msg;
                    return RedirectToAction("Login");
                }
            }
            return View(obj);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public async Task<Dropdowns> GetDropdownValues()
        {
            var response = new Dropdowns();
            response.RoleName = await _db.RoleName.OrderBy(n => n.id).ToListAsync();
            return response;
        }
    }
}
