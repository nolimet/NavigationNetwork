 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Entities.Enemies.Models 
{
	public class EnemyModel : TowerDefence.Entities.Enemies.Models.IEnemyModel 
	{
		public event Action OnChange;
			// Components
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Entities.Components.IComponent>> OnChangeComponents;
		private System.Collections.Generic.IList<TowerDefence.Entities.Components.IComponent> _Components ; 
		public System.Collections.Generic.IList<TowerDefence.Entities.Components.IComponent> Components 
		{
			get => _Components;
			set 
			{
						
				if (_Components != null)
				{
					((ObservableCollection<TowerDefence.Entities.Components.IComponent>)_Components).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerComponentsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Entities.Components.IComponent>) == null) 
				{
					_Components = new ObservableCollection<TowerDefence.Entities.Components.IComponent>(value);
				}
				else
				{
					_Components = value;
				}

				if (_Components != null)
				{
					((ObservableCollection<TowerDefence.Entities.Components.IComponent>)_Components).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerComponentsEvents);
				}

				OnChangeComponents?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Health
		public event System.Action<System.Double> OnChangeHealth;
		private System.Double _Health ; 
		public System.Double Health 
		{
			get => _Health;
			set 
			{
								_Health = value; 

				OnChangeHealth?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// HealthBar
		public event System.Action<TowerDefence.UI.Game.Health.HealthDrawer> OnChangeHealthBar;
		private TowerDefence.UI.Game.Health.HealthDrawer _HealthBar ; 
		public TowerDefence.UI.Game.Health.HealthDrawer HealthBar 
		{
			get => _HealthBar;
			set 
			{
								_HealthBar = value; 

				OnChangeHealthBar?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// HealthOffset
		public event System.Action<UnityEngine.Vector3> OnChangeHealthOffset;
		private UnityEngine.Vector3 _HealthOffset ; 
		public UnityEngine.Vector3 HealthOffset 
		{
			get => _HealthOffset;
			set 
			{
								_HealthOffset = value; 

				OnChangeHealthOffset?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// MaxHealth
		public event System.Action<System.Double> OnChangeMaxHealth;
		private System.Double _MaxHealth ; 
		public System.Double MaxHealth 
		{
			get => _MaxHealth;
			set 
			{
								_MaxHealth = value; 

				OnChangeMaxHealth?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public EnemyModel() { 
				Components = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Entities.Components.IComponent>();
			}

		private void TriggerComponentsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeComponents?.Invoke(Components);
			OnChange?.Invoke();
		}

		}
}