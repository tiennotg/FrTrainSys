using System;
using OpenBveApi.Runtime;

namespace FrTrainSys
{
	public class KVB: FrTrainDevice
	{
		private enum GreenKvbAspects
		{
			none,double0, kvbp, running, l, p, b
		};
		
		private enum YellowKvbAspects
		{
			none,double0, triple0, running, l, p
		}
		
		public KVB (TrainSoundManager soundManager,
		            TrainHandleManager handleManager,
		            TrainControlManager controlManager): base(soundManager, handleManager, controlManager)
		{
			reset();
		}
		
		public override void elapse (OpenBveApi.Runtime.ElapseData data)
		{
		}
		
		public override void reset ()
		{
			controlManager.setState(cabControls.GreenKVB, (int) GreenKvbAspects.none);
			controlManager.setState(cabControls.YellowKVB, (int) YellowKvbAspects.triple0);
		}
		
		public override void trainEvent (TrainEvent _event)
		{
		}
	}
}

