using System;
namespace FrTrainSys
{
	public class TrainHandleManager
	{
		private OpenBveApi.Runtime.VehicleSpecs specs;
		private OpenBveApi.Runtime.Handles handles;
		private bool applyHandles;

		public TrainHandleManager (OpenBveApi.Runtime.VehicleSpecs specs)
		{
			this.specs = specs;
			handles = new OpenBveApi.Runtime.Handles(0,0,0,false);
			applyHandles = false;
		}

		public void elapse (ref OpenBveApi.Runtime.ElapseData data)
		{
			if (applyHandles)
			{
				data.Handles = this.handles;
				applyHandles = false;
			}
		}

		public void applyEmergencyBrake ()
		{
			applyHandles = true;
			handles.BrakeNotch = specs.BrakeNotches+1;
			handles.PowerNotch = 0;
		}
	}
}

