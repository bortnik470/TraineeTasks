using Kitchen.CookingSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SoupRecipets
{
    internal class StandartSoupRecipet : IDishRecipe
    {
        public StandartSoupRecipet()
        {
            Name = "Standart soup";
        }

        public string Name {  get; set; }  

        public void StartToCook()
        {
            Thread.CurrentThread.Name = Name;

            CookingProcesses.Wash();

            CookingProcesses.Peel();

            CookingProcesses.Cut(10000);

            new Thread(() => CookingProcesses.Boil(12000)).Start();

            CookingProcesses.Mix();
        }
    }
}
