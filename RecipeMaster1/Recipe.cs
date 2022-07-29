using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMaster1
{
    using Microsoft.VisualBasic.CompilerServices;
    using Newtonsoft.Json.Converters;

    public class Recipe : RecipeEntity
    {
        public List<ComponentRef> SubComponents { get; set; }

        public List<string> Process { get; set; }

        public Measure Measure { get; set; }

        public string GenerateDetailedDescription()
        {
            return string.Empty;
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

        public void PrintBasicRecipe()
        {
            Console.WriteLine(Name);
            Console.WriteLine($"Target measure: {Measure.Description}");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
            Console.WriteLine("= Ingredients:");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
            var ing = GetTotalIngredients();
            foreach (var ingr in ing.Collection.Values)
            {
                Console.WriteLine(ingr.Measure.Description + " " + ingr.Ing.Name);
            }

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
            Console.WriteLine("= components:");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
            foreach (var component in SubComponents)
            {
                var componentName = component.RemoteEntity.GetComponentName();
                var measure = component.GetMeasure();

                Console.WriteLine(measure.Description + " " + componentName);
            }
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
            Console.WriteLine("= Process:");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");

            if (Process == null)
            {
                Console.WriteLine("Process not defined!`");
            }
            else
            {
                for (int i = 0; i < Process.Count; i++)
                {
                    Console.WriteLine($"{i}) {FormatProcessLine(Process[i])}");
                }
            }
        }

        private string FormatProcessLine(string s)
        {
            foreach (var comp in SubComponents)
            {
                s = s.Replace("{" + comp.Id + "}", $"{comp.GetMeasure().Description} {comp.GetComponentName()}");
            }

            return s;
        }
    }
}
