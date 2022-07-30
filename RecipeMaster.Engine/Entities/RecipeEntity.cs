// <copyright file="RecipeEntity.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities;

using Newtonsoft.Json;
using Transformations;

public abstract class RecipeEntity
{
    public string Id { get; set; }

    public virtual string Name { get; set; }

    public string Description { get; set; }

    [JsonIgnore]
    public abstract HashSet<string> TotalFlavors { get; }

    [JsonIgnore] public virtual string FinalName => Name;

    public abstract Measure GetMeasure();

    public virtual string GetComponentName()
    {
        return Name;
    }
}