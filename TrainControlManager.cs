using System;
namespace FrTrainSys
{
	public class TrainControlManager
	{
		public const int cabControlCount = 256;
		private int[] panel;

		public TrainControlManager (ref int[] panel)
		{
			panel = new int[cabControlCount];
			for (int i=0; i<cabControlCount; i++)
				panel[i] = 0;
			this.panel = panel;
		}
	}
}

