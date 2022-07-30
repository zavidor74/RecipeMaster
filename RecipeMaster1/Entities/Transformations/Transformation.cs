// <copyright file="Transformation.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities.Transformations;

public class Transformation
{
    public virtual Measure GetMeasure(Measure m)
    {
        throw new NotImplementedException();
    }
}