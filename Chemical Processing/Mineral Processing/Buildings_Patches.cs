namespace Chemical_Processing.Mineral_Processing
{
    using System.Collections.Generic;
    using HarmonyLib;
    using STRINGS;
    using KMod;
    using Klei;
    using System.Linq;
    using ProcGen;
    using Newtonsoft.Json;
    using Chemical_Processing.Chemicals;


    //===> MINERAL DEPOSIT <==========================================================================================
    namespace MineralDeposit_Patch
    {
        [HarmonyPatch(typeof(EntityConfigManager))][HarmonyPatch("LoadGeneratedEntities")]
        public static class EntityConfigManager_LoadGeneratedEntities_Patch
        {
            public static void Prefix()
            {
                Strings.Add($"STRINGS.CREATURES.SPECIES.{MineralDepositConfig.ID.ToUpper()}.NAME", MineralDepositConfig.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{MineralDepositConfig.ID.ToUpper()}.DESC", MineralDepositConfig.Description);
            }
        }

    }

    //===> MINERAL DRILL <============================================================================================
    namespace MineralDrill_Patch
    {
        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class MineralDrillTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("HighTempForging").unlockedItemIDs.Add("MineralDrill");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class MineralDrillUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.MINERALDRILL.NAME",
                "凿岩机"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.MINERALDRILL.DESC",
                "钻入矿藏获取固体资源."
                };
                Strings.Add(value2);
                string[] value3 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.MINERALDRILL.EFFECT",
                "一种坚固的维护良好的钻探设备，能够钻穿矿藏。钻井作业需要不断供应精炼金属。使用的材料也会影响钻头可以研磨的矿产资源."
                };
                Strings.Add(value3);
                ModUtil.AddBuildingToPlanScreen("Refining", "MineralDrill");
            }
        }
    }

    //===> SYNGAS REFINERY <=========================================================================================
    namespace SyngasRefinery_Patch
    {

        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class SyngasRefineryTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("Distillation").unlockedItemIDs.Add("SyngasRefinery");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class SyngasRefineryUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.SYNGASREFINERY.NAME",
                "合成气精炼厂"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.SYNGASREFINERY.DESC",
                "能够催化部分氧化反应生产合成气的炼油厂."
                };
                Strings.Add(value2);
                string[] array = new string[2];
                array[0] = "STRINGS.BUILDINGS.PREFABS.SYNGASREFINERY.EFFECT";
                string[] values = new string[]
                {
                "这个设备" +
                " 能够从不同的材料例如 ",
                UI.FormatAsLink("木材", ITEMS.INDUSTRIAL_PRODUCTS.WOOD.NAME),
                ", ",
                UI.FormatAsLink("油页岩", "SOLIDOILSHALE"),
                ", 和 ",
                UI.FormatAsLink("沥青", "BITUMEN"),
                " 中产出 ",
                UI.FormatAsLink("合成气体", "SYNGAS"),
                "."
                };
                array[1] = string.Concat(values);
                Strings.Add(array);
                ModUtil.AddBuildingToPlanScreen("Refining", "SyngasRefinery");
            }
        }
    }

    //===> VISCOSE FRAME <=========================================================================================
    namespace ViscoseFrame_Patch
    {

        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class ViscoseFrameTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("Clothing").unlockedItemIDs.Add("ViscoseFrame");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class ViscoseFrameUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.VISCOSEFRAME.NAME",
                "人造丝织机"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.VISCOSEFRAME.DESC",
                "一种能够用粘性凝胶生产纤维素纤维的化学织机."
                };
                Strings.Add(value2);
                string[] array = new string[2];
                array[0] = "STRINGS.BUILDINGS.PREFABS.VISCOSEFRAME.EFFECT";
                string[] values = new string[]
                {
                "这个装置可使用 ",
                UI.FormatAsLink("木材", ITEMS.INDUSTRIAL_PRODUCTS.WOOD.NAME),
                " 与 ",
                UI.FormatAsLink("合成气体", "SYNGAS"),
                " 经过复杂的化学反应产出 ",
                UI.FormatAsLink("人造纤维", RayonFabricConfig.TAG.ProperName()),
                ", 并且在运行时不断输出 ",
                UI.FormatAsLink("蒸汽", "STEAM"),
                };
                array[1] = string.Concat(values);
                Strings.Add(array);
                ModUtil.AddBuildingToPlanScreen("Refining", "ViscoseFrame");
            }
        }
    }

    //===> PLASMA FURNACE <=========================================================================================
    namespace GlassFoundry_Patch
    {

        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class GlassFoundryTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("HighTempForging").unlockedItemIDs.Add("GlassFoundry");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class GlassFoundryUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.GLASSFOUNDRY.NAME",
                "等离子炉"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.GLASSFOUNDRY.DESC",
                "等离子炉使用电弧加热器（等离子管）产生低温等离子流."
                };
                Strings.Add(value2);
                string[] value3 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.GLASSFOUNDRY.EFFECT",
                "电冶金提供了比标准冶金工艺更好的冶炼工艺。"
                };
                Strings.Add(value3);
                ModUtil.AddBuildingToPlanScreen("Refining", "GlassFoundry");
            }
        }
    }

    //===> ADVANCED KILN <==================================================================================================
    namespace AdvancedKiln_Patch
    {
        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class AdvancedKilnTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("HighTempForging").unlockedItemIDs.Add("AdvancedKiln");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class AdvancedKilnUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.ADVANCEDKILN.NAME",
                "高级窑炉"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.ADVANCEDKILN.DESC",
                "一种利用电磁感应产生热量的高级窑炉。"
                };
                Strings.Add(value2);
                string[] value3 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.ADVANCEDKILN.EFFECT",
                "与大多数其他燃料加热方式相比，电磁感应窑炉的优点是清洁、节能和可控的加热过程。"
                };
                Strings.Add(value3);
                ModUtil.AddBuildingToPlanScreen("Refining", "AdvancedKiln");
            }
        }
    }

    //===> AQUEOUS MILL <=========================================================================================
    namespace AqueousMill_Patch
    {

        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class AqueousMillTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("BasicRefinement").unlockedItemIDs.Add("AqueousMill");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class AqueousMillUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.AQUEOUSMILL.NAME",
                "离心压力机"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.AQUEOUSMILL.DESC",
                "将污泥分离成基本元素。"
                };
                Strings.Add(value2);
                string[] value3 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.AQUEOUSMILL.EFFECT",
                "利用离心力将污泥分离成基本成分的专用压力机。"
                };
                Strings.Add(value3);
                ModUtil.AddBuildingToPlanScreen("Refining", "AqueousMill");
            }
        }
    }

    //===> MINERAL MILL <==================================================================================================
    namespace MineralMill_Patch
    {
        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class MineralMillTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("BasicRefinement").unlockedItemIDs.Add("MineralMill");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class MineralMillUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.MINERALMILL.NAME",
                "选矿厂"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.MINERALMILL.DESC",
                "一个球形的磨机由一个绕其轴线旋转的空心圆柱壳组成。它自身的一部分可以填充金属球。"
                };
                Strings.Add(value2);
                string[] value3 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.MINERALMILL.EFFECT",
                "球磨机是一种研磨机，用于研磨或混合矿物选矿过程中使用的材料。"
                };
                Strings.Add(value3);
                ModUtil.AddBuildingToPlanScreen("Refining", "MineralMill");
            }
        }
    }

    //===> COMPOST PROCESSOR <==============================================================================================
    namespace CompostProcessor_Patch
    {
        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class CompostProcessorTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("Agriculture").unlockedItemIDs.Add("CompostProcessor");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class CompostProcessorUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.COMPOSTPROCESSOR.NAME",
                "堆肥处理机"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.COMPOSTPROCESSOR.DESC",
                "从污染水和碎岩中生产泥土"
                };
                Strings.Add(value2);
                string[] array = new string[2];
                array[0] = "STRINGS.BUILDINGS.PREFABS.COMPOSTPROCESSOR.EFFECT";
                string[] values = new string[]
                {
                "使用 ",UI.FormatAsLink("污染水", "DIRTYWATER")," and ",UI.FormatAsLink("碎岩", "CRUSHEDROCK")," 产出 ",UI.FormatAsLink("泥土", "DIRT"),"。"};
                array[1] = string.Concat(values);
                Strings.Add(array);
                ModUtil.AddBuildingToPlanScreen("Refining", "CompostProcessor");
            }
        }
    }

    //===> PETROLEUM DISTILLERY <===========================================================================================
    namespace PetroleumDistillery_Patch
    {
        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class PetroleumDistilleryTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("ImprovedCombustion").unlockedItemIDs.Add("PetroleumDistillery");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class PetroleumDistilleryUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.PETROLEUMDISTILLERY.NAME",
                "石油蒸馏厂"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.PETROLEUMDISTILLERY.DESC",
                "一种自控蒸馏装置，利用感应从原油中生产石油。有一个额外的特殊装置，可以泵出产生的天然气。"
                };
                Strings.Add(value2);
                string[] array = new string[2];
                array[0] = "STRINGS.BUILDINGS.PREFABS.PETROLEUMDISTILLERY.EFFECT";
                string[] values = new string[]
                {
                "从 ",
                UI.FormatAsLink("原油", "CRUDEOIL"),
                " 中蒸馏出 ",
                UI.FormatAsLink("石油", "PETROLEUM"),
                " 和 ",
                UI.FormatAsLink("天然气", "METHANE"),
                "。"
                };
                array[1] = string.Concat(values);
                Strings.Add(array);
                ModUtil.AddBuildingToPlanScreen("Refining", "PetroleumDistillery");
            }
        }
    }

    //===> PYROLYSIS KILN <=============================================================================================
    namespace PyrolysisKiln_Patches
    {
        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class PyrolysisKilnTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("BasicRefinement").unlockedItemIDs.Add("PyrolysisKiln");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class PyrolysisKilnUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.PYROLYSISKILN.NAME",
                "热解窑"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.PYROLYSISKILN.DESC",
                "一种基本的窑炉，利用热解过程将木柴转化为可用的煤。"
                };
                Strings.Add(value2);
                string[] array = new string[2];
                array[0] = "STRINGS.BUILDINGS.PREFABS.PYROLYSISKILN.EFFECT";
                string[] values = new string[]
                {
                "燃烧 ",
                UI.FormatAsLink("木材", "WOODLOG"),
                " 生产 ",
                UI.FormatAsLink("煤炭", "CARBON"),
                "，每秒释放20KDTU的热量与100克的",
                UI.FormatAsLink("二氧化碳", "CARBONDIOXIDE"),
                };
                array[1] = string.Concat(values);
                Strings.Add(array);
                ModUtil.AddBuildingToPlanScreen("Refining", "PyrolysisKiln");
            }
        }
    }

    //===> NAPHTHA DISTILLERY <=========================================================================================
    namespace NaphthaDistillery_Patch
    {

        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class NaphthaDistilleryTechMod
        {
            private static void Postfix()
            {
                Db.Get().Techs.Get("ImprovedCombustion").unlockedItemIDs.Add("NaphthaDistillery");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class NaphthaDistilleryUI
        {
            private static void Prefix()
            {
                string[] value = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.NAPHTHADISTILLERY.NAME",
                "石脑油蒸馏厂"
                };
                Strings.Add(value);
                string[] value2 = new string[]
                {
                "STRINGS.BUILDINGS.PREFABS.NAPHTHADISTILLERY.DESC",
                "一种自控蒸馏装置，利用电磁感应从原油中生产石脑油。有一个额外的特殊装置，可以泵出产生的高硫天然气。"
                };
                Strings.Add(value2);
                string[] array = new string[2];
                array[0] = "STRINGS.BUILDINGS.PREFABS.NAPHTHADISTILLERY.EFFECT";
                string[] values = new string[]
                {
                "蒸馏 ",
                UI.FormatAsLink("原油", "CRUDEOIL"),
                "产出 ",
                UI.FormatAsLink("石脑油", "NAPHTHA "),
                " 与 ",
                UI.FormatAsLink("高硫天然气", "SOURGAS"),
                "。"
                };
                array[1] = string.Concat(values);
                Strings.Add(array);
                ModUtil.AddBuildingToPlanScreen("Refining", "NaphthaDistillery");
            }
        }
    }

}
