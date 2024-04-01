using RecipeRequirement.Interfaces;
using System.Collections;

namespace Kitchen.CookingSimulator.UtilityClasses
{
    internal class RecipeGetter
    {
        ReflectionUtility reflectionUtility;

        public RecipeGetter(string pathToAssembly)
        {
            reflectionUtility = new ReflectionUtility(pathToAssembly);
        }

        public IEnumerable<IDishRecipe> GetAllRecipes()
        {
            var recipeTypes = reflectionUtility.GetTypes();

            foreach (var recipeType in recipeTypes)
                yield return (IDishRecipe)reflectionUtility.CreateClass(recipeType);
        }
    }
}
