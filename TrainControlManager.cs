using System;
using System.Collections.Generic;

namespace FrTrainSys
{
	/* list of cab controls */
	static class cabControls
	{
		public static int count = 256;
		
		public static int LSSF = 1;
		public static int YellowKVB = 10;
		public static int GreenKVB = 11;
	}
	
	public class TrainControlManager
	{
		private int[] panel;
		
		private List<int> controlBlinking;
		private OpenBveApi.Runtime.Time globalTime;
		private OpenBveApi.Runtime.Time lastTime;
		private int halfBlinkingPeriod = 100; /* (in ms) duration of a control state while blinking */

		public TrainControlManager (ref OpenBveApi.Runtime.LoadProperties properties)
		{
			controlBlinking = new List<int>();
			lastTime = new OpenBveApi.Runtime.Time(0);
			
			panel = new int[cabControls.count];
			for (int i=0; i<cabControls.count; i++)
				panel[i] = 0;
			properties.Panel = panel;
		}
		
		public void elapse (OpenBveApi.Runtime.ElapseData data)
		{
			globalTime = data.TotalTime;
			
			if (globalTime.Milliseconds - lastTime.Milliseconds > halfBlinkingPeriod)
			{
				controlBlinking.ForEach(delegate (int control) {
					if (getState(control) == 1)
						setState(control, 0);
					else
						setState(control, 1);
				});
				lastTime = globalTime;
			}
		}
		
		public int getState (int control)
		{
			return panel[control];
		}
		
		public void setState (int control, int state)
		{
			panel[control] = state;
		}
		
		public void startBlinking (int control)
		{
			if (!controlBlinking.Contains(control))
				controlBlinking.Add(control);
		}
		
		public void stopBlinking (int control)
		{
			if (controlBlinking.Contains(control))
				controlBlinking.Remove(control);
		}
	}
}

