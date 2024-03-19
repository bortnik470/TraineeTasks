using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SecondDishRecipets
{
    internal class SecondDishRecipet
    {
        public static void StartToCook()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            new Thread(() => CookingProcesses.Fry(12000)).Start();
        }
    }
}
