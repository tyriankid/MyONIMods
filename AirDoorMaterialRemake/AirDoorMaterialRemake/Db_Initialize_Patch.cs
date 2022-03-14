using System;
using HarmonyLib;

namespace InsulatedDoorsMod
{
	// Token: 0x02000009 RID: 9
	[HarmonyPatch(typeof(Db))]
	[HarmonyPatch("Initialize")]
	public static class Db_Initialize_Patch
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000028C6 File Offset: 0x00000AC6
		public static void Prefix()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028C8 File Offset: 0x00000AC8
		public static void Postfix()
		{
			doorHelpers.DoorBuildMenu("InsulatedManualPressureDoor", "Base", "ManualPressureDoor");
			doorHelpers.DoorBuildMenu("InsulatedPressureDoor", "Base", "PressureDoor");
			doorHelpers.DoorBuildMenu("TinyInsulatedManualPressureDoor", "Base", "InsulatedManualPressureDoor");
			doorHelpers.DoorBuildMenu("TinyInsulatedManualPressureGasDoor", "Base", "TinyInsulatedManualPressureDoor");
			doorHelpers.DoorBuildMenu("TinyInsulatedPressureDoor", "Base", "InsulatedPressureDoor");
			doorHelpers.doorTechTree("InsulatedManualPressureDoor", "TemperatureModulation");
			doorHelpers.doorTechTree("InsulatedPressureDoor", "HVAC");
			doorHelpers.doorTechTree("TinyInsulatedManualPressureDoor", "TemperatureModulation");
			doorHelpers.doorTechTree("TinyInsulatedManualPressureGasDoor", "TemperatureModulation");
			doorHelpers.doorTechTree("TinyInsulatedPressureDoor", "HVAC");
		}
	}
}
