
using SMShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMShop.Controllers
{
    public class CartController : Controller
    {

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public ViewResult Completed(Cart cart, string returnUrl, int id = 0)
        {
            ProductRepository pr = new ProductRepository();
            Product model1 = new Product();
            if (id != 0)
            {
                model1 = pr.GetProductById(id);
            }

            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl,
                Products = model1

            });
        }

        public ViewResult Checkout(Cart cart, CartItem cartItem, string returnUrl, int id = 0)
        {
            ProductRepository pr = new ProductRepository();
            Product model1 = new Product();
            if (id != 0)
            {
                model1 = pr.GetProductById(id);
            }
            var model = new About
            {
                cartIndexViewModel = new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl },
                shoppingDetails = new ShoppingDetails(model1),
                Products = model1
            };
            return View(model);
        }

        //public Cart GetCart()//Для сохранения и извлечения объектов Cart (1 способ)
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        public ActionResult AddToCart(Cart cart, Product product, string returnUrl, string categoryName, int quantity)
        {
            GamesEntities db = new GamesEntities();
            Product prod = db.Product.FirstOrDefault(b => b.Id == product.Id);
            if (prod != null)
            {
                cart.Add(prod, quantity);
                //GetCart().Add(prod, quantity);
            }
            return RedirectToAction("Index", "Home", new { returnUrl, category = categoryName });

        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, Product product, string returnUrl)
        {
            GamesEntities db = new GamesEntities();
            Product prod = db.Product.FirstOrDefault(b => b.Id == product.Id);
            if (prod != null)
            {
                cart.RemoveLine1(prod);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult AddToCart1(Cart cart, Product product, string returnUrl, string categoryName, int quantity = 1)
        {
            GamesEntities db = new GamesEntities();
            Product prod = db.Product.FirstOrDefault(b => b.Id == product.Id);
            if (prod != null)
            {
                cart.Add1(prod, quantity);
            }
            return RedirectToAction("Index", "Cart", new { returnUrl, category = categoryName });
        }

        public RedirectToRouteResult RemoveFromCart1(Cart cart, Product product, string returnUrl, int quantity = 1)
        {
            GamesEntities db = new GamesEntities();
            Product prod = db.Product.FirstOrDefault(b => b.Id == product.Id);
            if (prod != null)
            {
                cart.RemoveLine(prod, quantity);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart) // визуализирует представление, передавая в качестве данных представления текущий объект Cart
        {
            return PartialView(cart);
        }

        [HttpPost]
        // меод Checkout декорируется  атрибутом HttpPost.Это значит, что он будет вызываться для запроса POST, когда пользователь отправляет форму
        public ActionResult Checkout(Cart cart, ShoppingDetails shoppingDetails, CartItem cartItem, string returnUrl, string categoryName, int id = 0) // возвращает стандартное представление, передавая ему в качестве модели представления объект ShoppingDetails
        {
            ProductRepository pr = new ProductRepository();
            Product model1 = new Product();
            if (id != 0)
            {
                model1 = pr.GetProductById(id);

            }

            if (shoppingDetails.Surname != null && shoppingDetails.Name != null && shoppingDetails.Patronomic != null && shoppingDetails.Country != null &&
           shoppingDetails.Line1 != null && shoppingDetails.Line2 != null && shoppingDetails.Line3 != null)
            {
                Session["Surname"] = shoppingDetails.Surname.ToString();
                Session["Name"] = shoppingDetails.Name.ToString();
                Session["Patronomic"] = shoppingDetails.Patronomic.ToString();


                Session["Country"] = shoppingDetails.Country.ToString();
                Session["City"] = shoppingDetails.City.ToString();
                Session["Line1"] = shoppingDetails.Line1.ToString();
                Session["Line2"] = shoppingDetails.Line2.ToString();
                Session["Line3"] = shoppingDetails.Line3.ToString();
            }

            if (shoppingDetails.Surname != null && shoppingDetails.Name != null && shoppingDetails.Patronomic != null && shoppingDetails.Country != null &&
               shoppingDetails.Line1 != null && shoppingDetails.Line2 != null && shoppingDetails.Line3 != null && shoppingDetails.Sum != null && shoppingDetails.Quantity != null)
            {
                Session["Surname"] = shoppingDetails.Surname.ToString();
                Session["Name"] = shoppingDetails.Name.ToString();
                Session["Patronomic"] = shoppingDetails.Patronomic.ToString();
                Session["Country"] = shoppingDetails.Country.ToString();
                Session["City"] = shoppingDetails.City.ToString();
                Session["Line1"] = shoppingDetails.Line1.ToString();
                Session["Line2"] = shoppingDetails.Line2.ToString();
                Session["Line3"] = shoppingDetails.Line3.ToString();

                Session["sum"] = shoppingDetails.Sum.ToString();
                Session["quantity"] = shoppingDetails.Quantity.ToString();

            }



            if (ModelState.IsValid)
            {
                return RedirectToAction("Completed", "Cart", new { Id = id });
            }
            else
            {
                ModelState.AddModelError("", "Заполните поля!");
                var model = new About
                {
                    cartIndexViewModel = new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl },
                    shoppingDetails = new ShoppingDetails(model1),

                    Products = model1
                };
                return View(model);
            }
        }


        public RedirectToRouteResult CartClear(Cart cart, ShoppingDetails shoppingDetails, string returnUrl, string categoryName) // возвращает стандартное представление, передавая ему в качестве модели представления объект ShoppingDetails
        {
            Session["Surname"] = null;
            Session["Name"] = null;
            Session["Patronomic"] = null;
            cart.Clear();
            return RedirectToAction("Index", "Home", new { returnUrl, category = categoryName });
        }

    }
}