using System;
namespace FrTrainSys
{
	public class TrainEvent
	{
		private int eventType;
		private int data;

		public TrainEvent (int eventType, int data)
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

		public int getEventData ()
		{
			return this.data;
		}

		public void setEventData (int data)
		{
			this.data = data;
		}
	}
}

