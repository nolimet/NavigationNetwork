 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT !! This script is auto generated and will be replaced soon
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Entities.Enemies.Models {
	public class EnemyModel : TowerDefence.Entities.Enemies.Models.IEnemyModel {
		public event Action OnChange;
			// health
		public event System.Action<System.Double> OnChangehealth;
		private System.Double _health ; 
		public System.Double health {
			get => _health;
			set {
								_health = value; 

				OnChangehealth?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// healthBar
		public event System.Action<TowerDefence.UI.Health.HealthDrawer> OnChangehealthBar;
		private TowerDefence.UI.Health.HealthDrawer _healthBar ; 
		public TowerDefence.UI.Health.HealthDrawer healthBar {
			get => _healthBar;
			set {
								_healthBar = value; 

				OnChangehealthBar?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// healthOffset
		public event System.Action<UnityEngine.Vector3> OnChangehealthOffset;
		private UnityEngine.Vector3 _healthOffset ; 
		public UnityEngine.Vector3 healthOffset {
			get => _healthOffset;
			set {
								_healthOffset = value; 

				OnChangehealthOffset?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// maxHealth
		public event System.Action<System.Double> OnChangemaxHealth;
		private System.Double _maxHealth ; 
		public System.Double maxHealth {
			get => _maxHealth;
			set {
								_maxHealth = value; 

				OnChangemaxHealth?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// obj
		public event System.Action<TowerDefence.Entities.Enemies.EnemyBase> OnChangeobj;
		private TowerDefence.Entities.Enemies.EnemyBase _obj ; 
		public TowerDefence.Entities.Enemies.EnemyBase obj {
			get => _obj;
			set {
								_obj = value; 

				OnChangeobj?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// transform
		public event System.Action<UnityEngine.Transform> OnChangetransform;
		private UnityEngine.Transform _transform ; 
		public UnityEngine.Transform transform {
			get => _transform;
			set {
								_transform = value; 

				OnChangetransform?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public EnemyModel() { 
				}

			}
}