using DataBinding.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBinding.Helpers;
using UnityEngine;

namespace TowerDefence.Systems.Selection.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ISelectionModel : IModelBase
    {
        IList<ISelectable> Selection { get; }

        bool Draggin { get; set; }
        Vector3 DragStartPosition { get; set; }
        Vector3 DragEndPosition { get; set; }
    }
}