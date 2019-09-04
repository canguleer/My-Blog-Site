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
    public class EtiketsController : Controller
    {
        private BloggEntities db = new BloggEntities();

        // GET: Etikets
        public ActionResult Index()
        {
            var etiket = db.Etiket.Include(e => e.Makale);
            return View(etiket.ToList());
        }

        // GET: Etikets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etiket etiket = db.Etiket.Find(id);
            if (etiket == null)
            {
                return HttpNotFound();
            }
            return View(etiket);
        }

        // GET: Etikets/Create
        public ActionResult Create()
        {
            ViewBag.MakaleId = new SelectList(db.Makale, "MakaleId", "Baslık");
            return View();
        }

        // POST: Etikets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EtiketId,EtiketAdi,MakaleId")] Etiket etiket)
        {
            if (ModelState.IsValid)
            {
                db.Etiket.Add(etiket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MakaleId = new SelectList(db.Makale, "MakaleId", "Baslık", etiket.MakaleId);
            return View(etiket);
        }

        // GET: Etikets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etiket etiket = db.Etiket.Find(id);
            if (etiket == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakaleId = new SelectList(db.Makale, "MakaleId", "Baslık", etiket.MakaleId);
            return View(etiket);
        }

        // POST: Etikets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EtiketId,EtiketAdi,MakaleId")] Etiket etiket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etiket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakaleId = new SelectList(db.Makale, "MakaleId", "Baslık", etiket.MakaleId);
            return View(etiket);
        }

        // GET: Etikets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etiket etiket = db.Etiket.Find(id);
            if (etiket == null)
            {
                return HttpNotFound();
            }
            return View(etiket);
        }

        // POST: Etikets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Etiket etiket = db.Etiket.Find(id);
            db.Etiket.Remove(etiket);
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
