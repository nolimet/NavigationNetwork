﻿using System;
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

        public WorldEditorManager(ILevelEditorModel levelEditorModel)
        {
            this.levelEditorModel = levelEditorModel;
            levelEditorBindingContext.Bind(levelEditorModel, x => x.World, OnWorldChanged);
        }

        private void OnWorldChanged(IWorldLayoutModel world)
        {
            gridBindingContext?.Dispose();
            gridBindingContext = new BindingContext();
            if (world is null) return;

            gridBindingContext.Bind(world, x => x.Height, OnwWorldSizeChanged);
            gridBindingContext.Bind(world, x => x.Width, OnwWorldSizeChanged);
        }

        private void OnwWorldSizeChanged(uint _)
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
            gridWorld.CreateWorld(world.ToGridSettings())
                .ContinueWith(() => levelEditorModel.RebuildingWorld = false)
                .Preserve()
                .Forget();
        }

        public void Dispose()
        {
            levelEditorBindingContext?.Dispose();
            gridBindingContext?.Dispose();
        }
    }
}