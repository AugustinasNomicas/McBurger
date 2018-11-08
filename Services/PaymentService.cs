using SolidPrinciples.Model;
using SolidPrinciples.Utilities;
using SolidPrinciples.Utilities.Exceptions;

namespace SolidPrinciples.Services
{
    public class PaymentService
    {
        public void Charge(PaymentDetails paymentDetails, Order order)
        {
            switch (paymentDetails.PaymentMethod)
            {
                case PaymentMethod.ContactCreditCard:
                    ChargeCard(paymentDetails, order);
                    break;
                case PaymentMethod.ContactLessCreditCard:
                    AuthorizePayment(order.TotalAmount);
                    ChargeCard(paymentDetails, order);
                    break;
                default:
                    throw new NotValidPaymentException("Can not charge customer");
            }
        }
        
        public void AuthorizePayment(double totalAmount)
        {
            if (totalAmount > 20) throw new UnAuthorizedContactLessPayment("Amount is too big");
            Logger.Info(string.Format("Payment for {0} has been authorized", totalAmount));
        }

        private void ChargeCard(PaymentDetails paymentDetails, Order order)
        {
            using (var ccMachine = new CreditCardMachine())
            {
                try
                {
                    ccMachine.CardNumber = paymentDetails.CreditCardNumber;
                    ccMachine.ExpiresMonth = paymentDetails.ExpiresMonth;
                    ccMachine.ExpiresYear = paymentDetails.ExpiresYear;
                    ccMachine.NameOnCard = paymentDetails.CardholderName;
                    ccMachine.AmountToCharge = order.TotalAmount;

                    ccMachine.Charge();
                }
                catch (RejectedCardException ex)
                {
                    throw new OrderException("The card gateway rejected the card.", ex);
                }
            }
        }
    }
}