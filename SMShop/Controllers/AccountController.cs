using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMShop.Models;

namespace SysorovShop.Controllers
{
    public class AccountController : Controller
    {


        // GET: Account
        public ActionResult Index()
        {

            using (GamesEntities db = new GamesEntities())
            {
                return View(db.Users.ToList());
            }
        }

        public ActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Register(Users account)
        {

            if (ModelState.IsValid)
            {
                using (GamesEntities db = new GamesEntities())
                {
                    var us = db.Users.FirstOrDefault(x => x.Username == account.Username);

                    if (us == null)
                    {

                        db.Users.Add(account);
                        db.SaveChanges();


                        if (us == null)
                        {
                            var usr = db.Users.FirstOrDefault(u => u.Username == account.Username && u.Password == account.Password);
                            Session["UserID"] = usr.Id.ToString();
                            Session["FirstName"] = usr.FirstName.ToString();
                            Session["LastName"] = usr.LastName.ToString();
                            Session["Status"] = usr.StatusID;

                            return RedirectToAction("Index", "Home");
                        }
                    }

                    if (us != null)
                    {
                        ModelState.AddModelError("", "Логин занят!");

                    }



                }

            }
            return View();
        }




        //Вход
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            GamesEntities db = new GamesEntities();

            var usr = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (usr != null)
            {
                Session["UserID"] = usr.Id.ToString();
                Session["FirstName"] = usr.FirstName.ToString();
                Session["LastName"] = usr.LastName.ToString();
                Session["Status"] = usr.StatusID;

                return RedirectToAction("Index", "Home");
            }
            if (user.Username != null && user.Password != null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль!");
                return View();
            }

            return View();
        }

        public ActionResult AccountExit()
        {
            Session["UserId"] = null;
            Session["FirstName"] = null;
            Session["LastName"] = null;
            Session["Status"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}