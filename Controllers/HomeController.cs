using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using job_offers.Models;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            return View(db.categories .ToList());
        }
        public ActionResult details(int jobid)
        {
            var job = db.jobs.Find(jobid);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["jobid"] = jobid;
            return View(job);
        }
        [Authorize]
        public ActionResult apply()
        {

            return View();
        }

        [HttpPost]
        public ActionResult apply(string message )
        {
            var userid = User.Identity.GetUserId();
            var jobid = (int)Session["jobid"];
            var check = db.applyforjobs.Where(a => a.userid == userid && a.jobid == jobid).ToList();
            if (check.Count<1)
            {
                var data = new applyforjob();
                data.jobid = jobid;
                data.message = message;
                data.userid = userid;
                data.applydate = DateTime.Now;
                db.applyforjobs.Add(data);
                db.SaveChanges();

                ViewBag.result ="your application is sent successfuly ";
              

            }
            else
            {
                ViewBag.result="you allready was applied to this job ";
            }

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
       
        public ActionResult searsh(string searshbox )
        {
            var result = db.jobs.Where(a => a.jobtitle.Contains(searshbox)
            || a.jobdiscrption.Contains(searshbox)
            || a.category.categoryname.Contains(searshbox)
            || a.category.categorydicrption.Contains(searshbox)).ToList();
            
            return View(result);
        }

        [Authorize]
        public ActionResult getjobsbyuser()
        {
            var userid = User.Identity.GetUserId();
            var jobs = db.applyforjobs.Where(a => a.userid == userid);

            return View(jobs.ToList());
        }
        [Authorize]
        public ActionResult getjobsbypuplisher()
        {
            var userid = User.Identity.GetUserId();
            var jobs = from app in db.applyforjobs
                       join joob in db.jobs
                       on app.jobid equals joob.id
                       where joob.user.Id == userid
                       select app;

            var groubeded = from j in jobs
                            group j by j.job.jobtitle
                            into gr
                            select new jobsview
                            {
                                jobtitle = gr.Key,
                                items = gr
                                
                            };

            return View(groubeded.ToList());
        }
        public ActionResult detailsofjob(int id)
        {
           
            var job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        public ActionResult Edit(int id)
        {
            var job = db.applyforjobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: roles/Edit/5
        [HttpPost]
        public ActionResult Edit(applyforjob job)
        {
            if (ModelState.IsValid)
            {
                job.applydate = DateTime.Now;      
                db.Entry(job).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("getjobsbyuser");
            }
            return View(job);
        }

        public ActionResult Delete(int id)
        {
            var job = db.applyforjobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: roles/Delete/5
        [HttpPost]
        public ActionResult Delete(applyforjob job)
        {
            var myjob = db.applyforjobs.Find(job.id);
            db.applyforjobs.Remove(myjob);
            db.SaveChanges();
            return RedirectToAction("getjobsbyuser");



        }


        public ActionResult Contact()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult Contact(contact c )
        {
            var email = new MailMessage();
            var loginfo = new NetworkCredential("salaheid308@gmail.com", "01095591324");
            email.From = new MailAddress(c.emial);
            email.To.Add(new MailAddress("salaheid308@gmail.com"));
            email.Subject = c.subject;
            email.IsBodyHtml = true;
            string body = "sender name :" + c.name + "<br>" +
                          "the email of sender :" + c.emial + "<br>" +
                          "the title of message:" + c.subject + "<br>" +
                          "the content of message:" + c.message + "<br>";
            email.Body = body;

            var smtpc = new SmtpClient("smtp.gmail.com", 587);
            smtpc.EnableSsl = true;
            smtpc.Credentials = loginfo;
            smtpc.Send(email);

            return RedirectToAction("index");
        }

    }
}