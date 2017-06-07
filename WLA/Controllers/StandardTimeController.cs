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
    public class StandardTimeController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: StandardTime
        public ActionResult Index()
        {
            return View(db.Standard_Time.ToList());
        }

        // GET: StandardTime/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard_Time standard_Time = db.Standard_Time.Find(id);
            if (standard_Time == null)
            {
                return HttpNotFound();
            }
            return View(standard_Time);
        }

        // GET: StandardTime/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StandardTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tahun,Day,Saturday,Sunday,Holiday,Annual_Leaves,Sick_Permission,Working_Days,Effective_Working_Days,Working_Hours,Utilitation_Level,Effective_Working_Hours")] Standard_Time standard_Time)
        {
            if (ModelState.IsValid)
            {
                db.Standard_Time.Add(standard_Time);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(standard_Time);
        }

        // GET: StandardTime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard_Time standard_Time = db.Standard_Time.Find(id);
            if (standard_Time == null)
            {
                return HttpNotFound();
            }
            return View(standard_Time);
        }

        // POST: StandardTime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tahun,Day,Saturday,Sunday,Holiday,Annual_Leaves,Sick_Permission,Working_Days,Effective_Working_Days,Working_Hours,Utilitation_Level,Effective_Working_Hours")] Standard_Time standard_Time)
        {
            if (ModelState.IsValid)
            {
                db.Entry(standard_Time).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(standard_Time);
        }

        // GET: StandardTime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Standard_Time standard_Time = db.Standard_Time.Find(id);
            if (standard_Time == null)
            {
                return HttpNotFound();
            }
            return View(standard_Time);
        }

        // POST: StandardTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Standard_Time standard_Time = db.Standard_Time.Find(id);
            db.Standard_Time.Remove(standard_Time);
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
