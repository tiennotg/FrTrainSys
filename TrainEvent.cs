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
	}
}

