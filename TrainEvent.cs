using System;
namespace FrTrainSys
{
	public enum EventTypes
	{
		EventTypeKeyUp, EventTypeKeyDown,
		EventTypeSwitchReverser, EventTypeChangePower,
		EventTypeChangeBrake, EventTypeBlowHorn,
		EventTypeChangeDoors, EventTypeChangeSignalAspect,
		EventTypeGetBeacon
	};

	public class TrainEvent
	{
		private EventTypes eventType;
		private object data;

		public TrainEvent (EventTypes eventType, object data)
		{
			this.eventType = eventType;
			this.data = data;
		}

		public EventTypes getEventType ()
		{
			return this.eventType;
		}

		public void setEventType (EventTypes eventType)
		{
			this.eventType = eventType;
		}

		public object getEventData ()
		{
			return this.data;
		}

		public void setEventData (object data)
		{
			this.data = data;
		}
	}
}

