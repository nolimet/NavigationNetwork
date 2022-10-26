using DataBinding;
using TMPro;
using TowerDefence.Systems.Waves;
using TowerDefence.Systems.Waves.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefence.UI.Game.Waves
{
    public class WaveHudController : MonoBehaviour
    {
        private readonly BindingContext bindingContext = new();

        [SerializeField] private Button startWavesButton;
        [SerializeField] private Button forceNextWaveButton;
        [SerializeField] private Toggle autoPlayToggle;
        [SerializeField] private TextMeshProUGUI waveCounter;

        [Inject] private readonly IWavePlayStateModel wavePlayStateModel;
        [Inject] private readonly WaveController waveController;

        void Awake()
        {
            startWavesButton.onClick.AddListener(StartWaves);
            forceNextWaveButton.onClick.AddListener(ForceNextWave);
            autoPlayToggle.onValueChanged.AddListener(OnAutoPlayToggleChanged);

            bindingContext.Bind(wavePlayStateModel, m => m.wavesPlaying, OnWavesPlayingChanged);
            bindingContext.Bind(wavePlayStateModel, m => m.autoPlayEnabled, OnAutoPlayChanged);
            bindingContext.Bind(wavePlayStateModel, x => x.activeWave, OnWaveCountChanged);
            bindingContext.Bind(wavePlayStateModel, m => m.totalWaves, OnWaveCountChanged);
        }

        private void OnWaveCountChanged(int _)
        {
            waveCounter.text = $"Wave {wavePlayStateModel.activeWave} of {wavePlayStateModel.totalWaves}";
        }

        private void OnAutoPlayChanged(bool enabled)
        {
            autoPlayToggle.SetIsOnWithoutNotify(enabled);
        }

        private void OnAutoPlayToggleChanged(bool isOn)
        {
            wavePlayStateModel.autoPlayEnabled = isOn;
        }

        private void OnWavesPlayingChanged(bool playing)
        {
            startWavesButton.interactable = !playing;
            forceNextWaveButton.interactable = playing;
        }

        private void ForceNextWave()
        {
            waveController.ForceStartNextWave();
        }

        private void StartWaves()
        {
            waveController.StartWavePlayBack();
        }
    }
}