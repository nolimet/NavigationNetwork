 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.CameraManager 
{
	public class CameraContainer : TowerDefence.Systems.CameraManager.ICameraContainer 
	{
		public event Action OnChange;
			// Cameras
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Systems.CameraManager.CameraReference>> OnChangeCameras;
		private System.Collections.Generic.IList<TowerDefence.Systems.CameraManager.CameraReference> _Cameras ; 
		public System.Collections.Generic.IList<TowerDefence.Systems.CameraManager.CameraReference> Cameras 
		{
			get => _Cameras;
			set 
			{
						
				if (_Cameras != null)
				{
					((ObservableCollection<TowerDefence.Systems.CameraManager.CameraReference>)_Cameras).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerCamerasEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Systems.CameraManager.CameraReference>) == null) 
				{
					_Cameras = new ObservableCollection<TowerDefence.Systems.CameraManager.CameraReference>(value);
				}
				else
				{
					_Cameras = value;
				}

				if (_Cameras != null)
				{
					((ObservableCollection<TowerDefence.Systems.CameraManager.CameraReference>)_Cameras).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerCamerasEvents);
				}

				OnChangeCameras?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public CameraContainer() { 
				Cameras = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Systems.CameraManager.CameraReference>();
			}

		private void TriggerCamerasEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeCameras?.Invoke(Cameras);
			OnChange?.Invoke();
		}

		}
}