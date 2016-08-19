using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class CustomerOrder
    {
        [Key]
        public int orderId { get; set; }

        public int userId { get; set; }

        public String status { get; set; }

        public decimal totalCost { get; set; }



       

        public virtual User User { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}