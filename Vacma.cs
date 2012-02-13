using System;
using OpenBveApi.Runtime;

namespace FrTrainSys
{
	public class Vacma: FrTrainDevice
	{
		private const int delayBeforeBreaking = 2500;
		private const int delayBeforeRinging = 57500;
		private Time globalTime;
		private Time time;
		
		public Vacma (TrainSoundManager soundManager,
		              TrainHandleManager handleManager,
		              TrainControlManager controlManager): base(soundManager, handleManager, controlManager)
		{
			time = new Time(0);
		}

		public override void elapse (ElapseData data)
		{
			globalTime = data.TotalTime;
		}

		public override void reset ()
		{
			time = globalTime;
		}

		public override void trainEvent (TrainEvent _event)
		{
			if (_event.getEventType() == EventTypes.EventTypeBlowHorn ||
			    _event.getEventType() == EventTypes.EventTypeChangeBrake ||
			    _event.getEventType() == EventTypes.EventTypeChangeDoors ||
			    _event.getEventType() == EventTypes.EventTypeChangePower ||
			    _event.getEventType() == EventTypes.EventTypeSwitchReverser)
			{
				this.reset();
			}
		}
	}
}

