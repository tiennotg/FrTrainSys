﻿using System;
using OpenBveApi.Runtime;

namespace FrTrainSys {
	/// <summary>The interface to be implemented by the plugin.</summary>
	public class FrTrainSys : IRuntime {

		private TrainSoundManager soundManager;
		private TrainHandleManager handleManager;
		private TrainControlManager cabControlManager;
		private VehicleSpecs trainSpecs;

		private ClosedSignal closedSignalDevice;
		private Vacma vacma;
		private KVB speedControl;
		
		/// <summary>Is called when the plugin is loaded.</summary>
		/// <param name="properties">The properties supplied to the plugin on loading.</param>
		/// <returns>Whether the plugin was loaded successfully.</returns>
		public bool Load(LoadProperties properties) {
			soundManager = new TrainSoundManager(properties.PlaySound);
			cabControlManager = new TrainControlManager(ref properties);
			return true;
		}
		
		/// <summary>Is called when the plugin is unloaded.</summary>
		public void Unload() {
		}
		
		/// <summary>Is called after loading to inform the plugin about the specifications of the train.</summary>
		/// <param name="specs">The specifications of the train.</param>
		public void SetVehicleSpecs(VehicleSpecs specs) {
			trainSpecs = specs;
		}
		
		/// <summary>Is called when the plugin should initialize or reinitialize.</summary>
		/// <param name="mode">The mode of initialization.</param>
		public void Initialize(InitializationModes mode) {
			handleManager = new TrainHandleManager(trainSpecs);

			closedSignalDevice = new ClosedSignal(soundManager,handleManager, cabControlManager);
			vacma = new Vacma(soundManager,handleManager, cabControlManager);
			speedControl = new KVB(soundManager,handleManager,cabControlManager);
			
			speedControl.setParameters(new Speed(33.33), 400, 0.8, TrainTypes.V);
		}
		
		/// <summary>Is called every frame.</summary>
		/// <param name="data">The data passed to the plugin.</param>
		public void Elapse(ElapseData data) {
			closedSignalDevice.elapse(data);
			vacma.elapse(data);
			speedControl.elapse(data);
			handleManager.elapse(ref data);
			soundManager.elapse(data);
			cabControlManager.elapse(data);
		}
		
		/// <summary>Is called when the driver changes the reverser.</summary>
		/// <param name="reverser">The new reverser position.</param>
		public void SetReverser(int reverser) {
			vacma.trainEvent(new TrainEvent(EventTypes.EventTypeSwitchReverser, reverser));
		}
		
		/// <summary>Is called when the driver changes the power notch.</summary>
		/// <param name="powerNotch">The new power notch.</param>
		public void SetPower(int powerNotch) {
			vacma.trainEvent(new TrainEvent(EventTypes.EventTypeChangePower, powerNotch));
		}
		
		/// <summary>Is called when the driver changes the brake notch.</summary>
		/// <param name="brakeNotch">The new brake notch.</param>
		public void SetBrake(int brakeNotch) {
			vacma.trainEvent(new TrainEvent(EventTypes.EventTypeChangeBrake, brakeNotch));
		}
		
		/// <summary>Is called when a virtual key is pressed.</summary>
		/// <param name="key">The virtual key that was pressed.</param>
		public void KeyDown(VirtualKeys key) {
			closedSignalDevice.trainEvent(new TrainEvent(EventTypes.EventTypeKeyDown, key));
			vacma.trainEvent(new TrainEvent(EventTypes.EventTypeKeyDown, key));
		}
		
		/// <summary>Is called when a virtual key is released.</summary>
		/// <param name="key">The virtual key that was released.</param>
		public void KeyUp(VirtualKeys key) {
			vacma.trainEvent(new TrainEvent(EventTypes.EventTypeKeyUp, key));
		}
		
		/// <summary>Is called when a horn is played or when the music horn is stopped.</summary>
		/// <param name="type">The type of horn.</param>
		public void HornBlow(HornTypes type) {
			vacma.trainEvent(new TrainEvent(EventTypes.EventTypeBlowHorn, type));
		}
		
		/// <summary>Is called when the state of the doors changes.</summary>
		/// <param name="oldState">The old state of the doors.</param>
		/// <param name="newState">The new state of the doors.</param>
		public void DoorChange(DoorStates oldState, DoorStates newState) {
			vacma.trainEvent(new TrainEvent(EventTypes.EventTypeChangeDoors, newState));
		}
		
		/// <summary>Is called when the aspect in the current or in any of the upcoming sections changes, or when passing section boundaries.</summary>
		/// <param name="data">Signal information per section. In the array, index 0 is the current section, index 1 the upcoming section, and so on.</param>
		/// <remarks>The signal array is guaranteed to have at least one element. When accessing elements other than index 0, you must check the bounds of the array first.</remarks>
		public void SetSignal(SignalData[] signal) {
			closedSignalDevice.trainEvent(new TrainEvent(EventTypes.EventTypeChangeSignalAspect, signal));
			speedControl.trainEvent(new TrainEvent(EventTypes.EventTypeChangeSignalAspect, signal));
		}
		
		/// <summary>Is called when the train passes a beacon.</summary>
		/// <param name="beacon">The beacon data.</param>
		public void SetBeacon(BeaconData beacon) {
			closedSignalDevice.trainEvent(new TrainEvent(EventTypes.EventTypeGetBeacon, beacon));
			speedControl.trainEvent(new TrainEvent(EventTypes.EventTypeGetBeacon, beacon));
		}
		
		/// <summary>Is called when the plugin should perform the AI.</summary>
		/// <param name="data">The AI data.</param>
		public void PerformAI(AIData data) {
		}
		
	}
}