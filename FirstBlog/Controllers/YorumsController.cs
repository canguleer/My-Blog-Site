using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstBlog.Models;

namespace FirstBlog.Controllers
{
    [Authorize]
    public class YorumsController : Controller
    {
        private BloggEntities db = new BloggEntities();

        // GET: Yorums
        public ActionResult Index()
        {
            var yorum = db.Yorum.Include(y => y.Makale).Include(y =>y.Makale.Uye);
            return View(yorum.ToList());
        }

        // GET: Yorums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // GET: Yorums/Create
        public ActionResult Create()
        {
            ViewBag.MakaleId = new SelectList(db.Makale, "MakaleId", "Baslık");
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "KullaniciAdi");
            return View();
        }

        // POST: Yorums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YorumId,Icerik,UyeId,MakaleId,Tarih")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Yorum.Add(yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MakaleId = new SelectList(db.Makale, "MakaleId", "Baslık", yorum.MakaleId);
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "KullaniciAdi", yorum.UyeId);
            return View(yorum);
        }

        // GET: Yorums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakaleId = new SelectList(db.Makale, "MakaleId", "Baslık", yorum.MakaleId);
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "KullaniciAdi", yorum.UyeId);
            return View(yorum);
        }

        // POST: Yorums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YorumId,Icerik,UyeId,MakaleId,Tarih")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakaleId = new SelectList(db.Makale, "MakaleId", "Baslık", yorum.MakaleId);
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "KullaniciAdi", yorum.UyeId);
            return View(yorum);
        }

        // GET: Yorums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = db.Yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // POST: Yorums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yorum yorum = db.Yorum.Find(id);
            db.Yorum.Remove(yorum);
            db.SaveChanges();
            return RedirectToAction("Index");
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
