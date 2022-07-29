// <copyright file="UnityTransformation.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1;

public class UnityTransformation : Transformation
{
    public override Measure GetMeasure(Measure m)
    {
        return m;
    }
}