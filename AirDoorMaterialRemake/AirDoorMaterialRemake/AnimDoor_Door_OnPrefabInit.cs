using System;
using HarmonyLib;

// Token: 0x02000007 RID: 7
[HarmonyPatch(typeof(Door), "OnPrefabInit")]
internal class AnimDoor_Door_OnPrefabInit
{
	// Token: 0x06000011 RID: 17 RVA: 0x000027DF File Offset: 0x000009DF
	private static void Postfix(ref Door __instance)
	{
		__instance.overrideAnims = new KAnimFile[]
		{
			Assets.GetAnim("anim_use_remote_kanim")
		};
	}
}
