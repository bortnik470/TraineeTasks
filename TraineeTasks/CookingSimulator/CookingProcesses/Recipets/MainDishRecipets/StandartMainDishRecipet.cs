using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.MainDishRecipets
{
    internal class StandartMainDishRecipet : IDishRecipe
    {
        public StandartMainDishRecipet()
        {
            Name = "Main Dish";
        }

        public string Name { get; set; }

        public void StartToCook()
        {
            Thread.CurrentThread.Name = Name;

            CookingProcesses.Wash();

            CookingProcesses.Cut();

            new Thread(() => CookingProcesses.Fry()).Start();
        }
    }
}
