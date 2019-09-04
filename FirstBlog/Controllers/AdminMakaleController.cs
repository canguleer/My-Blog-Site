using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FirstBlog.Models;

namespace FirstBlog.Controllers
{
    [Authorize]
	public class AdminMakaleController : Controller
	{
		private BloggEntities db = new BloggEntities();

		// GET: AdminMakale
		public ActionResult Index()
		{
			var makale = db.Makale.Include(m => m.Kategori);
			return View(makale.ToList());
		}

		// GET: AdminMakale/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Makale makale = db.Makale.Find(id);
			if (makale == null)
			{
				return HttpNotFound();
			}
			return View(makale);
		}

		// GET: AdminMakale/Create
		public ActionResult Create()
		{
			ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAdi");
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Makale makale, string etiketler, HttpPostedFileBase Foto)
		{
			if (ModelState.IsValid)
			{

				if (Foto != null)
				{
					WebImage img = new WebImage(Foto.InputStream);
					FileInfo fotoinfo = new FileInfo(Foto.FileName);

					string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
					img.Resize(800, 350);
					img.Save("~/Uploads/MakaleFoto/" + newfoto);
					makale.Foto = "/Uploads/MakaleFoto/" + newfoto;



				}

				if (etiketler != null)
				{
					string[] etiketdizi = etiketler.Split(',');
					foreach (var i in etiketdizi)
					{
						var yenietiket = new Etiket { EtiketAdi = i };
						db.Etiket.Add(yenietiket);
					}



				}

				db.Makale.Add(makale);
				db.SaveChanges();
				return RedirectToAction("Index");
			}


			return View(makale);

		}

		// GET: AdminMakale/Edit/5
		public ActionResult Edit(int? id)
		{
			var makale = db.Makale.Where(m => m.MakaleId == id).SingleOrDefault();
			if (makale == null)
			{
				return HttpNotFound();
			}

			ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAdi", makale.KategoriId);
			return View(makale);
		}


		// POST: AdminMakale/Edit/5

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, HttpPostedFileBase Foto, Makale makale)
		{
			try
			{
				var makales = db.Makale.Where(m => m.MakaleId == id).SingleOrDefault();
				if (Foto != null)
				{
					if (System.IO.File.Exists(Server.MapPath(makales.Foto)))
					{
						System.IO.File.Delete(Server.MapPath(makales.Foto));
					}
					WebImage img = new WebImage(Foto.InputStream);
					FileInfo fotoinfo = new FileInfo(Foto.FileName);
					string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
					img.Resize(800, 350);
					img.Save("~/Uploads/MakaleFoto/" + newfoto);
					makales.Foto = "/Uploads/MakaleFoto/" + newfoto;
				}			

				
				makales.Baslık = makale.Baslık;
				makales.Icerik = makale.Icerik;
				makales.KategoriId = makale.KategoriId;


				db.SaveChanges();
				return RedirectToAction("Index");
			}
			catch
			{

				return View();
			}






		}

		// GET: AdminMakale/Delete/5
		public ActionResult Delete(int? id)
		{
			var makale = db.Makale.Where(m => m.MakaleId == id).SingleOrDefault();
			if (makale == null)
			{
				return HttpNotFound();
			}
			return View(makale);
		}

		// POST: AdminMakale/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			try
			{
				var makales = db.Makale.Where(m => m.MakaleId == id).SingleOrDefault();
				if (makales == null)
				{
					return HttpNotFound();
				}
				if (System.IO.File.Exists(Server.MapPath(makales.Foto)))
				{
					System.IO.File.Delete(Server.MapPath(makales.Foto));
				}
				foreach (var item in makales.Yorum.ToList())
				{
					db.Yorum.Remove(item);
				}
				foreach (var item in makales.Etiket.ToList())
				{
					db.Etiket.Remove(item);
				}

				db.Makale.Remove(makales);
				db.SaveChanges();

				return RedirectToAction("Index");

			}
			catch
			{

				return View();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
