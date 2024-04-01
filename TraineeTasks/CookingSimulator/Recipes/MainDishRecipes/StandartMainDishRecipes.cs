using RecipeRequirement;

namespace Kitchen.CookingSimulator.Recipes.MainDishRecipes
{
    internal class StandartMainDishRecipes : BaseRecipe
    {
        public StandartMainDishRecipes() : base("Main Dish") { }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut();

            var fry = CookingProcesses.Fry();

            CookingProcesses.Mix();

            fry.Wait();
        }
    }
}
