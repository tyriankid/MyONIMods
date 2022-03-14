using KSerialization;
using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fans
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class Fan : KMonoBehaviour,ISim33ms, ISim200ms, IEffectDescriptor
    {
        public static readonly Operational.Flag FanInFlag = new Operational.Flag("fanIn", Operational.Flag.Type.Requirement);
        public static readonly Operational.Flag FanOutFlag = new Operational.Flag("fanOut", Operational.Flag.Type.Requirement);

        [SerializeField]
        public ConduitType conduitType;
        [SerializeField]
        public float overpressureMass = 1f;
        //可过滤的元素
        public Element selectedElement;
        [MyCmpGet]
        private Storage storage;
        [MyCmpReq]
        private Operational operational;
        [MyCmpGet]
        private KSelectable selectable;
        [MyCmpGet]
        private PrimaryElement exhaustPE;

        private const float OperationalUpdateInterval = 1f;
        private float elapsedTime;
        private bool pumpable;
        private bool ventable;

        private Guid obstructedStatusGuid;
        private Guid overPressureStatusGuid;
        private Guid noElementStatusGuid;
        private Guid wrongElementStatusGuid;

        private int inputCell = -1;
        private int outputCell = -1;


        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();

            Vector3 position = transform.GetPosition();
            Rotatable rotatable = GetComponent<Rotatable>();
            Vector3 rotatedInputOffset = Rotatable.GetRotatedOffset(new Vector3(0, -1), rotatable.GetOrientation());
            Vector3 rotatedOutputOffset = Rotatable.GetRotatedOffset(new Vector3(0, 1), rotatable.GetOrientation());
            inputCell = Grid.PosToCell(position + rotatedInputOffset);
            outputCell = Grid.PosToCell(position + rotatedOutputOffset);
            selectedElement = base.GetComponent<FilterbleElementGas>().element;
            elapsedTime = 0.0f;
            pumpable = UpdatePumpOperational();
            ventable = UpdateVentOperational();
        }

        public void Sim33ms(float dt)
        {
            //每33ms判断一次待输入元素是否可输出,若不能,则清除当前库存内的元素
            //pumpable = UpdatePumpOperational();
            //DoFan(pumpable);
            DoFan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void Sim200ms(float dt)
        {
            elapsedTime += dt;

            if (elapsedTime >= OperationalUpdateInterval)
            {
                pumpable = UpdatePumpOperational();
                ventable = UpdateVentOperational();
                elapsedTime = 0.0f;
            }
            
            // perform pumping

            if (operational.IsOperational && pumpable && ventable)
                operational.SetActive(true, false);
            else
                operational.SetActive(false, false);
        }

        private void DoFan()
        {
            if (storage.items.Count == 0)
            {
                return;
            }
            if (Grid.Solid[outputCell])
            {
                return;
            }

                

            switch (conduitType)
            {
                case ConduitType.Gas:
                    EmitGas(outputCell);
                    break;
                case ConduitType.Liquid:
                    EmitLiquid(outputCell);
                    break;
            }
            
        }

        /// <summary>
        /// 获取输出格子是否能输出
        /// </summary>
        /// <returns></returns>
        public Vent.State GetEndPointState()
        {
            Vent.State state = Vent.State.Ready;
            if (!IsValidOutputCell(outputCell))
            {
                state = !Grid.Solid[outputCell] ? Vent.State.OverPressure : Vent.State.Blocked;
            }
            if (!IsOutputCellSelectedElement(outputCell))
            {
                state = Vent.State.Invalid;
            }
            return state;
        }

        public bool IsBlocked
        {
            get
            {
                return GetEndPointState() != Vent.State.Ready;
            }
        }

        /// <summary>
        /// 判断格子是否固体
        /// </summary>
        /// <param name="output_cell"></param>
        /// <returns></returns>
        private bool IsValidOutputCell(int output_cell)
        {
            bool flag = false;
            if (!Grid.Solid[output_cell])
            {
                flag = true;
                if (overpressureMass >= 0.0f)
                {
                    flag = Grid.Mass[output_cell] < (double)overpressureMass;
                }
            }
            return flag;
        }

        /// <summary>
        /// 判断待输入元素是否为过滤的元素
        /// </summary>
        /// <param name="output_cell"></param>
        /// <returns></returns>
        private bool IsOutputCellSelectedElement(int output_cell)
        {
            bool flag = false;
            Element elementIn = Grid.Element[output_cell];
            if (selectedElement != null && selectedElement.tag != GameTags.Void)
            {
                if (selectedElement.tag == elementIn.tag)
                {
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        /// 更新吸入
        /// </summary>
        /// <returns></returns>
        private bool UpdatePumpOperational()
        {
            Element.State expected_state = Element.State.Vacuum;
            switch (conduitType)
            {
                case ConduitType.Gas:
                    expected_state = Element.State.Gas;
                    break;
                case ConduitType.Liquid:
                    expected_state = Element.State.Liquid;
                    break;
            }
            bool flag = IsPumpable(expected_state);
            //noElementStatusGuid = selectable.ToggleStatusItem(expected_state != Element.State.Gas ? Db.Get().BuildingStatusItems.NoLiquidElementToPump : Db.Get().BuildingStatusItems.NoGasElementToPump, noElementStatusGuid, !flag, null);
            operational.SetFlag(FanInFlag, flag);
            return flag;
        }

        /// <summary>
        /// 更新输出
        /// </summary>
        /// <returns></returns>
        private bool UpdateVentOperational()
        {
            Vent.State outputState = GetEndPointState();
            bool obstructedFlag = outputState == Vent.State.Blocked;
            bool overPressureFlag = outputState == Vent.State.OverPressure;
            //bool wrongElementFlag = outputState == Vent.State.Invalid;
            obstructedStatusGuid = selectable.ToggleStatusItem(conduitType != ConduitType.Gas ? Db.Get().BuildingStatusItems.LiquidVentObstructed : Db.Get().BuildingStatusItems.GasVentObstructed, obstructedStatusGuid, obstructedFlag, null);
            overPressureStatusGuid = selectable.ToggleStatusItem(conduitType != ConduitType.Gas ? Db.Get().BuildingStatusItems.LiquidVentOverPressure : Db.Get().BuildingStatusItems.GasVentOverPressure, overPressureStatusGuid, overPressureFlag, null);
            //wrongElementStatusGuid = wrongElementStatusGuid = selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NoFilterElementSelected, wrongElementStatusGuid, wrongElementFlag, null);
            bool flag = !obstructedFlag && !overPressureFlag; //&& !wrongElementFlag;
            //如果所有验证都为true,则将输出元素设置为true
            operational.SetFlag(FanOutFlag, flag);
            return flag;
        }

        public List<Descriptor> GetDescriptors(BuildingDef def)
        {
            if (overpressureMass < 0.0f)
            {
                return new List<Descriptor>() { };
            }
            string formattedMass = GameUtil.GetFormattedMass(overpressureMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
            return new List<Descriptor>()
            {
                new Descriptor(string.Format(UI.BUILDINGEFFECTS.OVER_PRESSURE_MASS, formattedMass), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.OVER_PRESSURE_MASS, formattedMass), Descriptor.DescriptorType.Effect, false)
            };
        }

        

        /// <summary>
        /// 判断是否能抽取上方元素
        /// </summary>
        /// <param name="expected_state"></param>
        /// <returns></returns>
        private bool IsPumpable(Element.State expected_state)
        {
            //this.selectedElement = base.GetComponent<FilterbleElementGas>().element;
            //Debug.Log("选中的元素: "+ selectedElement.tag.Name);
            Element inputCellElement = Grid.Element[inputCell];
            ////如果带输入的元素是真空,则直接返回
            if (Grid.Element[inputCell].IsState(expected_state) && inputCellElement.tag != GameTags.Void)
            {
                return true;
            }
            else
            {
                return false;
            }
            ////判断待输入的元素是否与选择的元素相等

            //bool isElementRight = inputCellElement.tag == selectedElement.tag || selectedElement.tag== GameTags.Void;
            ////bool isPumpableState = inputCellElement.IsState(expected_state);
            //if(!isElementRight)
            //    Debug.Log(inputCellElement.tag.Name + "不能输出,退回");
            //else
            //    Debug.Log(inputCellElement.tag.Name + "可以输出,输出");
            //return  isElementRight;
        }



        

        private void CalculateDiseaseTransfer(
          PrimaryElement item1,
          PrimaryElement item2,
          float transfer_rate,
          out int disease_to_item1,
          out int disease_to_item2)
        {
            disease_to_item1 = (int)(item2.DiseaseCount * transfer_rate);
            disease_to_item2 = (int)(item1.DiseaseCount * transfer_rate);
        }

        private bool EmitCommon(int cell, PrimaryElement primary_element, EmitDelegate emit)
        {
            if (primary_element.Mass <= 0.0)
                return false;
            int disease_to_item1;
            int disease_to_item2;
            CalculateDiseaseTransfer(exhaustPE, primary_element, 0.05f, out disease_to_item1, out disease_to_item2);
            primary_element.ModifyDiseaseCount(-disease_to_item1, "Exhaust transfer");
            primary_element.AddDisease(exhaustPE.DiseaseIdx, disease_to_item2, "Exhaust transfer");
            exhaustPE.ModifyDiseaseCount(-disease_to_item2, "Exhaust transfer");
            exhaustPE.AddDisease(primary_element.DiseaseIdx, disease_to_item1, "Exhaust transfer");
            emit(cell, primary_element);
            primary_element.KeepZeroMassObject = true;
            primary_element.Mass = 0.0f;
            primary_element.ModifyDiseaseCount(int.MinValue, "Exhaust.SimUpdate");
            return true;
        }

        private void EmitLiquid(int cell)
        {
            int cell1 = Grid.CellBelow(cell);
            EmitDelegate emit = !Grid.IsValidCell(cell1) || Grid.Solid[cell1] ? emit_element : emit_particle;
            foreach (GameObject gameObject in storage.items)
            {
                PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
                //筛选的元素
                this.selectedElement = base.GetComponent<FilterbleElementGas>().element;
                Element elementIn = component.Element;
                //Debug.Log("筛选的元素" + selectedElement.tag + " primary元素" + component.Element.tag + " inputcell元素" + Grid.Element[inputCell].tag.Name);
                if (selectedElement != null && selectedElement.tag != GameTags.Void)
                {
                    if (elementIn.tag != selectedElement.tag)
                    {
                        //Debug.Log("不匹配的元素,返回");
                        cell = inputCell;
                    }
                    else
                    {
                        cell = outputCell;
                        //Debug.Log("匹配的元素,输出");
                    }
                }
                
                if (component.Element.IsLiquid && EmitCommon(cell, component, emit))
                    break;
            }
        }

        private void EmitGas(int cell)
        {
            foreach (GameObject gameObject in storage.items)
            {
                PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
                //筛选的元素
                this.selectedElement = base.GetComponent<FilterbleElementGas>().element;
                Element elementIn = component.Element;
                //Debug.Log("筛选的元素" + selectedElement.tag + " primary元素" + component.Element.tag + " inputcell元素" + Grid.Element[inputCell].tag.Name);
                if (selectedElement != null && selectedElement.tag != GameTags.Void)
                {
                    if (elementIn.tag != selectedElement.tag)
                    {
                        //Debug.Log("不匹配的元素,返回");
                        cell = inputCell;
                    }
                    else
                    {
                        cell = outputCell;
                        //Debug.Log("匹配的元素,输出");
                    }
                }
                ////筛选的元素
                //this.selectedElement = base.GetComponent<FilterbleElementGas>().element;

                //if (selectedElement != null && selectedElement.tag != GameTags.Void)
                //{
                //    Vent.State outputState = GetEndPointState();
                //    bool wrongElementFlag = outputState == Vent.State.Invalid;
                //    if (component.Element.tag != selectedElement.tag)
                //    {
                //        wrongElementStatusGuid = selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NoFilterElementSelected, wrongElementStatusGuid, wrongElementFlag, null);

                //        operational.SetFlag(FanOutFlag, false);
                //    }

                //}
                if (component.Element.IsGas && EmitCommon(cell, component, emit_element))
                    break;
            }
        }

        private delegate void EmitDelegate(int cell, PrimaryElement primary_element);
        private static EmitDelegate emit_element = (cell, primary_element) => SimMessages.AddRemoveSubstance(cell, primary_element.ElementID, CellEventLogger.Instance.ExhaustSimUpdate, primary_element.Mass, primary_element.Temperature, primary_element.DiseaseIdx, primary_element.DiseaseCount, true, -1);
        private static EmitDelegate emit_particle = (cell, primary_element) => FallingWater.instance.AddParticle(cell, (byte)ElementLoader.elements.IndexOf(primary_element.Element), primary_element.Mass, primary_element.Temperature, primary_element.DiseaseIdx, primary_element.DiseaseCount, true, false, true, false);
    }
}
