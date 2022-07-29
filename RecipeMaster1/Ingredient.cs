// <copyright file="Ingredient.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1;

public class Ingredient : RecipeEntity
{
    public UnitType UnitType { get; set; }

    public override Measure GetMeasure()
    {
        return new Measure()
        {
            UnitType = UnitType,
            Amount = 1
        };
    }
}