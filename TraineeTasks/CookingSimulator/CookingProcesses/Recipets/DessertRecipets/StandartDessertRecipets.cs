using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.DessertRecipets
{
    internal class StandartDessertRecipets : IDishRecipe
    {
        public StandartDessertRecipets()
        {
            Name = "Dessert";
        }

        public string Name { get; set; }
        public void StartToCook()
        {
            Thread.CurrentThread.Name = Name;

            CookingProcesses.Peel();

            CookingProcesses.Cut();

            CookingProcesses.Mix();
        }
    }
}
