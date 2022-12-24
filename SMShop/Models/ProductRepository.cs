using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SMShop.Models
{
    public class ProductRepository
    {
        IEnumerable<Product> _products;
        IEnumerable<Category> _category;

        public IEnumerable<Product> GetProducts(string category = null)
        {
            GamesEntities db = new GamesEntities();
            IEnumerable<Product> model = db.Product;
            if (category != null)
            {
                model = db.Product.Include(i=>i.Category).Where(x=>x.Category.Category1 == category);
                //model = db.Product.Where(w => w.CategoryId == category);
                return model;
            }
            return model;
        }


        public IEnumerable<Category> GetCategory()
        {

            GamesEntities db = new GamesEntities();
            IEnumerable<Category> model1 = db.Category;
            return model1;
        }
        public ProductRepository()
        {
           //
        }
            public Product GetProductById(int id)
            {
                GamesEntities db = new GamesEntities();

                Product product = null;

                product = db.Product.First(row => row.Id == id);

                return product;
         
        }


    }

    }

