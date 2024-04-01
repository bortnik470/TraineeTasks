using RecipeRequirement;

namespace EastCuisine
{
    public class Falafel : BaseRecipe
    {
        public Falafel() : base("Falafel")
        {
        }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Peel();
            CookingProcesses.Cut();
            var fry = CookingProcesses.Fry();

            fry.Wait();
        }
    }
}
