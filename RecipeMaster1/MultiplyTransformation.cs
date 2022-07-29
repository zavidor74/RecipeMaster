// <copyright file="MultiplyTransformation.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1;

public class MultiplyTransformation : Transformation
{
    public double Multiplier { get; set; }

    public override Measure GetMeasure(Measure m)
    {
        return new Measure()
        {
            UnitType = m.UnitType,
            Amount = m.Amount * Multiplier
        };
    }
}