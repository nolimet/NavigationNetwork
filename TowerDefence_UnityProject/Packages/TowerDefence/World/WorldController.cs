using TowerDefence.World.Path;
using TowerDefence.World.Path.Data;

namespace TowerDefence.World
{
    public class WorldController
    {
        private readonly PathBuilderService pathBuilderService;
        public PathWorldData pathWorldData { get; private set; }

        public WorldController(PathBuilderService pathBuilderService)
        {
            this.pathBuilderService = pathBuilderService;
        }

        public void SetPath(PathData pathData)
        {
            pathWorldData = pathBuilderService.GeneratePathWorldData(pathData);
        }

        public void DestroyCurrentPath()
        {
            pathWorldData.Destroy();
            pathWorldData = null;
        }
    }
}