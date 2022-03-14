using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Newtonsoft.Json;
using PeterHan.PLib.Database;
using PeterHan.PLib.Options;

namespace TempretureControler
{
    [JsonObject(MemberSerialization.OptIn)]
    [ConfigFile("TempretureControler.json", true, false)]
    [RestartRequired]
    public class TestModSettings
    {
        [Option("制冷强度 (KDTU/S)", "可以手动调节制冷器的制冷强度效果，单位：千DTU每秒",null)]
        [Limit(-2000,1)]
        [JsonProperty]
        public float coolingDTU { get; set; }

        [Option("制冷器是否需要电力 ", "可以手动开关制冷器的电力需求", null)]
        [Limit(1, 10000)]
        [JsonProperty]
        public bool coolingRequiresPower { get; set; }

        [Option("制冷器功率 (W)", "可以手动调节制冷器功率", null)]
        [Limit(1, 10000)]
        [JsonProperty]
        public float coolingPower { get; set; }

        [Option("制热强度 (KDTU/S)", "可以手动调节发热器的发热强度效果，单位：千DTU每秒",null)]
        [Limit(1, 10000)]
        [JsonProperty]
        public float heatingDTU { get; set; }

        [Option("发热器是否需要电力 ", "可以手动开关发热器的电力需求", null)]
        [Limit(1, 10000)]
        [JsonProperty]
        public bool heatingRequiresPower { get; set; }

        [Option("发热器功率 (W)", "可以手动调节发热器功率", null)]
        [Limit(1, 10000)]
        [JsonProperty]
        public float heatingPower { get; set; }



        public TestModSettings()
        {
            coolingDTU = -200f;
            heatingDTU = 500f;
            coolingRequiresPower = false;
            coolingPower = 360f;
            heatingRequiresPower = false;
            heatingPower = 420f;
        }

    }

}
