using System;
using TUNING;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class InsulatedPressureDoorConfig : IBuildingConfig
{
	// Token: 0x06000009 RID: 9 RVA: 0x000023B0 File Offset: 0x000005B0
	public override BuildingDef CreateBuildingDef()
	{
		string[] array = new string[]
		{
			"BuildableRaw",
			"RefinedMetal"
		};
		float[] construction_mass = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER5[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0]
		};
		string[] construction_materials = array;
		EffectorValues none = NOISE_POLLUTION.NONE;
		EffectorValues tier = BUILDINGS.DECOR.PENALTY.TIER1;
		EffectorValues noise = none;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("InsulatedPressureDoor", 1, 2, "Insulated_door_external_kanim", 30, 60f, construction_mass, construction_materials, 1600f, BuildLocationRule.Tile, tier, noise, 1f);
		buildingDef.ThermalConductivity = 0.01f;
		buildingDef.Overheatable = false;
		buildingDef.RequiresPowerInput = false;
		//buildingDef.EnergyConsumptionWhenActive = 180f;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.IsFoundation = true;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.TileLayer = ObjectLayer.FoundationTile;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R90;
		buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
		buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
		buildingDef.LogicInputPorts = DoorConfig.CreateSingleInputPortList(new CellOffset(0, 0));
		SoundEventVolumeCache.instance.AddVolume("door_external_kanim", "Open_DoorPressure", NOISE_POLLUTION.NOISY.TIER2);
		SoundEventVolumeCache.instance.AddVolume("door_external_kanim", "Close_DoorPressure", NOISE_POLLUTION.NOISY.TIER2);
		return buildingDef;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000024F0 File Offset: 0x000006F0
	public override void DoPostConfigureComplete(GameObject go)
	{
		Door door = go.AddOrGet<Door>();
		door.hasComplexUserControls = true;
		door.unpoweredAnimSpeed = 0.65f;
		door.poweredAnimSpeed = 8f;
		door.doorClosingSoundEventName = "MechanizedAirlock_closing";
		door.doorOpeningSoundEventName = "MechanizedAirlock_opening";
		go.AddOrGet<Insulator>();
		go.AddOrGet<ZoneTile>();
		go.AddOrGet<AccessControl>();
		go.AddOrGet<KBoxCollider2D>();
		Prioritizable.AddRef(go);
		go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Door;
		go.AddOrGet<Workable>().workTime = 5f;
		UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
		go.GetComponent<AccessControl>().controlEnabled = true;
		go.GetComponent<KBatchedAnimController>().initialAnim = "closed";
	}

	// Token: 0x04000009 RID: 9
	public const string ID = "InsulatedPressureDoor";

	// Token: 0x0400000A RID: 10
	public const string menu = "Base";

	// Token: 0x0400000B RID: 11
	public const string pred = "PressureDoor";

	// Token: 0x0400000C RID: 12
	public const string tech = "HVAC";
}
