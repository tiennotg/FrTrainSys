using System;
using OpenBveApi;

namespace FrTrainSys
{
	public class Vacma: FrTrainDevice
	{
		public Vacma (TrainSoundManager soundManager,
		              TrainHandleManager handleManager): base(soundManager, handleManager)
		{
		}

		public override void elapse (OpenBveApi.Runtime.ElapseData data)
		{
		}
	}
}

