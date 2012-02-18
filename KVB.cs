using System;
using OpenBveApi.Runtime;

namespace FrTrainSys
{
	public class KVB: FrTrainDevice
	{
		public enum TrainTypes
		{
			MA,ME,V
		}
		
		private Speed maxSpeed;
		private int trainLength;
		private double decelCoeff;
		private TrainTypes trainType;
		
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
			this.maxSpeed = maxSpeed;
			this.trainLength = trainLength;
			this.decelCoeff = decelCoeff;
			this.trainType = trainType;
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

