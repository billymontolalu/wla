using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WLA;

namespace WLA.Controllers
{
    public class PelaksanaController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: Pelaksana
        public ActionResult Index()
        {
            return View(db.Pelaksana.ToList());
        }

        // GET: Pelaksana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelaksana pelaksana = db.Pelaksana.Find(id);
            if (pelaksana == null)
            {
                return HttpNotFound();
            }
            return View(pelaksana);
        }

        // GET: Pelaksana/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pelaksana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Pelaksana pelaksana)
        {
            if (ModelState.IsValid)
            {
                db.Pelaksana.Add(pelaksana);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pelaksana);
        }

        // GET: Pelaksana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelaksana pelaksana = db.Pelaksana.Find(id);
            if (pelaksana == null)
            {
                return HttpNotFound();
            }
            return View(pelaksana);
        }

        // POST: Pelaksana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Pelaksana pelaksana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelaksana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pelaksana);
        }

        // GET: Pelaksana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelaksana pelaksana = db.Pelaksana.Find(id);
            if (pelaksana == null)
            {
                return HttpNotFound();
            }
            return View(pelaksana);
        }

        // POST: Pelaksana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pelaksana pelaksana = db.Pelaksana.Find(id);
            db.Pelaksana.Remove(pelaksana);
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
