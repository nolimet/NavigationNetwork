using System.Collections.Generic;
namespace TowerDefence.World.Grid
{
    internal class GridWorld
    {
        private IEnumerable<IGridNode> world;
        private readonly GridGenerator gridGenerator = new();

        public void CreateWorld(GridSettings settings)
        {
            world = gridGenerator.CreateNodes(settings);
        }

        public IEnumerable<IGridNode> GetPath(IGridNode start, IGridNode end)
        {

            return world;
        }
    }
}
