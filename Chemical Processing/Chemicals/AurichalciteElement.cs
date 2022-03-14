using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class AurichalciteElement
    {
        public static readonly Color32 AURICHALCITE_COLOR = new Color32(162, 235, 187, 255);
        public const string AURICHALCITE_ID = "AurichalciteOre";

        public static readonly SimHashes AurichalciteOreSimHash = (SimHashes)Hash.SDBMLower(AURICHALCITE_ID);

        static Texture2D TintTextureAurichalciteColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)AURICHALCITE_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        static Material CreateAurichalciteOreMaterial(Material source)
        {
            var solidAurichalciteOreMaterial = new Material(source);

            Texture2D newTexture = TintTextureAurichalciteColor(solidAurichalciteOreMaterial.mainTexture, "aurichalciteore");

            solidAurichalciteOreMaterial.mainTexture = newTexture;
            solidAurichalciteOreMaterial.name = "matAurichalciteOre";

            return solidAurichalciteOreMaterial;
        }

        public static void RegisterAurichalciteOreSubstance()
        {
            Substance aurichalcite_ore = Assets.instance.substanceTable.GetSubstance(SimHashes.Lead);

            ElementUtil.CreateRegisteredSubstance(
              name: AURICHALCITE_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("aurichalcite_kanim"),
              material: CreateAurichalciteOreMaterial(aurichalcite_ore.material),
              colour: AURICHALCITE_COLOR
            );
        }
    }
}
