using System;
using OpenBveApi;

namespace FrTrainSys
{
	public class TrainSoundManager
	{
		public enum SoundIndex
		{
			Vacma, VacmaRing, Beep, KvbOverSpeed, KvbClosedSignal
		};

		private OpenBveApi.Runtime.PlaySoundDelegate playSound;

		public TrainSoundManager (OpenBveApi.Runtime.PlaySoundDelegate playSound)
		{
			this.playSound = playSound;
		}
	}
}

