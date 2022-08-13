namespace RecipeMaster.Api
{
    using RecipeMaster1.Entities;
    using RecipeMaster1.Entities.Transformations;

    public interface IRecipeMasterServer
    {
        public List<Recipe> GetAllRecipes();
        List<Flavor> GetAllFlavors();
    }
}