using System;
using System.Collections.Generic;
using HarmonyLib;
using Chemical_Processing.Chemicals;

namespace Chemical_Processing.Mineral_Processing
{
    //===> SYNGAS FROM WOOD LUMBER <====================================================================================================
    // Ingredient: Wood Lumber - 100kg
    // Result: Syngas - 25kg
    //         Polluted Dirt - 75kg
    [HarmonyPatch(typeof(SyngasRefineryConfig), "ConfigureBuildingTemplate")]
    public static class WoodLumber_Syngas_Refining
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(WoodLogConfig.TAG, 100f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Syngas.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, true),
                new ComplexRecipe.RecipeElement(SimHashes.ToxicSand.CreateTag(), 75f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)

            };
            string id = ComplexRecipeManager.MakeRecipeID("SyngasRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 50f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Refine ",WoodLogConfig.TAG.ProperName()," to ",SimHashes.Syngas.CreateTag().ProperName(),". Produces ",SimHashes.ToxicSand.CreateTag().ProperName()," as waste product."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("SyngasRefinery")
            };
        }
    }

    //===> SYNGAS FROM BITUMEN <====================================================================================================
    // Ingredient: Bitumen - 100kg
    // Result: Syngas - 25kg
    //         Refined Coal - 75kg
    [HarmonyPatch(typeof(SyngasRefineryConfig), "ConfigureBuildingTemplate")]
    public static class Bitumen_Syngas_Refining
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Bitumen.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Syngas.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, true),
                new ComplexRecipe.RecipeElement(SimHashes.RefinedCarbon.CreateTag(), 75f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)

            };
            string id = ComplexRecipeManager.MakeRecipeID("SyngasRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 50f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Refine ",SimHashes.Bitumen.CreateTag().ProperName()," to ",SimHashes.Syngas.CreateTag().ProperName(),". Produces ",SimHashes.RefinedCarbon.CreateTag().ProperName()," as by-product."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("SyngasRefinery")
            };
        }
    }

    //===> SYNGAS FROM OIL SHALE <====================================================================================================
    // Ingredient: Oil Shale - 100kg
    // Result: Syngas - 50kg
    //         Petroleum - 50kg
    [HarmonyPatch(typeof(SyngasRefineryConfig), "ConfigureBuildingTemplate")]
    public static class OilShale_Syngas_Refining
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(OilShaleElement.SolidOilShaleSimHash.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Syngas.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, true),
                new ComplexRecipe.RecipeElement(SimHashes.Petroleum.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)

            };
            string id = ComplexRecipeManager.MakeRecipeID("SyngasRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 50f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Refine ",OilShaleElement.SolidOilShaleSimHash.CreateTag().ProperName()," to ",SimHashes.Syngas.CreateTag().ProperName(),". Produces ",SimHashes.Petroleum.CreateTag().ProperName()," as by-product."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("SyngasRefinery")
            };
        }
    }
}
