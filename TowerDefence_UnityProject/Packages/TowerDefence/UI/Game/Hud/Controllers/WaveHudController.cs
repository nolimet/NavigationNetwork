using System;
using System.Collections.Generic;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Systems.Waves;
using TowerDefence.Systems.Waves.Models;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Models;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.Hud.Controllers
{
    public class WaveHudController : IDisposable
    {
        private readonly BindingContext bindingContext = new();

        private const string StartWaveButtonId = "start_waves";
        private const string ForceNextWaveButtonId = "force_next_wave";
        private const string AutoPlayToggleId = "auto_wave_playback";
        private const string WaveCounterId = "wave_display";
        private const string UIContainerId = "GameUI-HUD";

        private readonly IWavePlayStateModel wavePlayStateModel;
        private readonly WaveController waveController;

        private Button startWavesButton;
        private Button forceNextWaveButton;
        private Toggle autoPlayToggle;
        private Label waveCounter;

        public WaveHudController(IWavePlayStateModel wavePlayStateModel, WaveController waveController, IUIContainers uiContainers)
        {
            this.wavePlayStateModel = wavePlayStateModel;
            this.waveController = waveController;

            bindingContext.Bind(wavePlayStateModel, m => m.WavesPlaying, OnWavesPlayingChanged);
            bindingContext.Bind(wavePlayStateModel, m => m.AutoPlayEnabled, OnAutoPlayChanged);
            bindingContext.Bind(wavePlayStateModel, x => x.ActiveWave, OnWaveCountChanged);
            bindingContext.Bind(wavePlayStateModel, m => m.TotalWaves, OnWaveCountChanged);

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

            if (!containers.TryFind(x => x.Name == UIContainerId, out var uiContainer) || uiContainer is not UIDocumentContainer uiDocumentContainer)
                return;

            var root = uiDocumentContainer.Document.rootVisualElement;

            startWavesButton = root.Q<Button>(StartWaveButtonId);
            forceNextWaveButton = root.Q<Button>(ForceNextWaveButtonId);
            autoPlayToggle = root.Q<Toggle>(AutoPlayToggleId);
            waveCounter = root.Q<Label>(WaveCounterId);

            startWavesButton.clicked += StartWaves;
            forceNextWaveButton.clicked += ForceNextWave;
            autoPlayToggle.RegisterValueChangedCallback(OnAutoPlayToggleChanged);

            OnWavesPlayingChanged(wavePlayStateModel.WavesPlaying);
        }

        private void OnWaveCountChanged(int _)
        {
            if (waveCounter is not null)
                waveCounter.text = ($"Wave {wavePlayStateModel.ActiveWave} of {wavePlayStateModel.TotalWaves}");
        }

        private void OnAutoPlayChanged(bool enabled)
        {
            autoPlayToggle?.SetValueWithoutNotify(enabled);
        }

        private void OnAutoPlayToggleChanged(ChangeEvent<bool> evt)
        {
            wavePlayStateModel.AutoPlayEnabled = evt.newValue;
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

        public void Dispose()
        {
            bindingContext?.Dispose();
        }
    }
}