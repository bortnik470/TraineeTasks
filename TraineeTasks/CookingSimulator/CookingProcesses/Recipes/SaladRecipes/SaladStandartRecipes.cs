using Kitchen.CookingSimulator.CookingProcesses.Recipets;
using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SaladRecipets
{
    internal class SaladStandartRecipes : BaseRecipe
    {
        public SaladStandartRecipes() : base("Salad") { }

        protected override void ActionsToCook()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            CookingProcesses.Mix();
        }
    }
}
