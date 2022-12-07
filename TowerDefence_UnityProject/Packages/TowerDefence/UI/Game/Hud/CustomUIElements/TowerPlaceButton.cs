using System;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.Hud.CustomUIElements
{
    public class TowerPlaceButton : Button
    {
        public new readonly string ussClassName = "tower-selection-button";
        public event Action<string> OnCallback;
        public readonly string CallbackValue;

        public TowerPlaceButton(string callbackValue)
        {
            CallbackValue = callbackValue;
            clicked += OnClicked;
            AddToClassList(ussClassName);
        }

        private void OnClicked()
        {
            OnCallback?.Invoke(CallbackValue);
        }
    }
}