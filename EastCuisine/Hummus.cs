using RecipeRequirement;

namespace EastCuisine
{
    public class Hummus : BaseRecipe
    {
        public Hummus() : base("Hummus")
        {
        }

        protected override void ActionsToCookAsync()
        {
            CookingProcesses.Peel();
            CookingProcesses.Wash();

            var boil = CookingProcesses.Boil();

            boil.Wait();
        }
    }
}
