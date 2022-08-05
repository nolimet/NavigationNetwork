namespace TowerDefence.Systems.WorldLoader.Data
{
    public readonly struct LevelMetadata
    {
        public readonly string DisplayName;
        public readonly string RelativeLevelPath;

        public LevelMetadata(string displayName, string relativeLevelPath)
        {
            DisplayName = displayName;
            RelativeLevelPath = relativeLevelPath;
        }
    }
}