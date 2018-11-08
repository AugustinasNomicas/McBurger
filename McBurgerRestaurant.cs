using System;
using SolidPrinciples.Hardware.Api;
using SolidPrinciples.Model;
using SolidPrinciples.Services;
using SolidPrinciples.Services.Calculator;
using SolidPrinciples.Utilities;
using SolidPrinciples.Utilities.Exceptions;

namespace SolidPrinciples
{
    public class McBurgerRestaurant
    {
        public void ExecuteOrder(Order order, PaymentDetails paymentDetails, bool printReceipt)
        {
            CalculateAmount(order);
            Charge(paymentDetails, order);
            PrepareOrder(order);

            if (printReceipt)
            {
                PrintReceipt(order);
            }
        }

        private void CalculateAmount(Order order)
        {
            var calculatorService = new CalculatorService();
            order.TotalAmount = calculatorService.CalculateAmount(order.Items);
        }

        private void AuthorizePayment(double purchaseAmount)
        {
            var paymentService = new PaymentService();
            paymentService.AuthorizePayment(purchaseAmount);
        }

        private void Charge(PaymentDetails paymentDetails, Order order)
        {
            var paymentService = new PaymentService();
            paymentService.Charge(paymentDetails, order);
        }

        private void PrintReceipt(Order order)
        {
            var printService = new PrintService();
            printService.PrintReceipt(order);
        }

        private void PrepareOrder(Order order)
        {
            var cookingService = new CookingService();
            cookingService.Prepare(order);
        }
    }
}