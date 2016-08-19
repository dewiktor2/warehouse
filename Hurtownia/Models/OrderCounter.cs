using Hurtownia.DAL;
using Hurtownia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class OrderCounter
    {
        private List<Statystyki> lista_statystyk = new List<Statystyki>();
        public List<Statystyki> Lista_statystyk
        {
            get { return lista_statystyk; }
        }
        HurtowniaContext db = new HurtowniaContext();

        public void Counting()
        {
            foreach (Order k in db.OrderItems)
            {
                int sum = 0;
                Statystyki temp = new Statystyki();
                foreach (Product product in db.Products)
                {

                    if (product.Id == k.productId)
                    {
                        sum += k.quantity;
                    }
                }

                temp.Nazwa = k.Product.Name;
                if (!lista_statystyk.Exists(x => string.Compare(x.Nazwa, temp.Nazwa, true) == 0))
                {

                    temp.suma = sum;
                    lista_statystyk.Add(temp);
                }
                else if (lista_statystyk.Exists(x => string.Compare(x.Nazwa, temp.Nazwa, true) == 0))
                {
                    temp.suma += sum;
                    lista_statystyk.Add(temp);
                }
            }
          
        }

      public void costam()
      {
           List<Statystyki> nodupes = lista_statystyk.Distinct().ToList();
            foreach(Statystyki item in nodupes)
            {
                Console.WriteLine(" "+item.Nazwa);
                Console.WriteLine(" " + item.suma);
            }
      }

        public object Total(string p)
        {

            var zmienna = lista_statystyk.Distinct().Where(e => e.Nazwa == p).Sum(x => x.suma);
              return zmienna;
        }
    }
    }
    
