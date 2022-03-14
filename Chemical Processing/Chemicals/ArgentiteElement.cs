using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class ArgentiteElement
    {
        public static readonly Color32 ARGENTITE_COLOR = new Color32(140, 131, 104, 255);
        public const string ARGENTITE_ID = "ArgentiteOre";

        public static readonly SimHashes ArgentiteOreSimHash = (SimHashes)Hash.SDBMLower(ARGENTITE_ID);

        static Texture2D TintTextureArgentiteColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)ARGENTITE_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        static Material CreateArgentiteOreMaterial(Material source)
        {
            var solidArgentiteOreMaterial = new Material(source);

            Texture2D newTexture = TintTextureArgentiteColor(solidArgentiteOreMaterial.mainTexture, "argentiteore");

            solidArgentiteOreMaterial.mainTexture = newTexture;
            solidArgentiteOreMaterial.name = "matArgentiteOre";

            return solidArgentiteOreMaterial;
        }

        public static void RegisterArgentiteOreSubstance()
        {
            Substance argentite_ore = Assets.instance.substanceTable.GetSubstance(SimHashes.AluminumOre);

            ElementUtil.CreateRegisteredSubstance(
              name: ARGENTITE_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("argentite_kanim"),
              material: CreateArgentiteOreMaterial(argentite_ore.material),
              colour: ARGENTITE_COLOR
            );
        }
    }
}
