using System;
using System.Collections.Generic;
using HarmonyLib;
using STRINGS;
using TUNING;
using Chemical_Processing.Chemicals;

namespace Chemical_Processing.Mineral_Processing
{
    //===> ROCK CRUSHER: SILVER ORE CRUSHING <============================================================================
    // Ingredient: Silver Ore - 100kg
    // Result: Silver - 50kg
    //         Sulfur - 25kg
    //         Sand - 25kg
    [HarmonyPatch(typeof(RockCrusherConfig), "ConfigureBuildingTemplate")]
    public static class RockCrusher_SilverOre_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(ArgentiteElement.ArgentiteOreSimHash.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SilverElement.SolidSilverSimHash.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.Sulfur.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("RockCrusher", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Crush down ",ArgentiteElement.ArgentiteOreSimHash.CreateTag().ProperName()," to ",SilverElement.SolidSilverSimHash.CreateTag().ProperName()," and ",SimHashes.Sulfur.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("RockCrusher")
            };
        }
    }

    //===> ROCK CRUSHER: ZINC ORE CRUSHING <==============================================================================
    // Ingredient: Zinc Ore - 100kg
    // Result: Zinc - 50kg
    //         Copper - 25kg
    //         Sand - 25kg
    [HarmonyPatch(typeof(RockCrusherConfig), "ConfigureBuildingTemplate")]
    public static class RockCrusher_ZincOre_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(AurichalciteElement.AurichalciteOreSimHash.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(ZincElement.SolidZincSimHash.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.Copper.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("RockCrusher", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Crush down ",AurichalciteElement.AurichalciteOreSimHash.CreateTag().ProperName()," to ",ZincElement.SolidZincSimHash.CreateTag().ProperName()," and ",SimHashes.Copper.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("RockCrusher")
            };
        }
    }

    //===> METAL REFINERY: BRASS FROM COPPER AND ZINC <===================================================================
    // Ingredient: Copper - 70kg
    //            Zinc - 30kg
    // Result: Brass - 100kg
    [HarmonyPatch(typeof(MetalRefineryConfig), "ConfigureBuildingTemplate")]
    public static class MetalRefinery_Brass_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Copper.CreateTag(), 70f),
                new ComplexRecipe.RecipeElement(ZincElement.SolidZincSimHash.CreateTag(), 30f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(BrassElement.SolidBrassSimHash.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("MetalRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.Copper.CreateTag().ProperName()," and ",ZincElement.SolidZincSimHash.CreateTag().ProperName()," to produce ",BrassElement.SolidBrassSimHash.CreateTag().ProperName()," alloy."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MetalRefinery")
            };
        }
    }

    //===> METAL REFINERY: SILVER FROM SILVER ORE <===================================================================================
    // Ingredient: Silver Ore - 100kg
    // Result: Silver - 100kg
    [HarmonyPatch(typeof(MetalRefineryConfig), "ConfigureBuildingTemplate")]
    public static class MetalRefinery_SilverOre_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(ArgentiteElement.ArgentiteOreSimHash.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SilverElement.SolidSilverSimHash.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("MetalRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",ArgentiteElement.ArgentiteOreSimHash.CreateTag().ProperName()," to ",SilverElement.SolidSilverSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MetalRefinery")
            };
        }
    }

    //===> METAL REFINERY: ZINC FROM ZINC ORE <===================================================================================
    // Ingredient: Zinc Ore - 100kg
    // Result: Zinc - 100kg
    [HarmonyPatch(typeof(MetalRefineryConfig), "ConfigureBuildingTemplate")]
    public static class MetalRefinery_ZincOre_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(AurichalciteElement.AurichalciteOreSimHash.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(ZincElement.SolidZincSimHash.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("MetalRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",AurichalciteElement.AurichalciteOreSimHash.CreateTag().ProperName()," to ",ZincElement.SolidZincSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MetalRefinery")
            };
        }
    }

    //===> MOLECULAR FORGE: FIBERGLASS RECIPE <=======================================================================================
    //Ingredient: Sand - 60kg
    //            Borax - 10kg
    //            Plastic - 30kg
    //Result: Fiberglass - 100kg
    [HarmonyPatch(typeof(SupermaterialRefineryConfig), "ConfigureBuildingTemplate")]
    public static class MolecularForge_Fiberglass_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 60f),
                new ComplexRecipe.RecipeElement(SimHashes.Polypropylene.CreateTag(), 30f),
                new ComplexRecipe.RecipeElement(BoraxElement.SolidBoraxSimHash.CreateTag(), 10f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(FiberglassElement.SolidFiberglassSimHash.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Fuse together a mixture of ",SimHashes.Sand.CreateTag().ProperName()," and",BoraxElement.SolidBoraxSimHash.CreateTag().ProperName()," with addition of ",SimHashes.Polypropylene.CreateTag().ProperName()," to produce ",FiberglassElement.SolidFiberglassSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("SupermaterialRefinery")
            };
        }
    }

    //===> MOLECULAR FORGE: TUNGSTEN CARBIDE RECIPE <=================================================================================
    // Ingredient: Tungsten - 50kg
    //             Fullerene - 50kg
    //             Borax - 50kg
    // Result: Tungsten Carbide - 100kg
    [HarmonyPatch(typeof(SupermaterialRefineryConfig), "ConfigureBuildingTemplate")]
    public static class MolecularForge_TungstenCarbide_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Tungsten.CreateTag(), 50f),
                new ComplexRecipe.RecipeElement(SimHashes.Fullerene.CreateTag(), 50f),
                new ComplexRecipe.RecipeElement(BoraxElement.SolidBoraxSimHash.CreateTag(), 20f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(TungstenCarbideElement.SolidTungstenCarbideSimHash.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Fuse together a mixture of ",SimHashes.Tungsten.CreateTag().ProperName()," and",SimHashes.Fullerene.CreateTag().ProperName()," with addition of ",BoraxElement.SolidBoraxSimHash.CreateTag().ProperName()," to produce ",TungstenCarbideElement.SolidTungstenCarbideSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("SupermaterialRefinery")
            };
        }
    }

    //===> MOLECULAR FORGE: HALON GAS RECIPE <========================================================================================
    // Ingredient: Fullerene - 1kg
    //             Chlorine - 25kg
    //             Silver - 4kg
    // Result: Halon Gas - 30kg
    [HarmonyPatch(typeof(SupermaterialRefineryConfig), "ConfigureBuildingTemplate")]
    public static class MolecularForge_HalonGas_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.ChlorineGas.CreateTag(), 25f),
                new ComplexRecipe.RecipeElement(SimHashes.Fullerene.CreateTag(), 1f),
                new ComplexRecipe.RecipeElement(SilverElement.SolidSilverSimHash.CreateTag(), 4f)               
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(HalonGasElement.HalonGasSimHash.CreateTag(), 30f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("SupermaterialRefinery", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Atomically combine a mixture of ",SimHashes.ChlorineGas.CreateTag().ProperName()," and",SimHashes.Fullerene.CreateTag().ProperName()," with addition of ",SilverElement.SolidSilverSimHash.CreateTag().ProperName()," to produce ",HalonGasElement.HalonGasSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("SupermaterialRefinery")
            };
        }
    }
}
