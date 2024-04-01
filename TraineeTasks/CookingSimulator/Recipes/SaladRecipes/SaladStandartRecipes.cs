using RecipeRequirement;

namespace Kitchen.CookingSimulator.Recipes.SaladRecipes
{
    internal class SaladStandartRecipes : BaseRecipe
    {
        public SaladStandartRecipes() : base("Salad") { }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Wash();

            CookingProcesses.Cut(10000);

            CookingProcesses.Mix();
        }
    }
}
