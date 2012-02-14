using System;
using OpenBveApi.Runtime;
using System.Collections.Generic;

namespace FrTrainSys
{
	public enum SoundIndex
	{
		Vacma, VacmaRing, Beep, KvbOverSpeed, KvbClosedSignal
	};
	
	public class TrainSoundManager
	{		
		private List<SoundHandle> loopSounds;

		private PlaySoundDelegate playSound;
		private const double volume = 1.0;
		private const double pitch = 1.0;

		public TrainSoundManager (PlaySoundDelegate playSound)
		{
			this.playSound = playSound;
			loopSounds = new List<SoundHandle>();
		}

		public void playSoundOnce (SoundIndex index)
		{
			playSound((int) index,volume,pitch,false);
		}
		
		public int startSound(SoundIndex index)
		{
			SoundHandle handle = this.playSound((int) index,volume,pitch,true);
			loopSounds.Add(handle);
			return loopSounds.IndexOf(handle);
		}
		
		public void stopSound(int id)
		{
			if (id < loopSounds.Count && id >= 0)
			{
				loopSounds[id].Stop();
				loopSounds.RemoveAt(id);
			}
		}
	}
}

