using System;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public static class HalonGasElement
    {
        public static readonly Color32 HALON_COLOR = new Color32(94, 153, 255, 255);
        public const string HalonGas_ID = "HalonGas";

        public static readonly SimHashes HalonGasSimHash = (SimHashes)Hash.SDBMLower(HalonGas_ID);

        static Texture2D TintTextureHalonColor(Texture sourceTexture, string name)
        {
            Texture2D newTexture = Util.DuplicateTexture(sourceTexture as Texture2D);
            var pixels = newTexture.GetPixels32();
            for (int i = 0; i < pixels.Length; ++i)
            {
                var gray = ((Color)pixels[i]).grayscale * 1.5f;
                pixels[i] = (Color)HALON_COLOR * gray;
            }
            newTexture.SetPixels32(pixels);
            newTexture.Apply();
            newTexture.name = name;
            return newTexture;
        }

        public static void RegisterHalonGasSubstance()
        {
            ElementUtil.CreateRegisteredSubstance(
              name: HalonGas_ID,
              state: Element.State.Gas,
              kanim: ElementUtil.FindAnim("gas_tank_kanim"),
              material: Assets.instance.substanceTable.liquidMaterial,
              colour: HALON_COLOR
            );
        }
    }
}
