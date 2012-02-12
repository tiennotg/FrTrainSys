using System;
using OpenBveApi;

namespace FrTrainSys
{
	public class Vacma: FrTrainDevice
	{
		public Vacma (TrainSoundManager soundManager,
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

