// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RecipeMaster1;

Console.WriteLine("Hello, World!");

Ingredient flour = new Ingredient
{
    Id = "14d1f940-5476-45ff-8e8a-4893e068affa",
    Name = "Flour",
    UnitType = UnitType.Gram
};

Ingredient sugarPowder = new Ingredient
{
    Id = "015ecab0-bf5e-4a74-ba7d-e304cb663ceb",
    Name = "Sugar Powder",
    UnitType = UnitType.Gram
};

Ingredient salt = new Ingredient
{
    Id = "804785cb-23a3-41ed-a82a-1dcefbbc066e",
    Name = "Salt",
    UnitType = UnitType.Tbsp
};

Ingredient butter = new Ingredient
{
    Id = "43d90ccd-4b31-45a1-953f-ab93004afee6",
    Name = "Butter",
    UnitType = UnitType.Gram
};

Ingredient egg = new Ingredient
{
    Id = "baca1e9b-2807-4b01-a845-58b974488cc7",
    Name = "Large Egg",
    UnitType = UnitType.Item
};

Ingredient lemonJuice = new Ingredient
{
    Id = "824f1995-6e11-4459-ab44-8596800e0fb5",
    Name = "Lemon Juice",
    UnitType = UnitType.Gram
};

Recipe pastryDough = new Recipe()
{
    Id = "623d450a-2327-4405-b290-4956e7f2eecb",
    Name = "Pastry dough",
    Measure = new Measure()
    {
        UnitType = UnitType.Volume,
        Amount = 400
    },
    SubComponents = new List<ComponentRef>
    {
        new ComponentRef
        {
            Id = "2895c00b-e7dd-4cab-9446-5c2ec0c25a8c",
            RefId = flour.Id,
            Transformation = new MultiplyTransformation
            {
                Multiplier = 300
            }
        },
        new ComponentRef
        {
            Id = "772315b6-b1fb-49d4-99df-17b5faeb397a",
            RefId = sugarPowder.Id,
            Transformation = new MultiplyTransformation
            {
                Multiplier = 100
            }
        },
        new ComponentRef
        {
            Id = "fbf979c5-79d8-4bf4-9b9d-efb28b487ce2",
            RefId = salt.Id,
            Transformation = new MultiplyTransformation
            {
                Multiplier = 0.25
            }
        },
        new ComponentRef
        {
            Id = "57869793-1858-4bb2-893d-b0a50d989806",
            RefId = butter.Id,
            Transformation = new MultiplyTransformation
            {
                Multiplier = 200
            }
        },
        new ComponentRef
        {
            Id = "68d39763-635e-4a4f-bec5-c945c8c2fe52",
            RefId = egg.Id
        },
    },
    Process = new List<string>()
    {
        "Put {2895c00b-e7dd-4cab-9446-5c2ec0c25a8c}, {772315b6-b1fb-49d4-99df-17b5faeb397a}, {fbf979c5-79d8-4bf4-9b9d-efb28b487ce2} in a food processor and mix a little until homogenous",
        "Add {57869793-1858-4bb2-893d-b0a50d989806} and mix in pulses until you get a sand like texture",
        "Add {68d39763-635e-4a4f-bec5-c945c8c2fe52} and mix until getting a ball of dough. Don't mix too much.",
        "Take out of the processor, wrap with plastic wrap and save in the fridge for 1-2 hours before use"
    }
};

Recipe lemonCurd = new Recipe()
{
    Id = "f4e3d0b3-b85b-4b10-93bc-c2a6a5758395",
    Name = "Lemon Curd",
    Measure = new Measure()
    {
        UnitType = UnitType.Volume,
        Amount = 200
    },
    SubComponents = new List<ComponentRef>
    {
        new ComponentRef
        {
            Id = "136302af-eb29-499f-aae1-6ad6eae04942",
            RefId = lemonJuice.Id,
            Transformation = new MultiplyTransformation
            {
                Multiplier = 100
            }
        },
    }
};

Recipe lemonTartRecipe = new Recipe();
lemonTartRecipe.Name = "Lemon Tart";
lemonTartRecipe.Id = "ed2ff130-27b2-46e5-99b3-8cfae5d521b4";
lemonTartRecipe.Measure = new TartMeasure
{
    Radius = 11,
    Height = 2.5
};


Recipe lemonTartRecipe2 = new Recipe();
lemonTartRecipe2.Name = "Lemon Tart2";
lemonTartRecipe2.Id = "57c2b8cc-756d-4d02-9a3f-dea664fc5165";
lemonTartRecipe2.Measure = new TartMeasure
{
    Radius = 22,
    Height = 3
};

lemonTartRecipe2.SubComponents = new List<ComponentRef>()
{
    new ComponentRef()
    {
        Id = "43454f1b-b030-46c1-b1c9-02109857794e",
        RefId = lemonTartRecipe.Id,
        Transformation = new TartRetargetTransformation(lemonTartRecipe.Measure as TartMeasure, lemonTartRecipe2.Measure as TartMeasure)
    }
};



lemonTartRecipe.SubComponents = new List<ComponentRef>
{
   
    new ComponentRef
    {
        Id = "1ec7a68d-558b-4e77-b5e8-1d2a8c00f7e9",
        RefId = pastryDough.Id,
        Transformation = new MultiplyTransformation()
        {
            Multiplier = 0.5
        }
    }
};

RecipeMaster rm = new RecipeMaster
{
    Recipes = new List<Recipe>
    {
        lemonTartRecipe,
        lemonTartRecipe2,
        lemonCurd,
        pastryDough
    },
    Ingredients = new List<Ingredient>
    {
        flour,
        sugarPowder,
        salt,
        butter,
        egg,
        lemonJuice
    }
};

rm.Build();

lemonTartRecipe2.PrintBasicRecipe();



//rm.PrintRecipe(lemonTartRecipe);
//pastryDough.PrintBasicRecipe();
//lemonTartRecipe.PrintBasicRecipe();
//var set = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore};
//set.Converters.Add(new StringEnumConverter());
//Console.WriteLine(JsonConvert.SerializeObject(rm, Formatting.None, set));



// Need to complete the getmeasure. For that we need to be able to join all the transformations into a single transformation (need e.g. transformation.merge maybe), and finally take the final unit of the ingredient.
// This will allow showing simple recipes.
// For complex we can use the assigned/override measure, but this will also need transformation merge.