using HemtentaTdd2017.webshop;
using Moq;
using System;
using Xunit;

namespace Test
{
    public class WebshopTest
    {
        const decimal LEGAL_PRICE = 10;
        const int LEGAL_AMOUNT = 2;

        Product _legalProduct;
        IBasket _basket;
        IWebshop _webshop;
        Mock<IBilling> _billing;

        public WebshopTest()
        {
            _basket = new Basket();
            _webshop = new Webshop(_basket);
            _billing = new Mock<IBilling>();
            _legalProduct = new Product { Price = LEGAL_PRICE };
        }

        [Fact]
        public void AddProduct_LegalProductAndAmount_Success()
        {
            _basket.AddProduct(_legalProduct, LEGAL_AMOUNT);

            Assert.Equal(LEGAL_PRICE * LEGAL_AMOUNT, _basket.TotalCost);
        }

        [Fact]
        public void AddProduct_LegalProductButIllegalAmount_Failure()
        {
            _basket.AddProduct(_legalProduct, -1);

            Assert.Equal(0, _basket.TotalCost);
        }

        [Fact]
        public void AddProduct_ProductIsNull_Throws()
        {
            Assert.Throws<Exception>(() => _basket.AddProduct(null, LEGAL_AMOUNT));
        }

        [Fact]
        public void AddProduct_ProductPriceIsNegative_Throws()
        {
            var product = new Product { Price = decimal.MinusOne };

            Assert.Throws<Exception>(() => _basket.AddProduct(product, LEGAL_AMOUNT));
        }

        [Fact]
        public void RemoveProduct_LegalProductAndAmount_Success()
        {
            _basket.AddProduct(_legalProduct, LEGAL_AMOUNT);
            _basket.RemoveProduct(_legalProduct, LEGAL_AMOUNT);

            Assert.Equal(0, _basket.TotalCost);
        }

        [Fact]
        public void RemoveProduct_DecreaseAmount_Success()
        {
            _basket.AddProduct(_legalProduct, LEGAL_AMOUNT);
            _basket.RemoveProduct(_legalProduct, LEGAL_AMOUNT - 1);

            Assert.Equal(LEGAL_PRICE, _basket.TotalCost);
        }

        [Fact]
        public void RemoveProduct_ProductIsNull_Throws()
        {
            Assert.Throws<Exception>(() => _basket.RemoveProduct(null, LEGAL_AMOUNT));
        }

        [Fact]
        public void RemoveProduct_BasketItemIsNull_Throws()
        {
            Assert.Throws<Exception>(() => _basket.RemoveProduct(_legalProduct, 1));
        }

        [Fact]
        public void Checkout_BalanceIsGreaterOrEqualThanCost_Success()
        {
            _basket.AddProduct(_legalProduct, LEGAL_AMOUNT);

            _billing.SetupGet(x => x.Balance).Returns(_basket.TotalCost);

            _webshop.Checkout(_billing.Object);

            _billing.Verify(x => x.Pay(_basket.TotalCost), Times.Once);
        }

        [Fact]
        public void Checkout_InsufficientFunds_Throws()
        {
            _basket.AddProduct(_legalProduct, LEGAL_AMOUNT);

            _billing.SetupGet(x => x.Balance).Returns(_basket.TotalCost - 1);

            Assert.Throws<Exception>(() => _webshop.Checkout(_billing.Object));
        }

        [Fact]
        public void Checkout_BillingIsNull_Throws()
        {
            Assert.Throws<Exception>(() => _webshop.Checkout(null));
        }

        [Fact]
        public void Checkout_BasketIsNull_Throws()
        {
            var webshop = new Webshop(null);

            Assert.Throws<Exception>(() => webshop.Checkout(_billing.Object));
        }
    }
}