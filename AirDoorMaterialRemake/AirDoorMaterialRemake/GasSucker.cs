using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Klei;
using STRINGS;
using KSerialization;
namespace ElementGenerator
{ 
	public class GasSucker : KMonoBehaviour, ISim1000ms,  ISingleSliderControl, ISliderControl
	{
		internal static readonly HashSet<int> SoggyCells = new HashSet<int>();

		public float dripRate = 100f;
		public float dripKG = 100f;
		[Serialize]
		private float sliderTemperature = 23f;
		[Serialize]
		private float outTemperature = 23f;

		//可过滤的元素
		public FilterbleElement filterbleElementGas ;

		public Element selectedElement;

		private int myCell;
		private int cellBelow;
		private int inputCell = -1;
		private int outputCell = -1;

		//private static StatusItem filterStatusItem;

		[MyCmpGet]
		private Building building;

		[MyCmpReq]
		private Operational operational;

		[MyCmpGet]
		private Door myDoor;

		[MyCmpGet]
		private Storage storage;


		private Rotatable rotatable;

		protected override void OnSpawn()
		{
			myCell = building.GetCell();
			Debug.Log("mycell: " + myCell);
			Vector3 position = transform.GetPosition();
			rotatable = GetComponent<Rotatable>();
			Debug.Log("rotatable: " + rotatable.ToString());
			if (myCell > 0)
			{
				outputCell = getOutPutCellByDirection();
				inputCell = getInPutCellByDirection();
			}
		
			//outputCell = Grid.PosToCell(position + rotatedOutputOffset);
			//inputCell = Grid.PosToCell(position + rotatedInputOffset);


			//选择元素(作废)
			//Building component = base.GetComponent<Building>();
			//this.OnFilterChanged(ElementLoader.FindElementByHash(this.FilteredElement).tag);
			//this.filterable.onFilterChanged += this.OnFilterChanged;
			//base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, GasSucker.filterStatusItem, this);
		}

		/// <summary>
		/// 根据建筑方向获取输出元素的格子位置
		/// </summary>
		/// <returns></returns>
		private int getOutPutCellByDirection()
		{
			Orientation op = rotatable.GetOrientation();
			Debug.Log("output建筑朝向:" + op.ToString());
			switch (op)
			{
				
				case Orientation.R90:
					return Grid.CellRight(myCell);
				case Orientation.R180:
					return Grid.CellAbove(myCell);
				case Orientation.R270:
					return Grid.CellLeft(myCell);
				case Orientation.Neutral:
					return Grid.CellBelow(myCell);
				default:
					return myCell;
			}
		}

		private int getInPutCellByDirection()
		{
			Orientation op = rotatable.GetOrientation();
			Debug.Log("input建筑朝向:" + op.ToString());
			switch (op)
			{
				case Orientation.R90:
					return Grid.CellLeft(myCell);
				case Orientation.R180:
					return Grid.CellBelow(myCell);
				case Orientation.R270:
					return Grid.CellRight(myCell);
				case Orientation.Neutral:
					return Grid.CellAbove(myCell);
				default:
					return myCell;
			}
		}


		protected override void OnCleanUp()
		{
			SoggyCells.Remove(building.GetCell());
		}

		public void Sim1000ms(float dt)
		{
		
			Emit(dt);
			if (storage.MassStored() > 0f)
			{
				SoggyCells.Add(myCell);
			}
			else
			{
				SoggyCells.Remove(myCell);
			}
		}

