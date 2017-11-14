using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJSS.ShoppingBasketApp.Interfaces;

namespace BJSS.ShoppingBasketApp.Concretes
{
    public class OffersCalculator : IOffers
    {
        IDiscounts _discountCalculator;
        public OffersCalculator(IDiscounts discountCalculator)
        {
            _discountCalculator = discountCalculator;
        }

        //method to caluclulate sums for offers which rely on other basket items or itself
        public decimal CalculateSum(Basket basket)
        {
            int qualifyNumberOfSoups = 2;
            decimal halfPrice = (decimal)0.50;
            decimal total = 0;
            var tinnedSoups = basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals("soup") && p.Quantity > 1);
            var loavesOfBread = basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals("bread"));

            var itemsNotBread = basket.BasketContents.Where(p => !p.Purchase.Name.ToLower().Equals("bread"));

            //Item bread that relies on other items in the basket for offers with level of complexity
            if (tinnedSoups != null && loavesOfBread != null)
            {
                var countOfSoups = tinnedSoups.Quantity;
                var countOfBread = loavesOfBread.Quantity;

                if (countOfSoups >= qualifyNumberOfSoups && countOfBread >= 1)
                {
                    var numberOfBreadsEligible = countOfSoups / qualifyNumberOfSoups;

                    var fullPricedBread = countOfBread - numberOfBreadsEligible;
                    
                    if (fullPricedBread > 0)
                    {
                        //Full priced bread (no discount i.e Percent Discount 1
                        total = fullPricedBread * loavesOfBread.Purchase.UnitPrice;

                        //Bread eligible for discount
                        total+= _discountCalculator.CalculateSum(new Purchased { Quantity = numberOfBreadsEligible, Offers = Offers.HalfPrice, Purchase = new LineItem { Name = "Bread", UnitPrice = loavesOfBread.Purchase.UnitPrice } }, halfPrice);
                    }
                    else if(fullPricedBread <= 0)
                    {
                        //All bread is eligible for discount by half
                        total = _discountCalculator.CalculateSum(new Purchased { Quantity = loavesOfBread.Quantity, Offers = Offers.HalfPrice, Purchase = new LineItem { Name = "Bread", UnitPrice = loavesOfBread.Purchase.UnitPrice } }, halfPrice);
                    }
                    decimal priceSaved = loavesOfBread.Quantity * loavesOfBread.Purchase.UnitPrice - total ;
                    basket.BasketContents.SingleOrDefault(p=> p.Purchase.Name.ToLower().Equals("bread")).OfferApplies = true; basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals("bread")).PricePaid = total;
                    basket.BasketContents.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals("bread")).PriceSaved = priceSaved;
                }
            }
            else if(loavesOfBread != null)
            {
                //ordinary buying bread loaves, no Soup, therefore no offers.
                total = loavesOfBread.Quantity * loavesOfBread.Purchase.UnitPrice;
            }
            //Also extensibility can be achieved via new interface for Product that relates to itself:
            //Extending functionality beyond the test 2 for one offer e.g
            //e.g Two for one, Three for one e.t.c. Commented out as not part of test
            //Also extensibility can be achieved via new interface for Product that relates to itself

            ////Bread Summed above. Non Bread items: In basket regular Price, below
            foreach (var item in itemsNotBread)
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
        /*
        private void WorkOutTotal(Purchased item, Offers offer, ref decimal total)
        {
            var whole = item.Quantity / (int)offer;
            var mod = item.Quantity % (int)offer;
            item.PricePaid = whole * item.Purchase.UnitPrice + (mod * item.Purchase.UnitPrice);
            item.PriceSaved = item.Quantity * item.Purchase.UnitPrice - item.PricePaid;
            total += item.PricePaid;
            item.OfferApplies = true;
        }
        */
    }
}
