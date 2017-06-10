using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WLA.Models;

namespace WLA.Controllers
{
    public class PeriodeController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: Periode
        public ActionResult Index()
        {
            return View(db.Periode.ToList());
        }

        // GET: Periode/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periode periode = db.Periode.Find(id);
            if (periode == null)
            {
                return HttpNotFound();
            }
            return View(periode);
        }

        // GET: Periode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Periode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Periode periode)
        {
            if (ModelState.IsValid)
            {
                db.Periode.Add(periode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(periode);
        }

        // GET: Periode/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periode periode = db.Periode.Find(id);
            if (periode == null)
            {
                return HttpNotFound();
            }
            return View(periode);
        }

        // POST: Periode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Periode periode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periode);
        }

        // GET: Periode/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Periode periode = db.Periode.Find(id);
            if (periode == null)
            {
                return HttpNotFound();
            }
            return View(periode);
        }

        // POST: Periode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Periode periode = db.Periode.Find(id);
            db.Periode.Remove(periode);
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
