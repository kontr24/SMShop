using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMShop.Models
{
    public class ShoppingDetails
    {

        public ShoppingDetails()
        {
        }

        public ShoppingDetails(Product model1)
        {
            Quantity = "1";
            Sum = model1.Price.ToString();
        }


        [Required(ErrorMessage = "Укажите вашу фамилию")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите ваше отчество")]
        public string Patronomic { get; set; }


        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите улицу")]
        [Display(Name = "Улица")]
        public string Line1 { get; set; }
        [Required(ErrorMessage = "Укажите дом")]
        [Display(Name = "Дом")]
        public string Line2 { get; set; }
        [Required(ErrorMessage = "Укажите квартиру")]
        [Display(Name = "Квартира")]
        public string Line3 { get; set; }

        public bool GiftWrap { get; set; }


        [Required(ErrorMessage = "Сумма")]
        public string Sum { get; set; }
        [Required(ErrorMessage = "Укажите количество товара")]
        public string Quantity { get; set; }

    }
}