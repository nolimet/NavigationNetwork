using System;
using UnityEngine.UIElements;

namespace TowerDefence.UI.UIElements
{
    public class LevelSelectionButton : Button
    {
        public new readonly string ussClassName = "level-selection-button";
        public event Action<string> callback;
        public readonly string callbackValue = "";

        public LevelSelectionButton(string callbackValue) : base()
        {
            clicked += OnClicked;
            AddToClassList(ussClassName);
        }

        private void OnClicked()
        {
            callback?.Invoke(callbackValue);
        }
    }
}