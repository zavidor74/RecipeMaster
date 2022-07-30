// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RecipeMaster1;
using RecipeMaster1.Entities;
using RecipeMaster1.Entities.Transformations;


//RecipeMasterDatabase.RecipeMasterDatabase db = PlayWithRecipeMaster.TestHelpers.GenerateAdHocDb();
//db.SaveTo("c:\\todelete\\recipemaster.json");

var db2 = RecipeMasterDatabase.RecipeMasterDatabase.LoadFrom("c:\\todelete\\recipemaster.json");
RecipeMaster1.RecipeMaster.Instance.AttachDb(db2);

RecipeMaster1.RecipeMaster.Instance.PrintBasicRecipe("57c2b8cc-756d-4d02-9a3f-dea664fc5165");

/*

RecipeMaster.Instance.Build();

lemonTartRecipe2.PrintBasicRecipe();
*/

//rm.PrintRecipe(lemonTartRecipe);
//pastryDough.PrintBasicRecipe();
//lemonTartRecipe.PrintBasicRecipe();
//var set = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore};
//set.Converters.Add(new StringEnumConverter());
//Console.WriteLine(JsonConvert.SerializeObject(rm, Formatting.None, set));



// Need to complete the getmeasure. For that we need to be able to join all the transformations into a single transformation (need e.g. transformation.merge maybe), and finally take the final unit of the ingredient.
// This will allow showing simple recipes.
// For complex we can use the assigned/override measure, but this will also need transformation merge.