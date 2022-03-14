namespace Chemical_Processing.Mineral_Processing
{
    using System;
    using System.Collections.Generic;
    using KSerialization;
    using NightLib;
    using NightLib.AddBuilding;
    using TUNING;
    using UnityEngine;

    [SerializationConfig(MemberSerialization.OptIn)]
    public class PetroleumDistilleryConfig : IBuildingConfig
    {
        public const string ID = "PetroleumDistillery";
        public const SimHashes INPUT_ELEMENT = SimHashes.CrudeOil;
        private const SimHashes OUTPUT_LIQUID_ELEMENT = SimHashes.Petroleum;
        public const float CONSUMPTION_RATE = 5f;
        public const float PETROLEUM_RATE = 2.5f;
        public const float SOLID_WASTE_RATE = 0.1f;
        public const float METHANE_WASTE_RATE = 0.05f;
        public const float OUTPUT_TEMPERATURE = 346.5f;
        public const float WASTE_OUTPUT_TEMPERATURE = 366.5f;

        private static readonly PortDisplayOutput MethaneOutputPort = new PortDisplayOutput(ConduitType.Gas, new CellOffset(1, 1));

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            go.AddOrGet<BuildingComplete>().isManuallyOperated = false;

            ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
            conduitConsumer.conduitType = ConduitType.Liquid;
            conduitConsumer.consumptionRate = 5f;
            conduitConsumer.capacityTag = SimHashes.CrudeOil.CreateTag();
            conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
            conduitConsumer.capacityKG = 100f;
            conduitConsumer.forceAlwaysSatisfied = true;

            ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
            conduitDispenser.conduitType = ConduitType.Liquid;
            conduitDispenser.invertElementFilter = true;
            conduitDispenser.elementFilter = new SimHashes[]{SimHashes.CrudeOil};
            go.AddOrGet<Storage>().showInUI = true;

            ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
            elementConverter.consumedElements = new ElementConverter.ConsumedElement[]{new ElementConverter.ConsumedElement(SimHashes.CrudeOil.CreateTag(), 5f)};
            elementConverter.outputElements = new ElementConverter.OutputElement[]
            {
                new ElementConverter.OutputElement(2.5f, SimHashes.Petroleum, 296.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0),
                new ElementConverter.OutputElement(0.5f, SimHashes.Bitumen, 300.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0),
                new ElementConverter.OutputElement(0.1f, SimHashes.Methane, 300.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0)
            };

            Prioritizable.AddRef(go);
            AlgaeDistillery algaeDistillery = go.AddOrGet<AlgaeDistillery>();
            algaeDistillery.emitMass = 20f;
            algaeDistillery.emitTag = new Tag("Bitumen");
            algaeDistillery.emitOffset = new Vector3(2f, 1f);

            PipedDispenser dispenser = go.AddComponent<PipedDispenser>();
            dispenser.elementFilter = new SimHashes[] { SimHashes.Methane };
            dispenser.AssignPort(MethaneOutputPort);
            dispenser.alwaysDispense = true;
            dispenser.SkipSetOperational = true;

            PipedOptionalExhaust exhaust = go.AddComponent<PipedOptionalExhaust>();
            exhaust.dispenser = dispenser;
            exhaust.elementHash = SimHashes.Methane;
            exhaust.elementTag = SimHashes.Methane.CreateTag();
            exhaust.capacity = 0.2f;
            this.AttachPort(go);

            Prioritizable.AddRef(go);
        }

        private void AttachPort(GameObject go)
        {
            PortDisplayController controller = go.AddComponent<PortDisplayController>();
            controller.Init(go);

            controller.AssignPort(go, MethaneOutputPort);
        }

        public override BuildingDef CreateBuildingDef()
        {
            EffectorValues tier = NOISE_POLLUTION.NOISY.TIER5;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("PetroleumDistillery", 4, 3, "petroleum_distillery_kanim", 100, 30f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.ALL_METALS, 800f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER1, tier, 0.2f);
            buildingDef.Overheatable = false;
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 240f;
            buildingDef.ExhaustKilowattsWhenActive = 0.5f;
            buildingDef.SelfHeatKilowattsWhenActive = 4f;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
            buildingDef.OutputConduitType = ConduitType.Liquid;
            buildingDef.PowerInputOffset = new CellOffset(1, 0);
            buildingDef.InputConduitType = ConduitType.Liquid;
            buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
            buildingDef.OutputConduitType = ConduitType.Liquid;
            buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
            return buildingDef;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            this.AttachPort(go);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            this.AttachPort(go);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGetDef<PoweredActiveController.Def>();
        }
    }
}
