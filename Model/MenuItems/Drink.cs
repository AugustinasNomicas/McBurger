using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples.Model.MenuItems
{
    public class Drink : MenuItem
    {
        public override MenuItem SendToService()
        {
            Ingredients.Add("Coca-cola");
            return base.SendToService();
        }
    }
}
