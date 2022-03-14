using STRINGS;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TUNING;
using UnityEngine;
using Chemical_Processing.Chemicals;

namespace Chemical_Processing.Mineral_Processing
{
    public class ViscoseFrameConfig : IBuildingConfig
    {
        public const string ID = "ViscoseFrame";
        private Tag FUEL_TAG = SimHashes.Syngas.CreateTag();

        private static readonly List<Storage.StoredItemModifier> ViscoseFrameStoredItemModifiers;

        static ViscoseFrameConfig()
        {
            List<Storage.StoredItemModifier> list1 = new List<Storage.StoredItemModifier>();
            list1.Add(Storage.StoredItemModifier.Hide);
            list1.Add(Storage.StoredItemModifier.Preserve);
            list1.Add(Storage.StoredItemModifier.Insulate);
            list1.Add(Storage.StoredItemModifier.Seal);
            ViscoseFrameStoredItemModifiers = list1;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
            GourmetCookingStation fabricator = go.AddOrGet<GourmetCookingStation>();
            fabricator.heatedTemperature = 368.15f;
            fabricator.duplicantOperated = false;
            fabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
            go.AddOrGet<ComplexFabricatorWorkable>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, fabricator);
            fabricator.fuelTag = this.FUEL_TAG;
            fabricator.outStorage.capacityKg = 10f;
            fabricator.inStorage.SetDefaultStoredItemModifiers(ViscoseFrameStoredItemModifiers);
            fabricator.buildStorage.SetDefaultStoredItemModifiers(ViscoseFrameStoredItemModifiers);
            fabricator.outStorage.SetDefaultStoredItemModifiers(ViscoseFrameStoredItemModifiers);
            ConduitConsumer local1 = go.AddOrGet<ConduitConsumer>();
            local1.capacityTag = this.FUEL_TAG;
            local1.capacityKG = 10f;
            local1.alwaysConsume = true;
            local1.storage = fabricator.inStorage;
            local1.forceAlwaysSatisfied = true;
            ElementConverter converter = go.AddOrGet<ElementConverter>();
            converter.consumedElements = new ElementConverter.ConsumedElement[] { new ElementConverter.ConsumedElement(this.FUEL_TAG, 0.8f) };
            converter.outputElements = new ElementConverter.OutputElement[] { new ElementConverter.OutputElement(0.025f, SimHashes.Steam, 373.15f, false, false, 0f, 3f, 1f, 0xff, 0) };
            this.ConfigureRecipes();
            Prioritizable.AddRef(go);
        }

        private void ConfigureRecipes()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
{
                new ComplexRecipe.RecipeElement(WoodLogConfig.TAG, 150f)
};
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(RayonFabricConfig.TAG, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("ViscoseFrame", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 30f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Produces ",RayonFabricConfig.TAG.ProperName()," from the pulp of ",WoodLogConfig.TAG.ProperName(),"."}));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("ViscoseFrame")
            };
        }

        public override BuildingDef CreateBuildingDef()
        {
            EffectorValues noise = NOISE_POLLUTION.NOISY.TIER3;
            BuildingDef def = BuildingTemplates.CreateBuildingDef("ViscoseFrame", 6, 4, "viscose_frame_kanim", 30, 30f, TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.OnFloor, TUNING.BUILDINGS.DECOR.NONE, noise, 0.2f);
            BuildingTemplates.CreateElectricalBuildingDef(def);
            def.AudioCategory = "Metal";
            def.AudioSize = "large";
            def.Overheatable = true;
            def.OverheatTemperature = 348.15f;
            def.EnergyConsumptionWhenActive = 480f;
            def.ExhaustKilowattsWhenActive = 0.5f;
            def.SelfHeatKilowattsWhenActive = 32f;
            def.PowerInputOffset = new CellOffset(0, 0);
            def.InputConduitType = ConduitType.Gas;
            def.UtilityInputOffset = new CellOffset(-1, 0);
            def.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 1));
            return def;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGetDef<PoweredActiveController.Def>();
        }
    }
}
