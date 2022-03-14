using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace InsulatedDoorsMod
{
	// Token: 0x02000008 RID: 8
	[HarmonyPatch(typeof(Localization), "Initialize")]
	public static class Loc_Initialize_Patch
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002808 File Offset: 0x00000A08
		public static void Postfix()
		{
			Loc_Initialize_Patch.Translate(typeof(STRINGS));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000281C File Offset: 0x00000A1C
		public static void Translate(Type root)
		{
			Localization.RegisterForTranslation(root);
			string output_folder = Loc_Initialize_Patch.LoadStrings();
			LocString.CreateLocStringKeys(root, null);
			Localization.GenerateStringsTemplate(root, output_folder);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002844 File Offset: 0x00000A44
		private static string LoadStrings()
		{
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			int num = directoryName.IndexOf("archived_versions");
			string path;
			if (num != -1)
			{
				path = directoryName.Substring(0, num - 1);
			}
			else
			{
				path = directoryName;
			}
			string text = Path.Combine(path, "translations");
			string path2 = text;
			Localization.Locale locale = Localization.GetLocale();
			string path3 = Path.Combine(path2, ((locale != null) ? locale.Code : null) + ".po");
			if (File.Exists(path3))
			{
				Localization.OverloadStrings(Localization.LoadStringsFile(path3, false));
			}
			return text;
		}
	}
}
