
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace FirstBlog.Models
{
    public class Autorizing : IDisposable
    {
        private BloggEntities db = new BloggEntities();


        public List<Claim> GetClaims( Uye data)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, data.Email));
            claims.Add(new Claim(ClaimTypes.Sid, data.UyeId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, data.AdSoyad));
            claims.Add(new Claim(ClaimTypes.Role, data.Yetki.Yetki1));
            return claims;
        }


        public void SignIn(List<Claim> claims, bool hatirla)
        {
            var claimsIdentity = new ClaimsIdentity(claims,
            DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = hatirla }, claimsIdentity);
            HttpContext.Current.User = new ClaimsPrincipal(AuthenticationManager.AuthenticationResponseGrant.Principal);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

    }
}
