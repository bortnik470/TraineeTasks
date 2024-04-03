using RecipeRequirement;
using RecipeRequirement.Interfaces;
using System.Configuration;

namespace Kitchen.CookingSimulator.UtilityClasses
{
    internal class RecipeGetter
    {
        ReflectionUtility? reflectionUtility;
        private readonly string pathToRecipes;

        public RecipeGetter(string pathToRecipes)
        {
            this.pathToRecipes = pathToRecipes;
            reflectionUtility = null;
        }

        public IEnumerable<IDishRecipe> GetAllRecipes()
        {
            var recipeFilesPath = Directory.EnumerateFiles(pathToRecipes);
            foreach (var filePath in recipeFilesPath)
            {
                if (reflectionUtility == null)
                {
                    reflectionUtility = new ReflectionUtility(filePath);
                }
                else reflectionUtility.changeAssembly(filePath);

                Type interfaceType = typeof(IDishRecipe);

                var recipeTypes = reflectionUtility.GetTypes();

                foreach (var recipeType in recipeTypes)
                {
                    if (interfaceType.IsAssignableFrom(recipeType) && !recipeType.IsInterface)
                    {
                        yield return (IDishRecipe)reflectionUtility.CreateClass(recipeType);
                    }
                }
            }
        }
    }
}