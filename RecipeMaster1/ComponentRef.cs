﻿// <copyright file="ComponentRef.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1;

using Newtonsoft.Json;

public class ComponentRef : RecipeEntity
{
    public string RefId { get; set; }

    public Transformation Transformation { get; set; } = null;

    [JsonIgnore]
    public RecipeEntity RemoteEntity { get; set; }

    public override Measure GetMeasure()
    {
        if (Transformation == null)
        {
            return RemoteEntity.GetMeasure();
        }
        else
        {
            return Transformation.GetMeasure(RemoteEntity.GetMeasure());
        }
    }

    public override string GetComponentName()
    {
        return RemoteEntity.Name;
    }
}