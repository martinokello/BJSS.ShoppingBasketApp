using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJSS.ShoppingBasketApp.Interfaces;

namespace BJSS.ShoppingBasketApp.Concretes
{
    public class Basket : IBasket,IPrinter
    {
        IOffersBuyTwoAnotherHalfPrice _offersCalculator;
        IDiscounts _discountCalculator;

        private IList<Purchased> _purchases;

        public Basket(IOffersBuyTwoAnotherHalfPrice offersCalculator, IDiscounts discountCalculator, IList<Purchased> purchases)
        {
            _purchases = purchases??new List<Purchased>();
            _offersCalculator = offersCalculator;
            _discountCalculator = discountCalculator;
        }

        public IList<Purchased> BasketContents {
            get { return _purchases; }
        }
        public void AddToBasket(Purchased purchase)
        {
            _purchases.Add(purchase);
        }

        public void Print()
        {
            var builder = new StringBuilder();
            builder.Append(string.Format("SubTotal: £{0}\n", SubTotal()));
            var offerApplies = _purchases.Where(p => p.OfferApplies);
            foreach(var offered in offerApplies)
            {
                switch (offered.Offers)
                {
                    case Offers.HalfPrice:
                        builder.Append(string.Format("{0} Half Price: £-{1}\n",offered.Purchase.Name, offered.PriceSaved));
                    break;
                    case Offers.TwoTinsSoupHalfPriceBread:
                        builder.Append(string.Format("{0} Two tins Of Soup Half Price Bread:  £-{1}\n",offered.Purchase.Name, offered.PriceSaved));
                    break;
                    //Can Implement others: but not required.
                }
                if(offered.PurchaseType== PurchaseType.IsDiscount)
                {
                    builder.Append(string.Format("{0} {1}% discount : £-{2}\n", offered.Purchase.Name,offered.PriceSaved/(offered.Quantity*offered.Purchase.UnitPrice) * 100, offered.PriceSaved));
                }
            }
            builder.Append(string.Format("Total Price £{0}\n", Total()));

            Console.Out.Write(builder.ToString());
        }

        public decimal SubTotal()
        {
            return _purchases.Sum(p => p.Quantity * p.Purchase.UnitPrice);
        }

        public decimal Total()
        {
            var resultOffersTotal = _offersCalculator.CalculateSum(this, _purchases.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals("bread")), _purchases.SingleOrDefault(p => p.Purchase.Name.ToLower().Equals("soup")));
            var resultDiscountsTotal = _purchases.Where(q=> q.PurchaseType == PurchaseType.IsDiscount).Sum(p => _discountCalculator.CalculateSum(p, p.DiscountPercent));

            return resultDiscountsTotal + resultOffersTotal;
        }
    }
}
