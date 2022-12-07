using System.Collections.Generic;
using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Entities.Enemies.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface IEnemiesModel : IModelBase
    {
        public IList<IEnemyObject> Enemies { get; set; }
    }
}