using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FShop.Models;
using PagedList;

namespace FShop.Controllers
{
    public class HangsController : Controller
    {
        private FShopDB db = new FShopDB();

        // GET: Hangs
        public ActionResult Index(string sortOrder, string searchString,string minGia, string maxGia, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SapTheoGia = sortOrder == "gia" ? "gia_desc" : "gia";
            ViewBag.SapTheoNCC = sortOrder == "ncc" ? "ncc_desc" : "ncc";
            

            var hangs = db.Hangs.Select(p => p);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                hangs = hangs.Where(p => p.TenHang.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(minGia) && !String.IsNullOrEmpty(maxGia))
            {
                try
                {
                    double a = Double.Parse(minGia);
                    double b = Double.Parse(maxGia);
                    hangs = hangs.Where(p => ((Double)p.Gia > a && (Double)p.Gia < b));

                }
                catch(Exception ex)
                {

                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                hangs = hangs.Where(p => p.TenHang.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ten_desc":
                    hangs = hangs.OrderByDescending(s => s.TenHang);
                    break;
                case "gia":
                    hangs = hangs.OrderBy(s => s.Gia);
                    break;
                case "gia_desc":
                    hangs = hangs.OrderByDescending(s => s.Gia);
                    break;
                case "ncc":
                    hangs = hangs.OrderBy(s => s.Nha_CC.TenNCC);
                    break;
                case "ncc_desc":
                    hangs = hangs.OrderByDescending(s => s.Nha_CC.TenNCC);
                    break;
                default:
                    hangs = hangs.OrderBy(s => s.TenHang);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);


            return View(hangs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Hangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang hang = db.Hangs.Find(id);
            if (hang == null)
            {
                return HttpNotFound();
            }
            return View(hang);
        }

        // GET: Hangs/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.Nha_CC, "MaNCC", "TenNCC");
            Hang hang = new Hang();
            if (hang == null)
            {
                return HttpNotFound();
            }
            return View(hang);
        }

        // POST: Hangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,MaNCC,TenHang,Gia,LuongCo,MoTa,ChietKhau,HinhAnh")] Hang hang)
        {
            try
            {
                hang.HinhAnh = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/HangImages/" + FileName);
                    f.SaveAs(UploadPath);
                    hang.HinhAnh = FileName;
                }
                if (ModelState.IsValid)
                {
                    db.Hangs.Add(hang);
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                ViewBag.MaNCC = new SelectList(db.Nha_CC, "MaNCC", "TenNCC", hang.MaNCC);
                return View(hang);
            }
            

            
        }

        // GET: Hangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang hang = db.Hangs.Find(id);
            if (hang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.Nha_CC, "MaNCC", "TenNCC", hang.MaNCC);
            return View(hang);
        }

        // POST: Hangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,MaNCC,TenHang,Gia,LuongCo,MoTa,ChietKhau,HinhAnh")] Hang hang)
        {
            try
            {
                hang.HinhAnh = db.Hangs.AsNoTracking().Where(p => p.MaHang == hang.MaHang).First().HinhAnh;
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/HangImages/" + FileName);
                    f.SaveAs(UploadPath);
                    hang.HinhAnh = FileName;
                }
                if (ModelState.IsValid)
                {
                    db.Entry(hang).State = EntityState.Modified;
                    db.SaveChanges();
                   
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                ViewBag.MaNCC = new SelectList(db.Nha_CC, "MaNCC", "TenNCC", hang.MaNCC);
                return View(hang);
            }
            
            
        }

        // GET: Hangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hang hang = db.Hangs.Find(id);
            if (hang == null)
            {
                return HttpNotFound();
            }
            return View(hang);
        }

        // POST: Hangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Hang hang = db.Hangs.Find(id);
            try
            {
               
                db.Hangs.Remove(hang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không được xóa dữ liệu này " + ex.Message;
                return View("Delete", hang);
            }
           
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
