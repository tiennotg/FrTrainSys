using System;
using OpenBveApi;

namespace FrTrainSys
{
	public class TrainSoundManager
	{
		private OpenBveApi.Runtime.PlaySoundDelegate playSound;

		public TrainSoundManager (OpenBveApi.Runtime.PlaySoundDelegate playSound)
		{
			this.playSound = playSound;
		}
	}
}

