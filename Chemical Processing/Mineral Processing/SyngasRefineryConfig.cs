using System;
using System.Collections.Generic;
using NightLib;
using TUNING;
using UnityEngine;
using Chemical_Processing.Chemicals;

namespace Chemical_Processing.Mineral_Processing
{
    public class SyngasRefineryConfig : IBuildingConfig
    {
        public const string ID = "SyngasRefinery";
        private static readonly List<Storage.StoredItemModifier> RefineryStorageModifier;

        static SyngasRefineryConfig()
        {
            List<Storage.StoredItemModifier> list1 = new List<Storage.StoredItemModifier>();
            list1.Add(Storage.StoredItemModifier.Hide);
            list1.Add(Storage.StoredItemModifier.Preserve);
            list1.Add(Storage.StoredItemModifier.Insulate);
            list1.Add(Storage.StoredItemModifier.Seal);
            RefineryStorageModifier = list1;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();

            ComplexFabricator fabricator = go.AddOrGet<ComplexFabricator>();
            fabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            fabricator.duplicantOperated = false;

            Storage standardStorage = go.AddOrGet<Storage>();
            standardStorage.capacityKg = 5000f;

            BuildingTemplates.CreateComplexFabricatorStorage(go, fabricator);
            fabricator.outStorage.capacityKg = 5000f;
            fabricator.inStorage.SetDefaultStoredItemModifiers(RefineryStorageModifier);
            fabricator.buildStorage.SetDefaultStoredItemModifiers(RefineryStorageModifier);
            fabricator.outStorage = standardStorage;
            fabricator.outputOffset = new Vector3(0f, 0f);

            ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
            conduitDispenser.storage = standardStorage;
            conduitDispenser.conduitType = ConduitType.Gas;
            conduitDispenser.invertElementFilter = false;
            conduitDispenser.elementFilter = new SimHashes[] { SimHashes.Syngas };

            Prioritizable.AddRef(go);
        }

        public override BuildingDef CreateBuildingDef()
        {
            EffectorValues noise = NOISE_POLLUTION.NOISY.TIER5;
            BuildingDef def1 = BuildingTemplates.CreateBuildingDef("SyngasRefinery", 2, 4, "syngas_distillery_kanim", 100, 30f, TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.ALL_METALS, 800f, BuildLocationRule.OnFloor, TUNING.BUILDINGS.DECOR.PENALTY.TIER1, noise, 0.2f);
            def1.Overheatable = false;
            def1.RequiresPowerInput = true;
            def1.EnergyConsumptionWhenActive = 60f;
            def1.ExhaustKilowattsWhenActive = 2f;
            def1.SelfHeatKilowattsWhenActive = 4f;
            def1.AudioCategory = "HollowMetal";
            def1.ViewMode = OverlayModes.LiquidConduits.ID;
            def1.OutputConduitType = ConduitType.Gas;
            def1.PowerInputOffset = new CellOffset(0, 0);
            def1.UtilityOutputOffset = new CellOffset(1, 0);
            return def1;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGetDef<PoweredActiveController.Def>();
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
        }

    }
}
