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
            Console.WriteLine($"----------\nStart to cook a {Name}\n----------");

            Thread.CurrentThread.Name = Name;

            CookingProcesses.Wash();

            CookingProcesses.Cut();

            Thread fryThread = new Thread(() => CookingProcesses.Fry())
            {
                Name = this.Name
            };

            fryThread.Start();

            fryThread.Join();
            Console.WriteLine($"----------\n{Name} cooked\n----------");
        }
    }
}
