using System.Collections.Generic;

namespace SolidPrinciples.Model.MenuItems
{
    public class CheeseBurger : MenuItem
    {
        private readonly List<string> _ingredients = new List<string>();

        protected virtual void GetPrerequisites()
        {
            _ingredients.Add("Bread");
            _ingredients.Add("Ham");
            _ingredients.Add("Salad");
            _ingredients.Add("Fries");
        }

        protected virtual void Prepare()
        {
            TransformToBurger(_ingredients);
        }

        private void TransformToBurger(List<string> ingredientsList)
        {
            //Do some magic
        }

        public override MenuItem SendToService()
        {
            GetPrerequisites();
            Prepare();
            return base.SendToService();
        }
    }
}
