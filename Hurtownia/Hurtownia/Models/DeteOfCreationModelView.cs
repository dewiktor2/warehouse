using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hurtownia.Models
{
    public class DeteOfCreationModelView
    {

        [DataType(DataType.Date)]
        public DateTime? CreationTime { get; set; }
        public int UserCount { get; set; }
    }
}