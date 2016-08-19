using Hurtownia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hurtownia.ViewModels
{
    public class Statystyki
    {
        public String Nazwa { get; set; }
        public int iloscZamowionychProduktow { get; set; }
        public int suma { get; set; }
        public OrderCounter OrderCounter { get; set; }
    }
}