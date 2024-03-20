using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SecondDishRecipets
{
    internal class SecondDishRecipet : IDishRecipe
    {
        public SecondDishRecipet()
        {
            Name = "Second Dish";
        }

        public string Name { get; set; }
        public void StartToCook()
        {
            Thread.CurrentThread.Name = Name;

            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            new Thread(() => CookingProcesses.Fry(12000)).Start();
        }
    }
}
