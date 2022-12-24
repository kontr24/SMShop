﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMShop.Models
{
    public class CartItem
    {
        public CartItem()
        {

        }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
