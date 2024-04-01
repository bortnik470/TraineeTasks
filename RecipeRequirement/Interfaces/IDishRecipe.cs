namespace RecipeRequirement.Interfaces
{
    public interface IDishRecipe
    {
        public string Name { get; }
        public void StartToCook();
    }
}
