using System;
using STRINGS;

namespace InsulatedDoorsMod
{
	// Token: 0x0200000C RID: 12
	public class STRINGS
	{
		// Token: 0x0200000E RID: 14
		public class BUILDINGS
		{
			// Token: 0x0200000F RID: 15
			public class PREFABS
			{
				// Token: 0x02000010 RID: 16
				public class INSULATEDMANUALPRESSUREDOOR
				{
					// Token: 0x04000013 RID: 19
					public static LocString NAME = UI.FormatAsLink("Insulated Manual Airlock", "InsulatedManualPressureDoor");

					// Token: 0x04000014 RID: 20
					public static LocString DESC = "The lowered thermal conductivity of insulated door slows any heat passing through them.";

					// Token: 0x04000015 RID: 21
					public static LocString EFFECT = "Mantain temperature between two rooms";
				}

				// Token: 0x02000011 RID: 17
				public class INSULATEDPRESSUREDOOR
				{
					// Token: 0x04000016 RID: 22
					public static LocString NAME = UI.FormatAsLink("Insulated Mechanized Airlock", "InsulatedPressureDoor");

					// Token: 0x04000017 RID: 23
					public static LocString DESC = "Tiny Mechanized Airlocks fitted for very tiny duplicant.";

					// Token: 0x04000018 RID: 24
					public static LocString EFFECT = "Mantain temperature between two rooms";
				}

				// Token: 0x02000012 RID: 18
				public class TINYINSULATEDMANUALPRESSUREDOOR
				{
					// Token: 0x04000019 RID: 25
					public static LocString NAME = UI.FormatAsLink("`Tiny Insulated Manual Airlock", "TinyInsulatedManualPressureDoor");

					// Token: 0x0400001A RID: 26
					public static LocString DESC = "The lowered thermal conductivity of insulated door slows any heat passing through them.";

					// Token: 0x0400001B RID: 27
					public static LocString EFFECT = "Mantain temperature between two rooms";
				}
				public class TINYINSULATEDMANUALPRESSUREGASDOOR
				{
					// Token: 0x04000019 RID: 25
					public static LocString NAME = UI.FormatAsLink("`Tiny Insulated Manual Airlock", "TinyInsulatedManualPressureGasDoor");

					// Token: 0x0400001A RID: 26
					public static LocString DESC = "The lowered thermal conductivity of insulated door slows any heat passing through them.";

					// Token: 0x0400001B RID: 27
					public static LocString EFFECT = "Mantain temperature between two rooms";
				}
				
				// Token: 0x02000013 RID: 19
				public class TINYINSULATEDPRESSUREDOOR
				{
					// Token: 0x0400001C RID: 28
					public static LocString NAME = UI.FormatAsLink("Tiny Insulated Mechanized Airlock", "TinyInsulatedPressureDoor");

					// Token: 0x0400001D RID: 29
					public static LocString DESC = "Tiny Mechanized Airlocks fitted for very tiny duplicant.";

					// Token: 0x0400001E RID: 30
					public static LocString EFFECT = "Mantain temperature between two rooms";
				}
			}
		}
	}
}
