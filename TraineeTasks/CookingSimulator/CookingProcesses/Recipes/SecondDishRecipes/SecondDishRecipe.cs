using Kitchen.CookingSimulator.CookingProcesses.Recipets;
using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SecondDishRecipets
{
    internal class SecondDishRecipe : BaseRecipe
    {
        public SecondDishRecipe() : base("Second Dish") { }

        protected override void ActionsToCook()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            var fry = CookingProcesses.Fry();

            fry.Wait();
        }
    }
}
