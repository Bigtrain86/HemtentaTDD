using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    public class Basket : IBasket
    {
        Product _product;
        int amountOfProducts;


        public decimal TotalCost
        {
            get
            {
                return _product.Price * amountOfProducts;
            }
        }

        public void AddProduct(Product p, int amount)
        {
            if (p == null || amount <= 0)
            {
                throw new Exception();
            }

            _product = p;
            amountOfProducts += amount;
        }

        public void RemoveProduct(Product p, int amount)
        {
            if (p == null || amount <= 0)
            {
                throw new Exception();
            }
            if (amount > amountOfProducts)

            {
                throw new Exception();
            }

            amountOfProducts -= amount;
            _product = p;
        }


    }
}
