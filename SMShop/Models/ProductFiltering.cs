using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMShop.Models
{
    public class ProductFiltering
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public SelectList SortBrandAndPrice { get; set; }
        public SelectList Categories { get; set; }

        public SelectList CountDiscs { get; set; }
        public SelectList Weights { get; set; }
        public SelectList Counts { get; set; }
        public SelectList Materials { get; set; }
        public SelectList Sources { get; set; }
        public SelectList Origins { get; set; }


    }
}