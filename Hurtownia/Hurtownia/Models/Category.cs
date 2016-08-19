using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class Category
    {

        public int Id { get; set; }

        public String Name { get; set; }

        public string Image { get; set; }

        //IEnumberable
        
        public virtual ICollection<Product> Products { get; set; }
    }
}