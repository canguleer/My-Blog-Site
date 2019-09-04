using FirstBlog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace FirstBlog.Controllers
{
    public class AdminController : Controller
    {
        private BloggEntities db = new BloggEntities();
        [Authorize]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        


        public ActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public ActionResult GirisBasarili(Uye uye)
        {
            result result = new result();
            var kontrol = db.Uye.Where(e => e.Email == uye.Email && e.Sifre == uye.Sifre).FirstOrDefault();
            if (kontrol != null) //Check the database
            {
                int userid = kontrol.UyeId;
                List<Claim> claims = new Autorizing().GetClaims(kontrol); //Get the claims from the headers or db or your user store
                if (null != claims)
                {
                    new Autorizing().SignIn(claims, false);
                    var identity = (ClaimsIdentity)User.Identity;
                    List<string> rol = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                    result.adres = "../Admin/Index";
                    result.sonuc = true;
                }
            }
            else
            {
                result.sonuc = false;
                result.mesaj = "Hatalı kullanıcı adı ve/vaya şifre";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }

}