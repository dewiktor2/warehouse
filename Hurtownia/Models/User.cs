using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class User
    {
        public int UserId { get; set; }


        [Required(ErrorMessage = "Name required-Cant be Blank")]
        [StringLength(25, ErrorMessage = "You need to add Name")]
        public String UserName { get; set; }

      //  [Required(ErrorMessage = "Name required-Cant be Blank")]
          [StringLength(25, ErrorMessage = "You need to add Name")]
        public String Name { get; set; }

     //   [Required(ErrorMessage = "Surname required-Cant be Blank")]
        [StringLength(25, ErrorMessage = "You need to add Surname")]
        public String  Surname { get; set; }

        [StringLength(25, ErrorMessage = "You need to add Surname")]
        public String Email { get; set; }

    //    [Required(ErrorMessage = "Date cant be blank")]
        [DataType(DataType.Date)] 
        public DateTime? DateOfCreation { get; set; }
        

        public virtual ICollection<OrderShipping> OrderShipping { get; set; }

        public virtual ICollection<Statistic> Results { get; set; }

        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
    }
}