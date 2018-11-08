using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples.Model
{
    public class MenuItem
    {
        public virtual MenuItem SendToService()
        {
            return this;
        }
    }
}
