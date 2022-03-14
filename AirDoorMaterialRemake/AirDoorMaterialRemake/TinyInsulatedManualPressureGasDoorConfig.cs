using System;
using TUNING;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class TinyInsulatedManualPressureGasDoorConfig : IBuildingConfig
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002200 File Offset: 0x00000400
	public override BuildingDef CreateBuildingDef()
	{
		string[] array = new string[]
		{
			"BuildableRaw",
			"Metal"
		};
		float[] construction_mass = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0]
		};
		string[] construction_materials = array;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("TinyInsulatedManualPressureGasDoor", 1, 1, "Tiny_Insulated_door_manual_kanim", 30, 60f, construction_mass, construction_materials, 1600f, BuildLocationRule.Tile, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NONE, 1f);
		buildingDef.ThermalConductivity = 0.01f;
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.IsFoundation = true;
		buildingDef.TileLayer = ObjectLayer.FoundationTile;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
		buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_gear_LP", NOISE_POLLUTION.NOISY.TIER1);
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_open", NOISE_POLLUTION.NOISY.TIER2);
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_close", NOISE_POLLUTION.NOISY.TIER2);
		
		return buildingDef;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002310 File Offset: 0x00000510
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		//base.ConfigureBuildingTemplate(go, prefab_tag);

		//BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);

        Door door = go.AddOrGet<Door>();
        door.hasComplexUserControls = true;
        door.unpoweredAnimSpeed = 0.65f;
        door.doorType = Door.DoorType.ManualPressure;


        SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();

		simCellOccupier.setLiquidImpermeable = true;//不透水
		simCellOccupier.setGasImpermeable = true;//不透气

		simCellOccupier.doReplaceElement = false;
		simCellOccupier.notifyOnMelt = true;

        go.AddOrGet<Insulator>();
        go.AddOrGet<ZoneTile>();
        go.AddOrGet<AccessControl>();
        go.AddOrGet<KBoxCollider2D>();
        go.AddOrGet<TileTemperature>();
		Prioritizable.AddRef(go);
		go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Door;
		go.AddOrGet<Workable>().workTime = 5f;
		
		//UnityEngine.Object.DestroyImmediate(go.GetComponent<Switch>());

		
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000238A File Offset: 0x0000058A
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<OperationalController.Def>();


		//DoorRotatableConsumer elementConsumer1 = go.AddComponent<DoorRotatableConsumer>();
		//elementConsumer1.configuration = ElementConsumer.Configuration.AllGas;
		//elementConsumer1.consumptionRate = 500f;//suckRate;
		//elementConsumer1.storeOnConsume = true;
		//elementConsumer1.showInStatusPanel = false;
		//elementConsumer1.consumptionRadius = 1;
		//elementConsumer1.rotatableCellOffset = new Vector3(0f, -1f);
		//elementConsumer1.showDescriptor = false;

		//go.AddOrGetDef<PoweredActiveController.Def>();
		//BuildingTemplates.DoPostConfigure(go);
		var storage = go.AddOrGet<Storage>();
		storage.capacityKg = 50000f;
		storage.showCapacityStatusItem = true;

		//新增物质生成
		go.AddOrGet<GasSucker>();
		//新增可筛选元素
		go.AddOrGet<FilterbleElement>().elementState = Filterable.ElementState.Gas;
		//go.AddOrGet<FilterbleElementGas>().SelectedTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		//go.AddOrGet<FilterbleElementLiquid>().elementState = Filterable.ElementState.Liquid;

		//EntityTemplateExtensions.AddOrGet<FilterbleElement>(go);

		//go.AddOrGet<FilterbleElement>().elementState = Filterable.ElementState.Gas;

		//FanMine fan = go.AddOrGet<FanMine>();
		//fan.conduitType = ConduitType.Gas;
		//fan.overpressureMass = -1f;
		//go.GetComponent<KPrefabID>().AddTag(GameTags.FloorTiles, false);
		//go.GetComponent<KPrefabID>().prefabInitFn += delegate (GameObject gameObject)
		//{
		//	new TinySuckDoorStateMachine.Instance(gameObject.GetComponent<KPrefabID>()).StartSM();
		//};
	}

	// Token: 0x04000005 RID: 5
	public const string ID = "TinyInsulatedManualPressureGasDoorConfig";

	// Token: 0x04000006 RID: 6
	public const string menu = "Base";

	// Token: 0x04000007 RID: 7
	public const string pred = "InsulatedManualPressureGasDoor";

	// Token: 0x04000008 RID: 8
	public const string tech = "TemperatureModulation";
}
