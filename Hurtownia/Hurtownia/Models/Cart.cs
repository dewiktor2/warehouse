using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class Cart
    {

        private List<Carta> collection = new List<Carta>();
        private Product product;
        public void AddItem(Product product,int quantity)
        {

            Carta karta = collection.Where(p => p.Product.Id == product.Id).FirstOrDefault();

            if(karta ==null)
            {

                collection.Add(new Carta { Product = product, Quantity=quantity });

            }
            else
            {
                karta.Quantity += quantity;
            }
        }
        public void RemoveItem(Product product)
        {

            collection.RemoveAll(l => l.Product.Id == product.Id);
        }
        public decimal Total()
        {

            return collection.Sum(e => e.Product.Price * e.Quantity);
        }
        public void clear()
        {

            collection.Clear();
        }
        public IEnumerable<Carta> carts
        {

            get { return  collection;}
        }
    }
    public class Carta
    {

        public  Product Product { get; set; }
        public int Quantity { get; set; }
    }
}