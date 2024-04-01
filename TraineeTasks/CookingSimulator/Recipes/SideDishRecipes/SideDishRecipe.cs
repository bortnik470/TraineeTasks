using RecipeRequirement;

namespace Kitchen.CookingSimulator.Recipes.SideDishRecipes
{
    internal class SideDishRecipe : BaseRecipe
    {
        public SideDishRecipe() : base("Side Dish") { }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Wash();

            CookingProcesses.Peel();

            CookingProcesses.Cut(10000);

            var boil = CookingProcesses.Boil();

            CookingProcesses.Mix();

            boil.Wait();
        }
    }
}
