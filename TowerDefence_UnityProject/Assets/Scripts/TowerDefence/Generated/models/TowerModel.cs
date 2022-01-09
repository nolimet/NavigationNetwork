 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT !! This script is auto generated and will be replaced soon
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.World.Towers.Models {
	public class TowerModel : TowerDefence.World.Towers.Models.ITowerModel {
		public event Action OnChange;
			// Components
		public event System.Action<System.Collections.Generic.IList<TowerDefence.World.Towers.Components.ITowerComponent>> OnChangeComponents;
		private System.Collections.Generic.IList<TowerDefence.World.Towers.Components.ITowerComponent> _Components ; 
		public System.Collections.Generic.IList<TowerDefence.World.Towers.Components.ITowerComponent> Components {
			get => _Components;
			set {
						
				if (_Components != null)
				{
					((ObservableCollection<TowerDefence.World.Towers.Components.ITowerComponent>)_Components).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerComponentsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.World.Towers.Components.ITowerComponent>) == null) 
				{
					_Components = new ObservableCollection<TowerDefence.World.Towers.Components.ITowerComponent>(value);
				}
				else
				{
					_Components = value;
				}

				if (_Components != null)
				{
					((ObservableCollection<TowerDefence.World.Towers.Components.ITowerComponent>)_Components).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerComponentsEvents);
				}

				OnChangeComponents?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Position
		public event System.Action<UnityEngine.Vector3> OnChangePosition;
		private UnityEngine.Vector3 _Position ; 
		public UnityEngine.Vector3 Position {
			get => _Position;
			set {
								_Position = value; 

				OnChangePosition?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Range
		public event System.Action<System.Double> OnChangeRange;
		private System.Double _Range ; 
		public System.Double Range {
			get => _Range;
			set {
								_Range = value; 

				OnChangeRange?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// TowerObject
		public event System.Action<TowerDefence.World.Towers.ITowerObject> OnChangeTowerObject;
		private TowerDefence.World.Towers.ITowerObject _TowerObject ; 
		public TowerDefence.World.Towers.ITowerObject TowerObject {
			get => _TowerObject;
			set {
								_TowerObject = value; 

				OnChangeTowerObject?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// TowerRenderer
		public event System.Action<TowerDefence.World.Towers.TowerBase> OnChangeTowerRenderer;
		private TowerDefence.World.Towers.TowerBase _TowerRenderer ; 
		public TowerDefence.World.Towers.TowerBase TowerRenderer {
			get => _TowerRenderer;
			set {
								_TowerRenderer = value; 

				OnChangeTowerRenderer?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public TowerModel() { 
				Components = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.World.Towers.Components.ITowerComponent>();
			}

		private void TriggerComponentsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeComponents?.Invoke(Components);
			OnChange?.Invoke();
		}

		}
}