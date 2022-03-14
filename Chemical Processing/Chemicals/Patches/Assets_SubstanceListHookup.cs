using System;
using HarmonyLib;
using STRINGS;

// All code used for adding new elements to the game, was taken from Heinermann Blood Element mod, which he had the 
// kindness of sharing through his Github page: https://github.com/heinermann/ONI_Mods/tree/master/Blood
// Special thanks for Heinerman for sharing this incredible code.

namespace Chemical_Processing.Chemicals.Patches
{
	[HarmonyPatch(typeof(Assets), "SubstanceListHookup")]
	internal class Assets_SubstanceListHookup
	{
		private static void Prefix()
		{
            ElementUtil.RegisterElementStrings("SolidSilver", "银", "(Ag) 银是一种柔软、白色、有光泽的过渡金属，具有很高的导电性和导热性。");
            ElementUtil.RegisterElementStrings("MoltenSilver", "熔融银", "(Ag) 处于熔融状态的银。");
            ElementUtil.RegisterElementStrings("SilverGas", "Silver Gas", "(Ag) 处于气化状态的银。");
            ElementUtil.RegisterElementStrings("ArgentiteOre", "银矿", "(Ag<sub>2</sub>S) 银矿是一种立方硫化银，是一种导电金属，是"+ UI.FormatAsLink("精炼银", "SOLIDSILVER") + "的主要来源。 "   );
            ElementUtil.RegisterElementStrings("SolidBorax", "硼砂", "硼砂，也称为硼酸钠，是一种重要的硼化合物，主要用于制造 " + UI.FormatAsLink("玻璃纤维", "SOLIDFIBERGLASS") + "，并用作冶金助焊剂。");
            ElementUtil.RegisterElementStrings("SolidOilShale", "油页岩", "油页岩是一种富含有机质的细粒沉积岩，含有重质原油、硫化物和重金属。");
            ElementUtil.RegisterElementStrings("SolidFiberglass", "玻璃纤维", "玻璃纤维是由硼硅酸盐 " + UI.FormatAsLink("Glass", "GLASS") + "和" + UI.FormatAsLink("Plastic", "POLYPROPYLENE") + "聚合而成的热固性聚合物基体。虽然纤维的压缩性很弱，但这种复合材料具有中等的绝缘性能，由于其相对的柔韧性，可以很容易地用于许多不同的应用。");
            ElementUtil.RegisterElementStrings("SolidTungstenCarbide", "碳化钨", "(WC) 碳化钨是" + UI.FormatAsLink("Tungsten", "TUNGSTEN") + "和 " + UI.FormatAsLink("Carbon", "CARBON") + "的化合物。这种化合物的硬度几乎是钢的两倍，硬度略低于金刚石，具有许多建筑和工业用途，同时也是核应用的有效中子反射器。");
            ElementUtil.RegisterElementStrings("HalonGas", "哈龙气体", "一种具有绝对冰点的卤代烃气体，用作制冷剂。在接近水沸点的温度下变得不稳定，降解为其他元素。无氟！");
            ElementUtil.RegisterElementStrings("SolidTitanium", "钛", "(Ti) 钛是一种有光泽的过渡金属，具有银色、低密度和高强度。");
            ElementUtil.RegisterElementStrings("MoltenTitanium", "熔融钛", "(Ti) 熔融状态下的钛。");
            ElementUtil.RegisterElementStrings("TitaniumGas", "钛气体", "(Ti) 气态状态下的钛。");
            ElementUtil.RegisterElementStrings("SolidZinc", "锌", "(Zn) 锌是一种蓝白色、有光泽、抗磁性的金属，稍脆。");
            ElementUtil.RegisterElementStrings("MoltenZinc", "熔融锌", "(Zn) 熔融状态下的锌。");
            ElementUtil.RegisterElementStrings("ZincGas", "气态锌", "(Zn)气态状态下的锌。");
            ElementUtil.RegisterElementStrings("AurichalciteOre", "绿铜锌矿", "((Zn,Cu)<sub>5</sub>(CO<sub>3</sub>)<sub>2</sub>(OH)<sub>6</sub>) 绿铜锌矿是一种碳酸盐矿物，是 " + UI.FormatAsLink("Zinc", "SOLIDZINC") + "的主要来源。");
            ElementUtil.RegisterElementStrings("SolidBrass", "黄铜", "黄铜是" + UI.FormatAsLink("Copper", "COPPER") + "和" + UI.FormatAsLink("Zinc", "SOLIDZINC") + "的合金，由于具有低熔点、高加工性、耐久性、导电性和导热性等特性，被广泛用于制造器皿。 ");

        }

        private static void Postfix()
		{
            SilverElement.RegisterSolidSilverSubstance();                      //#:1
            SilverElement.RegisterMoltenSilverSubstance();                     //#:2
            SilverElement.RegisterSilverGasSubstance();                        //#:3
            ArgentiteElement.RegisterArgentiteOreSubstance();                  //#:4  
            BoraxElement.RegisterSolidBoraxSubstance();                        //#:5
            OilShaleElement.RegisterSolidOilShaleSubstance();                  //#:6
            FiberglassElement.RegisterSolidFiberglassSubstance();              //#:7
            TungstenCarbideElement.RegisterSolidTungstenCarbideSubstance();    //#:8
            HalonGasElement.RegisterHalonGasSubstance();                       //#:9
            TitaniumElement.RegisterSolidTitaniumSubstance();                  //#:10
            TitaniumElement.RegisterMoltenTitaniumSubstance();                 //#:11
            TitaniumElement.RegisterTitaniumGasSubstance();                    //#:12
            ZincElement.RegisterSolidZincSubstance();                          //#:13
            ZincElement.RegisterMoltenZincSubstance();                         //#:14
            ZincElement.RegisterZincGasSubstance();                            //#:15
            AurichalciteElement.RegisterAurichalciteOreSubstance();            //#:16
            BrassElement.RegisterSolidBrassSubstance();                        //#:17
        }
	}

    //===> RAYON FIBER PATCH <=================================================================================
    namespace RadfuelPellet_Patch
    {
        [HarmonyPatch(typeof(EntityConfigManager), "LoadGeneratedEntities")]
        public static class EntityConfigManager_LoadGeneratedEntities_Patch
        {
            public static void Prefix()
            {
                Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{RayonFabricConfig.ID.ToUpperInvariant()}.NAME", RayonFabricConfig.Name);
                Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{RayonFabricConfig.ID.ToUpperInvariant()}.DESC", RayonFabricConfig.Description);
            }
        }
    }
}
