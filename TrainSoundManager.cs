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
		private const double volume = 1.0;
		private const double pitch = 1.0;

		public TrainSoundManager (OpenBveApi.Runtime.PlaySoundDelegate playSound)
		{
			this.playSound = playSound;
		}

		public void playSoundOnce (SoundIndex index)
		{
			playSound((int) index,volume,pitch,false);
		}
	}
}

