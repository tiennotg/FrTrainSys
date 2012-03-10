using System;
using OpenBveApi.Runtime;

namespace FrTrainSys
{
	public enum DecelerationControlType
	{
		low,strong
	}
	
	public class SpeedLimitComputer
	{
		private Speed currentSpeedLimit;
		
		private bool speedTransition;
		private Speed newSpeedLimit;
		private int newSpeedLimitTargetDistance;
		
		private bool decelerationControlEnabled;
		private DecelerationControlType decelerationType;
		private int decelerationTargetDistance;
		
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
			
			decelerationControlEnabled = false;
			
			currentSpeedLimit = maxSpeed;
		}
		
		public void setTargetSpeed (Speed newSpeedLimit, int targetDistance)
		{
			if (targetDistance == 0)
				currentSpeedLimit = newSpeedLimit;
			else
			{
				speedTransition = true;
				this.newSpeedLimit = newSpeedLimit;
				newSpeedLimitTargetDistance = targetDistance;
			}
		}
		
		public void startDecelerationControl (int targetDistance, DecelerationControlType type)
		{
			decelerationTargetDistance = targetDistance;
			decelerationType = type;
			decelerationControlEnabled = true;
		}
		
		public void stopDecelerationControl ()
		{
			decelerationControlEnabled = false;
		}
		
		public Speed getCurrentSpeedLimit (ElapseData data)
		{
			return currentSpeedLimit;
		}
	}
}

