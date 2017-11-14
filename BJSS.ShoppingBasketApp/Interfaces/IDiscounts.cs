using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJSS.ShoppingBasketApp.Concretes;

namespace BJSS.ShoppingBasketApp.Interfaces
{
    //Contract Responsible for calculating strictly Discounts for Specific Purchase.
    public interface IDiscounts
    {
        decimal CalculateSum(Purchased purchases, decimal percentDiscount);
    }
}
