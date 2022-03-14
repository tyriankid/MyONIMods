namespace Chemical_Processing.Mineral_Processing
{
    using System;
    using System.Collections.Generic;
    using HarmonyLib;
    using STRINGS;
    using TUNING;
    using Chemical_Processing.Chemicals;

    public class random_number
    {
        public readonly Random _random = new Random();
    }

    //===> EGG SHELL MILLING <========================================================================================================
    // Ingredient: Eggshell - 5kg
    // Result: Lime - 5kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class Eggshell_Milling_Recipe
    {
        public static void Postfix()
        {
            Element element5 = ElementLoader.FindElementByHash(SimHashes.Lime);
            ComplexRecipe.RecipeElement[] inputs = new ComplexRecipe.RecipeElement[] { new ComplexRecipe.RecipeElement("EggShell", 5f) };
            ComplexRecipe.RecipeElement[] outputs = new ComplexRecipe.RecipeElement[] { new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Lime).tag, 5f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false) };
            string str5 = ComplexRecipeManager.MakeObsoleteRecipeID("MineralMill", element5.tag);
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", inputs, outputs);
            ComplexRecipe recipe3 = new ComplexRecipe(id, inputs, outputs);
            recipe3.time = 30f;
            recipe3.description = string.Format((string)STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), STRINGS.MISC.TAGS.EGGSHELL);
            recipe3.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            List<Tag> list4 = new List<Tag>();
            list4.Add(TagManager.Create("MineralMill"));
            recipe3.fabricators = list4;
            ComplexRecipeManager.Get().AddObsoleteIDMapping(str5, id);
        }
    }

    //===> POKESHELL MOLT MILLING <===================================================================================================
    // Ingredient: Pokeshell Molt - 1kg
    // Result: Lime - 10kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class PokeshellMolt_Milling_Recipe
    {
        public static void Postfix()
        {
            Element element5 = ElementLoader.FindElementByHash(SimHashes.Lime);
            ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
            {
            new ComplexRecipe.RecipeElement("CrabShell", 1f)
            };
            ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
            {
            new ComplexRecipe.RecipeElement(element5.tag, 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            ComplexRecipe complexRecipe5 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MineralMill", array9, array10), array9, array10, 0);
            complexRecipe5.time = 40f;
            complexRecipe5.description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME);
            complexRecipe5.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe5.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //====> SMALL POKESHELL MOLT MILLING <============================================================================================
    // Ingredient: Small Pokeshell Molt - 1kg
    // Result: Lime - 5kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class SmallPokeshellMolt_Milling_Recipe
    {
        public static void Postfix()
        {
            Element element4 = ElementLoader.FindElementByHash(SimHashes.Lime);
            ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
            {
            new ComplexRecipe.RecipeElement("BabyCrabShell", 1f)
            };
            ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
            {
            new ComplexRecipe.RecipeElement(element4.tag, 5f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            ComplexRecipe complexRecipe4 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MineralMill", array7, array8), array7, array8, 0);
            complexRecipe4.time = 40f;
            complexRecipe4.description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME);
            complexRecipe4.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe4.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> TABLE SALT MILLING <=======================================================================================================
    // Ingredient: Salt - 100kg
    // Result: Borax - 5kg
    //         Table Salt - 5kg
    //         Sand - 95kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class Salt_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Salt.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(BoraxElement.SolidBoraxSimHash.CreateTag(), 5f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 90f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),
                new ComplexRecipe.RecipeElement(TableSaltConfig.ID.ToTag(), 5f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)             
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 70f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {"Grind down ",SimHashes.Salt.CreateTag().ProperName()," and extract small traces of rare ",BoraxElement.SolidBoraxSimHash.CreateTag().ProperName(),". Produces ",SimHashes.Sand.CreateTag().ProperName()," and",TableSaltConfig.ID.ToTag().ProperName()," as waste product."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> ABYSSALITE MILLING <=======================================================================================================
    // Ingredient: Abyssalite - 500kg
    // Result: Tungsten - 10kg
    //         Wolframite - 20kg
    //         Phosphorus - 220kg
    //         Crushed Rock - 250kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class Abyssalite_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Katairite.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Tungsten.CreateTag(), 10f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 250f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),
                new ComplexRecipe.RecipeElement(SimHashes.Wolframite.CreateTag(), 20f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),   
                new ComplexRecipe.RecipeElement(SimHashes.Phosphorus.CreateTag(), 220f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)   
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 70f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {"Grind down ",SimHashes.Katairite.CreateTag().ProperName()," and extract small nuggets of ",SimHashes.Tungsten.CreateTag().ProperName()," and some ",SimHashes.Wolframite.CreateTag().ProperName(),". Produces ",SimHashes.CrushedRock.CreateTag().ProperName()," and",SimHashes.Phosphorus.CreateTag().ProperName()," as waste product."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> MAFIC ROCK MILLING <=======================================================================================================
    // Ingredient: Mafic Rock - 500kg
    // Result: Zinc Ore - 50kg
    //         Iron Ore - 50kg
    //         Phosphorus - 100kg
    //         Crushed Rock - 300kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class MaficRock_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MaficRock.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(AurichalciteElement.AurichalciteOreSimHash.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),
                new ComplexRecipe.RecipeElement(SimHashes.IronOre.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 300f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),     
                new ComplexRecipe.RecipeElement(SimHashes.Phosphorus.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false) 
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 70f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Grind down ",SimHashes.MaficRock.CreateTag().ProperName()," extracting ",ZincElement.SolidZincSimHash.CreateTag().ProperName()," and ",SimHashes.IronOre.CreateTag().ProperName(),". Produces ",SimHashes.Phosphorus.CreateTag().ProperName()," and",SimHashes.CrushedRock.CreateTag().ProperName()," as waste products."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> CRUSHED ROCK MILLING <=====================================================================================================
    // Ingredient: Crushed Rock - 500kg
    // Result: Sand - 500kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class CrushedRock_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Further grind down ",SimHashes.CrushedRock.CreateTag().ProperName()," to ",SimHashes.Sand.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> GRANITE MILLING <==========================================================================================================
    // Ingredient: Granite - 500kg
    // Result: Rust - 40kg
    //         Aluminum Ore - 40kg
    //         Crushed Rock - 300kg
    //         Sand - 120kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class Granite_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Granite.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Rust.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.AluminumOre.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),   
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 300f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),  
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 120f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)   
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 70f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
               "Grind down ",SimHashes.Granite.CreateTag().ProperName()," and extract ",SimHashes.Rust.CreateTag().ProperName()," and ",SimHashes.AluminumOre.CreateTag().ProperName(),". Produces ",SimHashes.CrushedRock.CreateTag().ProperName()," and ",SimHashes.Sand.CreateTag().ProperName()," as waste products."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> IGNEOUS ROCK MILLING <=====================================================================================================
    // Ingredient: Igneous Rock - 500kg
    // Result: Pyrite - 40kg
    //         Sulfur - 40kg
    //         Crushed Rock - 150kg
    //         Sand - 110kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class IngneousRock_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.IgneousRock.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.FoolsGold.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),
                new ComplexRecipe.RecipeElement(SimHashes.Sulfur.CreateTag(), 200f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false), 
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 150f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false),
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 110f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)  
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 70f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
               "Grind down ",SimHashes.IgneousRock.CreateTag().ProperName()," and extract ",SimHashes.FoolsGold.CreateTag().ProperName()," and ",SimHashes.Sulfur.CreateTag().ProperName(),". Produces ",SimHashes.CrushedRock.CreateTag().ProperName()," and ",SimHashes.Sand.CreateTag().ProperName()," as waste products."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> SEDIMENTARY ROCK MILLING <=================================================================================================
    // Ingredient: Sedimentary Rock - 500kg
    // Result: Gold Amalgam - 40kg
    //         Silver Ore - 40kg
    //         Crushed Rock - 300kg
    //         Sand - 120kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class SedimentaryRock_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.SedimentaryRock.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.GoldAmalgam.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(ArgentiteElement.ArgentiteOreSimHash.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 300f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.Clay.CreateTag(), 120f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false) 
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 70f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
               "Grind down ",SimHashes.SedimentaryRock.CreateTag().ProperName()," and extract ",SimHashes.GoldAmalgam.CreateTag().ProperName()," and ",ArgentiteElement.ArgentiteOreSimHash.CreateTag().ProperName(),". Produces ",SimHashes.CrushedRock.CreateTag().ProperName()," and ",SimHashes.Sand.CreateTag().ProperName()," as waste products."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> SANDSTONE MILLING - VANILLA <==============================================================================================
    // Ingredient: Sandstone - 500kg
    // Result: Copper Ore - 40kg
    //         Electrum - 40kg
    //         Crushed Rock - 100kg
    //         Sand - 320kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class Sandstone_Milling_Vanilla_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.SandStone.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Cuprite.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.Electrum.CreateTag(), 40f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 120f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 300f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 70f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Grind down ",SimHashes.SandStone.CreateTag().ProperName()," and extract ",SimHashes.Cuprite.CreateTag().ProperName()," and ",SimHashes.Electrum.CreateTag().ProperName(),". Produces ",SimHashes.CrushedRock.CreateTag().ProperName()," and ",SimHashes.Sand.CreateTag().ProperName()," as waste products."}));
            complexRecipe.fabricators = new List<Tag>
                {
                    TagManager.Create("MineralMill")
                };
        }
    }

    //===> OBSIDIAN MILLING <=========================================================================================================
    // Ingredient: Obsidian - 500kg
    // Result: Sand - 500kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class Obsidian_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Obsidian.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Grind down ",SimHashes.Obsidian.CreateTag().ProperName()," and extract lots of ",SimHashes.Sand.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }

    //===> FOSSIL MILLING <===========================================================================================================
    // Ingredient: Fossil - 500kg
    // Result: Lime - 25kg
    //         Oil Shale - 150kg
    //         Crushed Rock - 100kg
    //         Sand - 225kg
    [HarmonyPatch(typeof(MineralMillConfig), "ConfigureBuildingTemplate")]
    public static class Fossil_Milling_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Fossil.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Lime.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false), 
                new ComplexRecipe.RecipeElement(OilShaleElement.SolidOilShaleSimHash.CreateTag(), 150f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false), 
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false), 
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 225f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false) 
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralMill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 70f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Grind down ",SimHashes.Fossil.CreateTag().ProperName()," and extract ",SimHashes.Lime.CreateTag().ProperName(),". Produces ",OilShaleElement.SolidOilShaleSimHash.CreateTag().ProperName()," and ",SimHashes.CrushedRock.CreateTag().ProperName()," as waste product."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralMill")
            };
        }
    }
}
