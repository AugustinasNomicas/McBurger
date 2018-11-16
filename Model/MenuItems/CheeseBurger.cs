using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples.Model.MenuItems
{
    public class CheeseBurger : MenuItem
    {
        public override void GetPrerequisites()
        {
            Ingredients.Add("Bread");
            Ingredients.Add("Ham");
            Ingredients.Add("Salad");
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
