using TowerDefence.World.Path;
using TowerDefence.World.Path.Data;

namespace TowerDefence.World
{
    public sealed class PathWorldController
    {
        private readonly PathBuilderService pathBuilderService;
        public PathWorldData PathWorldData { get; private set; }


        internal PathWorldController(PathBuilderService pathBuilderService)
        {
            this.pathBuilderService = pathBuilderService;
        }

        public void SetPath(PathData pathData)
        {
            PathWorldData = pathBuilderService.GeneratePathWorldData(pathData);
        }

        public void DestroyCurrentPath()
        {
            PathWorldData.Destroy();
            PathWorldData = null;
        }
    }
}