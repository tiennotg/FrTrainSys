using System;
using OpenBveApi;

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

		public abstract void elapse (OpenBveApi.Runtime.ElapseData data);
		public abstract void reset ();
		public abstract void trainEvent (TrainEvent _event);
	}
}

