using BusinessLayer.Concreate;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntitiyFramework;
using EntitiyLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        AdminManager am = new AdminManager(new EfAdminDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin p)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            string password = p.AdminPassword;
            string username = p.AdminUserName;
            string passwordHash = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));
            string usernameHash = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(username)));
            p.AdminPassword = passwordHash;
            p.AdminUserName = usernameHash;

            var adminUserInfo = am.AdminUsernameAndPass(p.AdminUserName, p.AdminPassword);

            if (adminUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminUserInfo.AdminUserName, false);
                Session["AdminUserName"] = adminUserInfo.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                ViewBag.errorMessage = "Kullanıcı Adı Veya Şifreniz Hatalıdır.";
                return RedirectToAction("Index");
            }
        }
    }
}