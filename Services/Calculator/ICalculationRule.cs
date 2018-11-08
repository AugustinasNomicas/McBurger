using SolidPrinciples.Model;

namespace SolidPrinciples.Services.Calculator
{
    public interface ICalculationRule
    {
        bool IsMatch(string itemId);
        double Apply(OrderItem item);
    }
}