using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMShop.Models
{
    public class CartIndexViewModel : About
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}