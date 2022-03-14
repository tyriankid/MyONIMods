namespace Chemical_Processing.Mineral_Processing
{
    using System;
    using System.Collections.Generic;
    using HarmonyLib;
    using Chemical_Processing.Chemicals;

    //===> SAND GLASS SMELTING <======================================================================================================
    // Ingredient: Sand - 300kg
    // Result: Glass - 100kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Sand_to_Glass_Recipe
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 300f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenGlass.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;

            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.Sand.CreateTag().ProperName()," to produce ",SimHashes.MoltenGlass.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> COPPER ORE SMELTING <======================================================================================================
    // Ingredient: Copper Ore - 500kg
    // Result: Copper - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class CopperOre_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Cuprite.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenCopper.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.Cuprite.CreateTag().ProperName()," to produce ",SimHashes.MoltenCopper.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> GOLD AMALGAM SMELTING <====================================================================================================
    // Ingredient: Gold Amalgam - 500kg
    // Result: Gold - 400kg
    //         Mercury - 100kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class GoldAmalgam_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.GoldAmalgam.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenGold.CreateTag(), 400f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false),
                new ComplexRecipe.RecipeElement(SimHashes.Mercury.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)

            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.GoldAmalgam.CreateTag().ProperName()," to produce ",SimHashes.MoltenGold.CreateTag().ProperName(),". Produces small amounts of ",SimHashes.Mercury.CreateTag().ProperName()," as waste product."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> ZINC ORE SMELTING <====================================================================================================
    // Ingredient: Zinc Ore - 500kg
    // Result: Zinc - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class ZincOre_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(AurichalciteElement.AurichalciteOreSimHash.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(ZincElement.MoltenZincSimHash.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",AurichalciteElement.AurichalciteOreSimHash.CreateTag().ProperName()," to produce ",ZincElement.MoltenZincSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> SILVER ORE SMELTING <====================================================================================================
    // Ingredient: Silver Ore - 500kg
    // Result: Silver - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class SilverOre_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(ArgentiteElement.ArgentiteOreSimHash.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SilverElement.MoltenSilverSimHash.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",ArgentiteElement.ArgentiteOreSimHash.CreateTag().ProperName()," to produce ",SilverElement.MoltenSilverSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> ELECTRUM SMELTING <========================================================================================================
    // Ingredient: Electrum - 500kg
    // Result: Gold - 300kg
    //         Silver - 200kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Electrum_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Electrum.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenGold.CreateTag(), 300f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false),
                new ComplexRecipe.RecipeElement(SilverElement.MoltenSilverSimHash.CreateTag(), 200f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;

            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.Electrum.CreateTag().ProperName()," to produce ",SimHashes.MoltenGold.CreateTag().ProperName()," and ",SilverElement.MoltenSilverSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> IRON ORE SMELTING <========================================================================================================
    // Ingredient: Iron Ore - 500kg
    // Result: Iron - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class IronOre_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.IronOre.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenIron.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.IronOre.CreateTag().ProperName()," to produce ",SimHashes.MoltenIron.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> PYRITE SMELTING <==========================================================================================================
    // Ingredient: Pyrite - 500kg
    // Result: Iron - 400kg
    //         Sulfur - 100kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Pyrite_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.FoolsGold.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenIron.CreateTag(), 400f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false),
                new ComplexRecipe.RecipeElement(SimHashes.LiquidSulfur.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.FoolsGold.CreateTag().ProperName()," to produce ",SimHashes.MoltenIron.CreateTag().ProperName(),". Produces small traces of ",SimHashes.LiquidSulfur.CreateTag().ProperName()," as by-product."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> ALUMINUM ORE SMELTING <====================================================================================================
    // Ingredient: Aluminum Ore - 500kg
    // Result: Aluminum - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class AluminumOre_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.AluminumOre.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenAluminum.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.AluminumOre.CreateTag().ProperName()," to produce ",SimHashes.MoltenAluminum.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> WOLFRAMITE SMELTING <======================================================================================================
    // Ingredient: Wolframite - 500kg
    // Result: Tungsten - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Wolframite_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Wolframite.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenTungsten.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.Wolframite.CreateTag().ProperName()," to produce ",SimHashes.MoltenTungsten.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> ABYSSALITE SMELTING <======================================================================================================
    // Ingredient: Abyssalite - 500kg
    //             Refined Carbon - 50kg
    // Result: Molten Tungsten - 50kg
    //         Liquid Phosphorus - 250kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Abyssalite_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Katairite.CreateTag(), 500f),
                new ComplexRecipe.RecipeElement(SimHashes.RefinedCarbon.CreateTag(), 50f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenTungsten.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false),
                new ComplexRecipe.RecipeElement(SimHashes.LiquidPhosphorus.CreateTag(), 250f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.Katairite.CreateTag().ProperName()," to produce ",SimHashes.MoltenTungsten.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> STEEL SMELTING <===========================================================================================================
    // Ingredient: Iron - 350kg
    //             Refined Carbon - 100kg
    //             Borax - 50kg
    // Result: Steel - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Steel_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Iron.CreateTag(), 350f),
                new ComplexRecipe.RecipeElement(SimHashes.RefinedCarbon.CreateTag(), 100f),
                new ComplexRecipe.RecipeElement(BoraxElement.SolidBoraxSimHash.CreateTag(), 50f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.MoltenSteel.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Refine ",SimHashes.Iron.CreateTag().ProperName()," with addition of ",BoraxElement.SolidBoraxSimHash.CreateTag().ProperName()," to produce ",SimHashes.MoltenSteel.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> ICE SMELTING <=============================================================================================================
    // Ingredient: Ice - 500kg
    // Result: Water - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Ice_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Ice.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, true)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 20f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.Ice.CreateTag().ProperName()," to produce ",SimHashes.Water.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> DIRT ICE SMELTING <========================================================================================================
    // Ingredient: Polluted Ice - 500kg
    // Result: Polluted Water - 500kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class DirtyIce_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.DirtyIce.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.DirtyWater.CreateTag(), 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, true)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 20f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.DirtyIce.CreateTag().ProperName()," to produce ",SimHashes.DirtyWater.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> BITUMEN SMELTING <=========================================================================================================
    // Ingredient: Bitumen - 500kg
    // Result: Naphtha - 100kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Bitumen_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Bitumen.CreateTag(), 500f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Naphtha.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, true)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 20f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt ",SimHashes.Bitumen.CreateTag().ProperName()," to produce ",SimHashes.Naphtha.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

    //===> TITANIUM SMELTING <===========================================================================================
    // Ingredient: Regolith - 500kg
    //             Borax - 50kg
    //             Chlorine Gas - 25kg
    // Result: Molten Titanium - 50kg
    //         Molten Iron - 25kg
    //         Molten Gold - 25kg
    //         Molten Glass - 100kg
    [HarmonyPatch(typeof(GlassFoundryConfig), "ConfigureBuildingTemplate")]
    public static class Titanium_Smelting_Foundry
    {
        public static void Postfix()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Regolith.CreateTag(), 400f),
                new ComplexRecipe.RecipeElement(BoraxElement.SolidBoraxSimHash.CreateTag(), 50f),
                new ComplexRecipe.RecipeElement(SimHashes.ChlorineGas.CreateTag(), 25f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(TitaniumElement.MoltenTitaniumSimHash.CreateTag(), 50f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false),
                new ComplexRecipe.RecipeElement(SimHashes.MoltenIron.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, true),
                new ComplexRecipe.RecipeElement(SimHashes.MoltenGold.CreateTag(), 25f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, true),
                new ComplexRecipe.RecipeElement(SimHashes.MoltenGlass.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, true)
            };
            string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 40f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Smelt meteoric ",SimHashes.Regolith.CreateTag().ProperName()," with reactive addition of ",SimHashes.ChlorineGas.CreateTag().ProperName()," to produce ",TitaniumElement.SolidTitaniumSimHash.CreateTag().ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("GlassFoundry")
            };
        }
    }

}

