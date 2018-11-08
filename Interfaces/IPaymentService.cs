using SolidPrinciples.Model;

namespace SolidPrinciples.Interfaces
{
    public interface IPaymentService
    {
        void Charge(PaymentDetails paymentDetails, Order order);
    }
}