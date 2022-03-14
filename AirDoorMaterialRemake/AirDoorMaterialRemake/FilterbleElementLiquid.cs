using System;

// Token: 0x02000532 RID: 1330
public class FilterbleElementLiquid : DevPump, ISim1000ms
{
	// Token: 0x170001BD RID: 445
	// (get) Token: 0x0600202D RID: 8237 RVA: 0x000A9DD4 File Offset: 0x000A7FD4
	public Element element
	{
		get
		{
			if (base.SelectedTag.IsValid)
			{
				return ElementLoader.GetElement(base.SelectedTag);
			}
			return ElementLoader.FindElementByHash(SimHashes.Void);
		}
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x000A9E08 File Offset: 0x000A8008
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		//初始为void，为void时不过滤任何元素
		base.SelectedTag = ElementLoader.FindElementByHash(SimHashes.Void).tag;
		//if (this.elementState == Filterable.ElementState.Liquid)
		//{
		//	base.SelectedTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		//	return;
		//}
		//if (this.elementState == Filterable.ElementState.Gas)
		//{
		//	base.SelectedTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		//}
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x000A9E58 File Offset: 0x000A8058
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.filterElementState = this.elementState;
	}

	// Token: 0x06002030 RID: 8240 RVA: 0x000A9E6C File Offset: 0x000A806C
	public void Sim1000ms(float dt)
	{
		//float num = 10f - this.storage.GetAmountAvailable(this.element.tag);
		//if (num <= 0f)
		//{
		//	return;
		//}
		//if (this.element.IsLiquid)
		//{
		//	this.storage.AddLiquid(this.element.id, num, this.element.defaultValues.temperature, byte.MaxValue, 0, false, true);
		//	return;
		//}
		//if (this.element.IsGas)
		//{
		//	this.storage.AddGasChunk(this.element.id, num, this.element.defaultValues.temperature, byte.MaxValue, 0, false, true);
		//}
	}

	// Token: 0x0400122F RID: 4655
	public Filterable.ElementState elementState = Filterable.ElementState.Liquid;

	// Token: 0x04001230 RID: 4656
	[MyCmpReq]
	private Storage storage;
}
