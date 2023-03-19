using System;
using Cysharp.Threading.Tasks;
using DataBinding;
using TowerDefence.Systems.LevelEditor.Models;
using TowerDefence.World.Grid;
using Object = UnityEngine.Object;

namespace TowerDefence.Systems.LevelEditor.Managers
{
    public class WorldEditorManager : IDisposable
    {
        private readonly ILevelEditorModel levelEditorModel;
        private readonly BindingContext levelEditorBindingContext = new();
        private readonly GridWorld gridWorld;

        private BindingContext gridBindingContext;

        internal WorldEditorManager(ILevelEditorModel levelEditorModel, GridWorld gridWorld)
        {
            this.levelEditorModel = levelEditorModel;
            this.gridWorld = gridWorld;

            levelEditorBindingContext.Bind(levelEditorModel, x => x.World, OnWorldChanged);
        }

        private void OnWorldChanged(IWorldLayoutModel world)
        {
            gridBindingContext?.Dispose();
            gridBindingContext = new BindingContext();
            if (world is null) return;
        }

        public async UniTask RebuildWorld()
        {
            if (levelEditorModel.World is null) return;
            var world = levelEditorModel.World;
            var size = world.Height * world.Width;


            while (world.Cells.Count < size)
            {
                world.Cells.Add(ModelFactory.Create<ICellModel>());
            }

            while (world.Cells.Count > size && world.Cells.Count != 0)
            {
                world.Cells.Remove(world.Cells[^1]);
            }

            if (levelEditorModel.RebuildingWorld) return;
            levelEditorModel.RebuildingWorld = true;

            await gridWorld.CreateWorld(world.ToGridSettings());

            levelEditorModel.RebuildingWorld = false;

            var selectableCells = Object.FindObjectsOfType<SelectableCell>();

            foreach (var cell in selectableCells)
            {
                var cellPos = cell.GridCell.Position;
                int index = (int)(cellPos.x * world.Width + cellPos.y);

                var cellModel = world.Cells[index];
                cellModel.worldCell = cell;
            }
        }


        public void Dispose()
        {
            levelEditorBindingContext?.Dispose();
            gridBindingContext?.Dispose();
        }
    }
}