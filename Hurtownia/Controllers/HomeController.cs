using Hurtownia.DAL;
using Hurtownia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace Hurtownia.Controllers
{
    public class HomeController : Controller
    {
        HurtowniaContext db = new HurtowniaContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(int ?page)
        {
            IQueryable<DeteOfCreationModelView> data = from user in db.Users
                                                   group user by user.DateOfCreation into dateGroup
                                                   select new DeteOfCreationModelView()
                                                   {
                                                       CreationTime = dateGroup.Key,
                                                       UserCount = dateGroup.Count()
                                                   };

            data = data.OrderBy(s => s.CreationTime
);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(data.ToPagedList(pageNumber, pageSize));


            
        }
        public ActionResult CategoryCounter(int ?page)
        {
            var zmienna = db.Products.GroupBy(p => p.Category.Name)
                .Select(lista => new CategoryCounterModelView()
                     {
                         Category = lista.Key,
                         Quantity = lista.Count()
                     });
            zmienna = zmienna.OrderBy(s => s.Category
);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(zmienna.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}