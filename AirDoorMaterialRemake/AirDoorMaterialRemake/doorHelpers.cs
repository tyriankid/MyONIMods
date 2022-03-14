using System;
using System.Collections.Generic;
using TUNING;

namespace InsulatedDoorsMod
{
	// Token: 0x0200000B RID: 11
	public class doorHelpers
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002A34 File Offset: 0x00000C34
		public static void DoorBuildMenu(string door, string menu, string pred)
		{
			int num = BUILDINGS.PLANORDER.FindIndex((PlanScreen.PlanInfo x) => x.category == menu);
			if (num < 0)
			{
				return;
			}
			IList<string> data = BUILDINGS.PLANORDER[num].data;
			int num2 = -1;
			foreach (string text in data)
			{
				if (text.Equals(pred))
				{
					num2 = data.IndexOf(text);
				}
			}
			if (num2 == -1)
			{
				return;
			}
			data.Insert(num2 + 1, door);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002ADC File Offset: 0x00000CDC
		public static void doorTechTree(string door, string group)
		{
			if (group == "none")
			{
				return;
			}
			Tech tech = Db.Get().Techs.TryGet(group);
			if (tech != null)
			{
				tech.unlockedItemIDs.Add(door);
			}
		}
	}
}
