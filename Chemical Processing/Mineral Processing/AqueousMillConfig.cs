using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;


namespace Chemical_Processing.Mineral_Processing
{
    public class AqueousMillConfig : IBuildingConfig
    {
        public const string ID = "AqueousMill";

        public override string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
            ComplexFabricator sludgePress = go.AddOrGet<ComplexFabricator>();
            sludgePress.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            sludgePress.duplicantOperated = false;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
            ComplexFabricatorWorkable workable = go.AddOrGet<ComplexFabricatorWorkable>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, sludgePress);

            ConduitDispenser dispenser = go.AddOrGet<ConduitDispenser>();
            dispenser.conduitType = ConduitType.Liquid;
            dispenser.alwaysDispense = true;
            dispenser.elementFilter = null;
            dispenser.storage = go.GetComponent<ComplexFabricator>().outStorage;
            this.AddRecipes(go);
            Prioritizable.AddRef(go);
        }

        private void AddRecipes(GameObject go)
        {
            float INPUT_KG = 400f;
            List<Element> composites = ElementLoader.elements.FindAll((Element e) => e.elementComposition != null);
            foreach (Element element in composites)
            {
                ComplexRecipe.RecipeElement[] input = new ComplexRecipe.RecipeElement[]
                {
                new ComplexRecipe.RecipeElement(element.tag, INPUT_KG)
                };
                ComplexRecipe.RecipeElement[] output = new ComplexRecipe.RecipeElement[element.elementComposition.Length];
                for (int i = 0; i < element.elementComposition.Length; i++)
                {
                    ElementLoader.ElementComposition comp = element.elementComposition[i];
                    Element compositeElement = ElementLoader.FindElementByName(comp.elementID);
                    bool shouldStore = compositeElement.IsLiquid;
                    output[i] = new ComplexRecipe.RecipeElement(compositeElement.tag, INPUT_KG * comp.percentage, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, shouldStore);
                }
                string obsolete_id = ComplexRecipeManager.MakeObsoleteRecipeID("AqueousMill", element.tag);
                string id = ComplexRecipeManager.MakeRecipeID("AqueousMill", input, output);
                ComplexRecipe complexRecipe = new ComplexRecipe(id, input, output, 0);
                complexRecipe.time = 30f;
                complexRecipe.description = string.Format(STRINGS.BUILDINGS.PREFABS.SLUDGEPRESS.RECIPE_DESCRIPTION, element.name);
                complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Composite;
                complexRecipe.fabricators = new List<Tag>
            {
                TagManager.Create("AqueousMill")
            };
                ComplexRecipeManager.Get().AddObsoleteIDMapping(obsolete_id, id);
            }
        }

        public override BuildingDef CreateBuildingDef()
        {
            EffectorValues noise = TUNING.NOISE_POLLUTION.NOISY.TIER3;
            BuildingDef def = BuildingTemplates.CreateBuildingDef("AqueousMill", 5, 4, "aqueous_mill_kanim", 30, 30f, TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4, MATERIALS.ALL_METALS, 1600f, BuildLocationRule.OnFloor, TUNING.BUILDINGS.DECOR.NONE, noise, 0.2f);
            BuildingTemplates.CreateElectricalBuildingDef(def);
            def.AudioCategory = "Metal";
            def.AudioSize = "large";
            def.EnergyConsumptionWhenActive = 120f;
            def.ExhaustKilowattsWhenActive = 1f;
            def.SelfHeatKilowattsWhenActive = 1f;
            def.OutputConduitType = ConduitType.Liquid;
            def.UtilityOutputOffset = new CellOffset(1, 0);
            def.PowerInputOffset = new CellOffset(0, 0);
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
