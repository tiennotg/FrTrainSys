using System;

namespace FrTrainSys
{
	public class ClosedSignal: FrTrainDevice
	{
		public ClosedSignal (TrainSoundManager soundManager,
		                     TrainHandleManager handleManager): base(soundManager,handleManager)
		{
		}

		private void beep ()
		{
			soundManager.playSoundOnce((int) TrainSoundManager.SoundIndex.Beep);
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

