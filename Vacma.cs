using System;
using OpenBveApi.Runtime;

namespace FrTrainSys
{
	public class Vacma: FrTrainDevice
	{
		private bool applyBreak;
		private bool securityTest;
		private bool hold;
		private int vacmaHorn;
		private int vacmaRing;
		
		private const int delayBeforeBreaking = 2500;
		private const int delayBeforeRinging = 57500;
		private Time globalTime;
		private Time time;
		
		public Vacma (TrainSoundManager soundManager,
		              TrainHandleManager handleManager,
		              TrainControlManager controlManager): base(soundManager, handleManager, controlManager)
		{
			time = new Time(0);
			securityTest = false;
			hold = false;
			vacmaHorn = -1;
			vacmaRing = -1;
			applyBreak = false;
		}

		public override void elapse (ElapseData data)
		{
			globalTime = data.TotalTime;
			
			if (!applyBreak && (data.Vehicle.Speed.KilometersPerHour > 0 || securityTest))
			{
				if (!hold && (globalTime.Milliseconds - time.Milliseconds) > delayBeforeBreaking)
				{
					if (vacmaHorn == -1)
						vacmaHorn = soundManager.startSound(SoundIndex.Vacma);
					if ((globalTime.Milliseconds - time.Milliseconds) > delayBeforeBreaking * 2)
					{
						applyBreak = true;
						soundManager.stopSound(vacmaHorn);
						handleManager.applyEmergencyBrake();
					}
				}
				else if ((globalTime.Milliseconds - time.Milliseconds) > delayBeforeRinging)
				{
					if (vacmaRing == -1)
						vacmaRing = soundManager.startSound(SoundIndex.VacmaRing);
					if ((globalTime.Milliseconds - time.Milliseconds) > (delayBeforeRinging + delayBeforeBreaking))
					{
						applyBreak = true;
						soundManager.stopSound(vacmaRing);
						handleManager.applyEmergencyBrake();
					}
				}
			}
			else
				time = globalTime;
		}

		private void resetHolding ()
		{
			hold = true;
			time = globalTime;
			if (vacmaHorn != -1)
			{
				soundManager.stopSound(vacmaHorn);
				vacmaHorn = -1;
			}
			if (vacmaRing != -1)
			{
				soundManager.stopSound(vacmaRing);
				vacmaRing = -1;
			}
		}
		
		public override void reset ()
		{
			applyBreak = false;
			resetHolding();
		}

		public override void trainEvent (TrainEvent _event)
		{
			VirtualKeys key;

			if (_event.getEventType() == EventTypes.EventTypeBlowHorn ||
			    _event.getEventType() == EventTypes.EventTypeChangeBrake ||
			    _event.getEventType() == EventTypes.EventTypeChangeDoors ||
			    _event.getEventType() == EventTypes.EventTypeChangePower ||
			    _event.getEventType() == EventTypes.EventTypeSwitchReverser)
			{
				time = globalTime;
			}
			
			if (_event.getEventType() == EventTypes.EventTypeKeyDown)
			{
				key = (VirtualKeys) _event.getEventData();
				
				if (key == VirtualKeys.S && !hold)
					resetHolding();
			}
			
			if (_event.getEventType() == EventTypes.EventTypeKeyUp)
			{
				key = (VirtualKeys) _event.getEventData();
				
				if (key == VirtualKeys.S)
				{
					hold = false;
					time = globalTime;
				}
			}
		}
	}
}

