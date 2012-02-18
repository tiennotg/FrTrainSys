using System;
using OpenBveApi.Runtime;

namespace FrTrainSys
{
	public enum TrainTypes
	{
		MA,ME,V
	}
	
	public class KVB: FrTrainDevice
	{		
		private SpeedLimitComputer speedLimiter;
		
		private enum GreenKvbAspects
		{
			none,double0, kvbp, running, l, p, b
		};
		
		private enum YellowKvbAspects
		{
			none,double0, triple0, running, l, p
		}
		
		public KVB (TrainSoundManager soundManager,
		            TrainHandleManager handleManager,
		            TrainControlManager controlManager): base(soundManager, handleManager, controlManager)
		{
			reset();
		}
		
		public void setParameters (Speed maxSpeed, int trainLength, double decelCoeff, TrainTypes trainType)
		{
			speedLimiter = new SpeedLimitComputer(maxSpeed, trainLength, decelCoeff, trainType);
		}
		
		public override void elapse (OpenBveApi.Runtime.ElapseData data)
		{
		}
		
		public override void reset ()
		{
		}
		
		public override void trainEvent (TrainEvent _event)
		{
		}
	}
}

