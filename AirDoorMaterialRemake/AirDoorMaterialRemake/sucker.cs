using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Klei;
using STRINGS;
using KSerialization;

public class sucker : KMonoBehaviour, ISim200ms, ISaveLoadable, ISingleSliderControl, ISliderControl
{
	internal static readonly HashSet<int> SoggyCells = new HashSet<int>();

	public float dripRate = 5000f;

	[Serialize]
	private float sliderTemperature = 23f;
	[Serialize]
	private float outTemperature = 23f;

	private int myCell;
	private int cellBelow;

	private static StatusItem filterStatusItem;

	[MyCmpGet]
	private Building building;

	[MyCmpGet]
	private Storage storage;

	[MyCmpGet]
	private ElementConsumer consumer;

	protected override void OnSpawn()
	{
		myCell = building.GetCell();
		cellBelow = Grid.GetCellInDirection(myCell, Direction.Down);
		consumer.EnableConsumption(true);

		//Building component = base.GetComponent<Building>();
		//base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, sucker.filterStatusItem, this);
	}

	protected override void OnCleanUp()
	{
		SoggyCells.Remove(building.GetCell());
	}

	public void Sim200ms(float dt)
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
		this.accumulator = Game.Instance.accumulators.Add("Source", this);
	}
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;
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
		return "液体经过后的温度";
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
		sucker carpetBelow = null;

		bool doDrip = true;

		if (Grid.Solid[cellBelow])
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
				carpetBelow = go.GetComponent<sucker>();
				if (carpetBelow != null) doDrip = true;
			}
		}

		if (!doDrip) return;

		PrimaryElement firstPrimaryElement = storage.FindFirstWithMass(GameTags.Liquid);
		if (firstPrimaryElement == null) return;

		Element element = firstPrimaryElement.Element;
		if (element == null) return;
		if (!element.IsLiquid) return;
		byte elementIndex = element.idx;

		float massToDrip = Mathf.Max(firstPrimaryElement.Mass, dripRate * dt);
		if (massToDrip <= 0f) return;

		Tag prefabTag = firstPrimaryElement.GetComponent<KPrefabID>().PrefabTag;
		
		storage.ConsumeAndGetDisease(prefabTag, massToDrip, out float mass, out SimUtil.DiseaseInfo diseaseInfo, out outTemperature);

		this.outTemperature = this.GetSliderValue(Convert.ToInt32( this.sliderTemperature))+ 273.15f;
		if (carpetBelow != null)
		{
			carpetBelow.storage.AddLiquid(element.id, mass, outTemperature, diseaseInfo.idx, diseaseInfo.count);
		}
		else
		{
			FallingWater.instance.AddParticle(myCell, elementIndex, mass, outTemperature , diseaseInfo.idx, 0, true, false, false, false);
		}
	}
}
