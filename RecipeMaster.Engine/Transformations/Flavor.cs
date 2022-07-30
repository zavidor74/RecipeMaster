// <copyright file="Flavor.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities.Transformations;

public class Flavor
{
    public string Id { get; set; }

    public string Name { get; set; }
    public List<FlavorProperty> FlavorProperties { get; set; }
}