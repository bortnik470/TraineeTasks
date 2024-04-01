using RecipeRequirement;

namespace EastCuisine
{
    public class Pilaf : BaseRecipe
    {
        public Pilaf() : base("Pilaf")
        {
        }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Wash();
            var fry = CookingProcesses.Fry();
            CookingProcesses.Peel();
            CookingProcesses.Cut();
            fry.Wait();
        }
    }
}
