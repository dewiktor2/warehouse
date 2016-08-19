using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class Order
    {
       
        [Key]
        public int itemId { get; set; }

        public int productId { get; set; }

       public int CustomerOrderId { get; set; }

 
        public int quantity { get; set; }



        
        public virtual Product Product { get; set; }
        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}