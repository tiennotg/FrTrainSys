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
		
		protected void startLoopSound (ref int id, SoundIndex index)
		{
			if (id == -1)
				id = soundManager.startSound(index);
		}
		
		protected void stopLoopSound (ref int id)
		{
			if (id != -1)
			{
				soundManager.stopSound(id);
				id = -1;
			}
		}

		public abstract void elapse (OpenBveApi.Runtime.ElapseData data);
		public abstract void reset ();
		public abstract void trainEvent (TrainEvent _event);
	}
}

