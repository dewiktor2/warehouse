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
    public class ProductController : Controller
    {
        private HurtowniaContext db = new HurtowniaContext();
        string ShoppingCartId { get; set; }
        // GET: /Product/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {


            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.DateSortParm = sortOrder == "Description";
            var products = from s in db.Products
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {

                products = products.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Description":
                    products = products.OrderBy(s => s.Description);
                    break;

                default:
                    products = products.OrderBy(s => s.Name
);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));

        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create
       [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
        //    IEnumerable<SelectListItem> lista = new SelectList(db.Categories, "Id", "Name");

       //     ViewBag.CategoryId = lista;
           
            
  return View();
            
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Name,Price,Quantity,CategoryId")] Product product)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Products.Add(product);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}


            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            //return View(product);

            try
            {

                HttpPostedFileBase file = Request.Files["imageFile"];
                if(file !=null && file.ContentLength>0)
                {
                    product.Image = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("../Images/") + product.Image);

                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            catch
            {

                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
                return View(product);
            }
        }

        // GET: /Product/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Name,Image,Price,Quantity,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
