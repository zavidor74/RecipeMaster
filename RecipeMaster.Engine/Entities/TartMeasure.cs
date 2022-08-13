// <copyright file="TartMeasure.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities;

using Newtonsoft.Json;

public class TartMeasure : Measure
{
    public double Radius { get; set; }

    public double Height { get; set; }

    public TartMeasure()
    {
        UnitType = global::RecipeMaster.Engine.Types.UnitType.Cm3;
    }

    [JsonIgnore]
    public override double Amount
    {
        get
        {
            return Radius * Radius * Math.PI * Height;
        }
        set
        {
            throw new InvalidOperationException();
        }
    }

    [JsonIgnore]
    public override string Description
    {
        get
        {
            return $"Tart of radius {Radius}cm and height of {Height}cm ";
        }
    }
}