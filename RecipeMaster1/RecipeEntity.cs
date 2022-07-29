// <copyright file="RecipeEntity.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1;

public abstract class RecipeEntity
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public abstract Measure GetMeasure();

    public virtual string GetComponentName()
    {
        return Name;
    }
}