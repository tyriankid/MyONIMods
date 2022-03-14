using System;
using HarmonyLib;

namespace InsulatedDoorsMod
{
	// Token: 0x0200000A RID: 10
	[HarmonyPatch(typeof(BuildingComplete), "OnSpawn")]
	public class InsulatedDoor_BuildingComplete_OnSpawn
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002964 File Offset: 0x00000B64
		public static void Postfix(ref BuildingComplete __instance)
		{
			if (string.Compare(__instance.name, "InsulatedManualPressureDoorComplete") == 0)
			{
				__instance.gameObject.AddOrGet<InsulatingDoor>();
			}
			if (string.Compare(__instance.name, "InsulatedPressureDoorComplete") == 0)
			{
				__instance.gameObject.AddOrGet<InsulatingDoor>();
			}
			//7合1多功能抽液门
			if (string.Compare(__instance.name, "TinyInsulatedManualPressureDoorComplete") == 0)
			{
				__instance.gameObject.AddOrGet<InsulatingDoor>();
			}
			
			if (string.Compare(__instance.name, "TinyInsulatedPressureDoorComplete") == 0)
			{
				__instance.gameObject.AddOrGet<InsulatingDoor>();
			}
			//7和1多功能抽气门
			if (string.Compare(__instance.name, "TinyInsulatedPressureGasDoorComplete") == 0)
			{
				__instance.gameObject.AddOrGet<InsulatingDoor>();
			}

			InsulatingDoor component = __instance.gameObject.GetComponent<InsulatingDoor>();
			if (component != null)
			{
				component.SetInsulation(__instance.gameObject, component.door.building.Def.ThermalConductivity);
			}
		}
	}
}
