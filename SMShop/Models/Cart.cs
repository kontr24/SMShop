using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMShop.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public IEnumerable<CartItem> Lines { get { return Items; } } //свойство Lines, позволеят обратиться к содержимому корзины

        public void Add(Product product, int quantity)// метод добавления в корзину
        {
            CartItem line = Items.Where(b => b.Product.Id == product.Id).FirstOrDefault();

            if (line == null)
            {

                Items.Add(new CartItem(product, quantity) { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product, int quantity)//метод удаления из в корзины
        {
            CartItem line = Items.Where(b => b.Product.Id == product.Id).FirstOrDefault();

            line.Quantity -= quantity;

            if (line.Quantity == 0)
            {
                Items.RemoveAll(l => l.Product.Id == product.Id);
            }
        }

        public void RemoveLine1(Product product)//метод удаления из корзины
        {
            Items.RemoveAll(l => l.Product.Id == product.Id);
        }

        public void Add1(Product product, int quantity)// метод добавления в корзину
        {
            CartItem line = Items.Where(b => b.Product.Id == product.Id).FirstOrDefault();

            if (line == null)
            {

                Items.Add(new CartItem(product, quantity) { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity = quantity;
            }
        }

        public decimal ComputeTotalValue() //метод вычисления общей стоимости товара
        {
            return Items.Sum(e => e.Product.Price * e.Quantity);
        }
        public void Clear() //метод удаления корзины путём удаления всех элементов
        {
            Items.Clear();
        }
    }
}