using System;

	public class TinySuckDoorStateMachine : GameStateMachine<TinySuckDoorStateMachine, TinySuckDoorStateMachine.Instance>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002154 File Offset: 0x00000354
		public override void InitializeStates(out StateMachine.BaseState defaultState)
		{
			defaultState = this.Off;
			this.Off.Enter("SetInactive", delegate (TinySuckDoorStateMachine.Instance smi)
			{
				//smi.GetComponent<KBatchedAnimController>().Play(OFF_ANIM, KAnim.PlayMode.Paused);
				smi.GetComponent<KBatchedAnimController>().Play(TinySuckDoorStateMachine.OFF_ANIMS, 0);
			}).EventTransition((GameHashes)Enum.Parse(typeof(GameHashes), "-592767678"), this.On, (TinySuckDoorStateMachine.Instance smi) => smi.GetComponent<Operational>().IsOperational);
			this.On.Enter("SetActive", delegate (TinySuckDoorStateMachine.Instance smi)
			{
				smi.GetComponent<KBatchedAnimController>().Play(TinySuckDoorStateMachine.ON_ANIMS, 0);
				smi.GetComponent<Operational>().SetActive(true, false);
			}).EventTransition((GameHashes)Enum.Parse(typeof(GameHashes), "-592767678"), this.Off, (TinySuckDoorStateMachine.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		}

		// Token: 0x04000003 RID: 3
		public GameStateMachine<TinySuckDoorStateMachine, TinySuckDoorStateMachine.Instance, IStateMachineTarget, object>.State Off;

		// Token: 0x04000004 RID: 4
		public GameStateMachine<TinySuckDoorStateMachine, TinySuckDoorStateMachine.Instance, IStateMachineTarget, object>.State On;

		// Token: 0x04000005 RID: 5
		//private static readonly HashedString ON_ANIM = "on_pre";
		//private static readonly HashedString OFF_ANIM = "on_pst";


		private static readonly HashedString[] ON_ANIMS = new HashedString[]
		{
		"on",
		"open"
		};

		// Token: 0x04000006 RID: 6
		private static readonly HashedString[] OFF_ANIMS = new HashedString[]
		{
			"off",
			"locked"
		//"working_pst",
		//"off"
		};

		// Token: 0x02000007 RID: 7
		public class Instance : GameStateMachine<TinySuckDoorStateMachine, TinySuckDoorStateMachine.Instance, IStateMachineTarget, object>.GameInstance
		{
			// Token: 0x0600000F RID: 15 RVA: 0x00002374 File Offset: 0x00000574
			public Instance(IStateMachineTarget master) : base(master)
			{
			}
		}
	}

