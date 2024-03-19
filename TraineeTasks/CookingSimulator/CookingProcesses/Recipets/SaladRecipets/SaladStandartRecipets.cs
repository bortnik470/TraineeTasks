using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SaladRecipets
{
    internal class SaladStandartRecipets
    {
        public static void StartToCook()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            CookingProcesses.Mix();
        }
    }
}
