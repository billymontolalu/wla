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
    public class ActivityGroupsController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: ActivityGroups
        public ActionResult Index()
        {
            return View(db.ActivityGroups.ToList());
        }

        // GET: ActivityGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityGroup activityGroup = db.ActivityGroups.Find(id);
            if (activityGroup == null)
            {
                return HttpNotFound();
            }
            return View(activityGroup);
        }

        // GET: ActivityGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ActivityGroup activityGroup)
        {
            if (ModelState.IsValid)
            {
                db.ActivityGroups.Add(activityGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityGroup);
        }

        // GET: ActivityGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityGroup activityGroup = db.ActivityGroups.Find(id);
            if (activityGroup == null)
            {
                return HttpNotFound();
            }
            return View(activityGroup);
        }

        // POST: ActivityGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ActivityGroup activityGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityGroup);
        }

        // GET: ActivityGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityGroup activityGroup = db.ActivityGroups.Find(id);
            if (activityGroup == null)
            {
                return HttpNotFound();
            }
            return View(activityGroup);
        }

        // POST: ActivityGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityGroup activityGroup = db.ActivityGroups.Find(id);
            db.ActivityGroups.Remove(activityGroup);
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
