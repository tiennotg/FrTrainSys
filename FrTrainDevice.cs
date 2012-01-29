using System;
namespace FrTrainSys
{
	abstract public class FrTrainDevice
	{
		protected TrainSoundManager soundManager;
		protected TrainHandleManager handleManager;

		public FrTrainDevice (TrainSoundManager soundManager,
		                      TrainHandleManager handleManager)
		{
			this.soundManager = soundManager;
			this.handleManager = handleManager;
		}
	}
}

