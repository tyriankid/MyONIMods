using HarmonyLib;
using PeterHan.PLib.Options;

namespace TempretureControler
{
    public class Patches
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            //public static void Prefix()
            //{
            //    Debug.Log("I execute before Db.Initialize!");
            //}
            public static void Postfix()
            {
                Db.Get().Techs.Get("TemperatureModulation").unlockedItemIDs.Add("CoolingElement");
                Db.Get().Techs.Get("TemperatureModulation").unlockedItemIDs.Add("HeatingElement");
                Debug.Log("I execute after Db.Initialize!");
            }
        }


        [HarmonyPatch(typeof(CoolingElementConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class Initialize_Patch_c0
        {
            public static void Postfix(CoolingElementConfig __instance, ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = (float)SingletonOptions<TestModSettings>.Instance.coolingDTU;// new CoolingElementSideScreen().sliderDTUS; //
            }
        }

        [HarmonyPatch(typeof(CoolingElementConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class Initialize_Patch_c1
        {
            public static void Postfix(CoolingElementConfig __instance, ref BuildingDef __result)
            {
                __result.RequiresPowerInput = (bool)SingletonOptions<TestModSettings>.Instance.coolingRequiresPower;
            }
        }

        [HarmonyPatch(typeof(CoolingElementConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class Initialize_Patch_c2
        {
            public static void Postfix(CoolingElementConfig __instance, ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = (float)SingletonOptions<TestModSettings>.Instance.coolingPower;
            }
        }

        [HarmonyPatch(typeof(HeatingElementConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class Initialize_Patch_h0
        {
            public static void Postfix(HeatingElementConfig __instance, ref BuildingDef __result)
            {
                __result.ExhaustKilowattsWhenActive = (float)SingletonOptions<TestModSettings>.Instance.heatingDTU;
            }
        }

        [HarmonyPatch(typeof(HeatingElementConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class Initialize_Patch_h1
        {
            public static void Postfix(HeatingElementConfig __instance, ref BuildingDef __result)
            {
                __result.RequiresPowerInput = (bool)SingletonOptions<TestModSettings>.Instance.heatingRequiresPower;
            }
        }

        [HarmonyPatch(typeof(HeatingElementConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        public class Initialize_Patch_h3
        {
            public static void Postfix(HeatingElementConfig __instance, ref BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = (float)SingletonOptions<TestModSettings>.Instance.heatingPower;
            }
        }
    }
}
    
