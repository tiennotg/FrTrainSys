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
		private bool enabled;
		private const double firstMargin = 5.0;
		private const double secondMargin = 10.0;
		private const int KvbEnable = 0;
		private const int KvbDisable = -1;
		
		private Speed newSpeedLimit;
		private int targetDistance;
		
		private SpeedLimitComputer speedLimiter;
		private int beepBeep;
		private int horn;
		
		private enum GreenKvbAspects
		{
			none,double0, kvbp, running, l, p, b
		};
		
		private enum YellowKvbAspects
		{
			none,double0, triple0, running, l, p
		}
		
		private void stopTrain ()
		{
			startLoopSound(ref horn, SoundIndex.KvbClosedSignal);
			handleManager.applyEmergencyBrake();
		}
		
		static public double kmhToMs (double kilometersPerHour)
		{
			return (kilometersPerHour * 1000) / 3600;
		}

		public KVB (TrainSoundManager soundManager,
		            TrainHandleManager handleManager,
		            TrainControlManager controlManager): base(soundManager, handleManager, controlManager)
		{
			beepBeep = -1;
			horn = -1;
			newSpeedLimit = new Speed(-1);
			enabled = false;
			reset();
		}
		
		public void setParameters (Speed maxSpeed, int trainLength, double decelCoeff, TrainTypes trainType)
		{
			speedLimiter = new SpeedLimitComputer(maxSpeed, trainLength, decelCoeff, trainType);
		}
		
		public override void elapse (OpenBveApi.Runtime.ElapseData data)
		{
			if (enabled)
			{
				Speed limit = speedLimiter.getCurrentSpeedLimit();
			
				if (data.Vehicle.Speed.KilometersPerHour == 0)
					stopLoopSound(ref horn);
				else if (data.Vehicle.Speed.KilometersPerHour > limit.KilometersPerHour + firstMargin)
				{
					startLoopSound(ref beepBeep, SoundIndex.KvbOverSpeed);
				
					if (data.Vehicle.Speed.KilometersPerHour > limit.KilometersPerHour + secondMargin)
						stopTrain();
				}
				else
					stopLoopSound(ref beepBeep);
			}
		}
		
		public override void reset ()
		{
			targetDistance = -1;
			newSpeedLimit = new Speed(-1);
			
			stopLoopSound(ref beepBeep);
			stopLoopSound(ref horn);
			
			controlManager.setState(cabControls.GreenKVB, (int) GreenKvbAspects.none);
			controlManager.setState(cabControls.YellowKVB, (int) YellowKvbAspects.triple0);
		}
		
		public override void trainEvent (TrainEvent _event)
		{
			if (_event.getEventType() == EventTypes.EventTypeGetBeacon)
			{
				BeaconData beacon = (BeaconData) _event.getEventData();
				
				if (beacon.Type == Beacons.Signal)
				{
					if (beacon.Signal.Aspect == 0)
						stopTrain();
					else if (beacon.Signal.Aspect <= ClosedSignal.signalAspectForConsideringClosed)
					{
						speedLimiter.setTargetSpeed(new Speed(0), (int) Math.Round(beacon.Signal.Distance));
						controlManager.setState(cabControls.GreenKVB, (int) GreenKvbAspects.none);
						if (beacon.Optional >= 0)
							controlManager.setState(cabControls.YellowKVB, (int) YellowKvbAspects.double0);
						else
							controlManager.setState(cabControls.YellowKVB, (int) YellowKvbAspects.triple0);
					}
					else
					{
						controlManager.setState(cabControls.GreenKVB, (int) GreenKvbAspects.running);
						controlManager.setState(cabControls.YellowKVB, (int) YellowKvbAspects.running);
					}
				}
				
				if (beacon.Type == Beacons.KvbControl)
				{
					if (beacon.Optional == KvbEnable)
						enabled = true;
					else if (beacon.Optional == KvbDisable)
						enabled = false;
				}
				
				if (beacon.Type == Beacons.SpeedLimit)
					newSpeedLimit = new Speed(kmhToMs((double) beacon.Optional));
				
				if (beacon.Type == Beacons.TargetDistance)
					targetDistance = beacon.Optional;
				
				if (targetDistance != -1 && newSpeedLimit.KilometersPerHour != -1)
					speedLimiter.setTargetSpeed(newSpeedLimit,targetDistance);
			}
		}
	}
}

