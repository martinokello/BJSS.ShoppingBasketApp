using BJSS.ShoppingBasketApp.Concretes;
using BJSS.ShoppingBasketApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJSS.ShoppingBasketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDiscounts discountEngine = new DiscountCalculator();
            IOffersBuyTwoAnotherHalfPrice offersEngine = new BuyTwoSecondHalfPriceCalculator(discountEngine);
            var purchases = new List<Purchased>();

            var basket = new Basket(offersEngine, discountEngine, purchases);

            basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Bread", UnitPrice = (decimal)0.80 }, Offers = Offers.HalfPrice, Quantity = 1, PurchaseType = PurchaseType.IsOffer });
            basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Milk", UnitPrice = (decimal)1.30 }, Offers = Offers.None, Quantity = 1, PurchaseType = PurchaseType.IsNormal });
            basket.AddToBasket(new Purchased { Purchase = new LineItem { Name = "Apples", UnitPrice = (decimal)1.00 }, Offers = Offers.None, Quantity = 1, DiscountPercent= (decimal)0.10, PurchaseType = PurchaseType.IsDiscount });

            basket.SubTotal();
            basket.Total();
            basket.Print();
        }
    }
}

