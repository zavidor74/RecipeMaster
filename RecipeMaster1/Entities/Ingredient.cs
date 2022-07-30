// <copyright file="Ingredient.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities;

public class Ingredient : RecipeEntity
{
    public UnitType UnitType { get; set; }

    public List<string> Flavors { get; set; }

    public override Measure GetMeasure()
    {
        return new Measure()
        {
            UnitType = UnitType,
            Amount = 1
        };
    }
}