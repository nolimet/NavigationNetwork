using System;
using Cysharp.Threading.Tasks;
using DataBinding;
using TowerDefence.Systems.LevelEditor.Models;
using TowerDefence.World.Grid;

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

        public UniTask RebuildWorld()
        {
            if (levelEditorModel.World is null) return UniTask.CompletedTask;
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

            if (levelEditorModel.RebuildingWorld) return UniTask.CompletedTask;
            levelEditorModel.RebuildingWorld = true;
            return gridWorld.CreateWorld(world.ToGridSettings())
                .ContinueWith(() => { return levelEditorModel.RebuildingWorld = false; });
        }

        public void Dispose()
        {
            levelEditorBindingContext?.Dispose();
            gridBindingContext?.Dispose();
        }
    }
}