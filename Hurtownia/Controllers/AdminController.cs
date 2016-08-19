using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hurtownia.Models;
using Hurtownia.DAL;
using PagedList;
using Hurtownia.ViewModels;
namespace Hurtownia.Controllers
{
     [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
         private List<Statystyki> lista = new List<Statystyki>();
         public List<Statystyki> Lista_statystyk
         {
             get { return lista; }
         }
        private HurtowniaContext db = new HurtowniaContext();

         
      
        // GET: /Admin/
        public ActionResult Index()
        {
            var results = db.Statistics.Include(r => r.Product).Include(r => r.User);
           // return View(results.ToList());
            return View(results.ToList());
        }


      

        // GET: /Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic result = db.Statistics.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: /Admin/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Description");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name");
            return View();
        }

        // POST: /Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ProductId,UserId,ProductType")] Statistic result)
        {
            if (ModelState.IsValid)
            {
                db.Statistics.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Description", result.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", result.UserId);
            return View(result);
        }

        // GET: /Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic result = db.Statistics.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Description", result.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", result.UserId);
            return View(result);
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProductId,UserId,ProductType")] Statistic result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Description", result.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", result.UserId);
            return View(result);
        }

        // GET: /Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic result = db.Statistics.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: /Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statistic result = db.Statistics.Find(id);
            db.Statistics.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderCounter()
        {
            Statystyki temp = new Statystyki();
            List<Statystyki> lista2 = new List<Statystyki>();
            OrderCounter oc = new OrderCounter();
           
            oc.Counting();
           
       
            lista = oc.Lista_statystyk;


            var noDupes = lista.Distinct().ToList();
                
               
           
            ViewData["Popular"] = noDupes;
           

             ViewData["FeaturedProduct"] = oc;
        ViewBag.Product = oc;
        TempData["FeaturedProduct"] = oc; 


            return View("OrderCounterStatistics");
           

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
         public ActionResult clear()
        {
            List<Statystyki> temp = Lista_statystyk;
            temp.Clear();
            ViewData["Popular"] = temp;
            OrderCounter oc = new OrderCounter();
           

            ViewData["FeaturedProduct"] = oc;
            ViewBag.Product = oc;
            TempData["FeaturedProduct"] = oc;


            return View("OrderCounterStatistics");
           
           
        }

    }
}
