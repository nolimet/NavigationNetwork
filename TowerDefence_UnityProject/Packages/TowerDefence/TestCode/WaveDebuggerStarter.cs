using TowerDefence.Input;
using TowerDefence.Systems.Waves;
using UnityEngine.InputSystem;

namespace TowerDefence.TestCode
{
    public class WaveDebuggerStarter
    {
        private readonly DebugActions debugActions;
        private readonly WaveController waveController;

        public WaveDebuggerStarter(DebugActions debugActions, WaveController waveController)
        {
            this.debugActions = debugActions;
            this.waveController = waveController;

            debugActions.Waves.StartWaves.performed += OnStartWavePressed;
            debugActions.Waves.StartWaves.Enable();
            debugActions.Waves.Enable();
            debugActions.Enable();
        }

        ~WaveDebuggerStarter()
        {
            debugActions.Waves.StartWaves.performed -= OnStartWavePressed;
        }

        private void OnStartWavePressed(InputAction.CallbackContext obj)
        {
            waveController.StartWavePlayBack();
        }
    }
}