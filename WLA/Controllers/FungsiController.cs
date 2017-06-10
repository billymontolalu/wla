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
    public class FungsiController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: Fungsi
        public ActionResult Index()
        {
            return View(db.Fungsi.ToList());
        }

        // GET: Fungsi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fungsi fungsi = db.Fungsi.Find(id);
            if (fungsi == null)
            {
                return HttpNotFound();
            }
            return View(fungsi);
        }

        // GET: Fungsi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fungsi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Fungsi fungsi)
        {
            if (ModelState.IsValid)
            {
                db.Fungsi.Add(fungsi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fungsi);
        }

        // GET: Fungsi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fungsi fungsi = db.Fungsi.Find(id);
            if (fungsi == null)
            {
                return HttpNotFound();
            }
            return View(fungsi);
        }

        // POST: Fungsi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Fungsi fungsi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fungsi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fungsi);
        }

        // GET: Fungsi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fungsi fungsi = db.Fungsi.Find(id);
            if (fungsi == null)
            {
                return HttpNotFound();
            }
            return View(fungsi);
        }

        // POST: Fungsi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fungsi fungsi = db.Fungsi.Find(id);
            db.Fungsi.Remove(fungsi);
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
