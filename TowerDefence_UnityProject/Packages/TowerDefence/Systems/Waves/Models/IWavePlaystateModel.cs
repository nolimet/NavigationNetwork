using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Systems.Waves.Models
{
    [DataModel(Shared = true, AddToZenject = true)]
    public interface IWavePlayStateModel : IModelBase
    {
        bool WavesPlaying { get; set; }
        bool AutoPlayEnabled { get; set; }

        int WavesLeft { get; set; }
        int TotalWaves { get; set; }
        int ActiveWave { get; set; }
    }
}