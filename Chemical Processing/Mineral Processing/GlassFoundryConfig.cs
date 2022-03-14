namespace Chemical_Processing.Mineral_Processing
{
    using System;
    using System.Collections.Generic;
    using KSerialization;
    using NightLib;
    using NightLib.AddBuilding;
    using TUNING;
    using UnityEngine;
    using Chemical_Processing.Chemicals;

    [SerializationConfig(MemberSerialization.OptIn)]
    public class GlassFoundryConfig : IBuildingConfig
    {
        public const string ID = "GlassFoundry";
        private const float INPUT_KG = 100f;
        private static readonly List<Storage.StoredItemModifier> FoundryStoredItemModifiers;

        private static readonly PortDisplayOutput GlassOutputPort = new PortDisplayOutput(ConduitType.Liquid, new CellOffset(0, -2));

        static GlassFoundryConfig()
        {
            List<Storage.StoredItemModifier> list1 = new List<Storage.StoredItemModifier>();
            list1.Add(Storage.StoredItemModifier.Hide);
            list1.Add(Storage.StoredItemModifier.Preserve);
            list1.Add(Storage.StoredItemModifier.Insulate);
            list1.Add(Storage.StoredItemModifier.Seal);
            FoundryStoredItemModifiers = list1;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();

            GlassForge fabricator = go.AddOrGet<GlassForge>();
            fabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;

            ComplexFabricatorWorkable workable = go.AddOrGet<ComplexFabricatorWorkable>();
            workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_fabricator_generic_kanim") };
            workable.AnimOffset = new Vector3(-1f, 0f, 0f);
            go.AddOrGet<LoopingSounds>();

            fabricator.duplicantOperated = true;

            Storage storage = go.AddOrGet<Storage>();
            storage.storageFilters = STORAGEFILTERS.LIQUIDS;
            storage.capacityKg = 2000f;

            BuildingTemplates.CreateComplexFabricatorStorage(go, fabricator);
            fabricator.outStorage.capacityKg = 2000f;
            fabricator.storeProduced = true;
            fabricator.inStorage.SetDefaultStoredItemModifiers(FoundryStoredItemModifiers);
            fabricator.buildStorage.SetDefaultStoredItemModifiers(FoundryStoredItemModifiers);
            fabricator.outStorage = storage;
            fabricator.outputOffset = new Vector3(1f, 0.5f);

            PipedDispenser dispenser = go.AddComponent<PipedDispenser>();
            dispenser.storage = storage;
            dispenser.elementFilter = null;
            dispenser.AssignPort(GlassOutputPort);
            dispenser.alwaysDispense = true;
            dispenser.SkipSetOperational = true;

            PipedOptionalExhaust exhaustGlass = go.AddComponent<PipedOptionalExhaust>();
            exhaustGlass.dispenser = dispenser;
            exhaustGlass.elementHash = SimHashes.MoltenGlass;
            exhaustGlass.elementTag = SimHashes.MoltenGlass.CreateTag();
            exhaustGlass.capacity = 100f;

            PipedOptionalExhaust exhaustCopper = go.AddComponent<PipedOptionalExhaust>();
            exhaustCopper.dispenser = dispenser;
            exhaustCopper.elementHash = SimHashes.MoltenCopper;
            exhaustCopper.elementTag = SimHashes.MoltenCopper.CreateTag();
            exhaustCopper.capacity = 500f;

            PipedOptionalExhaust exhaustIron = go.AddComponent<PipedOptionalExhaust>();
            exhaustIron.dispenser = dispenser;
            exhaustIron.elementHash = SimHashes.MoltenIron;
            exhaustIron.elementTag = SimHashes.MoltenIron.CreateTag();
            exhaustIron.capacity = 500f;

            PipedOptionalExhaust exhaustAluminum = go.AddComponent<PipedOptionalExhaust>();
            exhaustAluminum.dispenser = dispenser;
            exhaustAluminum.elementHash = SimHashes.MoltenAluminum;
            exhaustAluminum.elementTag = SimHashes.MoltenAluminum.CreateTag();
            exhaustAluminum.capacity = 500f;

            PipedOptionalExhaust exhaustGold = go.AddComponent<PipedOptionalExhaust>();
            exhaustGold.dispenser = dispenser;
            exhaustGold.elementHash = SimHashes.MoltenGold;
            exhaustGold.elementTag = SimHashes.MoltenGold.CreateTag();
            exhaustGold.capacity = 500f;

            PipedOptionalExhaust exhaustCobalt = go.AddComponent<PipedOptionalExhaust>();
            exhaustCobalt.dispenser = dispenser;
            exhaustCobalt.elementHash = SimHashes.MoltenCobalt;
            exhaustCobalt.elementTag = SimHashes.MoltenCobalt.CreateTag();
            exhaustCobalt.capacity = 500f;

            PipedOptionalExhaust exhaustMercury = go.AddComponent<PipedOptionalExhaust>();
            exhaustMercury.dispenser = dispenser;
            exhaustMercury.elementHash = SimHashes.Mercury;
            exhaustMercury.elementTag = SimHashes.Mercury.CreateTag();
            exhaustMercury.capacity = 500f;

            PipedOptionalExhaust exhaustSulfur = go.AddComponent<PipedOptionalExhaust>();
            exhaustSulfur.dispenser = dispenser;
            exhaustSulfur.elementHash = SimHashes.LiquidSulfur;
            exhaustSulfur.elementTag = SimHashes.LiquidSulfur.CreateTag();
            exhaustSulfur.capacity = 500f;

            PipedOptionalExhaust exhaustWater = go.AddComponent<PipedOptionalExhaust>();
            exhaustWater.dispenser = dispenser;
            exhaustWater.elementHash = SimHashes.Water;
            exhaustWater.elementTag = SimHashes.Water.CreateTag();
            exhaustWater.capacity = 500f;

            PipedOptionalExhaust exhaustDirtyWater = go.AddComponent<PipedOptionalExhaust>();
            exhaustDirtyWater.dispenser = dispenser;
            exhaustDirtyWater.elementHash = SimHashes.DirtyWater;
            exhaustDirtyWater.elementTag = SimHashes.DirtyWater.CreateTag();
            exhaustDirtyWater.capacity = 500f;

            PipedOptionalExhaust exhaustSteel = go.AddComponent<PipedOptionalExhaust>();
            exhaustSteel.dispenser = dispenser;
            exhaustSteel.elementHash = SimHashes.MoltenSteel;
            exhaustSteel.elementTag = SimHashes.MoltenSteel.CreateTag();
            exhaustSteel.capacity = 500f;

            PipedOptionalExhaust exhaustTungsten = go.AddComponent<PipedOptionalExhaust>();
            exhaustTungsten.dispenser = dispenser;
            exhaustTungsten.elementHash = SimHashes.MoltenTungsten;
            exhaustTungsten.elementTag = SimHashes.MoltenTungsten.CreateTag();
            exhaustTungsten.capacity = 100f;

            PipedOptionalExhaust exhaustNaphtha = go.AddComponent<PipedOptionalExhaust>();
            exhaustNaphtha.dispenser = dispenser;
            exhaustNaphtha.elementHash = SimHashes.Naphtha;
            exhaustNaphtha.elementTag = SimHashes.Naphtha.CreateTag();
            exhaustNaphtha.capacity = 500f;

            PipedOptionalExhaust exhaustPhosphorus = go.AddComponent<PipedOptionalExhaust>();
            exhaustPhosphorus.dispenser = dispenser;
            exhaustPhosphorus.elementHash = SimHashes.LiquidPhosphorus;
            exhaustPhosphorus.elementTag = SimHashes.LiquidPhosphorus.CreateTag();
            exhaustPhosphorus.capacity = 500f;

            //==> NEW ELEMENTS <=======

            PipedOptionalExhaust exhaustZinc = go.AddComponent<PipedOptionalExhaust>();
            exhaustZinc.dispenser = dispenser;
            exhaustZinc.elementHash = ZincElement.MoltenZincSimHash;
            exhaustZinc.elementTag = ZincElement.MoltenZincSimHash.CreateTag();
            exhaustZinc.capacity = 500f;

            PipedOptionalExhaust exhaustSilver = go.AddComponent<PipedOptionalExhaust>();
            exhaustSilver.dispenser = dispenser;
            exhaustSilver.elementHash = SilverElement.MoltenSilverSimHash;
            exhaustSilver.elementTag = SilverElement.MoltenSilverSimHash.CreateTag();
            exhaustSilver.capacity = 500f;

            PipedOptionalExhaust exhaustTitanium = go.AddComponent<PipedOptionalExhaust>();
            exhaustTitanium.dispenser = dispenser;
            exhaustTitanium.elementHash = TitaniumElement.MoltenTitaniumSimHash;
            exhaustTitanium.elementTag = TitaniumElement.MoltenTitaniumSimHash.CreateTag();
            exhaustTitanium.capacity = 500f;

            this.AttachPort(go);

            if (DlcManager.IsExpansion1Active())
            {
                    Tag material = SimHashes.Cobaltite.CreateTag();
                    Tag material3 = SimHashes.MoltenCobalt.CreateTag();
                    string name = ElementLoader.FindElementByHash(SimHashes.MoltenCobalt).name;
                    string name2 = ElementLoader.FindElementByHash(SimHashes.Cobaltite).name;
                    ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
                    {
                    new ComplexRecipe.RecipeElement(material, 500f)
                    };
                    ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
                    {
                    new ComplexRecipe.RecipeElement(material3, 500f, ComplexRecipe.RecipeElement.TemperatureOperation.Melted, false)
                    };
                    string id = ComplexRecipeManager.MakeRecipeID("GlassFoundry", array, array2);
                    ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
                    complexRecipe.time = 40f;
                    complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
                    complexRecipe.description = string.Format(string.Concat(new string[]
                    {
                    "Smelt ",name2," to produce ",name,"."}), name);
                    complexRecipe.fabricators = new List<Tag>
                {
                    TagManager.Create("GlassFoundry")
                };
            }

            Prioritizable.AddRef(go);
        }

        private void AttachPort(GameObject go)
        {
            PortDisplayController controller = go.AddComponent<PortDisplayController>();
            controller.Init(go);

            controller.AssignPort(go, GlassOutputPort);
        }

        public override BuildingDef CreateBuildingDef()
        {
            float[] singleArray1 = new float[] { 500f, 200f };
            string[] textArray1 = new string[] { SimHashes.Ceramic.ToString(), "RefinedMetal" };

            EffectorValues noise = TUNING.NOISE_POLLUTION.NOISY.TIER6;
            BuildingDef def = BuildingTemplates.CreateBuildingDef("GlassFoundry", 3, 3, "glass_foundry_kanim", 60, 90f, singleArray1, textArray1, 2400f, BuildLocationRule.OnFloor, TUNING.BUILDINGS.DECOR.PENALTY.TIER2, noise, 0.2f);
            def.RequiresPowerInput = true;
            def.EnergyConsumptionWhenActive = 2400f;
            def.SelfHeatKilowattsWhenActive = 8f;
            def.ViewMode = OverlayModes.Power.ID;
            def.AudioCategory = "HollowMetal";
            def.AudioSize = "large";
            return def;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            this.AttachPort(go);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            this.AttachPort(go);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            SymbolOverrideControllerUtil.AddToPrefab(go);
            go.AddOrGetDef<PoweredActiveStoppableController.Def>();
            go.GetComponent<KPrefabID>().prefabSpawnFn += delegate (GameObject game_object)
            {
                ComplexFabricatorWorkable component = game_object.GetComponent<ComplexFabricatorWorkable>();
                component.WorkerStatusItem = Db.Get().DuplicantStatusItems.Processing;
                component.AttributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
                component.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
                component.SkillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
                component.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
            };
        }
    }
}
