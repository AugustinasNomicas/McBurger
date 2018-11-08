using System.Collections.Generic;
using SolidPrinciples.Model;

namespace SolidPrinciples.Services
{
    public class CalculatorService
    {
        public double CalculateAmount(IEnumerable<OrderItem> items)
        {
            var total = 0d;
            foreach (var item in items)
            {
                if (item.ItemId == Constants.Drink)
                {
                    var setsOfThree = item.Quantity / 3;
                    total += (item.Quantity - setsOfThree) * item.Price;
                }
                else if (item.ItemId == Constants.CheeseBurger)
                {
                    total += item.Price * item.Quantity;
                }
                else if (item.ItemId == Constants.CheeseBurgerMenu)
                {
                    total += item.Price * item.Quantity * 0.9;
                }
            }

            return total;
        }
    }
}