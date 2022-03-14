using System;
using HarmonyLib;

    [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
    public class GeneratedBuildings_LoadGeneratedBuildings
{
		// Token: 0x06000008 RID: 8 RVA: 0x0000229C File Offset: 0x0000049C
		public static void Prefix()
		{
			Strings.Add(new string[]
			{
				"STRINGS.BUILDINGS.PREFABS.COOLINGELEMENT.NAME",
				"制冷器"
			});
			Strings.Add(new string[]
			{
				"STRINGS.BUILDINGS.PREFABS.COOLINGELEMENT.DESC",
				"一个可以在任何地方建造的发热器，可自由配置是否用电、用电功率、制冷效率，可用自动化控制，停止工作温度：-170.2°"
			});
			Strings.Add(new string[]
			{
				"STRINGS.BUILDINGS.PREFABS.COOLINGELEMENT.EFFECT",
				"在范围内吸收热量，可用大部分物质建造"
			});
			ModUtil.AddBuildingToPlanScreen("Utilities", "CoolingElement");

			Strings.Add(new string[]
			{
					"STRINGS.BUILDINGS.PREFABS.HEATINGELEMENT.NAME",
					"发热器"
			});
			Strings.Add(new string[]
			{
					"STRINGS.BUILDINGS.PREFABS.HEATINGELEMENT.DESC",
					"一个可以在任何地方建造的发热器，可自由配置是否用电、用电功率、发热效率，可用自动化控制"
			});
			Strings.Add(new string[]
			{
					"STRINGS.BUILDINGS.PREFABS.HEATINGELEMENT.EFFECT",
					"在范围内释放热量，建议使用隔热性强的材料，不易融化"
			});
			ModUtil.AddBuildingToPlanScreen("Utilities", "HeatingElement");
	}
	}

