using Klei.AI;
using HarmonyLib;
using System;
using System.Collections.Generic;

namespace Chemical_Processing.Chemicals.Patches
{
    [HarmonyPatch(typeof(LegacyModMain), "ConfigElements")]
    public static class ConfigElements_Patch
    {

        public static void Postfix()
        {
            Element silver = ElementLoader.FindElementByHash(SilverElement.SolidSilverSimHash);
            Element titanium = ElementLoader.FindElementByHash(TitaniumElement.SolidTitaniumSimHash);
            Element brass = ElementLoader.FindElementByHash(BrassElement.SolidBrassSimHash);

            //=: Giving Silver Decor and Temperature modifications :====================================================
            AttributeModifier silverDecorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, 0.4f, null, true, false, true);
            AttributeModifier silverTempModifier = new AttributeModifier(Db.Get().BuildingAttributes.OverheatTemperature.Id, -30f, silver.name);
            silver.attributeModifiers.Add(silverDecorModifier);
            silver.attributeModifiers.Add(silverTempModifier);

            //=: Giving Titanium Temperature modifications :============================================================
            AttributeModifier TitaniumTempModifier = new AttributeModifier(Db.Get().BuildingAttributes.OverheatTemperature.Id, 500f, titanium.name);
            titanium.attributeModifiers.Add(TitaniumTempModifier);

            //=: Giving Brass Decor and Temperature modifications :=====================================================
            AttributeModifier BrassDecorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, 0.25f, null, true, false, true);
            AttributeModifier BrassTempModifier = new AttributeModifier(Db.Get().BuildingAttributes.OverheatTemperature.Id, -10f, brass.name);
            brass.attributeModifiers.Add(BrassDecorModifier);
            brass.attributeModifiers.Add(BrassTempModifier);
        }
    }

    [HarmonyPatch(typeof(ElementLoader), "FinaliseElementsTable")]
    public static class ElementTagsModificationPatch
    {
        public static void Postfix()
        {
            //=: BITUMEN STORAGE SETTING :=============================================
            Element element = ElementLoader.FindElementByHash(SimHashes.Bitumen);
            element.materialCategory = GameTags.ManufacturedMaterial;
            element.disabled = false;
            List<Tag> list1 = new List<Tag>(element.oreTags);
            list1.Add(GameTags.ManufacturedMaterial);
            element.oreTags = list1.ToArray();
            GameTags.SolidElements.Add(element.tag);

            //=: SYNGAS ENABLING PATCH :===============================================
            //  I found out that Synthetic Gas could not be found while using infite element sources and
            //  gas element filters. After some checking in the element table, I have found this option
            //  called isDisable = true. Her in the ElementLoader I found the option disabled, then I set
            //  it to false. It seems this small tweak made Synthetic Gas now being shown in mentioned
            //  devices. I don't know why Klei did disabled these, perhaps to save memory?
            Element element2 = ElementLoader.FindElementByHash(SimHashes.Syngas);
            element2.disabled = false;

            //=: NAPHTHA AS COMBUSTIBLE LIQUID PATCH :=================================
            Element element3 = ElementLoader.FindElementByHash(SimHashes.Naphtha);
            List<Tag> list3 = new List<Tag>(element.oreTags);
            list3.Add(GameTags.CombustibleLiquid);
            element.oreTags = list3.ToArray();
            GameTags.LiquidElements.Add(element.tag);

        }
    }
}

