using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SideDishRecipets
{
    internal class SideDishRecipet : IDishRecipe
    {
        public SideDishRecipet()
        {
            Name = "Side Dish";
        }

        public string Name { get; set; }

        public void StartToCook()
        {
            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} start to cook a {Name}" +
                $"\n--------------");

            CookingProcesses.Wash();

            CookingProcesses.Peel();

            CookingProcesses.Cut(10000);

            Thread boilThread = new Thread(() => CookingProcesses.Boil(12000))
            {
                Name = Thread.CurrentThread.Name
            };

            boilThread.Start();

            boilThread.Join();
            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} finish to cook a {Name}" +
                $"\n--------------");
        }
    }
}
