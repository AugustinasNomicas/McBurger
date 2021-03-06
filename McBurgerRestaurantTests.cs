﻿using System.Linq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using SolidPrinciples.Model;
using SolidPrinciples.Utilities.Exceptions;

namespace SolidPrinciples
{
    [TestFixture]
    public class McBurgerRestaurantTests
    {
        private Fixture fixture;
        private McBurgerRestaurant restaurant;

        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture();
            restaurant = new McBurgerRestaurant();
        }
        
        [Test]
        public void Should_contain_cheeseBurger_ingredients()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurger)
                .CreateMany(1);
            var order = fixture.Build<Order>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = restaurant.ExecuteOrder(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().MenuItem; 
            
            Assert.True(preparedItem.IsPrepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Bread", preparedItem.Ingredients);
            Assert.Contains("Ham", preparedItem.Ingredients);
            Assert.Contains("Salad", preparedItem.Ingredients);
        }
        
        [Test]
        public void Should_contain_cheeseBurgerMeal_ingredients()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurgerMeal)
                .CreateMany(1);
            var order = fixture.Build<Order>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = restaurant.ExecuteOrder(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().MenuItem; 
            
            Assert.True(preparedItem.IsPrepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Bread", preparedItem.Ingredients);
            Assert.Contains("Ham", preparedItem.Ingredients);
            Assert.Contains("Salad", preparedItem.Ingredients);
            
            Assert.Contains("Fries", preparedItem.Ingredients);
            Assert.Contains("Coca-cola", preparedItem.Ingredients);
        }
        
        [Test]
        public void Should_drink_be_sent_to_service()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.Drink)
                .CreateMany(1);
            var order = fixture.Build<Order>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            var executedOrder = restaurant.ExecuteOrder(order, fakePaymentDetails, fakePrintReceipt);

            
            var preparedItem = executedOrder.Items.Single().MenuItem; 
            
            Assert.False(preparedItem.IsPrepared);
            Assert.True(preparedItem.IsSentToService);
            
            Assert.Contains("Coca-cola", preparedItem.Ingredients);
        }        

        [Test]
        public void Should_execute_order_when_payment_is_with_contact_and_print_receipt()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurgerMeal)
                .CreateMany(1);
            var order = fixture.Build<Order>()
                .With(o => o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = true;

            restaurant.ExecuteOrder(order, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_execute_order_when_order_is_3_drinks_and_payment_is_contactless_and_print_receipt()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 5)
                .With(c => c.ItemId, Constants.Drink)
                .CreateMany(3);
            var fakeOrder = fixture.Build<Order>()
                .With(c => c.Items, orderItems)
                .Create();

            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();
            var fakePrintReceipt = true;

            restaurant.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_throw_exception_when_order_is_5_burgers_and_payment_is_with_contactless()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 5)
                .With(c => c.Price, 5)
                .With(c => c.ItemId, Constants.CheeseBurger)
                .CreateMany(1);

            var fakeOrder = fixture.Build<Order>()
                .With(c => c.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();

            var fakePrintReceipt = false;

            restaurant.Invoking(y => y.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt))
                .Should().Throw<UnAuthorizedContactLessPayment>()
                .WithMessage("Amount is too big");
        }

        [Test]
        public void Should_throw_NotValidPaymentException_when_Payment_Method_is_mobile()
        {
            var fakeOrder = fixture.Build<Order>()
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.Mobile)
                .Create();
            var fakePrintReceipt = true;

            restaurant.Invoking(y => y.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt))
                .Should().Throw<NotValidPaymentException>()
                .WithMessage("Can not charge customer");
        }

        [Test]
        public void Should_execute_order_when_Payment_with_contact_but_without_print_receipt()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurgerMeal)
                .CreateMany(1);
            var fakeOrder = fixture.Build<Order>()
                .With(o=>o.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactCreditCard)
                .Create();
            var fakePrintReceipt = false;

            restaurant.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }

        [Test]
        public void Should_execute_order_when_order_is_1_menu_and_Payment_with_contactless_but_without_print_receipt()
        {
            var orderItems = fixture.Build<OrderItem>()
                .With(c => c.Quantity, 1)
                .With(c => c.Price, 10)
                .With(c => c.ItemId, Constants.CheeseBurgerMeal)
                .CreateMany(1);
            var fakeOrder = fixture.Build<Order>()
                .With(c => c.Items, orderItems)
                .Create();
            var fakePaymentDetails = fixture.Build<PaymentDetails>()
                .With(c => c.PaymentMethod, PaymentMethod.ContactLessCreditCard)
                .Create();
            var fakePrintReceipt = false;

            restaurant.ExecuteOrder(fakeOrder, fakePaymentDetails, fakePrintReceipt);
        }
    }
}
