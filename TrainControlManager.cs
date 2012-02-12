using System;
namespace FrTrainSys
{
	/* list of cab controls */
	public class cabControls
	{
		public static const int count = 256;
		
		public static const int LSSF = 8;
	}
	
	public class TrainControlManager
	{
		private int[] panel;
		private OpenBveApi.Runtime.Time globalTime;

		public TrainControlManager (ref OpenBveApi.Runtime.LoadProperties properties)
		{
			panel = new int[cabControls.count];
			for (int i=0; i<cabControls.count; i++)
				panel[i] = 0;
			properties.Panel = panel;
		}
		
		public void elapse (OpenBveApi.Runtime.ElapseData data)
		{
			globalTime = data.TotalTime;
		}
	}
}

