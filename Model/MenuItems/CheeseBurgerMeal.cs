using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples.Model.MenuItems
{
    public class CheeseBurgerMeal : CheeseBurger
    {
        private readonly Drink _drink = new Drink();

        protected override void GetPrerequisites()
        {
            base.GetPrerequisites();

            Ingredients.Add("Fries");
        }

        public override MenuItem SendToService()
        {
            GetPrerequisites();
            Prepare();
            _drink.SendToService();
            
            Ingredients.AddRange(_drink.Ingredients);
            
            base.SendToService();
            return this;
        }
    }
}