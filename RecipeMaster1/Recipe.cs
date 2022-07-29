using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMaster1
{
    using Microsoft.VisualBasic.CompilerServices;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public enum UnitType
    {
        Gram,
        Tbsp,
        Item,
        Volume
    }

    public abstract class RecipeEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public abstract Measure GetMeasure();

        public virtual string GetComponentName()
        {
            return Name;
        }
    }

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

    public class Measure
    {
        public UnitType UnitType { get; set; }

        public double Amount { get; set; }

        [JsonIgnore]
        public virtual string Description
        {
            get
            {
                return $"{Amount} {UnitType}";
            }
        }

        public static Measure operator+(Measure measureA, Measure measureB)
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

    public class RoundMeasure
    {
        public double Radius { get; set; }
        public double Height { get; set; }
    }

    public class ComponentRef : RecipeEntity
    {
        public string RefId { get; set; }

        public Transformation Transformation { get; set; } = null;

        [JsonIgnore]
        public RecipeEntity RemoteEntity { get; set; }

        public override Measure GetMeasure()
        {
            if (Transformation == null)
            {
                return RemoteEntity.GetMeasure();
            }
            else
            {
                return Transformation.GetMeasure(RemoteEntity.GetMeasure());
            }
        }

        public override string GetComponentName()
        {
            return RemoteEntity.Name;
        }
    }

    public class Ingredient : RecipeEntity
    {
        public UnitType UnitType { get; set; }

        public override Measure GetMeasure()
        {
            return new Measure()
            {
                UnitType = UnitType,
                Amount = 1
            };
        }
    }

    public class Transformation
    {
        public virtual Measure GetMeasure(Measure m)
        {
            throw new NotImplementedException();
        }
    }

    public class UnityTransformation : Transformation
    {
        public override Measure GetMeasure(Measure m)
        {
            return m;
        }
    }

    public class TartMeasure : Measure
    {
        public double Radius { get; set; }
        
        public double Height { get; set; }

        public TartMeasure()
        {
            UnitType = UnitType.Volume;
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

    public class RecipeMaster 
    {
        public List<Recipe> Recipes { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        private Dictionary<string, RecipeEntity> m_entities;

        public void PrintComplexRecipe(Recipe recipe)
        {
            Console.WriteLine(recipe.Name);
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==");
            Console.WriteLine("Components:");
            foreach (var component in recipe.SubComponents)
            {
                var componentName = GetComponentName(component);

                Console.WriteLine(componentName);
            }
        }

       

        private string GetMeasureName(ComponentRef component)
        {
            var desc = component.GetMeasure().Description;
            return desc;
        }

        private string GetComponentName(RecipeEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.Name))
            {
                return entity.Name;
            }

            if (entity is ComponentRef compRef)
            {
                return GetComponentName(m_entities[compRef.RefId]);
            }

            return "Unknown";
        }

        public void Build()
        {
            m_entities = new Dictionary<string, RecipeEntity>();
            
            foreach (var rcp in Recipes)
            {
                m_entities[rcp.Id] = rcp;
                Console.WriteLine("Adding " + rcp.Id);
            }

            foreach (var ing in Ingredients)
            {
                m_entities[ing.Id] = ing;
                Console.WriteLine("Adding " + ing.Id);
            }

            // Assign remote entities
            foreach (var entity in m_entities.Values)
            {
                if (entity is Recipe recipe)
                {
                    foreach (var subCom in recipe.SubComponents)
                    {
                        if (subCom is ComponentRef compRef)
                        {
                            compRef.RemoteEntity = m_entities[compRef.RefId];
                        }
                    }
                }
            }
        }
    }
}
