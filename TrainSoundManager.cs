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
		
		private struct LoopSound
		{
			public SoundHandle handle;
			public SoundIndex index;
		}
		
		private List<LoopSound> loopSounds;
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
			loopSounds = new List<LoopSound>();
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
		
		public void startSound(ref int id, SoundIndex index)
		{
			if (!loopSounds.Exists(delegate (LoopSound sound) {
				return sound.index == index;
			}))
			{
				LoopSound sound;
				sound.handle = this.playSound((int) index,volume,pitch,true);
				sound.index = index;
				loopSounds.Add(sound);
				id = loopSounds.IndexOf(sound);
			}
		}
		
		public void stopSound(ref int id)
		{
			if (id < loopSounds.Count && id >= 0)
			{
				loopSounds[id].handle.Stop();
				loopSounds.RemoveAt(id);
				id = -1;
			}
		}
	}
}

