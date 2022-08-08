﻿using System;
using UnityEngine.UIElements;

namespace TowerDefence.UI.UIElements
{
    public class CallbackButton : Button
    {
        public event Action<string> callback;
        public string id = "";

        public CallbackButton() : base()
        {
            base.clicked += OnClicked;
        }

        public CallbackButton(string id)
        {
            this.id = id;
            base.clicked += OnClicked;
        }

        private void OnClicked()
        {
            callback?.Invoke(id);
        }
    }
}