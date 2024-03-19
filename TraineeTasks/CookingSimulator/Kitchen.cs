using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.DessertRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.MainDishRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SaladRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SecondDishRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SideDishRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SoupRecipets;
using TraineeTasks.CookingSimulator.Enums;

namespace TraineeTasks.CookingSimulator
{
    internal class Kitchen
    {
        public void Start()
        {
            Thread humanSimulationThread = new Thread(() => Cook(DishName.Salad, DishName.Soup, DishName.MainDish, DishName.SideDish, DishName.SecondDish, 
                DishName.Desert));

            humanSimulationThread.Start();

            humanSimulationThread.Join();
        }

        private void Cook(params DishName[] dishName)
        {
            foreach (var dish in dishName)
            {
                switch (dish)
                {
                    case DishName.Desert:
                        new Thread(() => StandartDessertRecipets.StartToCook()).Start();
                        break;
                    case DishName.MainDish:
                        new Thread(() => StandartMainDishRecipet.StartToCook()).Start();
                        break;
                    case DishName.SecondDish:
                        new Thread(() => SecondDishRecipet.StartToCook()).Start();
                        break;
                    case DishName.Salad:
                        new Thread(() => SaladStandartRecipets.StartToCook()).Start();
                        break;
                    case DishName.SideDish:
                        new Thread(() => SideDishRecipet.StartToCook()).Start();
                        break;
                    case DishName.Soup:
                        new Thread(() => StandartSoupRecipet.StartToCook()).Start();
                        break;
                    case DishName.None:
                        break;
                }
            }
        }
    }
}
