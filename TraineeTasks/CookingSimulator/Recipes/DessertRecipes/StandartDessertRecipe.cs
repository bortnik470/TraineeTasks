using RecipeRequirement;

namespace Kitchen.CookingSimulator.Recipes.DessertRecipes
{
    internal class StandartDessertRecipe : BaseRecipe
    {
        public StandartDessertRecipe() : base("Dessert") { }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Peel();

            CookingProcesses.Cut();

            CookingProcesses.Mix();
        }
    }
}
