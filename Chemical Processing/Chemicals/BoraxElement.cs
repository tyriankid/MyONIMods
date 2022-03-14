using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class BoraxElement
    {
        public static readonly Color32 BORAX_COLOR = new Color32(245, 241, 211, 255);
        public const string BORAX_ID = "SolidBorax";

        public static readonly SimHashes SolidBoraxSimHash = (SimHashes)Hash.SDBMLower(BORAX_ID);

        static Texture2D TintTextureBoraxColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)BORAX_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        static Material CreateSolidBoraxMaterial(Material source)
        {
            var SolidBoraxMaterial = new Material(source);

            Texture2D newTexture = TintTextureBoraxColor(SolidBoraxMaterial.mainTexture, "solidborax");

            SolidBoraxMaterial.mainTexture = newTexture;
            SolidBoraxMaterial.name = "matSolidBorax";

            return SolidBoraxMaterial;
        }

        public static void RegisterSolidBoraxSubstance()
        {
            Substance borax_dust = Assets.instance.substanceTable.GetSubstance(SimHashes.SolidCarbonDioxide);

            ElementUtil.CreateRegisteredSubstance(
              name: BORAX_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("borax_kanim"),
              material: CreateSolidBoraxMaterial(borax_dust.material),
              colour: BORAX_COLOR
            );
        }
    }
}
