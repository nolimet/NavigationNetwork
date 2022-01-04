using DataBinding.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBinding.Helpers;

namespace TowerDefence.Systems.Selection.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ISelectionModel : IModelBase
    {
        IList<ISelectable> Selection { get; }
    }
}