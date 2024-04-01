using System.Configuration;
using RecipeRequirement.Interfaces;
using Kitchen.CookingSimulator.HelperClasses;
using Kitchen.CookingSimulator.Recipes.SecondDishRecipes;
using Kitchen.CookingSimulator.Recipes.SideDishRecipes;
using Kitchen.CookingSimulator.Recipes.DessertRecipes;
using Kitchen.CookingSimulator.Recipes.SoupRecipes;
using Kitchen.CookingSimulator.Recipes.MainDishRecipes;
using Kitchen.CookingSimulator.Recipes.SaladRecipes;
using RecipeRequirement;
using Kitchen.CookingSimulator.UtilityClasses;


namespace TraineeTasks.CookingSimulator
{
    internal class Kitchen
    {
        public void Start()
        {
            RecipeGetter recipeGetter = new RecipeGetter(ConfigurationManager.AppSettings["pathToRecipes"]);

            if (!int.TryParse(ConfigurationManager.AppSettings["maxThreadValue"], out CookingProcesses.maxThreadValue)) 
                throw new ConfigurationErrorsException("maxThreadValue in the app.config should contain an int value");

            IEnumerable<IDishRecipe> dishRecipes = recipeGetter.GetAllRecipes().Union([
                new StandartDessertRecipe(),
                new StandartMainDishRecipes(),
                new SecondDishRecipe(),
                new SaladStandartRecipes(),
                new SideDishRecipe(),
                new StandartSoupRecipe(),
                ]);

            CustomThreadPool threadPool = new CustomThreadPool(CookingProcesses.maxThreadValue, Cook);

            foreach(var dish in dishRecipes)
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
