using RecipeRequirement;

namespace Kitchen.CookingSimulator.Recipes.SecondDishRecipes
{
    internal class SecondDishRecipe : BaseRecipe
    {
        public SecondDishRecipe() : base("Second Dish") { }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            var fry = CookingProcesses.Fry();

            fry.Wait();
        }
    }
}
