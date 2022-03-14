using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000006 RID: 6
internal class InsulatingDoor : KMonoBehaviour
{
	// Token: 0x0600000F RID: 15 RVA: 0x000027A0 File Offset: 0x000009A0
	public void SetInsulation(GameObject go, float insulation)
	{
		IList<int> placementCells = go.GetComponent<Building>().PlacementCells;
		for (int i = 0; i < placementCells.Count; i++)
		{
			SimMessages.SetInsulation(placementCells[i], insulation);
		}
	}

	// Token: 0x04000011 RID: 17
	[MyCmpGet]
	public Door door;
}
