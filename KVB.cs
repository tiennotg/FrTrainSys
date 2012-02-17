using System;
using OpenBveApi.Runtime;

namespace FrTrainSys
{
	public class KVB: FrTrainDevice
	{
		public KVB (TrainSoundManager soundManager,
		            TrainHandleManager handleManager,
		            TrainControlManager controlManager): base(soundManager, handleManager, controlManager)
		{
		}
		
		public override void elapse (OpenBveApi.Runtime.ElapseData data)
		{
			
		}
		
		public override void reset ()
		{
			
		}
		
		public override void trainEvent (TrainEvent _event)
		{
			
		}
	}
}

