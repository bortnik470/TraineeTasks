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
            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} start to cook a {Name}" +
                $"\n--------------");

            CookingProcesses.Wash();

            CookingProcesses.Cut();

            Thread fryThread = new Thread(() => CookingProcesses.Fry())
            {
                Name = Thread.CurrentThread.Name
            };

            fryThread.Start();

            fryThread.Join();
            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} finish to cook a {Name}" +
                $"\n--------------");
        }
    }
}
