using Kitchen.CookingSimulator.CookingProcesses.Recipets;
using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.DessertRecipets
{
    internal class StandartDessertRecipe : BaseRecipe
    {
        public StandartDessertRecipe() : base("Dessert") { }

        protected override void ActionsToCook()
        {
            CookingProcesses.Peel();

            CookingProcesses.Cut();

            CookingProcesses.Mix();
        }
    }
}
