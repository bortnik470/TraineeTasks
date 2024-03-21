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
            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} start to cook a {Name}" +
                $"\n--------------");

            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            Thread fryThread = new Thread(() => CookingProcesses.Fry(12000))
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
