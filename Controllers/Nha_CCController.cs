using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FShop.Models;

namespace FShop.Controllers
{
    public class Nha_CCController : Controller
    {
        private FShopDB db = new FShopDB();

        // GET: Nha_CC
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            
            return View(db.Nha_CC.ToList());
        }

        // GET: Nha_CC/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nha_CC nha_CC = db.Nha_CC.Find(id);
            if (nha_CC == null)
            {
                return HttpNotFound();
            }
            return View(nha_CC);
        }

        // GET: Nha_CC/Create
        public ActionResult Create()
        {
            Nha_CC nha_CC = new Nha_CC();
            if (nha_CC == null)
            {
                return HttpNotFound();
            }
            return View(nha_CC);
        }

        // POST: Nha_CC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,DiaChi,DienThoai,Email")] Nha_CC nha_CC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Nha_CC.Add(nha_CC);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nha_CC);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(nha_CC);
            }
            

            
        }

        // GET: Nha_CC/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nha_CC nha_CC = db.Nha_CC.Find(id);
            if (nha_CC == null)
            {
                return HttpNotFound();
            }
            return View(nha_CC);
        }

        // POST: Nha_CC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,DiaChi,DienThoai,Email")] Nha_CC nha_CC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(nha_CC).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nha_CC);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(nha_CC);
            }
            
           
        }

        // GET: Nha_CC/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nha_CC nha_CC = db.Nha_CC.Find(id);
            if (nha_CC == null)
            {
                return HttpNotFound();
            }
            return View(nha_CC);
        }

        // POST: Nha_CC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Nha_CC nha_CC = db.Nha_CC.Find(id);
            try
            {
                db.Nha_CC.Remove(nha_CC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không được xóa dữ liệu này " + ex.Message;
                return View("Delete", nha_CC);
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
