using System;
using OpenBveApi.Runtime;
using System.Collections.Generic;

namespace FrTrainSys
{
	public enum SoundIndex
	{
		Vacma, VacmaRing, Beep, KvbOverSpeed, KvbClosedSignal, None
	};
	
	public class TrainSoundManager
	{		
		private List<SoundHandle> loopSounds;
		private SoundIndex lastSoundIndex;
		private const int soundOnceDelay = 1000;
		private Time lastSoundTime;
		private Time globalTime;

		private PlaySoundDelegate playSound;
		private const double volume = 1.0;
		private const double pitch = 1.0;

		public TrainSoundManager (PlaySoundDelegate playSound)
		{
			this.playSound = playSound;
			loopSounds = new List<SoundHandle>();
			lastSoundTime = new Time(0);
			lastSoundIndex = SoundIndex.None;
		}

		public void playSoundOnce (SoundIndex index)
		{
			if (index != lastSoundIndex || (globalTime.Milliseconds -  lastSoundTime.Milliseconds > soundOnceDelay))
			{
				lastSoundIndex = index;
				lastSoundTime = globalTime;
				playSound((int) index,volume,pitch,false);
			}
		}
		
		public void elapse (ElapseData data)
		{
			globalTime = data.TotalTime;
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

