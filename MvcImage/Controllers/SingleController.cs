using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcImage.Models;
using System.IO;

namespace MvcImage.Controllers
{
	public class SingleController : Controller
	{
		private CommonDbContext db = new CommonDbContext();

		// GET: /Single/
		public ActionResult Index()
		{
			return View(db.Images.ToList());
		}

		// GET: /Single/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Image image = db.Images.Find(id);
			if (image == null)
			{
				return HttpNotFound();
			}
			return View(image);
		}

		// GET: /Single/Create
		public ActionResult Create()
		{

			return View();
		}

		// POST: /Single/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,filename,type,size")] Image image)
		{
			if (ModelState.IsValid)
			{
				db.Images.Add(image);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(image);
		}

		// GET: /Single/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Image image = db.Images.Find(id);
			if (image == null)
			{
				return HttpNotFound();
			}
			return View(image);
		}

		// POST: /Single/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,filename,type,size")] Image image)
		{
			if (ModelState.IsValid)
			{
				db.Entry(image).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(image);
		}

		// GET: /Single/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Image image = db.Images.Find(id);
			if (image == null)
			{
				return HttpNotFound();
			}
			return View(image);
		}

		// POST: /Single/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Image image = db.Images.Find(id);
			db.Images.Remove(image);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
		{
				//string path = Server.MapPath(string.Format("~/Uploads/{0}", Request.Headers["X-File-Name"]));
				//Stream inputStream = Request.InputStream;

				//FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);

				//inputStream.CopyTo(fileStream);
				//fileStream.Close();
				
			if(files != null && files.Any())
			{
				foreach (var file in files)
				{
					if (file.ContentLength > 0)
					{
						var fileName = Path.GetFileName(file.FileName);
						var path = Path.Combine(Server.MapPath("~/Uploads/Saris/"), fileName);
						file.SaveAs(path);
					}
				}
			return Json(new { success = "success" }, JsonRequestBehavior.AllowGet);
   
			}
			Response.StatusCode = 400;
			return Json(new { success = "nope" }, JsonRequestBehavior.AllowGet);
		}


		public ActionResult UploadFile(HttpPostedFileBase file)
		{
			//string path = Server.MapPath(string.Format("~/Uploads/{0}", Request.Headers["X-File-Name"]));
			//Stream inputStream = Request.InputStream;

			//FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);

			//inputStream.CopyTo(fileStream);
			//fileStream.Close();
					if (file.ContentLength > 0)
					{
						var fileName = Path.GetFileName(file.FileName);
						var fullPath = Path.Combine(Server.MapPath("~/Uploads/Saris/"), fileName);

						int count = 1;

						string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
						string extension = Path.GetExtension(fullPath);
						string path = Path.GetDirectoryName(fullPath);
						string newFullPath = fullPath;

						while(System.IO.File.Exists(newFullPath)) 
						{
							string tempFileName = string.Format("{0}{1}", fileNameOnly, count++);
							newFullPath = Path.Combine(path, tempFileName + extension);
						}

						file.SaveAs(newFullPath  );
						return Json(new { success = "success" }, JsonRequestBehavior.AllowGet);

					}
				
				
			
			Response.StatusCode = 400;
			return Json(new { success = "nope" }, JsonRequestBehavior.AllowGet);
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
