using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.MainDishRecipets
{
    internal class StandartMainDishRecipet
    {
        public static void StartToCook()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut();

            new Thread(() => CookingProcesses.Fry()).Start();
        }
    }
}
