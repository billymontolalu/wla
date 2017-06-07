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
    public class ActivityGroupListsController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: ActivityGroupLists
        public ActionResult Index()
        {
            var activityGroupLists = db.ActivityGroupLists.Include(a => a.Activity).Include(a => a.ActivityGroup);
            return View(activityGroupLists.ToList());
        }

        // GET: ActivityGroupLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityGroupList activityGroupList = db.ActivityGroupLists.Find(id);
            if (activityGroupList == null)
            {
                return HttpNotFound();
            }
            return View(activityGroupList);
        }

        // GET: ActivityGroupLists/Create
        public ActionResult Create()
        {
            ViewBag.Activity_Id = new SelectList(db.Activities, "Id", "Name");
            ViewBag.ActivityGroup_Id = new SelectList(db.ActivityGroups, "Id", "Name");
            return View();
        }

        // POST: ActivityGroupLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Activity_Id,ActivityGroup_Id")] ActivityGroupList activityGroupList)
        {
            if (ModelState.IsValid)
            {
                db.ActivityGroupLists.Add(activityGroupList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Activity_Id = new SelectList(db.Activities, "Id", "Name", activityGroupList.Activity_Id);
            ViewBag.ActivityGroup_Id = new SelectList(db.ActivityGroups, "Id", "Name", activityGroupList.ActivityGroup_Id);
            return View(activityGroupList);
        }

        // GET: ActivityGroupLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityGroupList activityGroupList = db.ActivityGroupLists.Find(id);
            if (activityGroupList == null)
            {
                return HttpNotFound();
            }
            ViewBag.Activity_Id = new SelectList(db.Activities, "Id", "Name", activityGroupList.Activity_Id);
            ViewBag.ActivityGroup_Id = new SelectList(db.ActivityGroups, "Id", "Name", activityGroupList.ActivityGroup_Id);
            return View(activityGroupList);
        }

        // POST: ActivityGroupLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Activity_Id,ActivityGroup_Id")] ActivityGroupList activityGroupList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityGroupList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Activity_Id = new SelectList(db.Activities, "Id", "Name", activityGroupList.Activity_Id);
            ViewBag.ActivityGroup_Id = new SelectList(db.ActivityGroups, "Id", "Name", activityGroupList.ActivityGroup_Id);
            return View(activityGroupList);
        }

        // GET: ActivityGroupLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityGroupList activityGroupList = db.ActivityGroupLists.Find(id);
            if (activityGroupList == null)
            {
                return HttpNotFound();
            }
            return View(activityGroupList);
        }

        // POST: ActivityGroupLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityGroupList activityGroupList = db.ActivityGroupLists.Find(id);
            db.ActivityGroupLists.Remove(activityGroupList);
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
