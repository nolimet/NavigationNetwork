using System;
using UnityEngine.UIElements;

namespace TowerDefence.UI.MainMenu.UIElements
{
    public sealed class LevelSelectionButton : Button
    {
        public new readonly string ussClassName = "level-selection-button";
        public event Action<string> OnCallback;
        public readonly string CallbackValue;

        public LevelSelectionButton(string callbackValue) : base()
        {
            this.CallbackValue = callbackValue;
            clicked += OnClicked;
            AddToClassList(ussClassName);
        }

        private void OnClicked()
        {
            OnCallback?.Invoke(CallbackValue);
        }
    }
}