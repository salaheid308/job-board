﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using job_offers.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace job_offers.Controllers
{
    public class jobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: jobs
        public ActionResult Index()
        {
            var jobs = db.jobs.Include(j => j.category);
            return View(jobs.ToList());
        }

        // GET: jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: jobs/Create
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryname");
            return View();
        }

        // POST: jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( job job ,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
               

                    string bath = Path.Combine(Server.MapPath("~/uploads"), upload.FileName);
                    upload.SaveAs(bath);

                    job.jobimg = upload.FileName;
                
                job.userid = User.Identity.GetUserId();
                db.jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryname", job.categoryid);
            return View(job);
        }

        // GET: jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryname", job.categoryid);
            return View(job);
        }

        // POST: jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(job job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string old_bath = Path.Combine(Server.MapPath("~/uploads"),job.jobimg);
                if (upload !=null)
                {
                    System.IO.File.Delete(old_bath);
                    string bath = Path.Combine(Server.MapPath("~/uploads"), upload.FileName);
                    upload.SaveAs(bath);
                    job.jobimg = upload.FileName;
                }
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryname", job.categoryid);
            return View(job);
        }

        // GET: jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            job job = db.jobs.Find(id);
            db.jobs.Remove(job);
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
