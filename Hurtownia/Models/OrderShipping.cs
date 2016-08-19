using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class OrderShipping
    {
        private User user1;

        public OrderShipping(User user1)
        {
            // TODO: Complete member initialization
            this.user1 = user1;
        }

        public OrderShipping()
        {
            // TODO: Complete member initialization
        }
        public int Id { get; set; }


        public int UserId { get; set; }

        [Required(ErrorMessage="Wymagamy podania imienia")]
        public String Name { get; set; }

         [Required(ErrorMessage = "Wymagamy podania nazwiska")]
        public String Surname { get; set; }

         [Required(ErrorMessage = "Wymagamy podania adresu ulicy")]
        public string Adres { get; set; }

         [Required(ErrorMessage = "Wymagamy podania nr domu lub bloku")]
        public string Adres1 { get; set; }

         [Required(ErrorMessage = "Wymagamy podania miasta")]
        public String City { get; set; }

         [Required(ErrorMessage = "Wymagamy podania kodu pocztowego")]
        public String ZipCode { get; set; }

        
         public virtual User user { get; set; }

    }
}