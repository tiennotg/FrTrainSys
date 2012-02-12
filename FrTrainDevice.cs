using System;
using OpenBveApi;

namespace FrTrainSys
{
	abstract public class FrTrainDevice
	{
		protected TrainSoundManager soundManager;
		protected TrainHandleManager handleManager;
		protected TrainControlManager controlManager;

		public FrTrainDevice (TrainSoundManager soundManager,
		                      TrainHandleManager handleManager,
		                      TrainControlManager controlManager)
		{
			this.soundManager = soundManager;
			this.handleManager = handleManager;
			this.controlManager = controlManager;
		}

		public abstract void elapse (OpenBveApi.Runtime.ElapseData data);
		public abstract void reset ();
		public abstract void trainEvent (TrainEvent _event);
	}
}

