using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class TitaniumElement
    {
        public static readonly Color32 TITANIUM_COLOR = new Color32(153, 148, 116, 255);
        public const string SOLIDTITANIUM_ID = "SolidTitanium";
        public const string MOLTENTITANIUM_ID = "MoltenTitanium";
        public const string TITANIUMGAS_ID = "TitaniumGas";

        public static readonly SimHashes SolidTitaniumSimHash = (SimHashes)Hash.SDBMLower(SOLIDTITANIUM_ID);
        public static readonly SimHashes MoltenTitaniumSimHash = (SimHashes)Hash.SDBMLower(MOLTENTITANIUM_ID);
        public static readonly SimHashes TitaniumGasSimHash = (SimHashes)Hash.SDBMLower(TITANIUMGAS_ID);

        static Texture2D TintTextureTitaniumColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)TITANIUM_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        //==> LIQUID TITANIUM <==================================================================================================

        public static void RegisterMoltenTitaniumSubstance()
        {
            ElementUtil.CreateRegisteredSubstance(
              name: MOLTENTITANIUM_ID,
              state: Element.State.Liquid,
              kanim: ElementUtil.FindAnim("liquid_tank_kanim"),
              material: Assets.instance.substanceTable.liquidMaterial,
              colour: TITANIUM_COLOR
            );
        }

        //==> TITANIUM GAS <=====================================================================================================

        public static void RegisterTitaniumGasSubstance()
        {
            ElementUtil.CreateRegisteredSubstance(
              name: TITANIUMGAS_ID,
              state: Element.State.Gas,
              kanim: ElementUtil.FindAnim("gas_tank_kanim"),
              material: Assets.instance.substanceTable.liquidMaterial,
              colour: TITANIUM_COLOR
            );
        }

        //==> SOLID TITANIUM <===================================================================================================
        static Material CreateSolidTitaniumMaterial(Material source)
        {
            var solidTitaniumMaterial = new Material(source);

            Texture2D newTexture = TintTextureTitaniumColor(solidTitaniumMaterial.mainTexture, "solidtitanium");

            solidTitaniumMaterial.mainTexture = newTexture;
            solidTitaniumMaterial.name = "matSolidTitanium";

            return solidTitaniumMaterial;
        }

        public static void RegisterSolidTitaniumSubstance()
        {
            Substance cuprite = Assets.instance.substanceTable.GetSubstance(SimHashes.Cuprite);

            ElementUtil.CreateRegisteredSubstance(
              name: SOLIDTITANIUM_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("titanium_kanim"),
              material: CreateSolidTitaniumMaterial(cuprite.material),
              colour: TITANIUM_COLOR
            );
        }
    }
}
