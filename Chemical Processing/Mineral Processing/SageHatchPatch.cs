namespace Chemical_Processing.Mineral_Processing
{
    using System;
    using UnityEngine;
    using HarmonyLib;
    using System.Collections.Generic;

    public class CustomHatchDiet
    {
        [HarmonyPatch(typeof(BaseHatchConfig))]
        [HarmonyPatch("VeggieDiet")]
        public class SageHatchDiet
        {
            public static void Postfix(ref List<Diet.Info> __result, ref Tag poopTag, ref float caloriesPerKg, ref float producedConversionRate, ref string diseaseId, ref float diseasePerKgProduced)
            {
                __result.Add(new Diet.Info(new HashSet<Tag>(new Tag[]
                {
                    SimHashes.CrushedRock.CreateTag()
                }), (poopTag == GameTags.Metal) ? SimHashes.Carbon.CreateTag() : poopTag, caloriesPerKg, producedConversionRate, diseaseId, diseasePerKgProduced, false, false));
            }
        }
    }
}

