using KSerialization;
using UnityEngine;
using HarmonyLib;

    [SkipSaveFileSerialization]
    [SerializationConfig(MemberSerialization.OptIn)]
    public class DoorRotatableConsumer : PassiveElementConsumer
    {
        [SerializeField]
        public Vector3 rotatableCellOffset;
    }

    [HarmonyPatch(typeof(ElementConsumer))]
    [HarmonyPatch("GetSampleCell")]
    public static class ElementConsumer_GetSampleCell_Patch
    {
        [HarmonyPriority(-19999)] // Extremely low priority. We want this to happen last, since this will only overwrite DoorRotatable variable
        public static void Prefix(ElementConsumer __instance)
        {
            if (__instance is DoorRotatableConsumer)
            {
                Vector3 rotatableCellOffset = ((DoorRotatableConsumer)__instance).rotatableCellOffset;
                Rotatable rotatable = __instance.GetComponent<Rotatable>();
                if (rotatable != null) __instance.sampleCellOffset = Rotatable.GetRotatedOffset(rotatableCellOffset, rotatable.GetOrientation());
            }
        }
    }

