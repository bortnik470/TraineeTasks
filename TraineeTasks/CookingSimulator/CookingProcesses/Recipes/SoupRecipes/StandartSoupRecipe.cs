using Kitchen.CookingSimulator.CookingProcesses.Recipets;
using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SoupRecipets
{
    internal class StandartSoupRecipe : BaseRecipe
    {
        public StandartSoupRecipe() : base("Standart soup") { }

        protected override void ActionsToCook()
        {
            CookingProcesses.Wash();

            CookingProcesses.Peel();

            CookingProcesses.Cut(10000);

            var boil =  CookingProcesses.Boil();

            CookingProcesses.Mix();

            boil.Wait();
        }
    }
}
