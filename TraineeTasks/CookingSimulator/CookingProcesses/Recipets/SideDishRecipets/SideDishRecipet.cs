using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SideDishRecipets
{
    internal class SideDishRecipet
    {
        public static void StartToCook()
        {
            CookingProcesses.Wash();

            CookingProcesses.Peel();

            CookingProcesses.Cut(10000);

            new Thread(() =>CookingProcesses.Boil(12000)).Start();
        }
    }
}
