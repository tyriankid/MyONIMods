using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class OilShaleElement
    {
        public static readonly Color32 SHALE_COLOR = new Color32(64, 60, 53, 255);
        public const string OILSHALE_ID = "SolidOilShale";

        public static readonly SimHashes SolidOilShaleSimHash = (SimHashes)Hash.SDBMLower(OILSHALE_ID);

        static Texture2D TintTextureShaleColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)SHALE_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        static Material CreateSolidOilShaleMaterial(Material source)
        {
            var SolidOilShaleMaterial = new Material(source);

            Texture2D newTexture = TintTextureShaleColor(SolidOilShaleMaterial.mainTexture, "solidoilshale");

            SolidOilShaleMaterial.mainTexture = newTexture;
            SolidOilShaleMaterial.name = "matSolidOilShale";

            return SolidOilShaleMaterial;
        }

        public static void RegisterSolidOilShaleSubstance()
        {
            Substance shale = Assets.instance.substanceTable.GetSubstance(SimHashes.MaficRock);

            ElementUtil.CreateRegisteredSubstance(
              name: OILSHALE_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("oilshale_kanim"),
              material: CreateSolidOilShaleMaterial(shale.material),
              colour: SHALE_COLOR
            );
        }
    }
}
