using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Drawing;
namespace Hurtownia.Models
{
    public class Product
    {

        public int Id { get; set; }

         [Required(ErrorMessage = "Description required-Cant be Blank")]
        [StringLength(25, ErrorMessage = "Too Long")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Name required-Cant be Blank")]
        [StringLength(25,ErrorMessage="Too Long")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Price required")]
        [Range(1,10000 ,ErrorMessage = "Too Long")]
        public int Price { get; set; }


        [Required(ErrorMessage = "Quantity required")]
        [Range(1, 10000, ErrorMessage = "Too Long")]
        public int Quantity { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Category required")]
        public int CategoryId { get; set; }

       


        public virtual Category Category { get; set; }

        public virtual ICollection<Order> Order { get; set; }
       
        public virtual ICollection<Statistic> Results { get; set; }
    }
}