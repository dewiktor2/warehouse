using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public enum ProductType
    {
        ProduktMleczny,Chemiczne,Alkohole,Wypieki,Słodycze
    }

    public class Result
    {
        public int Id { get; set; }


        public int ProductId { get; set; }

        public int UserId { get; set; }

        
        public ProductType? ProductType { get; set; }

        public virtual Product Product  { get; set; }
        public virtual User User { get; set; }
    }
}