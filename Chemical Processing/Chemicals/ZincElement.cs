using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class ZincElement
    {
        public static readonly Color32 ZINC_COLOR = new Color32(201, 201, 195, 255);
        public const string SOLIDZINC_ID = "SolidZinc";
        public const string MOLTEZINC_ID = "MoltenZinc";
        public const string ZINCGAS_ID = "ZincGas";

        public static readonly SimHashes SolidZincSimHash = (SimHashes)Hash.SDBMLower(SOLIDZINC_ID);
        public static readonly SimHashes MoltenZincSimHash = (SimHashes)Hash.SDBMLower(MOLTEZINC_ID);
        public static readonly SimHashes ZincGasSimHash = (SimHashes)Hash.SDBMLower(ZINCGAS_ID);

        static Texture2D TintTextureZincColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)ZINC_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        //==> LIQUID ZINC <==================================================================================================

        public static void RegisterMoltenZincSubstance()
        {
            ElementUtil.CreateRegisteredSubstance(
              name: MOLTEZINC_ID,
              state: Element.State.Liquid,
              kanim: ElementUtil.FindAnim("liquid_tank_kanim"),
              material: Assets.instance.substanceTable.liquidMaterial,
              colour: ZINC_COLOR
            );
        }

        //==> ZINC GAS <=====================================================================================================

        public static void RegisterZincGasSubstance()
        {
            ElementUtil.CreateRegisteredSubstance(
              name: ZINCGAS_ID,
              state: Element.State.Gas,
              kanim: ElementUtil.FindAnim("gas_tank_kanim"),
              material: Assets.instance.substanceTable.liquidMaterial,
              colour: ZINC_COLOR
            );
        }

        //==> ZINC SILVER <===================================================================================================
        static Material CreateSolidZincMaterial(Material source)
        {
            var solidSilverMaterial = new Material(source);

            Texture2D newTexture = TintTextureZincColor(solidSilverMaterial.mainTexture, "solidzinc");

            solidSilverMaterial.mainTexture = newTexture;
            solidSilverMaterial.name = "matSolidZinc";

            return solidSilverMaterial;
        }

        public static void RegisterSolidZincSubstance()
        {
            Substance zinc_mat = Assets.instance.substanceTable.GetSubstance(SimHashes.Gold);

            ElementUtil.CreateRegisteredSubstance(
              name: SOLIDZINC_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("zinc_kanim"),
              material: CreateSolidZincMaterial(zinc_mat.material),
              colour: ZINC_COLOR
            );
        }
    }
}
