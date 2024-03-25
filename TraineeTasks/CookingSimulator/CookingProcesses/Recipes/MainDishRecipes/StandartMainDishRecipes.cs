using Kitchen.CookingSimulator.CookingProcesses.Recipets;
using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.MainDishRecipets
{
    internal class StandartMainDishRecipes : BaseRecipe
    {
        public StandartMainDishRecipes() : base("Main Dish") { }

        protected override void ActionsToCook()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut();

            var fry = CookingProcesses.Fry();

            CookingProcesses.Mix();

            fry.Wait();
        }
    }
}
