using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMaster1.Entities
{
    using global::RecipeMaster.Engine.Types;
    using Microsoft.VisualBasic.CompilerServices;
    using Newtonsoft.Json.Converters;
    using Transformations;

    public class Recipe : RecipeEntity
    {
        public List<ComponentRef> SubComponents { get; set; }

        public List<string> Process { get; set; }

        public Measure Measure { get; set; }

        public Texture Texture { get; set; }

        public RecipeType RecipeType { get; set; }

        public string GenerateDetailedDescription()
        {
            return string.Empty;
        }

        public override HashSet<string> TotalFlavors {
            get
            {
                HashSet<string> flavors = new HashSet<string>();
                foreach (var component in SubComponents)
                {
                    if (component.TotalFlavors != null)
                    {
                        foreach (var flavor in component.TotalFlavors)
                        {
                            flavors.Add(flavor);
                        }
                    }
                }

                return flavors;
            }
        }

        public override string FormattedProcess
        {
            get
            {
                if (Process == null || !Process.Any())
                {
                    return string.Empty;
                }

                StringBuilder sb = new StringBuilder();
                foreach (var s in Process)
                {
                    string ss = s;
                    foreach (var comp in SubComponents)
                    {
                        ss = ss.Replace("{" + comp.Id + "}", $"{comp.GetComponentName()}");
                    }

                    sb.AppendLine(ss);
                }

                return sb.ToString();
            }
        }

        public override Measure GetMeasure()
        {
            return Measure;
        }

        public IngredientsCollection GetTotalIngredients()
        {
            IngredientsCollection ingredients = new IngredientsCollection();

            foreach (var component in SubComponents)
            {
                var remoteComponent = component.RemoteEntity;

                if (remoteComponent is Ingredient ing)
                {
                    ingredients.Add(ing, component.GetMeasure());
                }

                if (remoteComponent is Recipe rcp)
                {
                    var remoteIngredients = rcp.GetTotalIngredients();
                    foreach (var ingredient in remoteIngredients.Collection)
                    {
                        ingredients.Add(ingredient.Value.Ing, component.Transformation.GetMeasure(ingredient.Value.Measure));
                    }
                }
            }

            return ingredients;
        }
    }
}
