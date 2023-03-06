 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.Waves.Models 
{
	public class WavePlayStateModel : TowerDefence.Systems.Waves.Models.IWavePlayStateModel 
	{
		public event Action OnChange;
			// ActiveWave
		public event System.Action<System.Int32> OnChangeActiveWave;
		private System.Int32 _ActiveWave ; 
		public System.Int32 ActiveWave 
		{
			get => _ActiveWave;
			set 
			{
								_ActiveWave = value; 

				OnChangeActiveWave?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// AutoPlayEnabled
		public event System.Action<System.Boolean> OnChangeAutoPlayEnabled;
		private System.Boolean _AutoPlayEnabled ; 
		public System.Boolean AutoPlayEnabled 
		{
			get => _AutoPlayEnabled;
			set 
			{
								_AutoPlayEnabled = value; 

				OnChangeAutoPlayEnabled?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// TotalWaves
		public event System.Action<System.Int32> OnChangeTotalWaves;
		private System.Int32 _TotalWaves ; 
		public System.Int32 TotalWaves 
		{
			get => _TotalWaves;
			set 
			{
								_TotalWaves = value; 

				OnChangeTotalWaves?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// WavesLeft
		public event System.Action<System.Int32> OnChangeWavesLeft;
		private System.Int32 _WavesLeft ; 
		public System.Int32 WavesLeft 
		{
			get => _WavesLeft;
			set 
			{
								_WavesLeft = value; 

				OnChangeWavesLeft?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// WavesPlaying
		public event System.Action<System.Boolean> OnChangeWavesPlaying;
		private System.Boolean _WavesPlaying ; 
		public System.Boolean WavesPlaying 
		{
			get => _WavesPlaying;
			set 
			{
								_WavesPlaying = value; 

				OnChangeWavesPlaying?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WavePlayStateModel() { 
				}

			}
}