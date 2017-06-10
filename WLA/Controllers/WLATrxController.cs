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

        // GET: WLATrx/Create/5
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Periode = new SelectList(db.Periode, "Id", "Name");
            ViewBag.Pelaksana = new SelectList(db.Pelaksana, "Id", "Name");

            var activities = from s in db.Activities select s;
            activities = activities.OrderBy(s => s.Name);
            ViewData["Activity"] = activities.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            
            var activityGroup = from s in db.ActivityGroups select s;
            activityGroup = activityGroup.OrderBy(s => s.Name);
            ViewData["ActivityGroup"] = activityGroup.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.WLAHeaderId = (id ?? 0);
            return View();
        }

        // POST: WLATrx/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Process_Time, Quantity, Frequency")] WLATrx wLATrx, FormCollection formData)
        {
            double workingHourYear = 1873.76;
            ViewBag.WLAHeaderId = formData["WLAHeaderId"].ToString();
            int WLAHeaderId = Convert.ToInt32(formData["WLAHeaderId"].ToString());
            int PeriodeId = Convert.ToInt32(formData["Periode"].ToString());
            int PelaksanaId = Convert.ToInt32(formData["Pelaksana"].ToString());
            int Activity = Convert.ToInt32(formData["Activity"].ToString());
            int ActivityGroup = Convert.ToInt32(formData["ActivityGroup"].ToString());

            if (ModelState.IsValid)
            {
                int Periode_Value = 0;
                //daily
                if (PeriodeId == 1)
                {
                    Periode_Value = 251;
                }
                //weekly
                else if (PeriodeId == 2)
                {
                    Periode_Value = 52;
                }
                //monthly
                else if (PeriodeId == 3)
                {
                    Periode_Value = 12;
                }
                //annualy
                else if (PeriodeId == 3)
                {
                    Periode_Value = 1;
                }

                wLATrx.Sub_Total_Aktivitas = (double)(Periode_Value * wLATrx.Process_Time * wLATrx.Quantity * wLATrx.Frequency)/60;

                wLATrx.WLAHeader = db.WLAHeaders.Where(c => c.Id == WLAHeaderId).First();
                wLATrx.Periode = db.Periode.Where(c => c.Id == PeriodeId).First();
                wLATrx.Pelaksana = db.Pelaksana.Where(c => c.Id == PelaksanaId).First();
                wLATrx.Activity = db.Activities.Where(c => c.Id == Activity).First();
                wLATrx.ActivityGroup = db.ActivityGroups.Where(c => c.Id == ActivityGroup).First();

                db.WLATrx.Add(wLATrx);
                db.SaveChanges();

                var data = db.WLATrx.Where(d => d.WLAHeader.Id == WLAHeaderId);
                var Sub_Total_Aktivitas = data.Sum(d => d.Sub_Total_Aktivitas);

                var dataHeader = db.WLAHeaders.Find(WLAHeaderId);
                dataHeader.Effective_Working_Hours = Sub_Total_Aktivitas;
                dataHeader.FTE = Sub_Total_Aktivitas / workingHourYear;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Periode = new SelectList(db.Periode, "Id", "Name");
            ViewBag.Pelaksana = new SelectList(db.Pelaksana, "Id", "Name");

            var activities = from s in db.Activities select s;
            activities = activities.OrderBy(s => s.Name);
            ViewData["Activity"] = activities.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            var activityGroup = from s in db.ActivityGroups select s;
            activityGroup = activityGroup.OrderBy(s => s.Name);
            ViewData["ActivityGroup"] = activityGroup.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

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
