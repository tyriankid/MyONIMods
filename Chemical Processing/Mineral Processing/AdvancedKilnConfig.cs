namespace Chemical_Processing.Mineral_Processing
{
    using System;
    using System.Collections.Generic;
    using TUNING;
    using UnityEngine;

    public class AdvancedKilnConfig : IBuildingConfig
    {

        public const string ID = "AdvancedKiln";
        private void ConfigureRecipes() { }
        private static readonly List<Storage.StoredItemModifier> KilnStoredItemModifiers;

        static AdvancedKilnConfig()
        {
            List<Storage.StoredItemModifier> list1 = new List<Storage.StoredItemModifier>();
            list1.Add(Storage.StoredItemModifier.Hide);
            list1.Add(Storage.StoredItemModifier.Preserve);
            list1.Add(Storage.StoredItemModifier.Insulate);
            list1.Add(Storage.StoredItemModifier.Seal);
            KilnStoredItemModifiers = list1;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            Prioritizable.AddRef(go);
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
            ComplexFabricator fabricator = go.AddOrGet<ComplexFabricator>();
            fabricator.heatedTemperature = 320.15f;
            fabricator.duplicantOperated = true;
            BuildingTemplates.CreateComplexFabricatorStorage(go, fabricator);
            fabricator.inStorage.SetDefaultStoredItemModifiers(KilnStoredItemModifiers);
            fabricator.buildStorage.SetDefaultStoredItemModifiers(KilnStoredItemModifiers);
            fabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            go.AddOrGet<ComplexFabricatorWorkable>().overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_fabricator_generic_kanim") };
            go.AddOrGet<ComplexFabricatorWorkable>().AnimOffset = new Vector3(-1f, 0f, 0f);
            fabricator.fetchChoreTypeIdHash = Db.Get().ChoreTypes.FabricateFetch.IdHash;
            go.AddOrGet<LoopingSounds>();
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
        }

        public override BuildingDef CreateBuildingDef()
        {
            EffectorValues tier = NOISE_POLLUTION.NOISY.TIER5;
            BuildingDef def1 = BuildingTemplates.CreateBuildingDef("AdvancedKiln", 3, 3, "fabricator_generic_kanim", 100, 30f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.REFINED_METALS, 800f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER1, tier, 0.2f);
            def1.Overheatable = false;
            def1.RequiresPowerInput = true;
            def1.EnergyConsumptionWhenActive = 800f;
            def1.ExhaustKilowattsWhenActive = 16f;
            def1.SelfHeatKilowattsWhenActive = 4f;
            def1.AudioCategory = "HollowMetal";
            def1.PowerInputOffset = new CellOffset(1, 0);
            return def1;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<PoweredActiveController.Def>().showWorkingStatus = true;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            base.DoPostConfigurePreview(def, go);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            base.DoPostConfigureUnderConstruction(go);
        }
    }

}
