namespace Chemical_Processing.Mineral_Processing
{

    using System;
    using TUNING;
    using UnityEngine;

    class PyrolysisKilnConfig : IBuildingConfig
    {
        public const string ID = "PyrolysisKiln";

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            Prioritizable.AddRef(go);
            Electrolyzer electrolyzer = go.AddOrGet<Electrolyzer>();
            electrolyzer.maxMass = 10f;
            electrolyzer.hasMeter = false;
            Storage storage = go.AddOrGet<Storage>();
            storage.capacityKg = 330f;
            storage.showInUI = true;

            ElementConverter converter = go.AddOrGet<ElementConverter>();
            converter.consumedElements = new ElementConverter.ConsumedElement[] { new ElementConverter.ConsumedElement(WoodLogConfig.TAG, 1f) };
            converter.outputElements = new ElementConverter.OutputElement[] { new ElementConverter.OutputElement(0.33f, SimHashes.Carbon, 312.15f, false, true, 0f, 1f, 1f, 0xff, 0), new ElementConverter.OutputElement(0.1f, SimHashes.CarbonDioxide, 370.15f, false, false, 0f, 1f, 1f, 0xff, 0) };

            ManualDeliveryKG ykg = go.AddOrGet<ManualDeliveryKG>();
            ykg.SetStorage(storage);
            ykg.requestedItemTag = WoodLogConfig.TAG;
            ykg.capacity = 500f;
            ykg.refillMass = 150f;
            ykg.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;

            AlgaeDistillery algaeDistillery = go.AddOrGet<AlgaeDistillery>();
            algaeDistillery.emitMass = 20f;
            algaeDistillery.emitTag = new Tag("Carbon");
            algaeDistillery.emitOffset = new Vector3(0f, 1f);
        }

        public override BuildingDef CreateBuildingDef()
        {
            EffectorValues noise = NOISE_POLLUTION.NOISY.TIER3;
            BuildingDef def = BuildingTemplates.CreateBuildingDef("PyrolysisKiln", 1, 2, "pyrolysis_kiln_kanim", 30, 30f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.ALL_METALS, 800f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER1, noise, 0.2f);
            def.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 1));
            def.ExhaustKilowattsWhenActive = 16f;
            def.SelfHeatKilowattsWhenActive = 4f;
            def.AudioCategory = "HollowMetal";
            def.Breakable = true;
            return def;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGetDef<PoweredActiveController.Def>();
        }
     }
}
