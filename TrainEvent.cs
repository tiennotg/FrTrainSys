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
		private int eventType;
		private object data;

		public TrainEvent (int eventType, object data)
		{
			this.eventType = eventType;
			this.data = data;
		}

		public int getEventType ()
		{
			return this.eventType;
		}

		public void setEventType (int eventType)
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

