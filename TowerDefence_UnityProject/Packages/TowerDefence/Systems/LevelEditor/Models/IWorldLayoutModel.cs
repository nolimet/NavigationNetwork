using System.Collections.Generic;
using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Systems.LevelEditor.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface IWorldLayoutModel : IModelBase
    {
        IList<Cell> Cells { get; }
    }

    public record Cell(byte weight, bool supportsTower)
    {
        public byte weight { get; } = weight;
        public bool supportsTower { get; } = supportsTower;
    }
}