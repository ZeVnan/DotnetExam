using Microsoft.AspNetCore.Mvc;
using WebEC.Models;

namespace WebEC.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext Db = new QlbanVaLiContext();
        [HttpGet]
        public IActionResult LogIn()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                string? role = HttpContext.Session.GetString("UserRole");
                if (role == "0")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                }
                
            }
        }
        [HttpPost]
        public IActionResult LogIn(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                TUser? _user =
                    Db
                    .TUsers
                    .Where(x => x.Username == user.Username && x.Password == user.Password)
                    .FirstOrDefault();
                if (_user is not null)
                {
                    HttpContext.Session.SetString("UserName", _user.Username.ToString());
                    HttpContext.Session.SetString("UserRole", _user.LoaiUser.ToString());
                    if (_user.LoaiUser == 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    }
                }
            }
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("LogIn", "Access");
        }
        public IActionResult Register(TUser user)
        {
            TUser? _user =
                    Db
                    .TUsers
                    .Where(x => x.Username == user.Username)
                    .FirstOrDefault();
            if (_user == null)
            {
                if (ModelState.IsValid)
                {
                    Db.TUsers.Add(user);
                    Db.SaveChanges();
                    return RedirectToAction("LogIn");
                }
            }
            return View();
        }
    }
}
