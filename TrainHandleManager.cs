using System;
namespace FrTrainSys
{
	public class TrainHandleManager
	{
		private OpenBveApi.Runtime.VehicleSpecs specs;
		private OpenBveApi.Runtime.Handles handles;
		private bool applyHandles;
		private bool emergencyBrake;

		public TrainHandleManager (OpenBveApi.Runtime.VehicleSpecs specs)
		{
			this.specs = specs;
			handles = new OpenBveApi.Runtime.Handles(0,0,0,false);
			applyHandles = false;
		}

		public void elapse (ref OpenBveApi.Runtime.ElapseData data)
		{
			if (applyHandles || emergencyBrake)
			{
				data.Handles = this.handles;
				applyHandles = false;
			}
		}

		public void applyEmergencyBrake ()
		{
			emergencyBrake = true;
			handles.BrakeNotch = specs.BrakeNotches+1;
			handles.PowerNotch = 0;
		}
		
		public void removeBrake ()
		{
			applyHandles = true;
			emergencyBrake = false;
			handles.BrakeNotch = 0;
		}

		public void setBrake (int brake)
		{
			applyHandles = true;
			handles.BrakeNotch = brake;
		}
		
		public void setPower (int power)
		{
			applyHandles = true;
			handles.PowerNotch = power;
		}
	}
}

