 
 
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
			// activeWave
		public event System.Action<System.Int32> OnChangeactiveWave;
		private System.Int32 _activeWave ; 
		public System.Int32 activeWave 
		{
			get => _activeWave;
			set 
			{
								_activeWave = value; 

				OnChangeactiveWave?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// autoPlayEnabled
		public event System.Action<System.Boolean> OnChangeautoPlayEnabled;
		private System.Boolean _autoPlayEnabled ; 
		public System.Boolean autoPlayEnabled 
		{
			get => _autoPlayEnabled;
			set 
			{
								_autoPlayEnabled = value; 

				OnChangeautoPlayEnabled?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// totalWaves
		public event System.Action<System.Int32> OnChangetotalWaves;
		private System.Int32 _totalWaves ; 
		public System.Int32 totalWaves 
		{
			get => _totalWaves;
			set 
			{
								_totalWaves = value; 

				OnChangetotalWaves?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// wavesLeft
		public event System.Action<System.Int32> OnChangewavesLeft;
		private System.Int32 _wavesLeft ; 
		public System.Int32 wavesLeft 
		{
			get => _wavesLeft;
			set 
			{
								_wavesLeft = value; 

				OnChangewavesLeft?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// wavesPlaying
		public event System.Action<System.Boolean> OnChangewavesPlaying;
		private System.Boolean _wavesPlaying ; 
		public System.Boolean wavesPlaying 
		{
			get => _wavesPlaying;
			set 
			{
								_wavesPlaying = value; 

				OnChangewavesPlaying?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WavePlayStateModel() { 
				}

			}
}