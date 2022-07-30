// <copyright file="TartRetargetTransformation.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities.Transformations;

using Newtonsoft.Json;

public class TartRetargetTransformation : MultiplyTransformation
{
    private Measure m_sourceMeasure = new Measure() { Amount = 1 };
    private Measure m_targetMeasure = new Measure() { Amount = 1 };

    public Measure SourceMeasure
    {
        get => m_sourceMeasure;
        set
        {
            m_sourceMeasure = value;
            Multiplier = TargetMeasure.Amount / SourceMeasure.Amount;
        }
    }

    public Measure TargetMeasure
    {
        get => m_targetMeasure;
        set
        {
            m_targetMeasure = value;
            Multiplier = TargetMeasure.Amount / SourceMeasure.Amount;
        }
    }

    [JsonIgnore]
    public override double Multiplier { get; set; }


    public TartRetargetTransformation(Measure sourceMeasure, TartMeasure targetMeasure)
    {
        SourceMeasure = sourceMeasure;
        TargetMeasure = targetMeasure;
    }
}