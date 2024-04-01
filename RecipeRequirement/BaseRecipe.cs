using RecipeRequirement.Interfaces;

namespace RecipeRequirement
{
    public abstract class BaseRecipe : IDishRecipe
    {
        public string Name { get; private set; }

        public BaseRecipe(string name) => Name = name;

        public void StartToCook()
        {
            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} start to cook a {Name}" +
                $"\n--------------");

            ActionsToCookAsync();

            Console.WriteLine($"--------------\n" +
                $"{Thread.CurrentThread.Name} finish to cook a {Name}" +
                $"\n--------------");
        }

        protected abstract void ActionsToCookAsync();
    }
}
