using DataBinding;
using TowerDefence.Systems.Waves;
using TowerDefence.Systems.Waves.Models;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace TowerDefence.UI.Game.Waves
{
    public class WaveHudController : MonoBehaviour
    {
        private readonly BindingContext bindingContext = new();

        [SerializeField] private string startWaveButtonId;
        [SerializeField] private string forceNextWaveButtonId;
        [SerializeField] private string autoPlayToggleId;
        [SerializeField] private string waveCounterId;
        [Space] [SerializeField] private string uiContainerId;

        [Inject] private readonly IWavePlayStateModel wavePlayStateModel;
        [Inject] private readonly WaveController waveController;
        [Inject] private readonly IUIContainers uiContainers;

        private UIDocumentContainer documentContainer;
        private Button startWavesButton;
        private Button forceNextWaveButton;
        private Toggle autoPlayToggle;
        private TextField waveCounter;

        void Awake()
        {
            if (uiContainers.TryGetContainerWithId(uiContainerId, out var uiContainer) &&
                uiContainer is UIDocumentContainer uiDocumentContainer)
            {
                documentContainer = uiDocumentContainer;
                var root = uiDocumentContainer.Document.rootVisualElement;
                startWavesButton = root.Q<Button>(startWaveButtonId);
                forceNextWaveButton = root.Q<Button>(forceNextWaveButtonId);
                autoPlayToggle = root.Q<Toggle>(autoPlayToggleId);
                waveCounter = root.Q<TextField>(waveCounterId);
            }

            startWavesButton.clicked += StartWaves;
            forceNextWaveButton.clicked += ForceNextWave;

            bindingContext.Bind(wavePlayStateModel, m => m.wavesPlaying, OnWavesPlayingChanged);
            bindingContext.Bind(wavePlayStateModel, m => m.autoPlayEnabled, OnAutoPlayChanged);
            bindingContext.Bind(wavePlayStateModel, x => x.activeWave, OnWaveCountChanged);
            bindingContext.Bind(wavePlayStateModel, m => m.totalWaves, OnWaveCountChanged);
        }

        private void OnWaveCountChanged(int _)
        {
            waveCounter.SetValueWithoutNotify(
                $"Wave {wavePlayStateModel.activeWave} of {wavePlayStateModel.totalWaves}");
        }

        private void OnAutoPlayChanged(bool enabled)
        {
            autoPlayToggle.value = enabled;
        }

        private void OnAutoPlayToggleChanged(bool isOn)
        {
            wavePlayStateModel.autoPlayEnabled = isOn;
        }

        private void OnWavesPlayingChanged(bool playing)
        {
            startWavesButton.SetEnabled(!playing);
            forceNextWaveButton.SetEnabled(playing);
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