		#region 温控滑动条
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
		}
		// Token: 0x06000024 RID: 36 RVA: 0x000029C4 File Offset: 0x00000BC4
		public float GetSliderMin(int index)
		{
			return ( -270f);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000029F8 File Offset: 0x00000BF8
		public float GetSliderMax(int index)
		{
		
			return  9000f;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A2A File Offset: 0x00000C2A
		public float GetSliderValue(int index)
		{
			return this.sliderTemperature;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A32 File Offset: 0x00000C32
		public void SetSliderValue(float percent, int index)
		{
			this.sliderTemperature = percent;
		}

		public string GetSliderTooltipKey(int index)
		{
			return "STRINGS.UI.UISIDESCREENS.LIQUIDSOURCE.TOOLTIP";
		}
		public string GetSliderTooltip()
		{
			return "元素经过后的温度";
		}
		public int SliderDecimalPlaces(int index)
		{
			return 1;
		}
		public string SliderTitleKey
		{
			get
			{
				return "STRINGS.UI.UISIDESCREENS.LIQUIDSOURCE.TITLE";
			}
		}
		public string SliderUnits
		{
			get
			{
				return UI.UNITSUFFIXES.TEMPERATURE.CELSIUS;
			}
		}

		#endregion



		private void Emit(float dt)
		{
			if(myDoor.CurrentState==Door.ControlState.Locked)
            {
				return;
            }
			if (myCell <= 0)
			{
				return;
			}
			//筛选的元素
			this.selectedElement = base.GetComponent<FilterbleElement>().element;

		

			GasSucker carpetBelow = null;

			bool doDrip = true;

			if (Grid.Solid[outputCell])
			{
				// Do not drip if solid tile below
				doDrip = false;

				// But...
				Grid.ObjectLayers[(int)ObjectLayer.FoundationTile].TryGetValue(cellBelow, out GameObject go);
				if (go != null)
				{
					KPrefabID prefabID = go.GetComponent<KPrefabID>();

					// Drip if there's a mesh tile below
					if (prefabID.PrefabTag == MeshTileConfig.ID) doDrip = true;

					// Drip if there's another tile below, mass transfer implemented below
					carpetBelow = go.GetComponent<GasSucker>();
					if (carpetBelow != null) doDrip = true;
				}
			}

			if (!doDrip) return;



			//待输出的具体元素
			Element elementOut = selectedElement;
			//如果筛选器选择了None,则输出inputCell内的元素 
			if (selectedElement == null || selectedElement.tag == GameTags.Void)
			{
				//避免输出大于零质量的真空
				if (Grid.Element[inputCell].tag.Name == "Vacuum")
					return;
				elementOut = Grid.Element[inputCell];
			}
			//Debug.Log("outputElement:" + elementOut.tag);

			//PrimaryElement firstPrimaryElement = storage.FindFirstWithMass(GameTags.Gas);
			//if (elementOutPE == null) return;

			//if (elementOut == null) return;
			//if (!elementOut.IsGas) return;


			//float massToDrip = Mathf.Max(exhaustPE.Mass, dripRate * dt);
			//if (massToDrip <= 0f) return;

			//Tag prefabTag = exhaustPE.GetComponent<KPrefabID>().PrefabTag;
			//float mass;
			//storage.ConsumeAndGetDisease(prefabTag, massToDrip, out mass, out SimUtil.DiseaseInfo diseaseInfo, out outTemperature);
			//设置经过后的元素温度
			this.outTemperature = this.GetSliderValue(Convert.ToInt32( this.sliderTemperature) )+ 273.15f;

			Debug.Log("物质已生成:"+ elementOut.tag);
			FallingWater.instance.AddParticle(outputCell, elementOut.idx, dripKG, outTemperature, SimUtil.DiseaseInfo.Invalid.idx, 0, true, false, false, false);
			
		
		
			//Grid.Element[outputCell] = null;
			//SimMessages.AddRemoveSubstance(outputCell, elementIndex, CellEventLogger.Instance.ExhaustSimUpdate, exhaustPE.Mass, outTemperature, SimUtil.DiseaseInfo.Invalid.idx, 0, true, -1);
			//if (carpetBelow != null)
			//{
			//	SimMessages.AddRemoveSubstance(outputCell, elementIndex, CellEventLogger.Instance.ExhaustSimUpdate, exhaustPE.Mass, outTemperature, SimUtil.DiseaseInfo.Invalid.idx, 0, true, -1);
			//	//carpetBelow.storage.AddGasChunk (elementBefore.id, exhaustPE.Mass, outTemperature, SimUtil.DiseaseInfo.Invalid.idx,0,false);
			//}
			//else
			//{
			//	//SimMessages.AddRemoveSubstance(outputCell, elementIndex, CellEventLogger.Instance.ExhaustSimUpdate, exhaustPE.Mass, outTemperature, SimUtil.DiseaseInfo.Invalid.idx, 0, true, -1);
			//	FallingWater.instance.AddParticle(myCell, elementOut.idx, exhaustPE.Mass, outTemperature , SimUtil.DiseaseInfo.Invalid.idx, 0, true, false, false, false);
			//}

			//elementOutPE.KeepZeroMassObject = true;
			//elementOutPE.Mass = 0.0f;
		}
	}

}