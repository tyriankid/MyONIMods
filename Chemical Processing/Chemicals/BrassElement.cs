using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class BrassElement
    {
        public static readonly Color32 BRASS_COLOR = new Color32(250, 224, 152, 255);
        public const string SOLIDBRASS_ID = "SolidBrass";

        public static readonly SimHashes SolidBrassSimHash = (SimHashes)Hash.SDBMLower(SOLIDBRASS_ID);

        static Texture2D TintTextureBrassColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)BRASS_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        static Material CreateSolidBrassMaterial(Material source)
        {
            var SolidBrassMaterial = new Material(source);

            Texture2D newTexture = TintTextureBrassColor(SolidBrassMaterial.mainTexture, "solidbrass");

            SolidBrassMaterial.mainTexture = newTexture;
            SolidBrassMaterial.name = "matSolidBrass";

            return SolidBrassMaterial;
        }

        public static void RegisterSolidBrassSubstance()
        {
            Substance brass_metal = Assets.instance.substanceTable.GetSubstance(SimHashes.Aluminum);

            ElementUtil.CreateRegisteredSubstance(
              name: SOLIDBRASS_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("brass_kanim"),
              material: CreateSolidBrassMaterial(brass_metal.material),
              colour: BRASS_COLOR
            );
        }
    }
}
