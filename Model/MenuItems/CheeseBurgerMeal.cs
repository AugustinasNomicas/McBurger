using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples.Model.MenuItems
{
    public class CheeseBurgerMeal : MenuItem
    {
        public override void GetPrerequisites()
        {
            // Burger
            Ingredients.Add("Bread");
            Ingredients.Add("Ham");
            Ingredients.Add("Salad");
            
            // Fries
            Ingredients.Add("Fries");
            
            // Drink
            Ingredients.Add("Coca-cola");
        }

        public override void Prepare()
        {
            IsPrepared = true;
        }

        public override void SendToService()
        {
            IsSentToService = true;
        }
    }
}
