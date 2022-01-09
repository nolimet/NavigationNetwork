using DataBinding.BaseClasses;
using System.Collections.Generic;

namespace TowerDefence.Entities.Enemies.Models
{
    [DataBinding.Helpers.DataModel(AddToZenject = true, Shared = true)]
    public interface IEnemiesModel : IModelBase
    {
        public IList<IEnemyBaseModel> Enemies { get; set; }
    }
}