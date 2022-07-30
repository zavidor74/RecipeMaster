namespace RecipeMasterDatabase
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using RecipeMaster1.Entities;
    using RecipeMaster1.Entities.Transformations;

    public class RecipeMasterDatabase
    {
        public List<Recipe> Recipes { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<Flavor> Flavors { get; set; }

        public void SaveTo(string file)
        {
            var set = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };
            set.Converters.Add(new StringEnumConverter());

            File.WriteAllText(file, JsonConvert.SerializeObject(this, Formatting.None, set));
        }

        public static RecipeMasterDatabase LoadFrom(string file)
        {
            var set = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };
            set.Converters.Add(new StringEnumConverter());

            return JsonConvert.DeserializeObject<RecipeMasterDatabase>(File.ReadAllText(file), set);
        }
    }
}