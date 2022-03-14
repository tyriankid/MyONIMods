using System;
using TUNING;
using UnityEngine;

namespace TempretureControler
{
	public class CoolingElementConfig : IBuildingConfig
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override BuildingDef CreateBuildingDef()
		{
			int num = 1;
			int num2 = 1;
			string text = "coolingElement_kanim";
			int num3 = 30;
			float num4 = 30f;
			float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
			float num5 = 3200f;
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("CoolingElement", num, num2, text, num3, num4, tier, MATERIALS.ANY_BUILDABLE, num5, 0, BUILDINGS.DECOR.PENALTY.TIER1, NOISE_POLLUTION.NONE, 0.2f);
			buildingDef.RequiresPowerInput = false;
			buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
			buildingDef.Floodable = false;
			buildingDef.EnergyConsumptionWhenActive = 400f;
			buildingDef.ExhaustKilowattsWhenActive = -800f;
			buildingDef.SelfHeatKilowattsWhenActive = 0f;
			buildingDef.ViewMode = OverlayModes.Power.ID;
			buildingDef.AudioCategory = "SolidMetal";
			buildingDef.Overheatable = false;
			return buildingDef;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002107 File Offset: 0x00000307
		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			EntityTemplateExtensions.AddOrGet<MinimumOperatingTemperature>(go).minimumTemperature = 103f;
			//go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Any;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000211A File Offset: 0x0000031A
		public override void DoPostConfigureComplete(GameObject go)
		{
			EntityTemplateExtensions.AddOrGet<LogicOperationalController>(go);
			go.GetComponent<KPrefabID>().prefabInitFn += delegate (GameObject gameObject)
			{
				new CoolingElementStateMachine.Instance(gameObject.GetComponent<KPrefabID>()).StartSM();
			};
			BuildingTemplates.DoPostConfigure(go);
			//EntityTemplateExtensions.AddOrGet<CoolingElementSideScreen>(go);
			
		}

		// Token: 0x04000001 RID: 1
		public const string ID = "CoolingElement";


	}
}
