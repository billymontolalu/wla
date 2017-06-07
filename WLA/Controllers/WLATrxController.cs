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
    public class WLATrxController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: WLATrx
        public ActionResult Index()
        {
            return View(db.WLATrx.ToList());
        }

        // GET: WLATrx/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WLATrx wLATrx = db.WLATrx.Find(id);
            if (wLATrx == null)
            {
                return HttpNotFound();
            }
            return View(wLATrx);
        }

        // GET: WLATrx/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WLATrx/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Process_Time,Frequency,Sub_Total_Aktivitas,Effective_Working_Hours,FTE,Presentase")] WLATrx wLATrx)
        {
            if (ModelState.IsValid)
            {
                db.WLATrx.Add(wLATrx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wLATrx);
        }

        // GET: WLATrx/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WLATrx wLATrx = db.WLATrx.Find(id);
            if (wLATrx == null)
            {
                return HttpNotFound();
            }
            return View(wLATrx);
        }

        // POST: WLATrx/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Process_Time,Frequency,Sub_Total_Aktivitas,Effective_Working_Hours,FTE,Presentase")] WLATrx wLATrx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wLATrx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wLATrx);
        }

        // GET: WLATrx/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WLATrx wLATrx = db.WLATrx.Find(id);
            if (wLATrx == null)
            {
                return HttpNotFound();
            }
            return View(wLATrx);
        }

        // POST: WLATrx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WLATrx wLATrx = db.WLATrx.Find(id);
            db.WLATrx.Remove(wLATrx);
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
