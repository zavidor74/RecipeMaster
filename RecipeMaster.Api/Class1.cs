namespace RecipeMaster.Api
{
    using RecipeMaster1.Entities;

    public interface IRecipeMasterServer
    {
        public List<Recipe> GetAllRecipes();
    }
}