using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class FiberglassElement
    {
        public static readonly Color32 FIBERGLASS_COLOR = new Color32(242, 242, 201, 255);
        public const string FIBERGLASS_ID = "SolidFiberglass";

        public static readonly SimHashes SolidFiberglassSimHash = (SimHashes)Hash.SDBMLower(FIBERGLASS_ID);

        static Texture2D TintTextureFiberglassColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)FIBERGLASS_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        static Material CreateSolidFiberglassMaterial(Material source)
        {
            var SolidFiberglassMaterial = new Material(source);

            Texture2D newTexture = TintTextureFiberglassColor(SolidFiberglassMaterial.mainTexture, "solidfiberglass");

            SolidFiberglassMaterial.mainTexture = newTexture;
            SolidFiberglassMaterial.name = "matSolidFiberglass";

            return SolidFiberglassMaterial;
        }

        public static void RegisterSolidFiberglassSubstance()
        {
            Substance glassfiber = Assets.instance.substanceTable.GetSubstance(SimHashes.Fullerene);

            ElementUtil.CreateRegisteredSubstance(
              name: FIBERGLASS_ID,
              state: Element.State.Solid,
              kanim: ElementUtil.FindAnim("fiberglass_kanim"),
              material: CreateSolidFiberglassMaterial(glassfiber.material),
              colour: FIBERGLASS_COLOR
            );
        }
    }
}
