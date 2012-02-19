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
		private struct LoopSoundFor
		{
			public SoundHandle sound;
			public int start;
			public int duration;
			
			public LoopSoundFor (SoundHandle s, int startTime, int ms)
			{
				sound = s;
				start = startTime;
				duration = ms;
			}
		}
		
		private List<SoundHandle> loopSounds;
		private List<LoopSoundFor> loopSoundsFor;
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
			loopSoundsFor = new List<LoopSoundFor>();
			lastSoundTime = new Time(0);
			lastSoundIndex = SoundIndex.None;
		}
		
		public void playSoundOnce (SoundIndex index, int delayBeforeRepeating)
		{
			if (index != lastSoundIndex || (globalTime.Milliseconds -  lastSoundTime.Milliseconds > delayBeforeRepeating))
			{
				lastSoundIndex = index;
				lastSoundTime = globalTime;
				playSound((int) index,volume,pitch,false);
			}
		}

		public void playSoundOnce (SoundIndex index)
		{
			this.playSoundOnce(index, soundOnceDelay);
		}
		
		public void playSoundFor (SoundIndex index, int milliseconds)
		{
			SoundHandle newSound = playSound((int) index, volume, pitch, true);
			loopSoundsFor.Add(new LoopSoundFor(newSound, (int) globalTime.Milliseconds, milliseconds));
		}
		
		public void elapse (ElapseData data)
		{
			globalTime = data.TotalTime;
			
			loopSoundsFor.ForEach(delegate (LoopSoundFor sound) {
				if (globalTime.Milliseconds - sound.start > sound.duration)
				{
					sound.sound.Stop();
					loopSoundsFor.Remove(sound);
				}
			});
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

