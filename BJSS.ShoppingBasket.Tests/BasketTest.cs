using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJSS.ShoppingBasketApp.Concretes;
using System.Collections.Generic;
using BJSS.ShoppingBasketApp.Interfaces;

namespace BJSS.ShoppingBasket.Tests
{
    [TestClass]
    public class BasketTest
    {
        private IDiscounts _discountEngine;
        private IOffersBuyTwoAnotherHalfPrice _offersEngine;
        private Basket _basket;

        [TestInitialize]
        public void Setup()
        {
            _discountEngine = new DiscountCalculator();
            _offersEngine = new BuyTwoSecondHalfPriceCalculator(_discountEngine);
            var purchases = new List<Purchased>();

            _basket = new Basket(_offersEngine, _discountEngine, purchases);

        }
        [TestMethod]
        public void Test_Bread_Milk_Apples_Basket_Sums_Correctly()
        {
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Bread", UnitPrice = (decimal)0.80 }, Offers = Offers.HalfPrice, Quantity = 1, PurchaseType = PurchaseType.IsOffer });
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Milk", UnitPrice = (decimal)1.30 }, Offers = Offers.None, Quantity = 1, PurchaseType = PurchaseType.IsNormal });
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Apples", UnitPrice = (decimal)1.00 }, Offers = Offers.None, Quantity = 1, DiscountPercent = (decimal)0.10, PurchaseType = PurchaseType.IsDiscount });

            var subTotal =_basket.SubTotal();
            var total = _basket.Total();
            _basket.Print();

            Assert.AreEqual((decimal)3.10, subTotal);
            Assert.AreEqual((decimal)3.00, total);
        }

        [TestMethod]
        public void Test_1LoafOfBread_2TinsOfSoup_Milk_Apples_Basket_Sums_Correctly()
        {
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Soup", UnitPrice = (decimal)0.65 }, Offers = Offers.TwoTinsSoupHalfPriceBread, Quantity = 2, PurchaseType = PurchaseType.IsOffer });
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Bread", UnitPrice = (decimal)0.80 }, Offers = Offers.HalfPrice, Quantity = 1, PurchaseType = PurchaseType.IsOffer });
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Milk", UnitPrice = (decimal)1.30 }, Offers = Offers.None, Quantity = 1, PurchaseType = PurchaseType.IsNormal });
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Apples", UnitPrice = (decimal)1.00 }, Offers = Offers.None, Quantity = 1, DiscountPercent = (decimal)0.10, PurchaseType = PurchaseType.IsDiscount });

            var subTotal = _basket.SubTotal();
            var total = _basket.Total();
            _basket.Print();

            Assert.AreEqual((decimal)4.40, subTotal);
            Assert.AreEqual((decimal)3.90, total);
        }

        [TestMethod]
        public void Test_3LoavesOfBread_5TinsOfSoup_Milk_Apples_Basket_Sums_Correctly()
        {
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Soup", UnitPrice = (decimal)0.65 }, Offers = Offers.TwoTinsSoupHalfPriceBread, Quantity = 5, PurchaseType = PurchaseType.IsOffer });
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Bread", UnitPrice = (decimal)0.80 }, Offers = Offers.HalfPrice, Quantity = 3, PurchaseType = PurchaseType.IsOffer });
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Milk", UnitPrice = (decimal)1.30 }, Offers = Offers.None, Quantity = 1, PurchaseType = PurchaseType.IsNormal });
            _basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Apples", UnitPrice = (decimal)1.00 }, Offers = Offers.None, Quantity = 1, DiscountPercent = (decimal)0.10, PurchaseType = PurchaseType.IsDiscount });

            var subTotal = _basket.SubTotal();
            var total = _basket.Total();
            _basket.Print();

            Assert.AreEqual((decimal)7.95, subTotal);
            Assert.AreEqual((decimal)7.05, total);
        }
    }
}
