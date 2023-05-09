// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================

using System;
using TowerDefence.World.Grid;

namespace TowerDefence.Systems.LevelEditor.Models
{
    public class CellModel : ICellModel
    {
        public event Action OnChange;

        // SupportsTower
        public event Action<bool> OnChangeSupportsTower;
        private bool _SupportsTower;

        public bool SupportsTower
        {
            get => _SupportsTower;
            set
            {
                _SupportsTower = value;

                OnChangeSupportsTower?.Invoke(value);
                OnChange?.Invoke();
            }
        }

        // Weight
        public event Action<byte> OnChangeWeight;
        private byte _Weight;

        public byte Weight
        {
            get => _Weight;
            set
            {
                _Weight = value;

                OnChangeWeight?.Invoke(value);
                OnChange?.Invoke();
            }
        }

        // worldCell
        public event Action<SelectableCellGroup> OnChangeworldCell;
        private SelectableCellGroup worldCellGroup;

        public SelectableCellGroup WorldCellGroup
        {
            get => worldCellGroup;
            set
            {
                worldCellGroup = value;

                OnChangeworldCell?.Invoke(value);
                OnChange?.Invoke();
            }
        }
    }
}