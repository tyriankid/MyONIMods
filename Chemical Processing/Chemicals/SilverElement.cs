using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class SilverElement
    {
        public static readonly Color32 SILVER_COLOR = new Color32(107, 117, 125, 255);
        public const string SOLIDSILVER_ID = "SolidSilver";
        public const string MOLTENSILVER_ID = "MoltenSilver";
        public const string SILVERGAS_ID = "SilverGas";

        public static readonly SimHashes SolidSilverSimHash = (SimHashes)Hash.SDBMLower(SOLIDSILVER_ID);
        public static readonly SimHashes MoltenSilverSimHash = (SimHashes)Hash.SDBMLower(MOLTENSILVER_ID);
        public static readonly SimHashes SilverGasSimHash = (SimHashes)Hash.SDBMLower(SILVERGAS_ID);

        static Texture2D TintTextureSilverColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)SILVER_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        //==> LIQUID SILVER <==================================================================================================

        public static void RegisterMoltenSilverSubstance()
        {
            ElementUtil.CreateRegisteredSubstance(
              name: MOLTENSILVER_ID,
              state: Element.State.Liquid,
              kanim: ElementUtil.FindAnim("liquid_tank_kanim"),
              material: Assets.instance.substanceTable.liquidMaterial,
              colour: SILVER_COLOR
            );
        }

        //==> SILVER GAS <=====================================================================================================

        public static void RegisterSilverGasSubstance()
        {
            ElementUtil.CreateRegisteredSubstance(
              name: SILVERGAS_ID,
              state: Element.State.Gas,
              kanim: ElementUtil.FindAnim("gas_tank_kanim"),
              material: Assets.instance.substanceTable.liquidMaterial,
              colour: SILVER_COLOR
            );
        }

        //==> SOLID SILVER <===================================================================================================
        static Material CreateSolidSilverMaterial(Material source)
        {
            var solidSilverMaterial = new Material(source);

            Texture2D newTexture = TintTextureSilverColor(solidSilverMaterial.mainTexture, "solidsilver");

            solidSilverMaterial.mainTexture = newTexture;
            solidSilverMaterial.name = "matSolidSilver";

            return solidSilverMaterial;
        }

        public static void RegisterSolidSilverSubstance()
        {
            Substance electrum = Assets.instance.substanceTable.GetSubstance(SimHashes.Electrum);

            ElementUtil.CreateRegisteredSubstance(
              name: SOLIDSILVER_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("silver_kanim"),
              material: CreateSolidSilverMaterial(electrum.material),
              colour: SILVER_COLOR
            );
        }
    }
}
