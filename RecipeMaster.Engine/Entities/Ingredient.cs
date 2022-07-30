// <copyright file="Ingredient.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

using RecipeMaster.Engine.Types;
using RecipeMaster1.Entities.Transformations;

namespace RecipeMaster1.Entities;

public class Ingredient : RecipeEntity
{
    public UnitType UnitType { get; set; }

    public HashSet<string> Flavors { get; set; }

    public override HashSet<string> TotalFlavors => Flavors;

    public override Measure GetMeasure()
    {
        return new Measure()
        {
            UnitType = UnitType,
            Amount = 1
        };
    }
}