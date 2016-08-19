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
    public class ProfileController : Controller
    {
        private HurtowniaContext db = new HurtowniaContext();

        // GET: /Profile/
        public ActionResult Index()
        {
            
            User user = db.Users
                 .FirstOrDefault(p => p.UserName == User.Identity.Name);
            return View(new List<User> { user});
        }
        public ActionResult Profile()
        {

            User user = db.Users
                 .FirstOrDefault(p => p.UserName == User.Identity.Name);
            return View(user);
        }

    

        // GET: /Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UserId,UserName,Name,Surname,DateOfCreation,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
                 
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UserId,UserName,Name,Surname,DateOfCreation,Email")] User user)
        {

            if (ModelState.IsValid)
            {


               var zmienna= db.Entry(user).State = EntityState.Modified;
                try
                { 
            var  omg=  db.SaveChanges();
                }catch(Exception ex)
                {
                    Console.WriteLine(""+ex);
                }
                return RedirectToAction("Index");
            }
           
            return View(user);
        }

        // GET: /Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Address()
        {

            OrderShipping order = new OrderShipping();
            User user = db.Users
                   .FirstOrDefault(p => p.UserName == User.Identity.Name);
            order.UserId = user.UserId;
            order.Name = user.Name;
            order.Surname = user.Surname;
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Address([Bind(Include = "Id,UserId,Name,Surname,Adres,Adres1,City,ZipCode")] OrderShipping order)
        {
            if (ModelState.IsValid)
            {

                User user = db.Users
                   .FirstOrDefault(p => p.UserName == User.Identity.Name);
                order.UserId = user.UserId;
               db.Orders.Add(order);
               db.SaveChanges();
                return RedirectToAction("Index");
            }
            /*
            ViewBag.ProductId = new SelectList(db.Orders, "Id", "Description", result.ProductId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", result.UserId);
             * */
            return View(order);
        }

        public ActionResult DeliveryAddressDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderShipping ordr = db.Orders.FirstOrDefault(p => p.UserId == id);
            if (ordr == null)
            {
                return HttpNotFound();
            }
            return View(ordr);
        }

        public ActionResult EditAddress(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderShipping order = db.Orders
                 .FirstOrDefault(p => p.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAddress([Bind(Include = "Id,UserId,Name,Surname,Adres,Adres1,City,ZipCode")] OrderShipping order)
        {
            if (ModelState.IsValid)
            {

                User user = db.Users
                   .FirstOrDefault(p => p.UserName == User.Identity.Name);
                order.UserId = user.UserId;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         
      
            return View(order);
        }

        public ActionResult Orders(int ? page)
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

           

            var lista = db.OrderItems.Where(x => x.CustomerOrderId== id).OrderBy(x => x.Product.Name).ToList();




            return View(lista);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        static Random random = new Random();
      
            
        // GET: /Product/Edit/5
        [Authorize]
        public ActionResult Report(int? id)
        {
            Report report = new Report();
            User user = db.Users
                  .FirstOrDefault(p => p.UserName == User.Identity.Name);
            int value = random.Next(10000000, 90000999);
            report.ReportId = value;
            report.UserId = user.UserId;
            report.DateOfCreation = DateTime.Now;
            return View(report);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report([Bind(Include = "question,UserId,reply,ReportId,DateOfCreation,Date,DateOfAnwser")] Report report)
        {

            if (ModelState.IsValid)
            {

                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(report);
        }

        public ActionResult Reports(int? page)
        {




            var reports = db.Reports.Include(r => r.user);
            reports = reports.OrderBy(s => s.DateOfCreation
);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(reports.ToPagedList(pageNumber, pageSize));
           
       //     var lista = db.Reports.Where(x => x.UserId == id).OrderBy(x => x.status).ToList();




        //    return View(lista);
        }
    }
}
