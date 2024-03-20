using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SaladRecipets
{
    internal class SaladStandartRecipets : IDishRecipe
    {
        public SaladStandartRecipets()
        {
            Name = "Salad";
        }

        public string Name { get; set; }

        public void StartToCook()
        {
            Thread.CurrentThread.Name = Name;

            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            CookingProcesses.Mix();
        }
    }
}
