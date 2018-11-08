using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples.Model.MenuItems
{
    public class CheeseBurgerMeal : CheeseBurger
    {
        private Drink _drink;
        private List<MenuItem> _cheeseBurgerMealItems;

        protected override void GetPrerequisites()
        {
            base.GetPrerequisites();
            _drink = new Drink();
            _cheeseBurgerMealItems = new List<MenuItem>(2);
        }

        protected override void Prepare()
        {
            base.Prepare();            
            _cheeseBurgerMealItems.Add(_drink.SendToService());
        }

        public override MenuItem SendToService()
        {
            GetPrerequisites();
            Prepare();
            _cheeseBurgerMealItems.Add(base.SendToService());
            return this;
        }
    }
}
