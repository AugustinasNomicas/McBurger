using System.Collections.Generic;
using SolidPrinciples.Model;

namespace SolidPrinciples.Interfaces
{
    public interface ICalculatorService
    {
        double CalculateAmount(IEnumerable<OrderItem> items);
    }

}