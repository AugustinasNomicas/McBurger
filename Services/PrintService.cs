using System;
using SolidPrinciples.Hardware.Api;
using SolidPrinciples.Interfaces;
using SolidPrinciples.Model;
using SolidPrinciples.Utilities;

namespace SolidPrinciples.Services
{
    public class PrintService : IPrintService
    {
        private readonly HpPrinter _printer = new HpPrinter();
        
        public void PrintReceipt(Order order)
        {
            var customerEmail = order.CustomerEmail;
            if (string.IsNullOrEmpty(customerEmail)) return;
            
            var receipt = new Receipt
            {
                Title = "Receipt for your order placed on " + DateTime.Now,
                Body = "Your order details: \n "
            };
            foreach (var orderItem in order.Items)
            {
                receipt.Body += orderItem.Quantity + " of item " + orderItem.ItemId;
            }

            try
            {
                _printer.Print(receipt);
            }
            catch (Exception ex)
            {
                Logger.Error("Problem sending notification email", ex);
            }
        }
    }
}