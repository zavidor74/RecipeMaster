// <copyright file="Measure.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities;

using Newtonsoft.Json;

public class Measure
{
    public UnitType UnitType { get; set; }

    public virtual double Amount { get; set; }

    [JsonIgnore]
    public virtual string Description
    {
        get
        {
            return $"{Amount} {UnitType}";
        }
    }

    public static Measure operator +(Measure measureA, Measure measureB)
    {
        if (measureA.UnitType == measureB.UnitType)
        {
            return new Measure()
            {
                UnitType = measureA.UnitType,
                Amount = measureA.Amount + measureB.Amount
            };
        }

        throw new Exception($"Cant add unit {measureA.UnitType} with unit {measureB.UnitType}");
    }
}