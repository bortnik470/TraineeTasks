using RecipeRequirement;

namespace EastCuisine
{
    public class Knafeh : BaseRecipe
    {
        public Knafeh() : base("Knafeh")
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
