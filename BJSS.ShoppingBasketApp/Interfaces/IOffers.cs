using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJSS.ShoppingBasketApp.Concretes;

namespace BJSS.ShoppingBasketApp.Interfaces
{

    //Responsible for calculating strictly Offers for Items related to one another or itself.
    public interface IOffers
    {
        decimal CalculateSum(Basket basket);
    }
}
