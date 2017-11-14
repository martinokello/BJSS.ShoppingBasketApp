using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJSS.ShoppingBasketApp.Interfaces;

namespace BJSS.ShoppingBasketApp.Concretes
{
    public class DiscountCalculator : IDiscounts
    {
        //method to calculate discounts on a specific item
        public decimal CalculateSum(Purchased purchases, decimal percentDiscount)
        {
            if (percentDiscount > 1) throw new PercentDiscountException("PercentageDiscount not Valid!");
            if (percentDiscount > (decimal)0.00)
            {
                purchases.OfferApplies = true;
                purchases.PricePaid = purchases.Quantity * purchases.Purchase.UnitPrice -
                   (purchases.Quantity * purchases.Purchase.UnitPrice * percentDiscount);
                purchases.PriceSaved = (purchases.Quantity * purchases.Purchase.UnitPrice * percentDiscount);
            }
            return purchases.Quantity*purchases.Purchase.UnitPrice -
                   (purchases.Quantity*purchases.Purchase.UnitPrice*percentDiscount);
        }
    }

    public class PercentDiscountException:Exception
    {
        public PercentDiscountException(string message): base(message)
        {
            
        }
    }
}
