using RecipeRequirement;

namespace Kitchen.CookingSimulator.Recipes.SoupRecipes
{
    internal class StandartSoupRecipe : BaseRecipe
    {
        public StandartSoupRecipe() : base("Standart soup") { }

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
