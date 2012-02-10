using System;

namespace FrTrainSys
{
	public class ClosedSignal: FrTrainDevice
	{
		private const int signalCrossingDistance = 5;
		private const int signalAspectForConsideringClosed = 3;

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
			if (_event.getEventType() == EventTypes.EventTypeChangeSignalAspect)
			{
				OpenBveApi.Runtime.SignalData[] signal = (OpenBveApi.Runtime.SignalData[]) _event.getEventData();

				/* The first condition ensures that the train is really crossing a signal, and
				 * that is not an other signal whose aspect is changing. */
				if (System.Math.Abs(signal[0].Distance) < signalCrossingDistance
				    && signal[0].Aspect <= signalAspectForConsideringClosed)
				{
					this.beep();
				}
			}
		}
	}
}

