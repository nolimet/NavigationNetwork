using System.Collections.Generic;
using DataBinding;
using NoUtil.Extentsions;
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

        private Button startWavesButton;
        private Button forceNextWaveButton;
        private Toggle autoPlayToggle;
        private Label waveCounter;

        private void Awake()
        {
            bindingContext.Bind(wavePlayStateModel, m => m.wavesPlaying, OnWavesPlayingChanged);
            bindingContext.Bind(wavePlayStateModel, m => m.autoPlayEnabled, OnAutoPlayChanged);
            bindingContext.Bind(wavePlayStateModel, x => x.activeWave, OnWaveCountChanged);
            bindingContext.Bind(wavePlayStateModel, m => m.totalWaves, OnWaveCountChanged);

            bindingContext.Bind(uiContainers, m => m.Containers, OnUIContainersChanged);
        }

        private void OnUIContainersChanged(IList<IUIContainer> containers)
        {
            if (startWavesButton is not null)
            {
                startWavesButton.clicked -= StartWaves;
            }

            if (forceNextWaveButton is not null)
            {
                forceNextWaveButton.clicked -= ForceNextWave;
            }

            autoPlayToggle?.UnregisterValueChangedCallback(OnAutoPlayToggleChanged);

            if (!containers.TryFind(x => x.Name == uiContainerId, out var uiContainer) || uiContainer is not UIDocumentContainer uiDocumentContainer)
                return;

            var root = uiDocumentContainer.Document.rootVisualElement;

            startWavesButton = root.Q<Button>(startWaveButtonId);
            forceNextWaveButton = root.Q<Button>(forceNextWaveButtonId);
            autoPlayToggle = root.Q<Toggle>(autoPlayToggleId);
            waveCounter = root.Q<Label>(waveCounterId);

            startWavesButton.clicked += StartWaves;
            forceNextWaveButton.clicked += ForceNextWave;
            autoPlayToggle.RegisterValueChangedCallback(OnAutoPlayToggleChanged);

            OnWavesPlayingChanged(wavePlayStateModel.wavesPlaying);
        }

        private void OnWaveCountChanged(int _)
        {
            if (waveCounter is not null)
                waveCounter.text = ($"Wave {wavePlayStateModel.activeWave} of {wavePlayStateModel.totalWaves}");
        }

        private void OnAutoPlayChanged(bool enabled)
        {
            autoPlayToggle?.SetValueWithoutNotify(enabled);
        }

        private void OnAutoPlayToggleChanged(ChangeEvent<bool> evt)
        {
            wavePlayStateModel.autoPlayEnabled = evt.newValue;
        }

        private void OnWavesPlayingChanged(bool playing)
        {
            startWavesButton?.SetEnabled(!playing);
            forceNextWaveButton?.SetEnabled(playing);
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