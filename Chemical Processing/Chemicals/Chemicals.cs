using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Klei;
using KMod;
using Newtonsoft.Json;
using ProcGen;
using STRINGS;

// Custom Geyser world spawn code was taken from daviscook477 Ethanol Geyser mod. Davis had the kindness of sharing the source of
// his mod through his Github page: https://github.com/daviscook477/ONI-Mods/tree/master/src/EthanolGeyser
// Thank you David for updating your github version of the mod when I asked!

namespace Chemical_Processing.Chemicals
{
    public class Chemicals : UserMod2
    {
        [HarmonyPatch(typeof(Worlds), "UpdateWorldCache")]
        public class Worlds_UpdateWorldCache_Patch
        {
            private static void Postfix(Worlds __instance, string path, string prefix, ISet<string> referencedWorlds, List<YamlIO.Error> errors)
            {
                var json = "{\"names\":[\"poi/poi_mineral_deposit_node\"],\"listRule\":5,\"someCount\":1,\"moreCount\":1,\"times\":1,\"priority\":10,\"allowDuplicates\":false,\"allowExtremeTemperatureOverlap\":false,\"useRelaxedFiltering\":false,\"allowedCellsFilter\":[{\"tagcommand\":3,\"tag\":\"AtDepths\",\"minDistance\":2,\"maxDistance\":10,\"command\":1,\"temperatureRanges\":[],\"zoneTypes\":[],\"subworldNames\":[]},{\"tagcommand\":0,\"tag\":null,\"minDistance\":0,\"maxDistance\":0,\"command\":1,\"temperatureRanges\":[],\"zoneTypes\":[4],\"subworldNames\":[]},{\"tagcommand\":1,\"tag\":\"NoGlobalFeatureSpawning\",\"minDistance\":0,\"maxDistance\":0,\"command\":4,\"temperatureRanges\":[],\"zoneTypes\":[],\"subworldNames\":[]}]}";

                var templateSpawnRules = JsonConvert.DeserializeObject<ProcGen.World.TemplateSpawnRules>(json, new JsonSerializerSettings()
                {
                    ContractResolver = new PrivateResolver(),
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });

                foreach (var world in __instance.worldCache)
                {
                    if (!world.Value.worldTemplateRules.Any(x => x.names.Contains("poi_mineral_deposit_node")))
                    {
                        //UnityEngine.Debug.Log(JsonConvert.SerializeObject(world.Value));
                        world.Value.worldTemplateRules.Add(templateSpawnRules);
                    }
                }
            }
        }
    }
}