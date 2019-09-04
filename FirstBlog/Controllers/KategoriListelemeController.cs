using FirstBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstBlog.Controllers
{
    
    public class KategoriListelemeController : Controller
	{
		BloggEntities db = new BloggEntities();

		// GET: KategoriListeleme
		public ActionResult Index(int id)
		{

			var makaleler = db.Makale.Where(m => m.Kategori.KategoriId == id).ToList();
			return View(makaleler);
		}
	}
}

