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
    public class DocumentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Document
        public ActionResult Index()
        {
            return View(db.Document.ToList());
        }

        // GET: Document/Details/5
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

        // GET: Document/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DocumentUpload()
        {
            return View();
        }
        // POST: Document/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DocumentName,UploadDate,UploadedBy,FileBytes,ContentType,Discriminator,ContentLength")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Document.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        // GET: Document/Edit/5
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

        // POST: Document/Edit/5
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

        // GET: Document/Delete/5
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

        // POST: Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Document.Find(id);
            db.Document.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[Authorize]
        [HttpPost, ActionName("UploadDocument")]        
        public ActionResult UploadDocument(HttpPostedFileBase file, Document document)
        {
            if (file != null && file.ContentLength > 0)
            {
                byte[] uploadedFile = new byte[file.InputStream.Length];
                file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                if (ModelState.IsValid)
                {
                    document.UploadDate = DateTime.Now;
                    document.UploadedBy = "Me";
                    document.ContentType = "Photo";
                    document.Discriminator = "WTF is a Discriminator?";
                    document.ContentLength = 1;
                    document.FileBytes = uploadedFile;
                    db.Document.Add(document);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(document);
                }
            }
            else
            {
                return View(document);
            }
        }

        public ActionResult DownloadDocument(string documentName)
        {
            var Documents = db.Document.Where(d => d.DocumentName == documentName);

            var selectedDocument = Documents.FirstOrDefault();

            if (selectedDocument != null)
            {
                return File(selectedDocument.FileBytes, "application/octet-stream", $"{selectedDocument.DocumentName}.pdf");
                //return File(selectedDocument.FileBytes, "application/octet-stream", selectedDocument.DocumentName);
            }
            return RedirectToAction("DocumentUpload");
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
