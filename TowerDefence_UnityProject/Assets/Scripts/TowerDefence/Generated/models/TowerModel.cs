 
 
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
				}

			}
}