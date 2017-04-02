using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{

    public class Webshop : IWebshop
    {
        public decimal _TotalCost { get; set; }
        public IBasket _basket { get; set; }
        public IBilling _iBilling { get; set; }

        public Webshop(IBasket _basket)
        {
            this._basket = _basket;
        }

        public IBasket Basket
        {
            get
            {
                return _basket;
            }
        }

        public void Checkout(IBilling billing)
        {

            _iBilling = billing;
            var _TotalCost = _basket.TotalCost;
            billing.Pay(_TotalCost);
        }
    }
}