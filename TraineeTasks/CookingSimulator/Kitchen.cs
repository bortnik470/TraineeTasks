using Kitchen.CookingSimulator.HelperClasses;
using Kitchen.CookingSimulator.Interfaces;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.DessertRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.MainDishRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SaladRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SecondDishRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SideDishRecipets;
using TraineeTasks.CookingSimulator.CookingProcesses.Recipets.SoupRecipets;


namespace TraineeTasks.CookingSimulator
{
    internal class Kitchen
    {
        public void Start()
        {
            Queue<IDishRecipe> dishesToCook = new Queue<IDishRecipe>(new List<IDishRecipe> { new StandartDessertRecipets(), new StandartMainDishRecipet(),
                new SecondDishRecipet(), new SaladStandartRecipets(), 
                new SideDishRecipet(), new StandartSoupRecipet() });

            CustomThreadPool threadPool = new CustomThreadPool(CookingProcesses.CookingProcesses.maxThreadValue, Cook);

            foreach(var dish in dishesToCook)
            {
                threadPool.Start(dish);
            }

            Console.ReadLine();
        }

        private void Cook(object dish)
        {
            ((IDishRecipe)dish).StartToCook();
        }
    }
}
