using TowerDefence.World.Path;
using TowerDefence.World.Path.Data;

namespace TowerDefence.World
{
    public class PathWorldController
    {
        private readonly PathBuilderService pathBuilderService;
        public PathWorldData pathWorldData { get; private set; }


        internal PathWorldController(PathBuilderService pathBuilderService)
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