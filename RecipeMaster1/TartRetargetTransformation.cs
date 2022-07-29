// <copyright file="TartRetargetTransformation.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1;

public class TartRetargetTransformation : MultiplyTransformation
{
    public TartRetargetTransformation(Measure sourceMeasure, TartMeasure targetMeasure)
    {
        Multiplier = targetMeasure.Amount / sourceMeasure.Amount;
    }
}