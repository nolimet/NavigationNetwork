using System;
using UnityEngine.UIElements;

namespace TowerDefence.UI.MainMenu.UIElements
{
    public sealed class LevelSelectionButton : Button
    {
        public readonly string USSClassName = "level-selection-button";
        public event Action<string> OnCallback;
        public readonly string CallbackValue;

        public LevelSelectionButton(string callbackValue)
        {
            CallbackValue = callbackValue;
            clicked += OnClicked;
            AddToClassList(USSClassName);
        }

        private void OnClicked()
        {
            OnCallback?.Invoke(CallbackValue);
        }
    }
}