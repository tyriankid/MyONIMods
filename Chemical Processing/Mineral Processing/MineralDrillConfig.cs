using System;
using TUNING;
using UnityEngine;
using System.Collections.Generic;
using Chemical_Processing.Chemicals;

namespace Chemical_Processing.Mineral_Processing
{
    public class MineralDrillConfig : IBuildingConfig
    {
        private static readonly Tag DRILL_TAG = GameTags.RefinedMetal;
        public const string ID = "MineralDrill";
        private static readonly List<Storage.StoredItemModifier> DrillStoredItemModifiers;

        static MineralDrillConfig()
        {
            List<Storage.StoredItemModifier> list1 = new List<Storage.StoredItemModifier>();
            list1.Add(Storage.StoredItemModifier.Hide);
            list1.Add(Storage.StoredItemModifier.Preserve);
            list1.Add(Storage.StoredItemModifier.Insulate);
            list1.Add(Storage.StoredItemModifier.Seal);
            DrillStoredItemModifiers = list1;
        }

        //private void OnCancelWorkingOrder(ComplexFabricator CancelWorkingOrder, ComplexRecipe.RecipeElement[] ingredients)
        //{

        //}

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {

            Prioritizable.AddRef(go);
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
            ComplexFabricator fabricator = go.AddOrGet<ComplexFabricator>();
            fabricator.heatedTemperature = 346.15f;
            fabricator.duplicantOperated = false;
            BuildingTemplates.CreateComplexFabricatorStorage(go, fabricator);
            fabricator.inStorage.SetDefaultStoredItemModifiers(DrillStoredItemModifiers);
            fabricator.buildStorage.SetDefaultStoredItemModifiers(DrillStoredItemModifiers);
            fabricator.buildStorage.allowItemRemoval = false;
            fabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            fabricator.fetchChoreTypeIdHash = Db.Get().ChoreTypes.FabricateFetch.IdHash;
            go.AddOrGet<LoopingSounds>();
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
            this.ConfigureRecipes();
        }

        private void ConfigureRecipes()
        {
            //===> COPPER DRILL <===================================================================================================
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Copper.CreateTag(), 400f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id = ComplexRecipeManager.MakeRecipeID("MineralDrill", array, array2);
            ComplexRecipe complexRecipe = new ComplexRecipe(id, array, array2);
            complexRecipe.time = 150f;
            complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
            complexRecipe.description = string.Format(string.Concat(new string[]
            {
                "Engage a drilling operation using ",SimHashes.Copper.CreateTag().ProperName()," as drill bits." +
                "Possible minerals availabe at this layer:" +
                "\n",SimHashes.Sand.CreateTag().ProperName(),": 37.5%." +
                "\n",SimHashes.Dirt.CreateTag().ProperName(),": 22.5%." +
                "\n",SimHashes.CrushedRock.CreateTag().ProperName(),": 11%." +
                "\n",SimHashes.Carbon.CreateTag().ProperName(),": 11%." +
                "\n",SimHashes.Sulfur.CreateTag().ProperName(),": 11%." +
                "\n",SimHashes.Algae.CreateTag().ProperName(),": 11%.\n"
            }));
            complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("MineralDrill")
            };

