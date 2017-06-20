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
using System.IO;
using System.Text.RegularExpressions;
using OfficeOpenXml;

namespace WLA.Controllers
{
    public class WLAHeadersController : Controller
    {
        private wlaEntities db = new wlaEntities();

        // GET: WLAHeaders
        public ActionResult Index(string Fungsi, string Jabatan, int? page)
        {
            ViewBag.FungsiId = null;
            ViewBag.JabatanId = null;

            List<SelectListItem> fungsiList = db.Fungsi.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            fungsiList.Insert(0, new SelectListItem { Text = "Semua", Value = "0" });
            ViewBag.Fungsi = fungsiList;

            List<SelectListItem> jabatanList = db.Jabatan.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            jabatanList.Insert(0, new SelectListItem { Text = "Semua", Value = "0" });
            ViewBag.Jabatan = jabatanList;

            var wlaheader = from s in db.WLAHeaders
                            select s;
            wlaheader = wlaheader.OrderBy(s => s.Jabatan.Name);
            if (Fungsi != null && !Fungsi.Equals("0"))
            {
                int fungsiId = Convert.ToInt32(Fungsi);
                ViewBag.FungsiId = fungsiId;
                wlaheader = wlaheader.Where(s => s.Fungsi.Id == fungsiId);
            }

            if (Jabatan != null && !Jabatan.Equals("0"))
            {
                int jabatanId = Convert.ToInt32(Jabatan);
                ViewBag.JabatanId = jabatanId;
                wlaheader = wlaheader.Where(s => s.Jabatan.Id == jabatanId);
            }

            int pageSize = 5;
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
            ViewBag.wLATrx = (from s in db.WLATrx where s.WLAHeader.Id == (id ?? 0) orderby s.ActivityGroup.Name select s).ToList();
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

            var standarTime = db.Standard_Time.Where(g => g.Tahun == gm.WLAHeader.Tahun).ToArray();

            if (standarTime == null)
            {
                return HttpNotFound();
            }

            double efective_working_hour = standarTime[0].Effective_Working_Hours;
            gm.GroupModel = db.WLATrx.Where(
                c => c.WLAHeader.Id == id
            ).GroupBy(
                g => new
                {
                    ActivityGroup = g.ActivityGroup.Name,
                    ActivityGroupId = g.ActivityGroup.Id
                }).Select(
                g => new GroupModel
                {
                    ActivityGroup = g.Key.ActivityGroup,
                    ActivityGroupId = g.Key.ActivityGroupId,
                    Effective_Working_Hours = g.Sum(x => x.Sub_Total_Aktivitas),
                    FTE = g.Sum(x => x.Sub_Total_Aktivitas) / efective_working_hour
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
        public ActionResult Create([Bind(Include = "Id,Tahun")] WLAHeader wLAHeader, FormCollection formData)
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

        private string GetSafeFileName(string Filename, string Replacement)
        {
            if (Filename == null)
            {
                return string.Empty;
            }
            var illegalChars = String.Concat(String.Join(String.Empty, Path.GetInvalidFileNameChars().Select(x => x.ToString()).ToArray()), "~!@#$%^&()+=`';,");

            var illegalPattern = new Regex("[" + Regex.Escape(illegalChars) + "]");

            return Regex.Replace(illegalPattern.Replace(Filename, Replacement), @"\s+", " ");
        }

        public ActionResult AddAttachment(string ElementId)
        {
            JsonResult ReturnObject = new JsonResult();
            ReturnObject.ContentType = "text/html";
            Dictionary<string, string> JsonData = new Dictionary<string, string>();
            string AttachmentPath = string.Empty;

            try
            {
                AttachmentPath = Server.MapPath("~/Content/UploadData");

                if (!Directory.Exists(AttachmentPath))
                {
                    Directory.CreateDirectory(AttachmentPath);
                }
                var File = Request.Files[ElementId];
                string FileName = Path.GetFileName(File.FileName);
                var FileNameSplit = FileName.Split('.');
                if (FileNameSplit[FileNameSplit.Length - 1].ToLower() != "xlsx" && FileNameSplit[FileNameSplit.Length - 1].ToLower() != "xls")
                {
                    JsonData.Add("Url", string.Empty);
                    JsonData.Add("Filename", string.Empty);
                    JsonData.Add("Success", "false");
                    JsonData.Add("Message", "Please upload only excel file from templates.");
                }
                else
                {
                    FileName = GetSafeFileName(FileName, string.Empty);
                    AttachmentPath = AttachmentPath + "\\" + FileName;
                    File.SaveAs(AttachmentPath);
                    string Url = String.Format("<a class=\"lightLink\" target=\"_blank\" href=\"{0}\">{1}</a>", AttachmentPath, FileName);
                    JsonData.Add("Url", Url);
                    JsonData.Add("Filename", FileName);
                    JsonData.Add("Success", "true");
                    JsonData.Add("Message", string.Empty);
                }
            }
            catch (Exception e)
            {
                JsonData.Add("Url", string.Empty);
                JsonData.Add("Filename", string.Empty);
                JsonData.Add("Success", "false");
                JsonData.Add("Message", e.Message);
            }
            ReturnObject.Data = JsonData;
            return ReturnObject;
        }

        public ActionResult UploadData(string Filename)
        {
            Boolean IsSuccess = false;
            string strMessage = string.Empty;
            string UploadLoanPath = Server.MapPath("~/Content/UploadData/" + Filename);
            var i = 6;
            try
            {
                FileStream file = new FileStream(UploadLoanPath, FileMode.Open, FileAccess.Read);
                ExcelPackage ExcelPackage = new ExcelPackage(file);
                ExcelWorksheet ExcelWorksheet = ExcelPackage.Workbook.Worksheets["Sheet1"];
                try
                {
                    var hasValue = true;

                    int tahun = Convert.ToInt32(ExcelWorksheet.Cells[1, 3].Text.Trim());
                    string fungsi = ExcelWorksheet.Cells[2, 3].Text.Trim();
                    string jabatan = ExcelWorksheet.Cells[3, 3].Text.Trim();
                    var wlaheader = db.WLAHeaders.Where(s => s.Tahun == tahun && s.Fungsi.Name.Equals(fungsi) && s.Jabatan.Name.Equals(jabatan)).First();
                    if (wlaheader == null)
                    {
                        WLAHeader x = new WLAHeader();
                        x.Tahun = tahun;
                        x.Fungsi = db.Fungsi.Where(c => c.Name.Equals(fungsi)).First();
                        x.Jabatan = db.Jabatan.Where(c => c.Name.Equals(jabatan)).First();
                        db.WLAHeaders.Add(x);
                        db.SaveChanges();
                        wlaheader = db.WLAHeaders.Where(s => s.Tahun == tahun && s.Fungsi.Name.Equals(fungsi) && s.Jabatan.Name.Equals(jabatan)).First();
                    }
                    //Looping data dr excel
                    string current_grup = "";
                    while (hasValue)
                    {
                        WLATrx wLATrx = new WLATrx();
                        if (string.IsNullOrEmpty(ExcelWorksheet.Cells[i, 3].Text))
                        {
                            hasValue = false;
                            continue;
                        }
                        
                        string activity_s = ExcelWorksheet.Cells[i, 3].Text.Trim();
                        var activity = db.Activities.Where(s => s.Name.Equals(activity_s)).FirstOrDefault();
                        if (activity == null)
                        {
                            Activity x = new Activity();
                            x.Name = activity_s;
                            db.Activities.Add(x);
                            db.SaveChanges();
                            activity = db.Activities.Where(s => s.Name.Equals(activity_s)).First();
                        }
                        wLATrx.Activity = activity;

                        string activityGroup_s = ExcelWorksheet.Cells[i, 2].Text.Trim();
                        if (activityGroup_s.Equals(""))
                        {
                            activityGroup_s = current_grup;
                        }
                        else
                        {
                            current_grup = activityGroup_s;
                        }
                        var activityGroup = db.ActivityGroups.Where(s => s.Name.Equals(activityGroup_s)).FirstOrDefault();
                        if (activityGroup == null)
                        {
                            ActivityGroup x = new ActivityGroup();
                            x.Name = activityGroup_s;
                            db.ActivityGroups.Add(x);
                            db.SaveChanges();
                            activityGroup = db.ActivityGroups.Where(s => s.Name.Equals(activityGroup_s)).First();
                        }
                        wLATrx.ActivityGroup = activityGroup;

                        string pelaksana = ExcelWorksheet.Cells[i, 4].Text.Trim();
                        wLATrx.Pelaksana = db.Pelaksana.Where(c => c.Name.Equals(pelaksana)).FirstOrDefault();
                        if (wLATrx.Pelaksana == null)
                        {
                            strMessage = strMessage + "row " + i + " gagal, ";
                            i++;
                            continue;
                        }

                        string periode = ExcelWorksheet.Cells[i, 5].Text.Trim();
                        wLATrx.Periode = db.Periode.Where(c => c.Name.Equals(periode)).FirstOrDefault();
                        if (wLATrx == null)
                        {
                            strMessage = strMessage + "row " + i + " gagal, ";
                            i++;
                            continue;
                        }

                        int Periode_Value = 0;
                        WLAModel wm = new WLAModel();
                        Periode_Value = wm.getPeriode(wLATrx.Periode.Id);
                        wLATrx.WLAHeader = wlaheader;
                        wLATrx.Process_Time = Convert.ToInt32(ExcelWorksheet.Cells[i, 6].Text.Trim());
                        wLATrx.Quantity = Convert.ToInt32(ExcelWorksheet.Cells[i, 7].Text.Trim());
                        wLATrx.Frequency = Convert.ToInt32(ExcelWorksheet.Cells[i, 8].Text.Trim());
                        wLATrx.Sub_Total_Aktivitas = (double)(Periode_Value * wLATrx.Process_Time * wLATrx.Quantity * wLATrx.Frequency) / 60;
                        var wlaExist = db.WLATrx.Where(
                            s => s.WLAHeader.Id == wlaheader.Id &&
                            s.Activity.Name.Equals(activity_s) &&
                            s.ActivityGroup.Name.Equals(activityGroup_s)
                        ).FirstOrDefault();
                        if (wlaExist == null)
                        {
                            db.WLATrx.Add(wLATrx);
                        }
                        else
                        {
                            wlaExist.Frequency = wLATrx.Frequency;
                            wlaExist.Process_Time = wLATrx.Process_Time;
                            wlaExist.Quantity = wLATrx.Quantity;
                            wlaExist.Sub_Total_Aktivitas = wLATrx.Sub_Total_Aktivitas;
                        }

                        db.SaveChanges();

                        var standardTime = db.Standard_Time.Where(d => d.Tahun == tahun).FirstOrDefault();
                        if (standardTime != null)
                        {
                            var Sub_Total_Aktivitas = db.WLATrx.Where(d => d.WLAHeader.Id == wlaheader.Id).Sum(d => d.Sub_Total_Aktivitas);
                            var dataHeader = db.WLAHeaders.Find(wlaheader.Id);
                            dataHeader.Effective_Working_Hours = Sub_Total_Aktivitas;
                            dataHeader.FTE = Sub_Total_Aktivitas / standardTime.Effective_Working_Hours;
                            db.SaveChanges();
                        }

                        i++;
                    }

                }
                catch (Exception e)
                {
                    strMessage = "" + i;
                    file.Close();
                    file.Dispose();
                    throw new Exception(e.Message);
                }

                IsSuccess = true;

                file.Close();
                file.Dispose();
            }
            catch (Exception e)
            {
                strMessage = "" + i;
                throw new Exception(e.Message);
            }
            return Json(new
            {
                status = IsSuccess,
                message = strMessage
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UnitUpdate(FormCollection formData)
        {
            bool IsSuccess = false;
            string strMessage = "";

            int jumlah = Convert.ToInt32(formData["jumlah"].ToString());
            int WLAHeaderId = Convert.ToInt32(formData["WLAHeaderId"].ToString());
            int ActivityGroupId = Convert.ToInt32(formData["ActivityGroupId"].ToString());

            var wla = db.WLATrx.Where(s => s.WLAHeader.Id == WLAHeaderId && s.Activity.Type == 1 && s.ActivityGroup.Id == ActivityGroupId).FirstOrDefault();

            int Periode_Value = 0;
            WLAModel wm = new WLAModel();
            Periode_Value = wm.getPeriode(wla.Periode.Id);

            wla.Quantity = jumlah;
            wla.Sub_Total_Aktivitas = (double)(Periode_Value * wla.Process_Time * wla.Quantity * wla.Frequency) / 60;
            db.SaveChanges();

            var standardTime = db.Standard_Time.Where(d => d.Tahun == wla.WLAHeader.Tahun).FirstOrDefault();
            if (standardTime != null)
            {
                var Sub_Total_Aktivitas = db.WLATrx.Where(d => d.WLAHeader.Id == WLAHeaderId).Sum(d => d.Sub_Total_Aktivitas);
                var dataHeader = db.WLAHeaders.Find(WLAHeaderId);
                dataHeader.Effective_Working_Hours = Sub_Total_Aktivitas;
                dataHeader.FTE = Sub_Total_Aktivitas / standardTime.Effective_Working_Hours;
                db.SaveChanges();
            }

            return Json(new
            {
                status = IsSuccess,
                message = strMessage
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
