using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class TungstenCarbideElement
    {
        public static readonly Color32 CARBIDE_COLOR = new Color32(99, 90, 98, 255);
        public const string CARBIDE_ID = "SolidTungstenCarbide";

        public static readonly SimHashes SolidTungstenCarbideSimHash = (SimHashes)Hash.SDBMLower(CARBIDE_ID);

        static Texture2D TintTextureCarbideColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)CARBIDE_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        static Material CreateSolidTungstenCarbideMaterial(Material source)
        {
            var SolidTungstenCarbideMaterial = new Material(source);

            Texture2D newTexture = TintTextureCarbideColor(SolidTungstenCarbideMaterial.mainTexture, "solidtungstencarbide");

            SolidTungstenCarbideMaterial.mainTexture = newTexture;
            SolidTungstenCarbideMaterial.name = "matSolidTungstenCarbide";

            return SolidTungstenCarbideMaterial;
        }

        public static void RegisterSolidTungstenCarbideSubstance()
        {
            Substance carbide = Assets.instance.substanceTable.GetSubstance(SimHashes.Wolframite);

            ElementUtil.CreateRegisteredSubstance(
              name: CARBIDE_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("tungsten_carbide_kanim"),
              material: CreateSolidTungstenCarbideMaterial(carbide.material),
              colour: CARBIDE_COLOR
            );
        }
    }
}
