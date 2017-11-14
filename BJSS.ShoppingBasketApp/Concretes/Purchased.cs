using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJSS.ShoppingBasketApp.Concretes
{
    //Purchased Item in the Basket. Could have more than one quantity
    public class Purchased
    {
        public LineItem Purchase { get; set; }
        public int Quantity { get; set; }

        public Offers Offers { get;set;}

        public bool OfferApplies { get; set; }
        public decimal PricePaid { get; set; }

        public decimal PriceSaved { get; set; }

        public decimal DiscountPercent { get; set; }

        public PurchaseType PurchaseType { get; set; }
    }

    //PurchaseType IsCount or IsOffer or IsNormal Flags
    public enum PurchaseType
    {
        IsDiscount=1,
        IsOffer=2,
        IsNormal=3
    }
    //Purchase Offer Types specific e.g. 2 for 1,  (Offers relating items to one another or itself in Basket)
    public enum Offers
    {
        TwoTinsSoupHalfPriceBread = 1,
        TwoForOne = 2,
        ThreeForOne = 3,
        FourForOne = 4,
        HalfPrice = 5,
        None=0
    }

}
