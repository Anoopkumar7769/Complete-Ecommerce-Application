using eTickets.Data;
using eTickets.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace eTickets.Controllers.Account
{
    public class DropdownController : Controller
    {

        private readonly AppDbContext _db;
        
        public DropdownController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<RoleName> rn = new List<RoleName>();
            rn = (from item in _db.RoleName select item).ToList();
            rn.Insert(0, new RoleName { id = 0, name = "--Log in as--" });
            ViewBag.message = rn;
            return View();
        }
    }
}
