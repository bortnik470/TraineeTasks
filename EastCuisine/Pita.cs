using RecipeRequirement;

namespace EastCuisine
{
    public class Pita : BaseRecipe
    {
        public Pita() : base("Pita")
        {
        }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Mix();
            var fry = CookingProcesses.Fry();

            fry.Wait();
        }
    }
}