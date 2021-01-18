using System;
using TowerDefence.World.Path;
using TowerDefence.World.Path.Data;

namespace TowerDefence.World
{
    public class WorldController
    {
        private readonly PathBuilderService pathBuilderService;

        public WorldController(PathBuilderService pathBuilderService)
        {
            this.pathBuilderService = pathBuilderService;
        }
        public void SetPath(PathData pathData)
        {
            throw new NotImplementedException("Generate Path!");
        }
    }
}