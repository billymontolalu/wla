using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WLA.Models;
using PagedList;

namespace WLA.Controllers
{
    public class WLAHeadersController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: WLAHeaders
        public ActionResult Index(int? page)
        {
            var wlaheader = from s in db.WLAHeaders
                             select s;
            wlaheader = wlaheader.OrderBy(s => s.Jabatan.Name);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(wlaheader.ToPagedList(pageNumber, pageSize));
        }

        // GET: WLAHeaders/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            WLAHeader wLAHeader = db.WLAHeaders.Find(id);
            ViewBag.wLATrx = (from s in db.WLATrx where s.WLAHeader.Id == (id ?? 0) select s).ToList();
            if (wLAHeader == null)
            {
                return HttpNotFound();
            }

            ViewBag.WLAHeaderId = (id ?? 0);

            return View(wLAHeader);
        }

        // GET: WLAHeaders/Groups/5
        public ActionResult Groups(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Groups gm = new Groups();
            gm.WLAHeader = db.WLAHeaders.Find(id);

            var standarTime = db.Standard_Time.Where(g => g.Tahun == gm.WLAHeader.Tahun);

            gm.GroupModel = db.WLATrx.Where(
                c => c.WLAHeader.Id == id
            ).GroupBy(
                g => new { ActivityGroup = g.ActivityGroup.Name
            }).Select(
                g => new GroupModel
                {
                    ActivityGroup = g.Key.ActivityGroup,
                    Effective_Working_Hours = g.Sum(x => x.Sub_Total_Aktivitas)
                }
            ).ToList().ToArray();

            if (gm.WLAHeader == null)
            {
                return HttpNotFound();
            }

            ViewBag.WLAHeaderId = (id ?? 0);

            return View(gm);
        }

        // GET: WLAHeaders/Create
        public ActionResult Create()
        {
            var fungsi = from s in db.Fungsi select s;
            ViewData["Fungsi"] = fungsi.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            var jabatan = from s in db.Jabatan select s;
            ViewData["Jabatan"] = jabatan.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            return View();
        }

        // POST: WLAHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tahun,FTE")] WLAHeader wLAHeader, FormCollection formData)
        {
            if (ModelState.IsValid)
            {
                int fungsiId = Convert.ToInt32(formData["Fungsi"].ToString());
                int jabatanId = Convert.ToInt32(formData["Jabatan"].ToString());

                wLAHeader.Fungsi = db.Fungsi.Where(c => c.Id == fungsiId).First();
                wLAHeader.Jabatan = db.Jabatan.Where(c => c.Id == jabatanId).First();

                db.WLAHeaders.Add(wLAHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var fungsi = from s in db.Fungsi orderby s.Name select s;
            ViewData["Fungsi"] = fungsi.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            var jabatan = from s in db.Jabatan orderby s.Name select s;
            ViewData["Jabatan"] = jabatan.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            return View(wLAHeader);
        }

        // GET: WLAHeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WLAHeader wLAHeader = db.WLAHeaders.Find(id);
            if (wLAHeader == null)
            {
                return HttpNotFound();
            }

            ViewBag.Fungsi = new SelectList(db.Fungsi, "Id", "Name", wLAHeader.Fungsi.Id);
            ViewBag.Jabatan = new SelectList(db.Jabatan, "Id", "Name", wLAHeader.Jabatan.Id);
            
            return View(wLAHeader);
        }

        // POST: WLAHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tahun,FTE")] WLAHeader wLAHeader, FormCollection formData)
        {
            if (ModelState.IsValid)
            {
                var wlaheader = db.WLAHeaders.Find(Convert.ToInt32(formData["Id"].ToString()));
                int fungsiId = Convert.ToInt32(formData["Fungsi"].ToString());
                int jabatanId = Convert.ToInt32(formData["Jabatan"].ToString());

                var Fungsix = db.Fungsi.Where(c => c.Id == fungsiId).First();
                var Jabatanx = db.Jabatan.Where(c => c.Id == jabatanId).First();

                wlaheader.Fungsi = Fungsix;
                wlaheader.Jabatan = Jabatanx;
                wlaheader.FTE = wLAHeader.FTE;
                wlaheader.Tahun = wLAHeader.Tahun;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fungsi = new SelectList(db.Fungsi, "Id", "Name", wLAHeader.Fungsi.Id);
            ViewBag.Jabatan = new SelectList(db.Jabatan, "Id", "Name", wLAHeader.Jabatan.Id);

            return View(wLAHeader);
        }

        // GET: WLAHeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WLAHeader wLAHeader = db.WLAHeaders.Find(id);
            if (wLAHeader == null)
            {
                return HttpNotFound();
            }
            return View(wLAHeader);
        }

        // POST: WLAHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WLAHeader wLAHeader = db.WLAHeaders.Find(id);
            db.WLAHeaders.Remove(wLAHeader);
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
