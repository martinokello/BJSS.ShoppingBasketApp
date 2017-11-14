using BJSS.ShoppingBasketApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJSS.ShoppingBasketApp.Concretes
{
    public class BuyTwoSecondHalfPriceCalculator : IOffersBuyTwoAnotherHalfPrice
    {
        IDiscounts _discountCalculator;
        public BuyTwoSecondHalfPriceCalculator(IDiscounts discountCalculator)
        {
            _discountCalculator = discountCalculator;
        }
        public decimal CalculateSum(Basket basket, Purchased purchase, Purchased SecondItemHalfPrice)
        {
            if(purchase == null)
            {
                throw new Exception("The first Item cannot be empty or null");
            }
            
            int qualifyNumberOf2ndItems = 2;
            decimal halfPrice = (decimal)0.50;
            decimal total = 0;
            Purchased secondItems = null;
            if(SecondItemHalfPrice != null)
                secondItems = basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals(SecondItemHalfPrice.Purchase.Name.ToLower()) && p.Quantity > 1);
            var firstItems = basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals(purchase.Purchase.Name.ToLower()));

            var itemsInBasketNotFirst = basket.BasketContents.Where(p => !p.Purchase.Name.ToLower().Equals(purchase.Purchase.Name.ToLower()));

            //Item bread that relies on other items in the basket for offers with level of complexity
            if (secondItems != null && firstItems != null)
            {
                var countOfSecondItems = secondItems.Quantity;
                var countOfFirstItems = firstItems.Quantity;

                if (countOfSecondItems >= qualifyNumberOf2ndItems && countOfFirstItems >= 1)
                {
                    var actualNumberOfFirstItemsEligible = countOfSecondItems / qualifyNumberOf2ndItems;

                    var actualFullPricedFirstItems = countOfFirstItems - actualNumberOfFirstItemsEligible;

                    if (actualFullPricedFirstItems > 0)
                    {
                        //Full priced bread (no discount i.e Percent Discount 1
                        total = actualFullPricedFirstItems * firstItems.Purchase.UnitPrice;

                        //Bread eligible for discount
                        total += _discountCalculator.CalculateSum(new Purchased { Quantity = actualNumberOfFirstItemsEligible, Offers = Offers.HalfPrice, Purchase = new LineItem { Name = purchase.Purchase.Name.ToLower(), UnitPrice = firstItems.Purchase.UnitPrice } }, halfPrice);
                    }
                    else if (actualFullPricedFirstItems <= 0)
                    {
                        //All bread is eligible for discount by half
                        total = _discountCalculator.CalculateSum(new Purchased { Quantity = firstItems.Quantity, Offers = Offers.HalfPrice, Purchase = new LineItem { Name = purchase.Purchase.Name.ToLower(), UnitPrice = firstItems.Purchase.UnitPrice } }, halfPrice);
                    }
                    decimal priceSaved = firstItems.Quantity * firstItems.Purchase.UnitPrice - total;
                    basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals(purchase.Purchase.Name.ToLower())).OfferApplies = true;
                    basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals(purchase.Purchase.Name.ToLower())).PricePaid = total;
                    basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals(purchase.Purchase.Name.ToLower())).PriceSaved = priceSaved;
                }
            }
            else if (firstItems != null)
            {
                //ordinary buying bread loaves, no Soup, therefore no offers.
                total = firstItems.Quantity * firstItems.Purchase.UnitPrice;
            }            //Also extensibility can be achieved via new interface for Product that relates to itself:
            //Extending functionality beyond the test 2 for one offer e.g
            //e.g Two for one, Three for one e.t.c. Commented out as not part of test
            //Also extensibility can be achieved via new interface for Product that relates to itself

            ////Bread Summed above. Non Bread items: In basket regular Price, below
            foreach (var item in itemsInBasketNotFirst)
            {

                if (item.PurchaseType != PurchaseType.IsDiscount)
                    total += item.Quantity * item.Purchase.UnitPrice;
                /*Items that rely on itself can go into a separate interface
                switch (item.Offers)
                {
                    case Offers.TwoForOne:
                        WorkOutTotal(item, Offers.TwoForOne, ref total);
                        break;
                    case Offers.ThreeForOne:
                        WorkOutTotal(item, Offers.ThreeForOne, ref total);
                        break;
                    case Offers.FourForOne:
                        WorkOutTotal(item, Offers.FourForOne, ref total);
                        break;
                    default:if (item.PurchaseType != PurchaseType.IsDiscount)
                        total += item.Quantity * item.Purchase.UnitPrice;
                    break;
                }
                */
            }
            return total;
        }
    }
}
