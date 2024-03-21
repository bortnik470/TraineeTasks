using Kitchen.CookingSimulator.HelperClasses;
using Kitchen.CookingSimulator.Interfaces;
using System.Configuration;
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
            if (!int.TryParse(ConfigurationManager.AppSettings["maxThreadValue"], out CookingProcesses.CookingProcesses.maxThreadValue)) throw new Exception();

            Queue<IDishRecipe> dishesToCook = new Queue<IDishRecipe>( [ new StandartDessertRecipets(), new StandartMainDishRecipet(),
                new SecondDishRecipet(), new SaladStandartRecipets(), 
                new SideDishRecipet(), new StandartSoupRecipet() ] );

            CustomThreadPool threadPool = new CustomThreadPool(CookingProcesses.CookingProcesses.maxThreadValue, Cook);

            foreach(var dish in dishesToCook)
            {
                threadPool.AddDish(dish);
            }

            if(Console.ReadLine() != null) threadPool.EndWork();
        }

        private void Cook(object dish)
        {
            ((IDishRecipe)dish).StartToCook();
        }
    }
}