            //===> IRON DRILL <=========================================================================================
            ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Iron.CreateTag(), 400f)
            };
            ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id2 = ComplexRecipeManager.MakeRecipeID("MineralDrill", array3, array4);
            ComplexRecipe complexRecipe2 = new ComplexRecipe(id2, array3, array4);
            complexRecipe2.time = 150f;
            complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
            complexRecipe2.description = string.Format(string.Concat(new string[]
            {
                "Engage a drilling operation using ",SimHashes.Iron.CreateTag().ProperName()," as drill bits." +
                "Possible minerals availabe at this layer:" +
                "\n",OilShaleElement.SolidOilShaleSimHash.CreateTag().ProperName(),": 29%." +
                "\n",SimHashes.CrushedRock.CreateTag().ProperName(),": 22%." +
                "\n",ArgentiteElement.ArgentiteOreSimHash.CreateTag().ProperName(),": 7%." +
                "\n",AurichalciteElement.AurichalciteOreSimHash.CreateTag().ProperName(),": 7%." +
                "\n",SimHashes.GoldAmalgam.CreateTag().ProperName(),": 7%." +
                "\n",SimHashes.IronOre.CreateTag().ProperName(),": 7%." +
                "\n",SimHashes.AluminumOre.CreateTag().ProperName(),": 7%." +
                "\n",SimHashes.Cuprite.CreateTag().ProperName(),": 7%." +
                "\n",SimHashes.Salt.CreateTag().ProperName(),": 7%.\n"
            }));
            complexRecipe2.fabricators = new List<Tag>
            {
                TagManager.Create("MineralDrill")
            };

            //===> STEEL DRILL <========================================================================================
            ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Steel.CreateTag(), 200f)
            };
            ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            };
            string id3 = ComplexRecipeManager.MakeRecipeID("MineralDrill", array5, array6);
            ComplexRecipe complexRecipe3 = new ComplexRecipe(id3, array5, array6);
            complexRecipe3.time = 240f;
            complexRecipe3.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
            complexRecipe3.description = string.Format(string.Concat(new string[]
            {
                "Engage a drilling operation using ",SimHashes.Steel.CreateTag().ProperName()," as drill bits." +
                "Possible minerals availabe at this layer:" +
                "\n",OilShaleElement.SolidOilShaleSimHash.CreateTag().ProperName(),": 44.5%." +
                "\n",SimHashes.CrushedRock.CreateTag().ProperName(),": 22.5%." +
                "\n",SimHashes.Fossil.CreateTag().ProperName(),": 11%." +
                "\n",SimHashes.Phosphorite.CreateTag().ProperName(),": 11%." +
                "\n",SimHashes.Wolframite.CreateTag().ProperName(),": 11%.\n"
            }));
            complexRecipe3.fabricators = new List<Tag>
            {
                TagManager.Create("MineralDrill")
            };

            //if (DlcManager.IsExpansion1Active())
            //{
            //    //===> TUNGSTEN DRILL - DLC1 <==========================================================================
            //    ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
            //    {
            //    new ComplexRecipe.RecipeElement(SimHashes.Tungsten.CreateTag(), 200f)
            //    };
            //    ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
            //    {
            //    new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            //    };
            //    string id4 = ComplexRecipeManager.MakeRecipeID("MineralDrill", array7, array8);
            //    ComplexRecipe complexRecipe4 = new ComplexRecipe(id4, array7, array8);
            //    complexRecipe4.time = 240f;
            //    complexRecipe4.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
            //    complexRecipe4.description = string.Format(string.Concat(new string[]
            //    {
            //    "Engage a drilling operation using ",SimHashes.Tungsten.CreateTag().ProperName()," as drill bits." +
            //    "Possible minerals availabe at this layer:" +
            //    "\n",SimHashes.Katairite.CreateTag().ProperName(),": 37.5%." +
            //    "\n",SimHashes.Fossil.CreateTag().ProperName(),": 25%." +
            //    "\n",SimHashes.Diamond.CreateTag().ProperName(),": 12.5%." +
            //    "\n",SimHashes.Graphite.CreateTag().ProperName(),": 12.5%." +
            //    "\n",SimHashes.UraniumOre.CreateTag().ProperName(),": 12.5%.\n"
            //    }));
            //    complexRecipe4.fabricators = new List<Tag>
            //    {
            //        TagManager.Create("MineralDrill")
            //    };
            //}
            //else if (DlcManager.IsPureVanilla())
            //{
            //    //===> TUNGSTEN DRILL - VANILLA <==========================================================================
            //    ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
            //    {
            //    new ComplexRecipe.RecipeElement(SimHashes.Tungsten.CreateTag(), 200f)
            //    };
            //    ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
            //    {
            //    new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
            //    };
            //    string id5 = ComplexRecipeManager.MakeRecipeID("MineralDrill", array9, array10);
            //    ComplexRecipe complexRecipe5 = new ComplexRecipe(id5, array9, array10);
            //    complexRecipe5.time = 240f;
            //    complexRecipe5.nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient;
            //    complexRecipe5.description = string.Format(string.Concat(new string[]
            //    {
            //    "Engage a drilling operation using ",SimHashes.Tungsten.CreateTag().ProperName()," as drill bits." +
            //    "Possible minerals availabe at this layer:" +
            //    "\n",SimHashes.Katairite.CreateTag().ProperName(),": 50%." +
            //    "\n",SimHashes.Fossil.CreateTag().ProperName(),": 33%." +
            //    "\n",SimHashes.Diamond.CreateTag().ProperName(),": 17%.\n"
            //    }));
            //    complexRecipe5.fabricators = new List<Tag>
            //    {
            //        TagManager.Create("MineralDrill")
            //    };
            //}
        }

        public override BuildingDef CreateBuildingDef()
        {
            EffectorValues noise = NOISE_POLLUTION.NOISY.TIER2;
            BuildingDef def = BuildingTemplates.CreateBuildingDef("MineralDrill", 4, 6, "mineral_drill_kanim", 100, 120f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.NONE, noise, 0.2f);
            BuildingTemplates.CreateElectricalBuildingDef(def);
            def.SceneLayer = Grid.SceneLayer.BuildingFront;
            def.EnergyConsumptionWhenActive = 1000f;
            def.SelfHeatKilowattsWhenActive = 1f;
            def.PowerInputOffset = new CellOffset(1, 1);
            def.OverheatTemperature = 2273.15f;
            def.Floodable = false;
            def.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
            def.AttachmentSlotTag = (Tag)"mineralDrill";
            def.BuildLocationRule = BuildLocationRule.BuildingAttachPoint;
            def.ObjectLayer = ObjectLayer.AttachableBuilding;
            return def;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<PoweredActiveController.Def>().showWorkingStatus = true;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            base.DoPostConfigurePreview(def, go);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            base.DoPostConfigureUnderConstruction(go);
        }
    }
}