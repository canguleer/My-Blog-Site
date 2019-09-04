using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using FirstBlog.Models;
using PagedList;

namespace FirstBlog.Controllers
{
	public class HomeController : Controller
	{
		BloggEntities db = new BloggEntities();

		public ActionResult Index()
		{
			//var makale = db.Makale.OrderByDescending(m => m.MakaleId).Take(3).ToList();
			return View();
		}

        public ActionResult _makaleler(int? page)
        {
            int pagex = page.HasValue == false ? 1 : page.Value;
            int list = 3;
            IPagedList model = db.Makale.OrderByDescending(t => t.MakaleId).ToPagedList(pagex, list);
            return PartialView("_makaleList", model);
        }

		public ActionResult BlogAra(string Ara = null)
		{
			var aranan = db.Makale.Where(m => m.Baslık.Contains(Ara)).ToList();
			return View(aranan.OrderByDescending(m => m.Tarih));
		}



		public ActionResult MakaleDetay(int id)
		{
			var makale = db.Makale.Where(m => m.MakaleId == id).SingleOrDefault();
			if (makale == null)
			{
				return HttpNotFound();
			}
			return View(makale);
		}

		public ActionResult Hakkımda()
		{
			return View();
		}

		public ActionResult Iletisim()
		{
			return View();
		}


		public ActionResult YorumYap(string yorum, int makaleid)
		{
			//int uyeid = (int)Session["uyeid"];			


			db.Yorum.Add(new Yorum { MakaleId = makaleid, UyeId = 2, Icerik = yorum, Tarih = DateTime.Now });
			db.SaveChanges();
			return RedirectToAction("/MakaleDetay/" + makaleid);
		}



		public ActionResult OkunmaArttir(int Makaleid)
		{
          
			var makale = db.Makale.Where(m => m.MakaleId == Makaleid).SingleOrDefault();
			makale.Okunma += 1;
			db.SaveChanges();
			return View();
		}

		public ActionResult KategoriPartial()
		{
			return View(db.Kategori.ToList());
		}


		public ActionResult EtiketPartial()
		{
			return View(db.Etiket.ToList());
		}

		public ActionResult EnCokOkunmaPartial()
		{
			return View(db.Makale.ToList());
		}

		public ActionResult SonYayınlananlarPartial()
		{
			var makale = db.Makale.OrderByDescending(m => m.MakaleId).ToList();
			return View(makale);
		}










	}
}