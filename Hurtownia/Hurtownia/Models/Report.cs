using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class Report
    {

        public int ReportId { get; set; }



        [Required(ErrorMessage = "Name required-Cant be Blank")]
        public String question { get; set; }


        public String reply { get; set; }


        [DataType(DataType.Date)]
        public DateTime? DateOfCreation { get; set; }



        [DataType(DataType.Date)]
        public DateTime? DateOfAnwser { get; set; }

        //[Required(ErrorMessage = "Cant be null")]
        //public int reportIdentity { get; set; }

        //[Required(ErrorMessage = "Cant be null")]
        //public string message { get; set; }





        //[Required(ErrorMessage = "Cant be null")]
        //public string status { get;set; }

        public int UserId { get; set; }

        public virtual User user { get; set; }
    }
}