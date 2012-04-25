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
		private Speed decelerationInitSpeed;
		private int decelerationInitLocation;
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
		
		public void startDecelerationControl (Speed currentSpeed, int location, int targetDistance, DecelerationControlType type)
		{
			decelerationInitSpeed = currentSpeed;
			decelerationInitLocation = location;
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
			if (decelerationControlEnabled)
			{
				double distance = System.Math.Abs(decelerationTargetDistance - (int) Math.Round(data.Vehicle.Location));
				double s = (decelerationInitSpeed.MetersPerSecond / System.Math.Sqrt(2 * decelCoeff * decelerationInitLocation)) * System.Math.Sqrt(2 * decelCoeff * distance);
				
				if (decelerationType == DecelerationControlType.strong)
				{
					if (s > 4.16) /* 15 km/h */
						return new Speed(s);
					else
						return new Speed(4.16);
				}
				else
				{
					if (s > 8.33) /* 30 km/h */
						return new Speed(s);
					else
						return new Speed(8.33);
				}
			}
			else
				return currentSpeedLimit;
		}
	}
}

