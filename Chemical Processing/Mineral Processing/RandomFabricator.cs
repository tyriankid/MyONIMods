using System.Collections.Generic;
using HarmonyLib;
using STRINGS;
using KMod;
using Klei;
using System.Linq;
using ProcGen;
using Chemical_Processing.Chemicals;

// Random Fabricator was made by my friend Pether.pg. All credits for the creation goes to him, so thank you Pether!
// You all can check Pether's mod here on workshop: https://steamcommunity.com/profiles/76561198079671938/myworkshopfiles/

namespace Chemical_Processing.Mineral_Processing
{
    public class RandomFabricator
    {
        public static void SpawnRandoms(ComplexFabricator cf, ComplexRecipe.RecipeElement[] ingredients)
        {
            List<SimHashes> possibles;

            //==> COPPER DRILL <========================================================================================
            //Formula: (100 / total n# ) * number of elements 
            if (ingredients[0].material == SimHashes.Copper.CreateTag())
            {
                possibles = new List<SimHashes>() // n# = 9
                {
                    SimHashes.Sand,    // 33,5%
                    SimHashes.Sand,
                    SimHashes.Sand,

                    SimHashes.Dirt,   // 22,5%
                    SimHashes.Dirt,

                    SimHashes.CrushedRock, // 11%
                    SimHashes.Sulfur,
                    SimHashes.Carbon,
                    SimHashes.Algae
                };
            }

            //==> IRON DRILL <==========================================================================================
            else if (ingredients[0].material == SimHashes.Iron.CreateTag())
            {
                possibles = new List<SimHashes>() // n# = 14
                {
                    OilShaleElement.SolidOilShaleSimHash, // 28,5%
                    OilShaleElement.SolidOilShaleSimHash,
                    OilShaleElement.SolidOilShaleSimHash,
                    OilShaleElement.SolidOilShaleSimHash,

                    SimHashes.CrushedRock,                // 21,5%
                    SimHashes.CrushedRock,
                    SimHashes.CrushedRock,


                    SimHashes.AluminumOre,                          // 7%
                    SimHashes.IronOre,                              // 7%
                    SimHashes.Cuprite,                              // 7%
                    ArgentiteElement.ArgentiteOreSimHash,           // 7%
                    AurichalciteElement.AurichalciteOreSimHash,     // 7%
                    SimHashes.GoldAmalgam,                          // 7%
                    SimHashes.Salt                                  // 7%
                };
            }

            //==> STEEL DRILL <=========================================================================================
            else if (ingredients[0].material == SimHashes.Steel.CreateTag())
            {
                possibles = new List<SimHashes>() // n# = 9
                {
                    OilShaleElement.SolidOilShaleSimHash,  // 44,5%
                    OilShaleElement.SolidOilShaleSimHash,
                    OilShaleElement.SolidOilShaleSimHash,
                    OilShaleElement.SolidOilShaleSimHash,

                    SimHashes.CrushedRock,                 // 22,5%
                    SimHashes.CrushedRock,

                    SimHashes.Fossil,                      // 11%
                    SimHashes.Phosphorite,                 // 11%
                    SimHashes.Wolframite                   // 11%
                };
            }

            ////===> TUNGSTEN DRILL - DLC1 <=============================================================================
            //else if (ingredients[0].material == SimHashes.Tungsten.CreateTag() && DlcManager.IsExpansion1Active())
            //{
            //    possibles = new List<SimHashes>()
            //    {
            //        SimHashes.Katairite,                //37,5%
            //        SimHashes.Katairite,
            //        SimHashes.Katairite,

            //        SimHashes.Fossil,                   //25%
            //        SimHashes.Fossil,

            //        SimHashes.Diamond,                  //12,5%
            //        SimHashes.Graphite,                 //12,5%
            //        SimHashes.UraniumOre                //12,5%
            //    };
            //}

            ////===> TUNGSTEN DRILL - VANILLA <=========================================================================
            //else if (ingredients[0].material == SimHashes.Tungsten.CreateTag() && DlcManager.IsPureVanilla())
            //{
            //    possibles = new List<SimHashes>()
            //    {
            //        SimHashes.Katairite,                //50%
            //        SimHashes.Katairite,
            //        SimHashes.Katairite,

            //        SimHashes.Fossil,                   //33%
            //        SimHashes.Fossil,

            //        SimHashes.Diamond,                  //17%
            //    };
            //}

            //==> COMMON DROP <=========================================================================================
            else
                possibles = new List<SimHashes>() { SimHashes.Sand, SimHashes.CrushedRock };

            SimHashes spawned = possibles[UnityEngine.Random.Range(0, possibles.Count)];
            int amount = UnityEngine.Random.Range(50, 250);

            //Debug.Log($"FabricatorTest: Spawning {amount} kg of {spawned}");

            Element element = ElementLoader.GetElement(spawned.CreateTag());
            float temperature = 80 + 273.15f;
            int cell = Grid.PosToCell(cf.gameObject.transform.GetPosition());
            if (!element.IsLiquid && amount > 0)
                element.substance.SpawnResource(Grid.CellToPosCCC(cell, Grid.SceneLayer.Ore), amount, temperature, 0, 0);
        }
    }

    [HarmonyPatch(typeof(ComplexFabricator))]
    [HarmonyPatch("Sim200ms")]
    public static class Custom_ComplexFabricator
    {
        static float randomProgress = 7;

        public static void Prefix(ComplexFabricator __instance, float dt)
        {
            if (__instance.gameObject.name != MineralDrillConfig.ID + "Complete") return;
            if (__instance.CurrentWorkingOrder == null) return;

            randomProgress -= dt;
            if (randomProgress <= 0)
            {
                randomProgress = UnityEngine.Random.Range(10,10);
                //Debug.Log($"FabricatorTest: New random progress: {randomProgress}");
                RandomFabricator.SpawnRandoms(__instance, __instance.CurrentWorkingOrder.ingredients);
            }
        }
    }
}
