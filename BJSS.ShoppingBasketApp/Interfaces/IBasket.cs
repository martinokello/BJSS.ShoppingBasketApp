using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJSS.ShoppingBasketApp.Concretes;

namespace BJSS.ShoppingBasketApp.Interfaces
{

    //Responsible for Basket Functions.
    public interface IBasket
    {
        void AddToBasket(Purchased purchase);
        decimal SubTotal();
        decimal Total();
    }
}
