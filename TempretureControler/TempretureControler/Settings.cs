using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Klei;
using STRINGS;
using KSerialization;
using HarmonyLib;
using TempretureControler;

[SerializationConfig(KSerialization.MemberSerialization.OptIn)]
public class CoolingElementSideScreen : SideScreen,  ISaveLoadable, ISingleSliderControl, ISliderControl
{

	#region 温控滑动条
	public  float sliderDTUS = -300f;

	// Token: 0x06000024 RID: 36 RVA: 0x000029C4 File Offset: 0x00000BC4
	public float GetSliderMin(int index)
	{
		return (1f);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000029F8 File Offset: 0x00000BF8
	public float GetSliderMax(int index)
	{

		return 9000f;
	}

	//[MyCmpGet]
	//private BuildingDef building;
	//protected override void OnSpawn()
	//{
	//	building.ExhaustKilowattsWhenActive= this.GetSliderValue(-Convert.ToInt32(this.sliderDTUS)) ;
	//}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A2A File Offset: 0x00000C2A
		public float GetSliderValue(int index)
	{
		return this.sliderDTUS;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002A32 File Offset: 0x00000C32
	public void SetSliderValue(float percent, int index)
	{
		this.sliderDTUS = percent;
	}

	public string GetSliderTooltipKey(int index)
	{
		return "制冷效率设置";
	}
	public string GetSliderTooltip()
	{
		return "制冷器的效率 单位DTU/S";
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
			return "";
		}
	}

	#endregion

}
