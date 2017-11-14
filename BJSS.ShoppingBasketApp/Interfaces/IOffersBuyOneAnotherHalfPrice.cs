using BJSS.ShoppingBasketApp.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJSS.ShoppingBasketApp.Interfaces
{
    public interface IOffersBuyTwoAnotherHalfPrice
    {
        decimal CalculateSum(Basket basket, Purchased purchase, Purchased SecondItemHalfPrice);
    }
}
