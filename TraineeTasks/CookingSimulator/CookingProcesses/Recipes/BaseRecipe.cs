using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.CookingSimulator.CookingProcesses.Recipets
{
    internal abstract class BaseRecipe : IDishRecipe
    {
        public string Name { get; private set; }

        public BaseRecipe(string name) => Name = name;

        public void StartToCook()
        {
            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} start to cook a {Name}" +
                $"\n--------------");

            ActionsToCook();

            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} finish to cook a {Name}" +
                $"\n--------------");
        }

        protected abstract void ActionsToCook(); 
    }
}
