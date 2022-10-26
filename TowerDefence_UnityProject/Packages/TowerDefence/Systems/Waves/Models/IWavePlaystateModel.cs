using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Systems.Waves.Models
{
    [DataModel(Shared = true, AddToZenject = true)]
    public interface IWavePlayStateModel : IModelBase
    {
        bool wavesPlaying { get; set; }
        bool autoPlayEnabled { get; set; }

        int wavesLeft { get; set; }
        int totalWaves { get; set; }
        int activeWave { get; set; }
    }
}