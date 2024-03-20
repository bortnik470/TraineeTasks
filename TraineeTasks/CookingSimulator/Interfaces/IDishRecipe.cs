using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.CookingSimulator.Interfaces
{
    internal interface IDishRecipe
    {
        public string Name { get; }
        public void StartToCook();
    }
}
