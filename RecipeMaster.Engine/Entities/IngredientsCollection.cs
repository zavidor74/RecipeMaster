// <copyright file="IngredientsCollection.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace RecipeMaster1.Entities;

public class IngredientsCollection
{
    private Dictionary<string, (Ingredient Ing, Measure Measure)> m_ingList = new Dictionary<string, (Ingredient, Measure)>();

    public Dictionary<string, (Ingredient Ing, Measure Measure)> Collection => m_ingList;

    public void Add(Ingredient ing, Measure m)
    {
        if (m_ingList.ContainsKey(ing.Id))
        {
            m_ingList[ing.Id] = (ing, m + m_ingList[ing.Id].Measure);
        }
        else
        {
            m_ingList[ing.Id] = (ing, m);
        }
    }
}