using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication1.Models;

namespace job_offers.Controllers
{
    public class rolesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: roles/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
                
           
                return View(role);

            }   

        

        // GET: roles/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
               return  RedirectToAction("index");
            }
            return View(role);
        }

        // GET: roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            var myrole = db.Roles.Find(role.Id);
            db.Roles.Remove(myrole);
            db.SaveChanges();
                return RedirectToAction("Index");
            
                
            
        }
    }
}
