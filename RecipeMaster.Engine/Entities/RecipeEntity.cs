// <copyright file="RecipeEntity.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities;

using System.Diagnostics;
using System.Text;
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
    
    public string TotalFlavorsAsString => string.Join(",", TotalFlavors);

    public abstract Measure GetMeasure();

    public virtual string FormattedProcess { get; } = string.Empty;

    public virtual string GetComponentName()
    {
        return Name;
    }
}