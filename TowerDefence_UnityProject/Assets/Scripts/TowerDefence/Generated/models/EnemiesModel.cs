 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT !! This script is auto generated and will be replaced soon
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Entities.Enemies.Models {
	public class EnemiesModel : TowerDefence.Entities.Enemies.Models.IEnemiesModel {
		public event Action OnChange;
			// Enemies
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Entities.Enemies.Models.IEnemyModel>> OnChangeEnemies;
		private System.Collections.Generic.IList<TowerDefence.Entities.Enemies.Models.IEnemyModel> _Enemies ; 
		public System.Collections.Generic.IList<TowerDefence.Entities.Enemies.Models.IEnemyModel> Enemies {
			get => _Enemies;
			set {
						
				if (_Enemies != null)
				{
					((ObservableCollection<TowerDefence.Entities.Enemies.Models.IEnemyModel>)_Enemies).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerEnemiesEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Entities.Enemies.Models.IEnemyModel>) == null) 
				{
					_Enemies = new ObservableCollection<TowerDefence.Entities.Enemies.Models.IEnemyModel>(value);
				}
				else
				{
					_Enemies = value;
				}

				if (_Enemies != null)
				{
					((ObservableCollection<TowerDefence.Entities.Enemies.Models.IEnemyModel>)_Enemies).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerEnemiesEvents);
				}

				OnChangeEnemies?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public EnemiesModel() { 
				Enemies = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Entities.Enemies.Models.IEnemyModel>();
			}

		private void TriggerEnemiesEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeEnemies?.Invoke(Enemies);
			OnChange?.Invoke();
		}

		}
}