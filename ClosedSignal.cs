using System;

namespace FrTrainSys
{
	public class ClosedSignal: FrTrainDevice
	{
		public const int signalCrossingDistance = 5;
		public const int signalAspectForConsideringClosed = 2;
		private const OpenBveApi.Runtime.VirtualKeys ackKey = OpenBveApi.Runtime.VirtualKeys.A1;
		private const int tempo = 2500; /* Time before applying emergency brakes (ms) */

		private bool closedSignal;
		private OpenBveApi.Runtime.Time globalTime;
		private OpenBveApi.Runtime.Time time;

		public ClosedSignal (TrainSoundManager soundManager,
		                     TrainHandleManager handleManager,
		                     TrainControlManager controlManager): base(soundManager,handleManager,controlManager)
		{
			globalTime = new OpenBveApi.Runtime.Time(0);
			time = new OpenBveApi.Runtime.Time(0);
			this.reset();
		}

		private void beep ()
		{
			soundManager.playSoundOnce(SoundIndex.Beep);
		}

		private void crossingClosedSignal ()
		{
			this.beep();
			closedSignal = true;
			controlManager.startBlinking(cabControls.LSSF);
			time = globalTime;
		}

		public override void elapse (OpenBveApi.Runtime.ElapseData data)
		{
			globalTime = data.TotalTime;

			if (closedSignal && (globalTime.Milliseconds - time.Milliseconds) > tempo)
				handleManager.applyEmergencyBrake();
		}

		public override void reset ()
		{
			closedSignal = false;
		}

		public override void trainEvent (TrainEvent _event)
		{
			if (_event.getEventType() == EventTypes.EventTypeChangeSignalAspect)
			{
				OpenBveApi.Runtime.SignalData[] signal = (OpenBveApi.Runtime.SignalData[]) _event.getEventData();

				/* The first condition ensures that the train is really crossing a signal, and
				 * that is not an other signal whose aspect is changing. */

				if (System.Math.Abs(signal[0].Distance) < signalCrossingDistance)
				{
					if (signal[0].Aspect <= signalAspectForConsideringClosed)
						this.crossingClosedSignal();
					else
					{
						controlManager.stopBlinking(cabControls.LSSF);
						controlManager.setState(cabControls.LSSF, 0);
					}
				}
			}
			
			if (_event.getEventType() == EventTypes.EventTypeGetBeacon)
			{
				OpenBveApi.Runtime.BeaconData beacon = (OpenBveApi.Runtime.BeaconData) _event.getEventData();
				
				if (beacon.Type == Beacons.RepeatedSignal)
					this.crossingClosedSignal();
			}

			if (_event.getEventType() == EventTypes.EventTypeKeyDown)
			{
				OpenBveApi.Runtime.VirtualKeys key = (OpenBveApi.Runtime.VirtualKeys) _event.getEventData();
				if (key == ackKey)
				{
					if (closedSignal)
					{
						controlManager.stopBlinking(cabControls.LSSF);
						controlManager.setState(cabControls.LSSF, 1);
					}
					this.reset();
				}
			}
		}
	}
}

