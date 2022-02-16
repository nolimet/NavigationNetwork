using DataBinding.BaseClasses;
using DataBinding.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Systems.Selection.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ISelectionModel : IModelBase
    {
        IList<ISelectable> Selection { get; }

        bool Dragging { get; set; }
        Vector3 DragStartPosition { get; set; }
        Vector3 DragEndPosition { get; set; }
    }
}