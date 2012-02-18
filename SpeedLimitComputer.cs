using System;
using OpenBveApi.Runtime;

namespace FrTrainSys
{
	public class SpeedLimitComputer
	{
		private Speed currentSpeedLimit;
		
		private Speed maxSpeed;
		private int trainLength;
		private double decelCoeff;
		private TrainTypes trainType;
		
		public SpeedLimitComputer (Speed maxSpeed, int trainLength, double decelCoeff, TrainTypes trainType)
		{
			this.maxSpeed = maxSpeed;
			this.trainLength = trainLength;
			this.decelCoeff = decelCoeff;
			this.trainType = trainType;
		}
		
		public void setTargetSpeed (Speed newSpeedLimit, int targetDistance)
		{
			if (targetDistance == 0)
				currentSpeedLimit = newSpeedLimit;
		}
		
		public Speed getCurrentSpeedLimit ()
		{
			return currentSpeedLimit;
		}
	}
}

