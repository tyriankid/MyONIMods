using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000575 RID: 1397
[SerializationConfig(MemberSerialization.OptIn)]
public class ToggleSwitch : Switch
{
	// Token: 0x0600231F RID: 8991 RVA: 0x000B8680 File Offset: 0x000B6880
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.switchedOn = false;
		this.UpdateVisualState(false, false);
		int cell = Grid.PosToCell(base.transform.GetPosition());
		base.OnToggle += this.OnSwitchToggled;
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x000B8720 File Offset: 0x000B6920
	private void OnSwitchToggled(bool toggled_on)
	{
		bool connected = false;
		if (this.operational.IsOperational && toggled_on)
		{
			Debug.Log("OnSwitchToggled 开");
			this.operational.SetActive(true, false);
		}
		else
		{
			Debug.Log("OnSwitchToggled 关");
			this.operational.SetActive(false, false);
		}
		this.UpdateVisualState(connected, false);
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x000B8769 File Offset: 0x000B6969
	private void OnOperationalChanged(object data)
	{
		if (this.operational.IsOperational)
		{
			this.SetState(LogicCircuitNetwork.IsBitActive(0, this.logic_value));
			return;
		}
		this.UpdateVisualState(false, false);
	}


	// Token: 0x06002323 RID: 8995 RVA: 0x000B8A1C File Offset: 0x000B6C1C
	private void UpdateVisualState(bool connected, bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			if (this.switchedOn)
			{
				Debug.Log("UpdateVisualState 开");
			}
			else
			{
				Debug.Log("UpdateVisualState 关");
			}
		}
	}




	// Token: 0x04001487 RID: 5255
	[MyCmpGet]
	private Operational operational;


	// Token: 0x0400148B RID: 5259
	private int logic_value;

	// Token: 0x0400148C RID: 5260
	private bool wasOn;

}
