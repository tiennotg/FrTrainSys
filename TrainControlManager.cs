using System;
namespace FrTrainSys
{
	public class TrainControlManager
	{
		public const int cabControlCount = 256;
		private int[] panel;
		private OpenBveApi.Runtime.Time globalTime;

		public TrainControlManager (ref OpenBveApi.Runtime.LoadProperties properties)
		{
			panel = new int[cabControlCount];
			for (int i=0; i<cabControlCount; i++)
				panel[i] = 0;
			properties.Panel = panel;
		}
		
		public void elapse (OpenBveApi.Runtime.ElapseData data)
		{
			globalTime = data.TotalTime;
		}
	}
}

