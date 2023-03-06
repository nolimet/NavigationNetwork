using System;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.Hud.CustomUIElements
{
    public class TowerPlaceButton : Button
    {
        public readonly string USSClassName = "tower-selection-button";
        public event Action<string> OnCallback;
        public readonly string CallbackValue;

        public TowerPlaceButton(string callbackValue)
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