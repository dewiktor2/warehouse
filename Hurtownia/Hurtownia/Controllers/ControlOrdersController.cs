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
namespace Hurtownia.Controllers
{
    public class ControlOrdersController : Controller
    {
        private HurtowniaContext db = new HurtowniaContext();

        // GET: /ControlOrders/
        public ActionResult Index(int ?page)
        {

            var customerOrders = db.CustomerOrders.Include(r => r.User);
            customerOrders = customerOrders.OrderBy(s => s.status
);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(customerOrders.ToPagedList(pageNumber, pageSize));


        }

        public ActionResult Info(int? id)
        {



            var lista = db.OrderItems.Where(x => x.CustomerOrderId == id).OrderBy(x => x.Product.Name).ToList();




            return View(lista);
        }
        public ActionResult Shipping( string sortOrder,string currentFilter, string searchString, int? page)
        {
            var shipping = from s in db.Orders
                           select s;
            int id;
            int.TryParse(searchString, out id);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
           
            switch (sortOrder)
            {
                case "name_desc":
                    shipping = shipping.OrderByDescending(s => s.Name);
                    break;
            
               
                default:
                    shipping = shipping.OrderBy(s => s.Surname);
                    break;
            }
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
                shipping = shipping.Where(s =>
               s.Name.ToUpper().Contains(searchString.ToUpper())
                ||
               s.Surname.ToUpper().Contains(searchString.ToUpper()) ||
               s.UserId == id);
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(shipping.ToPagedList(pageNumber, pageSize));
        }

        // GET: /ControlOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerorder = db.CustomerOrders.Find(id);
            if (customerorder == null)
            {
                return HttpNotFound();
            }
            return View(customerorder);
        }

        // GET: /ControlOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ControlOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="orderId,userId,status,totalCost")] CustomerOrder customerorder)
        {
            if (ModelState.IsValid)
            {
                db.CustomerOrders.Add(customerorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerorder);
        }

        // GET: /ControlOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerorder = db.CustomerOrders.Find(id);
            if (customerorder == null)
            {
                return HttpNotFound();
            }
            return View(customerorder);
        }

        // POST: /ControlOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="orderId,userId,status,totalCost")] CustomerOrder customerorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerorder);
        }

        // GET: /ControlOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerOrder customerorder = db.CustomerOrders.Find(id);
            if (customerorder == null)
            {
                return HttpNotFound();
            }
            return View(customerorder);
        }

        // POST: /ControlOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerOrder customerorder = db.CustomerOrders.Find(id);
            db.CustomerOrders.Remove(customerorder);
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
