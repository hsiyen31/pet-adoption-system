using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Data;
using PetAdoptionSystem.Models;

namespace PetAdoptionSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly CmsContext _context;

        public AccountController(CmsContext context)
        {
            _context = context;
        }

        // =========================
        // Register 頁面
        // =========================
        public IActionResult Register()
        {
            return View();
        }

        // =========================
        // Register 功能
        // =========================
        [HttpPost]
        public IActionResult Register(User user)
        {
            // Email 不可重複
            var checkEmail = _context.Users
                .FirstOrDefault(x => x.uMail == user.uMail);

            if (checkEmail != null)
            {
                ViewBag.Error = "Email 已存在";
                return View();
            }

            // 自動產生資料
            user.UId = Guid.NewGuid();
            user.Role = 2;
            user.VerifiedStatus = 1;

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // =========================
        // Login 頁面
        // =========================
        public IActionResult Login()
        {
            return View();
        }

        // =========================
        // Login 功能
        // =========================
        [HttpPost]
        public IActionResult Login(string uMail, string Password)
        {
            var user = _context.Users
                .FirstOrDefault(x =>
                    x.uMail == uMail &&
                    x.Password == Password);

            if (user != null)
            {
                // Session
                HttpContext.Session.SetString("UId", user.UId.ToString());
                HttpContext.Session.SetString("uName", user.uName);
                HttpContext.Session.SetString("uMail", user.uMail);
                HttpContext.Session.SetInt32("Role", user.Role);

                // 管理員
                if (user.Role == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }

                // 一般會員
                else
                {
                    return RedirectToAction("Index", "Pet");
                }
            }

            ViewBag.Error = "帳號或密碼錯誤";

            return View();
        }
        // =========================
        // Logout
        // =========================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}