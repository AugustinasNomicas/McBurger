using SolidPrinciples.Model;

namespace SolidPrinciples.Services.Calculator
{
    public class BurgerRule : ICalculationRule
    {
        public bool IsMatch(string itemId) => itemId == Constants.CheeseBurger;

        public double Apply(OrderItem item)
        {
            return item.Price * item.Quantity;
        }
    }
}