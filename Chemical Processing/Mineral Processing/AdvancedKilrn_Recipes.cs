namespace Chemical_Processing.Mineral_Processing
{
    using System;
    using System.Collections.Generic;
    using HarmonyLib;
    using STRINGS;
    using TUNING;
    using UnityEngine;
    using Chemical_Processing.Chemicals;

    //===> REFINED CARBON <===========================================================================================================
    // Ingredient: Coal - 500kg
    // Result: Refined Coal - 500kg
    [HarmonyPatch(typeof(AdvancedKilnConfig), "ConfigureBuildingTemplate")]
    public static class RefinedCarbon_Kiln_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {   
                new ComplexRecipe.RecipeElement(SimHashes.Carbon.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.RefinedCarbon.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("AdvancedKiln", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Refine ",SimHashes.Carbon.CreateTag().ProperName(),"with intense heat, producing ",SimHashes.RefinedCarbon.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("AdvancedKiln")
            };
        }
    }

    //===> PHOSPHORITE COOKING <======================================================================================================
    // Ingredient: Phosphorus - 200kg
    //             Crushed Rock - 300kg
    // Result:     Phosphorite - 500kg
    [HarmonyPatch(typeof(AdvancedKilnConfig), "ConfigureBuildingTemplate")]
    public static class PhosphoriteKiln_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Phosphorus.CreateTag(), 200f),
                new ComplexRecipe.RecipeElement(SimHashes.CrushedRock.CreateTag(), 300f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Phosphorite.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("AdvancedKiln", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Uses high heat to chemically react ",SimHashes.Phosphorus.CreateTag().ProperName()," with ",SimHashes.CrushedRock.CreateTag().ProperName(),", and produce crude ",SimHashes.Phosphorite.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("AdvancedKiln")
            };
        }
    }

    //===> CERAMIC <==================================================================================================================
    // Ingredient: Clay - 500kg
    // Result: Ceramic - 500kg
    [HarmonyPatch(typeof(AdvancedKilnConfig), "ConfigureBuildingTemplate")]
    public static class Pure_Ceramic_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Clay.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Ceramic.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("AdvancedKiln", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Cook ",SimHashes.Clay.CreateTag().ProperName()," at high temperature, and produces ",SimHashes.Ceramic.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("AdvancedKiln")
            };
        }
    }

    //===> NAFTA SMELTING <===========================================================================================================
    // Ingredient: Plastic - 500kg
    // Result: Naphtha - 500kg
    [HarmonyPatch(typeof(AdvancedKilnConfig), "ConfigureBuildingTemplate")]
    public static class Naftha_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Polypropylene.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Naphtha.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("AdvancedKiln", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Cook ",SimHashes.Polypropylene.CreateTag().ProperName()," at high temperature, and produces ",SimHashes.Naphtha.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("AdvancedKiln")
            };
        }
    }

    //===> DIAMOND COOKING RECIPE <===================================================================================================
    // Ingredient: Refined Carbon - 500kg
    //             Oxylite - 50kg
    // Result: Diamond - 50kg
    [HarmonyPatch(typeof(AdvancedKilnConfig), "ConfigureBuildingTemplate")]
    public static class DiamondKiln_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.RefinedCarbon.CreateTag(), 500f),
                new ComplexRecipe.RecipeElement(SimHashes.OxyRock.CreateTag(), 50f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Diamond.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("AdvancedKiln", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Submit ",SimHashes.RefinedCarbon.CreateTag().ProperName()," to high temperature and pressure, and produces ",SimHashes.Diamond.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("AdvancedKiln")
            };
        }
    }

    //===> GRAPHITE COOKING - DLC1 <==================================================================================================
    // Ingredient: Coal - 300kg
    //             Bitumen - 200kg
    //             Oxylite - 50kg
    // Result: Graphite - 50kg
    [HarmonyPatch(typeof(AdvancedKilnConfig), "ConfigureBuildingTemplate")]
    public static class Graphite_Cooking_Recipe
    {
        public static void Postfix()
        {
            if (DlcManager.IsExpansion1Active())
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Carbon.CreateTag(), 300f),
                    new ComplexRecipe.RecipeElement(OilShaleElement.SolidOilShaleSimHash.CreateTag(), 200f),
                    new ComplexRecipe.RecipeElement(SimHashes.OxyRock.CreateTag(), 50f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                {
                    new ComplexRecipe.RecipeElement(SimHashes.Graphite.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
                };
                string id = ComplexRecipeManager.MakeRecipeID("AdvancedKiln", array, array2);
                ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
                complexRecipe.time = 40f;
                complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
                complexRecipe.description = string.Format(string.Concat(new string[]
                {
                    "Submit a mixture of ",SimHashes.Carbon.CreateTag().ProperName()," and ",OilShaleElement.SolidOilShaleSimHash.CreateTag().ProperName()," to high temperature treatment, and produces ",SimHashes.Graphite.CreateTag().ProperName(),"."}));
                complexRecipe.fabricators = new List<Tag>
                {
                    TagManager.Create("AdvancedKiln")
                };
            }
        }
    }
}
