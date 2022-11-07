// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TowerDefence.Entities.Enemies.Models
{
    public class EnemiesModel : IEnemiesModel
    {
        public event Action OnChange;

        // Enemies
        public event Action<IList<IEnemyObject>> OnChangeEnemies;
        private IList<IEnemyObject> _Enemies;

        public IList<IEnemyObject> Enemies
        {
            get => _Enemies;
            set
            {
                if (_Enemies != null)
                {
                    ((ObservableCollection<IEnemyObject>)_Enemies).CollectionChanged -= TriggerEnemiesEvents;
                }

                if (value != null && (value as ObservableCollection<IEnemyObject>) == null)
                {
                    _Enemies = new ObservableCollection<IEnemyObject>(value);
                }
                else
                {
                    _Enemies = value;
                }

                if (_Enemies != null)
                {
                    ((ObservableCollection<IEnemyObject>)_Enemies).CollectionChanged += TriggerEnemiesEvents;
                }

                OnChangeEnemies?.Invoke(value);
                OnChange?.Invoke();
            }
        }

        public EnemiesModel()
        {
            Enemies = new ObservableCollection<IEnemyObject>();
        }

        private void TriggerEnemiesEvents(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnChangeEnemies?.Invoke(Enemies);
            OnChange?.Invoke();
        }
    }
}