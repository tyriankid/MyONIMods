using System;

namespace TempretureControler
{
	public class HeatingElementStateMachine : GameStateMachine<HeatingElementStateMachine, HeatingElementStateMachine.Instance>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002154 File Offset: 0x00000354
		public override void InitializeStates(out StateMachine.BaseState defaultState)
		{
			defaultState = this.Off;
			this.Off.Enter("SetInactive", delegate (HeatingElementStateMachine.Instance smi)
			{
				//smi.GetComponent<KBatchedAnimController>().Play(OFF_ANIM, KAnim.PlayMode.Paused);
				smi.GetComponent<KBatchedAnimController>().Play(HeatingElementStateMachine.OFF_ANIMS, 0);
			}).EventTransition(GameHashes.OperationalChanged, this.On, (HeatingElementStateMachine.Instance smi) => smi.GetComponent<Operational>().IsOperational);
			this.On.Enter("SetActive", delegate (HeatingElementStateMachine.Instance smi)
			{
				smi.GetComponent<KBatchedAnimController>().Play(HeatingElementStateMachine.ON_ANIMS, 0);
				smi.GetComponent<Operational>().SetActive(true, false);
			}).EventTransition(GameHashes.OperationalChanged, this.Off, (HeatingElementStateMachine.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		}

		// Token: 0x04000003 RID: 3
		public GameStateMachine<HeatingElementStateMachine, HeatingElementStateMachine.Instance, IStateMachineTarget, object>.State Off;

		// Token: 0x04000004 RID: 4
		public GameStateMachine<HeatingElementStateMachine, HeatingElementStateMachine.Instance, IStateMachineTarget, object>.State On;

		// Token: 0x04000005 RID: 5
		//private static readonly HashedString ON_ANIM = "on_pre";
		//private static readonly HashedString OFF_ANIM = "on_pst";


		private static readonly HashedString[] ON_ANIMS = new HashedString[]
		{
		"on",
		"working_loop"
		};

		// Token: 0x04000006 RID: 6
		private static readonly HashedString[] OFF_ANIMS = new HashedString[]
		{
			"off",
			"closed"
		};

		// Token: 0x02000007 RID: 7
		public class Instance : GameStateMachine<HeatingElementStateMachine, HeatingElementStateMachine.Instance, IStateMachineTarget, object>.GameInstance
		{
			// Token: 0x0600000F RID: 15 RVA: 0x00002374 File Offset: 0x00000574
			public Instance(IStateMachineTarget master) : base(master)
			{
			}
		}
	}
}
