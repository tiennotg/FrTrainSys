using System;
namespace Plugin
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

		public void setEventType(int eventType)
		{
			this.eventType = eventType;
		}
	}
}

