using System;
using TUNING;
using UnityEngine;

namespace TempretureControler
{
	public class HeatingElementConfig : IBuildingConfig
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override BuildingDef CreateBuildingDef()
		{
			int num = 1;
			int num2 = 1;
			string text = "heatingElement_kanim";
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
			int num3 = 30;
			float num4 = 30f;
			float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
			float num5 = 3200f;
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("HeatingElement", num, num2, text, num3, num4, construction_mass,
				array, num5, 0, BUILDINGS.DECOR.PENALTY.TIER1, NOISE_POLLUTION.NONE, 0.2f);
			buildingDef.RequiresPowerInput = false;
			buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
			buildingDef.Floodable = false;
			buildingDef.EnergyConsumptionWhenActive = 520f;
			buildingDef.ExhaustKilowattsWhenActive = 5000f;
			buildingDef.SelfHeatKilowattsWhenActive = 20f;
			buildingDef.ViewMode = OverlayModes.Power.ID;
			buildingDef.AudioCategory = "SolidMetal";
			buildingDef.Overheatable = false;
			return buildingDef;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002107 File Offset: 0x00000307
		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			EntityTemplateExtensions.AddOrGet<MinimumOperatingTemperature>(go).minimumTemperature = 103f;
			//SpaceHeater spaceHeater = EntityTemplateExtensions.AddOrGet<SpaceHeater>(go);
			//spaceHeater.targetTemperature = 3500f;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000211A File Offset: 0x0000031A
		public override void DoPostConfigureComplete(GameObject go)
		{
			EntityTemplateExtensions.AddOrGet<LogicOperationalController>(go);
			go.GetComponent<KPrefabID>().prefabInitFn += delegate (GameObject gameObject)
			{
				new HeatingElementStateMachine.Instance(gameObject.GetComponent<KPrefabID>()).StartSM();
			};
		}

		// Token: 0x04000001 RID: 1
		public const string ID = "HeatingElement";

	}
}
