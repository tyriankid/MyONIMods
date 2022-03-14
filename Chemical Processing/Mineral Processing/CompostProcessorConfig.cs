namespace Chemical_Processing.Mineral_Processing
{
    using System;
    using TUNING;
    using UnityEngine;

    public class CompostProcessorConfig : IBuildingConfig
    {
        public const string ID = "CompostProcessor";
        private const ConduitType INPUT_CONDUIT_TYPE = ConduitType.Liquid;
        private const ConduitType OUTPUT_CONDUIT_TYPE = ConduitType.Gas;
        private const float DIRT_PER_LOAD = 100f;
        private const float DIRT_PRODUCTION_RATE = 1.2f;
        private const float CONTAMINATEDOXYGEN_PRODUCTION_RATE = 0.1f;
        private const float _TOTAL_PRODUCTION = 0.699f;
        private const float CRUSHEDROCK_CONSUMPTION_RATE = 0.65f;
        private const float DIRTY_WATER_CONSUMPTION_RATE = 0.039f;
        private const SimHashes EXHAUST_CONDUIT_ELEMENT = SimHashes.ContaminatedOxygen;
        public static readonly Tag COMPOST_TAG = GameTags.Compostable;


        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
            storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
            go.AddOrGet<WaterPurifier>();
            ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
            manualDeliveryKG.SetStorage(storage);
            manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
            manualDeliveryKG.requestedItemTag = new Tag("CrushedRock");
            manualDeliveryKG.capacity = 500f;
            manualDeliveryKG.refillMass = 50f;
            ManualDeliveryKG manualDeliveryKG2 = go.AddComponent<ManualDeliveryKG>();
            manualDeliveryKG2.SetStorage(storage);
            manualDeliveryKG2.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
            manualDeliveryKG2.requestedItemTag = CompostProcessorConfig.COMPOST_TAG;
            manualDeliveryKG2.capacity = 300f;
            manualDeliveryKG2.refillMass = 20f;
            ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
            conduitConsumer.conduitType = ConduitType.Liquid;
            conduitConsumer.consumptionRate = 10f;
            conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag;
            conduitConsumer.capacityKG = 0.2f;
            conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
            conduitConsumer.forceAlwaysSatisfied = true;
            ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
            conduitDispenser.conduitType = ConduitType.Gas;
            conduitDispenser.invertElementFilter = false;
            conduitDispenser.elementFilter = new SimHashes[]
            {
                SimHashes.ContaminatedOxygen
            };
            ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
            elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
            {
                new ElementConverter.ConsumedElement(new Tag("DirtyWater"), 0.039f),
                new ElementConverter.ConsumedElement(new Tag("CrushedRock"), 0.65f),
                new ElementConverter.ConsumedElement(CompostProcessorConfig.COMPOST_TAG, 0.25f)
            };
            elementConverter.outputElements = new ElementConverter.OutputElement[]
            {
                new ElementConverter.OutputElement(1.2f, SimHashes.Dirt, 300.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0),
                new ElementConverter.OutputElement(0.1f, SimHashes.ContaminatedOxygen, 315.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0)
            };
            go.AddOrGet<DropAllWorkable>();
            Prioritizable.AddRef(go);
            ElementDropper elementDropper = go.AddComponent<ElementDropper>();
            elementDropper.emitMass = 100f;
            elementDropper.emitTag = new Tag("Dirt");
            elementDropper.emitOffset = new Vector3(0f, 1f, 0f);
            Prioritizable.AddRef(go);
        }

        public override BuildingDef CreateBuildingDef()
        {
            EffectorValues tier = NOISE_POLLUTION.NOISY.TIER5;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("CompostProcessor", 4, 3, "compost_processor_kanim", 30, 30f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.ALL_METALS, 800f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER2, tier, 0.2f);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 120f;
            buildingDef.ExhaustKilowattsWhenActive = 1f;
            buildingDef.SelfHeatKilowattsWhenActive = 2f;
            buildingDef.InputConduitType = ConduitType.Liquid;
            buildingDef.OutputConduitType = ConduitType.Gas;
            buildingDef.UtilityOutputOffset = new CellOffset(0, 2);
            buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.PowerInputOffset = new CellOffset(1, 0);
            buildingDef.UtilityInputOffset = new CellOffset(0, 0);
            return buildingDef;
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
