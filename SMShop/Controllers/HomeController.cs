using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMShop.Models;
using System.Data.Entity;

namespace SMShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string category = null, /*int? categor = null,*/ string searchString = null, int? maxPrice = null, int? minPrice = null, string sortBrandAndPrice = null,
            string rusLanguage = null, int? countDiscs = null, int? weight = null, int? count = null, string material = null, string source = null, string addContent = null,
            string existGame = null, string origin = null)
        {

            GamesEntities db = new GamesEntities();

            ProductRepository pr = new ProductRepository();
            //IEnumerable<Product> model = pr.GetProducts(category);

            ViewBag.category = category;

            //ViewBag.categor = categor;



            IEnumerable<Product> products = db.Product.Include(p => p.Category);

            //поиск 
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                       || s.Description.ToUpper().Contains(searchString.ToUpper()));
            }
            //поиск


            //фильтрация
          
            if (!String.IsNullOrEmpty(category) && !category.Equals("Все"))
            {
                products = db.Product.Include(i => i.Category).Where(x => x.Category.Category1 == category);

                var cg = products.FirstOrDefault(x=>x.Category.Category1 == category);
                var prod = products.FirstOrDefault(x => x.RusLanguage == rusLanguage);
                var produ = products.FirstOrDefault(x => x.ExistGame == existGame);
                var produc = products.FirstOrDefault(x => x.Origin == origin);
                ViewBag.cg = cg;
                ViewBag.rusLanguage = prod;
                ViewBag.rusLanguages = rusLanguage;
                ViewBag.existGame = produ;
                ViewBag.existGames = existGame;
                ViewBag.origin = produc;
                ViewBag.origins = origin;
            }



            //if (categor != null && categor != 0)
            //{
            //    products = products.Where(p => p.CategoryId == categor);
            //}

            if (!String.IsNullOrEmpty(rusLanguage) && !rusLanguage.Equals("Все"))
            {
                products = products.Where(p => p.RusLanguage == rusLanguage);

            }
            if (!String.IsNullOrEmpty(addContent) && !addContent.Equals("Все"))
            {
                products = products.Where(p => p.AddContent == addContent);

            }
            if (countDiscs != null && countDiscs != 0)
            {
                products = products.Where(p => p.CountDiscs == countDiscs);
            }
            if (count != null && count != 0)
            {
                products = products.Where(p => p.Count == count);
            }
            if (weight != null && weight != 0)
            {
                products = products.Where(p => p.Weight == weight);
            }

            if (!String.IsNullOrEmpty(material) && !material.Equals("Все"))
            {
                products = products.Where(p => p.Material == material);

            }
            if (!String.IsNullOrEmpty(existGame) && !existGame.Equals("Все"))
            {
                products = products.Where(p => p.ExistGame == existGame);

            }

            if (!String.IsNullOrEmpty(source) && !source.Equals("Все"))
            {
                products = products.Where(p => p.Source == source);

            }
            if (!String.IsNullOrEmpty(origin) && !origin.Equals("Все"))
            {
                products = products.Where(p => p.Origin == origin);

            }

            if (minPrice.HasValue)
            {
                products = products.Where(x => x.Price >= minPrice);

            }

            if (maxPrice.HasValue)
            {
                products = products.Where(x => x.Price <= maxPrice);
            }

            //фильтрация

            //Сортировка
            if (sortBrandAndPrice == "по названию (Я-а,Z-A)")
            {
                products = products.OrderByDescending(s => s.Name);
            }


            if (sortBrandAndPrice == "по названию (А-я,A-Z)")
            {
                products = products.OrderBy(s => s.Name);
            }

            if (sortBrandAndPrice == "по убыванию цены")
            {
                products = products.OrderByDescending(s => s.Price);
            }

            if (sortBrandAndPrice == "по возрастанию цены")
            {
                products = products.OrderBy(s => s.Price);
            }
            //Сортировка

            List<Category> categories = db.Category.ToList();
            categories.Insert(0, new Category { Category1 = "Все", Id = 0 });

            ProductFiltering plvm = new ProductFiltering
            {
                Products = products.ToList(),

                Categories = new SelectList(categories, "Category1", "Category1"),


                SortBrandAndPrice = new SelectList(new List<string>()
                {
                    "Все",
                    "по убыванию цены",
                    "по возрастанию цены",
                    "по названию (Я-а,Z-A)",
                    "по названию (А-я,A-Z)"

                }),
                CountDiscs = new SelectList(new List<string>()
                {
                    "Все",
                    "1",
                    "2"

                }),
                Weights = new SelectList(new List<string>()
                {
                    "Все",
                    "8",
                    "9",
                    "10",
                    "11"

                }),
                Counts = new SelectList(new List<string>()
                {
                    "Все",
                    "11",
                    "14",
                    "15",
                    "16",
                    "21"
                }),
                Materials = new SelectList(new List<string>()
                {
                    "Все",
                    "Coton",
                    "Combo Plastic + metal",
                    "Glass",
                    "Metal"

                }),
                Sources = new SelectList(new List<string>()
                {
                    "Все",
                    "Diablo",
                    "Fallout",
                    "Warcraft",
                    "Witcher 3"

                }),
                Origins = new SelectList(new List<string>()
                {
                    "Все",
                    "China",
                    "Russia",
                    "USA"

                })



            };

            return View(plvm);
        }




        public ActionResult Product(int id = 0, string category = null)
        {
           

            ProductRepository pr = new ProductRepository();
         

            Product model = new Product();
            model = pr.GetProductById(id);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}