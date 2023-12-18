using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManagementApp.Models
{
    public class CombinedViewModel
    {
        public Building Building { get; set; }
        public IList<Appartment> appartments { get; set; }


    }
}