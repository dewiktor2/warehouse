using Hurtownia.DAL;
using Hurtownia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Hurtownia.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        HurtowniaContext db = new HurtowniaContext();

        public ViewResult Index()
        {
            return View(new CartViewModel
            {

                Cart = GetCart(),

            });

        }
        [Authorize]
        public ActionResult AddToCart(int? id)
        {
            Product product = db.Products
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                GetCart().AddItem(product, 1);

            }
            return RedirectToAction("index");
        }
        //[Authorize]
        //public ActionResult Update(int? id,int qty)
        //{
        //    Product product = db.Products
        //        .FirstOrDefault(p => p.Id == id);

        //    if (product != null)
        //    {
        //        GetCart().AddItem(product, qty);

        //    }
        //    return RedirectToAction("index");
        //}
        public ActionResult RemoveFromCart(int? id)
        {
            Product product = db.Products
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                GetCart().RemoveItem(product);

            }
            return RedirectToAction("index");
        }
        // [HttpPost]
        public ActionResult Zamow()
        {

            Cart cart = new Cart();

            cart = GetCart();

            String status = "przekazano";


            decimal total = cart.Total();

            User user = db.Users
                 .FirstOrDefault(p => p.UserName == User.Identity.Name);





            return View(new CartViewModel
            {

                Cart = GetCart(),

            });


        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Zamow([Bind(Include = "Cart,Product")] Cart cart)
        {

            cart = GetCart();
            String status = "przekazano";


            decimal total = cart.Total();

            User user = db.Users
                 .FirstOrDefault(p => p.UserName == User.Identity.Name);

            CustomerOrder customerOrder = new CustomerOrder();
            customerOrder.userId = user.UserId;
            customerOrder.status = status;
            customerOrder.totalCost = total;

            if (!cart.carts.Any())
            {
                return RedirectToAction("Index");
            }
            else
            {

                if (ModelState.IsValid)
                {


                    db.CustomerOrders.Add(customerOrder);
                    var result = db.SaveChanges();


                    foreach (Carta item in cart.carts)
                    {
                        Order order2 = new Order
                        {

                            productId = item.Product.Id,
                            quantity = item.Quantity,
                            CustomerOrderId = customerOrder.orderId
                        };
                        Product product = db.Products.Find(item.Product.Id);
                        product.Quantity -= item.Quantity;


                        if (item.Quantity > product.Quantity)
                        {

                            return View("Error");
                        }
                        else
                        {

                            db.OrderItems.Add(order2);
                            db.SaveChanges();
                        }

                    }
                    db.SaveChanges();

/*
                    String adres = user.Email;
                    var koszt = customerOrder.totalCost;

                    //   string path = Server.MapPath(@"Images/mleko.jpg");
                    // LinkedResource logo = new LinkedResource(path);
                    //   logo.ContentId = "companylogo";

                    var body3 = " <p> Hello " + user.Name + " your order has been created <p/> <p> Username: " + user.UserName + " <p/>TotalCost is : " + customerOrder.totalCost.ToString() + " <br/>   ";

                    var message = new MailMessage();
                    message.To.Add(new MailAddress(adres));  // replace with valid value 
                    message.From = new MailAddress("wiktor.debski92@live.com");  // replace with valid value
                    message.Subject = "Your Order information";


                    message.Body = string.Format(body3, user.Name, user.Surname, "Your order is now in process", "Total cost is :", koszt);
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "wiktor.debski92@live.com",  // replace with valid value
                            Password = ""
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp.live.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Send(message);
                        // return RedirectToAction("Sent");
                    }
*/
                    cart.clear();

                    return RedirectToAction("Index");
                }
            }
            return View(customerOrder);
        }

        private Cart GetCart()
        {

            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        private Cart setCart(Cart cart)
        {


            Session["Cart"] = cart;


            return cart;

        }

        [HttpPost]
        public ActionResult Update(int? id, int qty, String item2)
        {
            Cart cart = GetCart();
            Product product = db.Products
               .FirstOrDefault(p => p.Id == id);
            foreach (Carta item in cart.carts)
            {
                if (item.Product.Name == item2)
                {
                    item.Quantity = qty;
                }
                //   GetCart().AddItem(product, qty);S

            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult UpdateAll(Cart cart)
        {
            Cart carta = GetCart();

            foreach (Carta item in carta.carts)
            {
                int quantity = item.Quantity;
                foreach (Carta item2 in cart.carts)
                {
                    if (item.Product.Id == item2.Product.Id)
                    {
                        item.Quantity = item2.Quantity;
                    }

                }



            }
            setCart(carta);

            return RedirectToAction("index");
        }


    }


}
