using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KHBPA.Models;

namespace KHBPA.Controllers
{
    public class PhotoGalleryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PhotoGallery
        public ActionResult Index()
        {
            return View(db.Document.ToList());
        }

        // GET: PhotoGallery/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: PhotoGallery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhotoGallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DocumentName,UploadDate,UploadedBy,FileBytes,ContentType,Discriminator,ContentLength")] Document document)
        {
            if (ModelState.IsValid)
            {
                //  only need a string for the photo, so I'm hard coding
                //  these values so the DB doesn't throw a bitch fit over null values
                document.UploadDate = DateTime.Now;
                document.UploadedBy = "Me Bruh! Who the Love else!?";
                document.ContentType = "Photo";
                document.Discriminator = "WTF is a Discriminator?";
                document.ContentLength = 1;
                db.Document.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        // GET: PhotoGallery/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: PhotoGallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DocumentName,UploadDate,UploadedBy,FileBytes,ContentType,Discriminator,ContentLength")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: PhotoGallery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: PhotoGallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Document.Find(id);
            db.Document.Remove(document);
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
