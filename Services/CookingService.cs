using System.Collections.Generic;
using SolidPrinciples.Interfaces;
using SolidPrinciples.Model;
using SolidPrinciples.Model.MenuItems;

namespace SolidPrinciples.Services
{
    public class CookingService : ICookingService
    {
        private readonly Dictionary<string, MenuItem> _restaurantMenu = new Dictionary<string, MenuItem>
        {
            {Constants.Drink, new Drink()},
            {Constants.CheeseBurger, new CheeseBurger()},
            {Constants.CheeseBurgerMeal, new CheeseBurgerMeal()}
        };

        public void Prepare(Order order)
        {
            foreach (var orderItem in order.Items)
            {
                var menuItem = _restaurantMenu[orderItem.ItemId];
                menuItem.SendToService();
            }
        }
    }
}