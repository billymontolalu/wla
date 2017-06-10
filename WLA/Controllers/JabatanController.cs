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
    public class JabatanController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: Jabatan
        public ActionResult Index()
        {
            return View(db.Jabatan.ToList());
        }

        // GET: Jabatan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jabatan jabatan = db.Jabatan.Find(id);
            if (jabatan == null)
            {
                return HttpNotFound();
            }
            return View(jabatan);
        }

        // GET: Jabatan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jabatan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Jabatan jabatan)
        {
            if (ModelState.IsValid)
            {
                db.Jabatan.Add(jabatan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jabatan);
        }

        // GET: Jabatan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jabatan jabatan = db.Jabatan.Find(id);
            if (jabatan == null)
            {
                return HttpNotFound();
            }
            return View(jabatan);
        }

        // POST: Jabatan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Jabatan jabatan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jabatan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jabatan);
        }

        // GET: Jabatan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jabatan jabatan = db.Jabatan.Find(id);
            if (jabatan == null)
            {
                return HttpNotFound();
            }
            return View(jabatan);
        }

        // POST: Jabatan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jabatan jabatan = db.Jabatan.Find(id);
            db.Jabatan.Remove(jabatan);
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
