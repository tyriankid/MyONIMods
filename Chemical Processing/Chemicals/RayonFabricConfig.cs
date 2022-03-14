using Klei.AI;
using STRINGS;
using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace Chemical_Processing.Chemicals
{
    public class RayonFabricConfig : IEntityConfig
    {
        public const string ID = "RayonFiber";
        public static readonly Tag TAG = TagManager.Create("RayonFiber");
        private AttributeModifier decorModifier = new AttributeModifier("Decor", 0.1f, Name, true, false, true);

        public static string Name = UI.FormatAsLink("Rayon Fiber", ID.ToUpper());
        public static string Description = $"Rayon is a synthetic fiber, chemically made from regenerated cellulose extracted from Lumber.";

        public GameObject CreatePrefab()
        {
            List<Tag> additionalTags = new List<Tag>();
            additionalTags.Add(GameTags.IndustrialIngredient);
            GameObject go = EntityTemplates.CreateLooseEntity("RayonFiber", Name, Description, 1f, false, Assets.GetAnim("rayon_fiber_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, true, 0, SimHashes.Creature, new List<Tag>
        {
            GameTags.IndustrialIngredient,
            GameTags.BuildingFiber
        });
            go.AddOrGet<EntitySplitter>();
            go.AddOrGet<SimpleMassStatusItem>();
            return go;
        }

        public string[] GetDlcIds() =>
            DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
