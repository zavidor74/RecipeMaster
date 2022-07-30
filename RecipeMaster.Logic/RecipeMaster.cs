// <copyright file="RecipeMaster.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

using RecipeMaster1.Entities;

namespace RecipeMaster1;

using Entities.Transformations;
using RecipeMasterDatabase;

public class RecipeMaster
{
    private static Lazy<RecipeMaster> _instance = new Lazy<RecipeMaster>(() => new RecipeMaster());

    public static RecipeMaster Instance => _instance.Value;

    private RecipeMaster()
    {
        
    }

    public List<Recipe> Recipes { get; set; }

    public List<Ingredient> Ingredients { get; set; }

    public List<Flavor> Flavors { get; set; }

    private Dictionary<string, RecipeEntity> m_entities;
    private RecipeMasterDatabase _db;

    public void PrintComplexRecipe(Recipe recipe)
    {
        Console.WriteLine(recipe.Name);
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
        Console.WriteLine("Components:");
        foreach (var component in recipe.SubComponents)
        {
            var componentName = GetComponentName(component);

            Console.WriteLine(componentName);
        }
    }

    public void PrintBasicRecipe(string recipeId)
    {
        var rcp = _db.Recipes.Single(r => r.Id == recipeId);

        Console.WriteLine(rcp.Name);
        Console.WriteLine($"Target measure: {rcp.Measure.Description}");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
        Console.WriteLine("= Total Flavors:");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");

        foreach (var flavorId in rcp.TotalFlavors)
        {
            Console.WriteLine(_db.Flavors.Single(flv => flv.Id == flavorId).Name);
        }

        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
        Console.WriteLine("= Ingredients:");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
        var ing = rcp.GetTotalIngredients();
        foreach (var ingr in ing.Collection.Values)
        {
            Console.WriteLine(ingr.Measure.Description + " " + ingr.Ing.Name);
        }

        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");

        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
        Console.WriteLine("= components:");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
        foreach (var component in rcp.SubComponents)
        {
            var componentName = component.RemoteEntity.GetComponentName();
            var measure = component.GetMeasure();

            Console.WriteLine(measure.Description + " " + componentName);
        }
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");

        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
        Console.WriteLine("= Process:");
        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");

        if (rcp.Process == null)
        {
            Console.WriteLine("Process not defined!`");
        }
        else
        {
            for (int i = 0; i < rcp.Process.Count; i++)
            {
                Console.WriteLine($"{i}) {FormatProcessLine(rcp, rcp.Process[i])}");
            }
        }
    }

    private string FormatProcessLine(Recipe rcp, string s)
    {
        foreach (var comp in rcp.SubComponents)
        {
            s = s.Replace("{" + comp.Id + "}", $"{comp.GetMeasure().Description} {comp.GetComponentName()}");
        }

        return s;
    }

    private string GetMeasureName(ComponentRef component)
    {
        var desc = component.GetMeasure().Description;
        return desc;
    }

    private string GetComponentName(RecipeEntity entity)
    {
        if (!string.IsNullOrEmpty(entity.Name))
        {
            return entity.Name;
        }

        if (entity is ComponentRef compRef)
        {
            return GetComponentName(m_entities[compRef.RefId]);
        }

        return "Unknown";
    }

    public void AttachDb(RecipeMasterDatabase db)
    {
        _db = db;
        m_entities = new Dictionary<string, RecipeEntity>();

        foreach (var rcp in db.Recipes)
        {
            m_entities[rcp.Id] = rcp;
            Console.WriteLine("Adding " + rcp.Id);
        }

        foreach (var ing in db.Ingredients)
        {
            m_entities[ing.Id] = ing;
            Console.WriteLine("Adding " + ing.Id);
        }

        // Assign remote entities
        foreach (var entity in m_entities.Values)
        {
            if (entity is Recipe recipe)
            {
                foreach (var subCom in recipe.SubComponents)
                {
                    if (subCom is ComponentRef compRef)
                    {
                        compRef.RemoteEntity = m_entities[compRef.RefId];
                    }
                }
            }
        }
    }

}