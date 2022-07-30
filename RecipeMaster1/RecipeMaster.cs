// <copyright file="RecipeMaster.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

using RecipeMaster1.Entities;

namespace RecipeMaster1;

public class RecipeMaster 
{
    public List<Recipe> Recipes { get; set; }

    public List<Ingredient> Ingredients { get; set; }

    private Dictionary<string, RecipeEntity> m_entities;

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

    public void Build()
    {
        m_entities = new Dictionary<string, RecipeEntity>();
            
        foreach (var rcp in Recipes)
        {
            m_entities[rcp.Id] = rcp;
            Console.WriteLine("Adding " + rcp.Id);
        }

        foreach (var ing in Ingredients)
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