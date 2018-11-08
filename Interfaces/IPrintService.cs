using SolidPrinciples.Model;

namespace SolidPrinciples.Interfaces
{
    public interface IPrintService
    {
        void PrintReceipt(Order order);
    }
}